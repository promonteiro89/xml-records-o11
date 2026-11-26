// XmlToRecordList: XML to a record list, with its options, and round trips through RecordToXml /
// RecordListToXml with the same configuration. Dates are in DateTimeTests. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class XmlToRecordListTests
{
    public static void Run()
    {
        BasicTypes();
        Shapes();
        NestedRecordsAndLists();
        Options();
        InvalidXml();
        RoundTrips();
    }

    private static void BasicTypes()
    {
        STAllTypesStructure s = TD.ReadAllTypes("<AllTypes><Text>Hi &amp; &lt;x&gt;</Text><Number> 42 </Number><BigNumber>9876543210</BigNumber>"
            + "<Amount>1234.5</Amount><Flag>true</Flag><Day>2024-01-15</Day><Clock>13:45:30</Clock><Moment>2024-01-15T13:45:30</Moment></AllTypes>", XR.Defaults());
        TestLog.Eq("Text with entities is decoded", "Hi & <x>", s.ssText);
        TestLog.Eq("Integer with surrounding spaces", 42, s.ssNumber);
        TestLog.Eq("Long Integer", 9876543210L, s.ssBigNumber);
        TestLog.Eq("Decimal", 1234.5M, s.ssAmount);
        TestLog.Eq("Boolean true", true, s.ssFlag);
        TestLog.Eq("Date", XR.D(2024, 1, 15), s.ssDay);
        TestLog.Eq("Time on 1900-01-01", XR.T(13, 45, 30), s.ssClock);
        TestLog.Eq("Date Time", XR.D(2024, 1, 15, 13, 45, 30), s.ssMoment);

        TestLog.Eq("Boolean True", true, TD.ReadAllTypes("<AllTypes><Flag>True</Flag></AllTypes>", XR.Defaults()).ssFlag);
        TestLog.Eq("Boolean TRUE", true, TD.ReadAllTypes("<AllTypes><Flag>TRUE</Flag></AllTypes>", XR.Defaults()).ssFlag);
        TestLog.Eq("Boolean 1", true, TD.ReadAllTypes("<AllTypes><Flag>1</Flag></AllTypes>", XR.Defaults()).ssFlag);
        TestLog.Eq("Boolean 0", false, TD.ReadAllTypes("<AllTypes><Flag>0</Flag></AllTypes>", XR.Defaults()).ssFlag);
        TestLog.Eq("Integer with a plus sign", 7, TD.ReadAllTypes("<AllTypes><Number>+7</Number></AllTypes>", XR.Defaults()).ssNumber);
        TestLog.Eq("Long Integer minimum", long.MinValue, TD.ReadAllTypes("<AllTypes><BigNumber>-9223372036854775808</BigNumber></AllTypes>", XR.Defaults()).ssBigNumber);
        TestLog.Eq("Decimal with en-US thousands separators (1,234,567.5)", 1234567.5M, TD.ReadAllTypes("<AllTypes><Amount>1,234,567.5</Amount></AllTypes>", XR.Defaults()).ssAmount);

        RCDefaultXmlConfigRecord pt = XR.Defaults();
        pt.ssSTDefaultXmlConfig.ssTypeDecimalCulture = "pt-PT";
        TestLog.Eq("TypeDecimalCulture pt-PT reads 1,5", 1.5M, TD.ReadAllTypes("<AllTypes><Amount>1,5</Amount></AllTypes>", pt).ssAmount);
        RCDefaultXmlConfigRecord de = XR.Defaults();
        de.ssSTDefaultXmlConfig.ssTypeDecimalCulture = "de-DE";
        TestLog.Eq("TypeDecimalCulture de-DE reads 1.234,5", 1234.5M, TD.ReadAllTypes("<AllTypes><Amount>1.234,5</Amount></AllTypes>", de).ssAmount);
        Fails("TypeDecimalCulture pt-PT does not read 1.5", "<AllTypes><Amount>1.5</Amount></AllTypes>", pt, "Amount", typeof(FormatException));

        Fails("Boolean 10: ApplicationException naming element and value", "<AllTypes><Flag>10</Flag></AllTypes>", XR.Defaults(), "Flag", typeof(FormatException));
        Fails("Boolean yes: ApplicationException", "<AllTypes><Flag>yes</Flag></AllTypes>", XR.Defaults(), "Flag", typeof(FormatException));
        Fails("Integer overflow: ApplicationException (inner OverflowException)", "<AllTypes><Number>99999999999</Number></AllTypes>", XR.Defaults(), "Number", typeof(OverflowException));
        Fails("Integer 4.0: ApplicationException", "<AllTypes><Number>4.0</Number></AllTypes>", XR.Defaults(), "Number", typeof(FormatException));
        Fails("Decimal in scientific notation (1E3): ApplicationException", "<AllTypes><Amount>1E3</Amount></AllTypes>", XR.Defaults(), "Amount", typeof(FormatException));
        RCDefaultXmlConfigRecord badCulture = XR.Defaults();
        badCulture.ssSTDefaultXmlConfig.ssTypeDecimalCulture = "xx-INVALID";
        Fails("an unknown TypeDecimalCulture: ApplicationException (inner CultureNotFoundException)", "<AllTypes><Amount>1.5</Amount></AllTypes>", badCulture, "Amount", typeof(System.Globalization.CultureNotFoundException));

        // KI-02: Binary Data cannot be read (NotSupportedException); base64 is what RecordToXml should write
        object blob;
        try { blob = TD.ReadAllTypes("<AllTypes><Blob>AQID</Blob></AllTypes>", XR.Defaults()).ssBlob; }
        catch (Exception ex) { blob = ex; }
        TestLog.Check("Binary Data is read from base64 (AQID -> 01 02 03)",
            blob is byte[] && Convert.ToBase64String((byte[])blob) == "AQID", blob is Exception ? TestLog.Describe((Exception)blob) : TestLog.Show(blob));

        STAllTypesStructure empty = TD.ReadAllTypes("<AllTypes><Text/><Number></Number><Day/><Flag>  </Flag></AllTypes>", XR.Defaults());
        TestLog.Check("empty elements (and whitespace only) keep the attribute's default value",
            empty.ssText == "" && empty.ssNumber == 0 && empty.ssDay == XR.NullDate && empty.ssFlag == false,
            empty.ssText + "|" + empty.ssNumber + "|" + TestLog.Show(empty.ssDay) + "|" + empty.ssFlag);
        STAllTypesStructure attrs = TD.ReadAllTypes("<AllTypes Text=\"t\" Number=\"5\"><Flag>true</Flag><Day>2024-01-01</Day></AllTypes>", XR.Defaults());
        TestLog.Check("values in XML attributes of the record element are read when there is no element (no Attr_ prefix needed)",
            attrs.ssText == "t" && attrs.ssNumber == 5 && attrs.ssFlag && attrs.ssDay == XR.D(2024, 1, 1), attrs.ssText + "|" + attrs.ssNumber);
        STAllTypesStructure onlyAttrs = TD.ReadAllTypes("<AllTypes Text=\"t\" Number=\"5\"/>", XR.Defaults());
        TestLog.Check("values only in XML attributes (empty record element)", onlyAttrs.ssText == "t" && onlyAttrs.ssNumber == 5, onlyAttrs.ssText + "|" + onlyAttrs.ssNumber);
        TestLog.Eq("Text keeps surrounding spaces and a CRLF (no end-of-line normalization)",
            "1:{ x ,a\r\nb,,}", TD.Show(TD.ReadTests("<Test><Attribute1> x </Attribute1><Attribute2>a\r\nb</Attribute2></Test>", XR.NoConfig(), XR.Defaults())));
    }

    private static void Shapes()
    {
        RLProductRecordList products = new RLProductRecordList();
        XR.FromXml("<Product><Id>5</Id><Name>Pen</Name></Product>", products, XR.Defaults());
        TestLog.Check("an entity record list", products.Length == 1 && products[0].ssENProduct.ssId == 5 && products[0].ssENProduct.ssName == "Pen");

        // the record element holding only text: every attribute takes the element's own text
        TestLog.Eq("<Test>abc</Test>: every attribute is read from the element's text", "1:{abc,abc,abc,abc}",
            TD.Show(TD.ReadTests("<Test>abc</Test>", XR.NoConfig(), XR.Defaults())));
        TestLog.Throws("<AllTypes>abc</AllTypes>: the text is also tried as an Integer, ApplicationException",
            XR.Throws(delegate { TD.ReadAllTypes("<AllTypes>abc</AllTypes>", XR.Defaults()); }),
            typeof(ApplicationException), "Element: Number; Value: abc;");
        TestLog.Eq("elements that are not attributes of the record are ignored", "1:{a,,,}",
            TD.Show(TD.ReadTests("<Test><Zzz>q</Zzz><Attribute1>a</Attribute1></Test>", XR.NoConfig(), XR.Defaults())));

        RLTestRecordList existing = TD.Tests(TD.Test("old", "", "", ""));
        XR.FromXml("<Test><Attribute1>a</Attribute1></Test>", existing, XR.Defaults());
        TestLog.Eq("records already in the list are kept; new ones are appended", "2:{old,,,}{a,,,}", TD.Show(existing));

        RLNoteRecordList notes = new RLNoteRecordList();
        XR.FromXml("<Note Lang=\"pt\">  Olá  </Note>", notes, XR.Defaults());
        TestLog.Check("Attr_ attribute from an XML attribute, Cont_ attribute from the trimmed text",
            notes[0].ssSTNote.ssAttr_Lang == "pt" && notes[0].ssSTNote.ssCont_Text == "Olá", notes[0].ssSTNote.ssAttr_Lang + "|" + notes[0].ssSTNote.ssCont_Text);

        // KI-08: only the first structure of a record with two is read
        RLOrderCustomerRecordList oc = new RLOrderCustomerRecordList();
        XR.FromXml("<Order Id=\"7\"><Number>N1</Number></Order>", oc, XR.Defaults());
        TestLog.Check("a record with two structures: the first one (Order) is read", oc.Length == 1 && oc[0].ssSTOrder.ssAttr_Id == 7 && oc[0].ssSTOrder.ssNumber == "N1");
    }

    private static void NestedRecordsAndLists()
    {
        TestLog.Eq("nested list in a wrapper, nested record in Sequence mode", "1:{7|N1|C9|Ann|lines 2[P,2,3.5][Q,2,3.5]}",
            TD.Show(TD.ReadOrders("<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
                + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
                + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>", XR.NoConfig(), XR.Defaults())));
        TestLog.Eq("nested record in Child mode is read without configuring it", "1:{0||A|N|lines 0}",
            TD.Show(TD.ReadOrders("<Order><Customer><Customer Code=\"A\"><Name>N</Name></Customer></Customer></Order>", XR.NoConfig(), XR.Defaults())));
        TestLog.Eq("list items directly in the record (Sequence) are read without configuring it", "1:{1||||lines 2[A,0,0.0][B,0,0.0]}",
            TD.Show(TD.ReadOrders("<Order Id=\"1\"><OrderLine><Product>A</Product></OrderLine><OrderLine><Product>B</Product></OrderLine></Order>", XR.NoConfig(), XR.Defaults())));
        TestLog.Eq("list items as repeated elements named after the attribute (Lines) are read", "1:{1||||lines 2[A,0,0.0][B,0,0.0]}",
            TD.Show(TD.ReadOrders("<Order Id=\"1\"><Lines><Product>A</Product></Lines><Lines><Product>B</Product></Lines></Order>", XR.NoConfig(), XR.Defaults())));
        TestLog.Eq("the extension's TestHierarchical", "1:{s|0|0.0|False|rec x,,,|list 1:{a,,,}}",
            ShowH("<TestHierarchical><aString>s</aString><aInt>0</aInt><aDecimal>0.0</aDecimal><aBoolean>false</aBoolean><aRecordList><Test><Attribute1>a</Attribute1></Test></aRecordList><aRecord><Attribute1>x</Attribute1></aRecord></TestHierarchical>",
                XR.NoConfig(), XR.Defaults(), false));
    }

    private static void Options()
    {
        const string one = "<Test><Attribute1>a</Attribute1></Test>";
        TestLog.Eq("BypassRootRecordListNode True skips <RecordList>", "2:{a,,,}{a,,,}",
            TD.Show(Read("<RecordList>" + one + one + "</RecordList>", false, "", true, false, false)));
        TestLog.Throws("BypassRootRecordListNode False: <RecordList> is taken as a record, ApplicationException",
            XR.Throws(delegate { Read("<RecordList>" + one + "</RecordList>", false, "", false, false, false); }),
            typeof(ApplicationException), "The current node (RecordList) does not match the record definition (Test)");
        TestLog.Throws("BypassRootRecordListNode skips only a root named RecordList",
            XR.Throws(delegate { Read("<Items>" + one + "</Items>", false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (Items) does not match the record definition (Test)");
        TestLog.Eq("an empty <RecordList> gives no records", "0:", TD.Show(Read("<RecordList></RecordList>", false, "", true, false, false)));

        TestLog.Throws("an XML declaration with IgnoreXMLDeclarationAndNS False is taken as a record, ApplicationException",
            XR.Throws(delegate { Read("<?xml version=\"1.0\"?>" + one, false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (xml) does not match the record definition (Test)");
        TestLog.Eq("IgnoreXMLDeclarationAndNS True skips the declaration", "1:{a,,,}", TD.Show(Read("<?xml version=\"1.0\"?>\r\n" + one, true, "", true, false, false)));
        TestLog.Eq("a default namespace is read without IgnoreXMLDeclarationAndNS", "1:{a,,,}",
            TD.Show(Read("<Test xmlns=\"urn:x\"><Attribute1>a</Attribute1></Test>", false, "", true, false, false)));
        TestLog.Eq("IgnoreXMLDeclarationAndNS True removes xmlns:xsi and xsi: attributes", "1:{a,,,}",
            TD.Show(Read("<Test xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"a.xsd\"><Attribute1>a</Attribute1></Test>", true, "", true, false, false)));
        TestLog.Throws("prefixed elements (p:Test) don't match the record, ApplicationException",
            XR.Throws(delegate { Read("<p:Test xmlns:p=\"urn:x\"><p:Attribute1>a</p:Attribute1></p:Test>", false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (p:Test) does not match the record definition (Test)");
        TestLog.Throws("prefixed elements with IgnoreXMLDeclarationAndNS: the prefix declaration is removed and the XML no longer loads",
            XR.Throws(delegate { Read("<p:Test xmlns:p=\"urn:x\"><p:Attribute1>a</p:Attribute1></p:Test>", true, "", true, false, false); }),
            typeof(ApplicationException), "Unable to load xml document - set the ShowXMLInLogs input to True for more details");

        TestLog.Throws("a comment before the record with BypassComments False is taken as a record",
            XR.Throws(delegate { Read("<!-- c -->" + one, false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (#comment) does not match the record definition (Test)");
        TestLog.Eq("BypassComments True: a comment before the record is ignored", "1:{a,,,}", TD.Show(Read("<!-- c -->" + one, false, "", true, true, false)));
        TestLog.Throws("a comment inside <RecordList> with BypassComments False is taken as a record",
            XR.Throws(delegate { Read("<RecordList><!-- c -->" + one + "</RecordList>", false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (#comment) does not match the record definition (Test)");
        TestLog.Eq("BypassComments True: a comment inside <RecordList> is ignored", "1:{a,,,}", TD.Show(Read("<RecordList><!-- c -->" + one + "</RecordList>", false, "", true, true, false)));
        TestLog.Eq("a comment inside a value is skipped either way (InnerText)", "1:{ab,z,,}",
            TD.Show(Read("<Test><Attribute1>a<!-- c -->b</Attribute1><Attribute2>z</Attribute2></Test>", false, "", true, false, false)));

        // the AttributePrefix input is used only when DefaultConfig.AttributePrefix is empty, and the
        // structure's default is "Attr_", so with the platform defaults the input has no effect
        RLRecordXmlConfigRecordList renamed = XR.Config(XR.Record("Test", null, null, XR.Attr("Attribute1", "X_Attribute1")));
        TestLog.Eq("AttributePrefix input with DefaultConfig.AttributePrefix \"Attr_\": the input is not used", "1:{,b,,}",
            TD.Show(ReadWith("<Test Attribute1=\"v\"><Attribute2>b</Attribute2></Test>", renamed, "X_", XR.Defaults())));
        RLRecordXmlConfigRecordList attr = XR.Config(XR.Record("Test", null, null, XR.Attr("Attribute1", "Attr_Attribute1")));
        TestLog.Eq("AttributePrefix input with an empty DefaultConfig.AttributePrefix: the input is used", "1:{v,b,,}",
            TD.Show(ReadWith("<Test Attribute1=\"v\"><Attribute2>b</Attribute2></Test>", attr, "Attr_", XR.Zero())));
        TestLog.Eq("no AttributePrefix at all: an Attr_ attribute is looked up as an element", "1:{,b,,}",
            TD.Show(ReadWith("<Test Attribute1=\"v\"><Attribute2>b</Attribute2></Test>", attr, "", XR.Zero())));

        RLRecordXmlConfigRecordList item = XR.Config(XR.Record("Test", "Item", "urn:i"));
        TestLog.Eq("EffectiveName of the top records", "1:{a,,,}",
            TD.Show(TD.ReadTests("<RecordList><Item xmlns=\"urn:i\"><Attribute1>a</Attribute1></Item></RecordList>", item, XR.Defaults())));
        TestLog.Throws("EffectiveName set: an element with the structure's name is rejected (the message names the structure)",
            XR.Throws(delegate { TD.ReadTests("<Test><Attribute1>a</Attribute1></Test>", item, XR.Defaults()); }),
            typeof(ApplicationException), "The current node (Test) does not match the record definition (Test)");
    }

    private static void InvalidXml()
    {
        TestLog.Throws("malformed XML, ShowXMLInLogs False: generic message without the XML",
            XR.Throws(delegate { Read("<Test><Attribute1>a</Test>", false, "", true, false, false); }),
            typeof(ApplicationException), "Unable to load xml document - set the ShowXMLInLogs input to True for more details");
        Exception ex = XR.Throws(delegate { Read("<Test><Attribute1>a</Test>", false, "", true, false, true); });
        TestLog.Throws("malformed XML, ShowXMLInLogs True: message has the XML", ex, typeof(ApplicationException), "Unable to load xml document: <Test><Attribute1>a</Test>");
        TestLog.Check("malformed XML, ShowXMLInLogs True: the XmlException is the inner exception", ex != null && ex.InnerException is System.Xml.XmlException, TestLog.Describe(ex));
        TestLog.Throws("empty XML: ApplicationException", XR.Throws(delegate { Read("", false, "", true, false, false); }),
            typeof(ApplicationException), "Unable to load xml document - set the ShowXMLInLogs input to True for more details");
        TestLog.Throws("XML null with IgnoreXMLDeclarationAndNS: ApplicationException", XR.Throws(delegate { Read(null, true, "", true, false, true); }),
            typeof(ApplicationException), "Unable to load xml document: ");
        TestLog.Throws("a root that isn't the record: ApplicationException naming both",
            XR.Throws(delegate { Read("<Other><Attribute1>a</Attribute1></Other>", false, "", true, false, false); }),
            typeof(ApplicationException), "The current node (Other) does not match the record definition (Test)");
        TestLog.Throws("RecordList that is a record: ApplicationException",
            XR.Throws(delegate { XR.Ext.MssXmlToRecordList("<Test/>", TD.Test("", "", "", ""), XR.NoConfig(), false, "", XR.Defaults(), true, false, false); }),
            typeof(ApplicationException), "Parameter 'RecordList' must be a valid RecordList");
        TestLog.Throws("RecordList null: ApplicationException",
            XR.Throws(delegate { XR.Ext.MssXmlToRecordList("<Test/>", null, XR.NoConfig(), false, "", XR.Defaults(), true, false, false); }),
            typeof(ApplicationException), "Parameter 'RecordList' must be a valid RecordList");
    }

    // What RecordToXml writes with a configuration must read back with the same configuration.
    private static void RoundTrips()
    {
        string order = TD.Show(TD.Order().ssSTOrder);
        foreach (string name in new string[] { "platform defaults", "ExcludeIfNull False", "zero-initialized config" })
        {
            RCDefaultXmlConfigRecord c = name == "platform defaults" ? XR.Defaults() : name == "ExcludeIfNull False" ? XR.Raw() : XR.Zero();
            TestLog.Eq("round trip of an order, " + name, "1:{" + order + "}", TD.Show(TD.ReadOrders(XR.ToXml(TD.Order(), c), XR.NoConfig(), c)));
            TestLog.Eq("round trip of TestHierarchical, " + name, "1:{s|0|0.0|False|rec x,,,|list 1:{a,,,}}", ShowH(XR.ToXml(TD.Hierarchical(), c), XR.NoConfig(), c, false));
        }
        RLOrderRecordList two = new RLOrderRecordList();
        two.Append(TD.Order());
        two.Append(TD.Order());
        TestLog.Eq("round trip of a list with the root node, platform defaults", "2:{" + order + "}{" + order + "}",
            TD.Show(TD.ReadOrders(XR.ListToXml(two, XR.NoConfig(), XR.Defaults(), true), XR.NoConfig(), XR.Defaults())));

        RCAttributeXmlConfigRecord seq = XR.Attr("Lines");
        seq.ssSTAttributeXmlConfig.ssRecordListMode = "S";
        RCAttributeXmlConfigRecord child = XR.Attr("Customer");
        child.ssSTAttributeXmlConfig.ssRecordMode = "C";
        RoundTrip("RecordListMode S and RecordMode C", XR.Config(XR.Record("Order", null, null, seq, child)));
        RoundTrip("AttributeName on the order and its customer", XR.Config(XR.Record("Order", null, null, XR.Attr("Number", "OrderNo"), XR.Attr("Attr_Id", "Attr_Key")),
            XR.Record("Order/Customer", null, null, XR.Attr("Name", "FullName"))));
        RoundTrip("EffectiveName of the order", XR.Config(XR.Record("Order", "PurchaseOrder", null)));
        RoundTrip("EffectiveName of the list items (key OrderLine)", XR.Config(XR.Record("OrderLine", "Line", null)));
        RoundTrip("EffectiveName of the customer in Child mode (key Customer)", XR.Config(XR.Record("Order", null, null, child), XR.Record("Customer", "Buyer", null)));
        // KI-06: a hierarchical key for the EffectiveName of a nested record or list item is used when
        // writing but not when reading (the reader looks the child up without its parent)
        RoundTrip("EffectiveName of the list items (key Order/OrderLine)", XR.Config(XR.Record("Order/OrderLine", "Line", null)));
        RoundTrip("EffectiveName of the customer in Child mode (key Order/Customer)", XR.Config(XR.Record("Order", null, null, child), XR.Record("Order/Customer", "Buyer", null)));
        // KI-05: with a Namespace the nested list is not found (XPath without the namespace) and comes back empty
        RoundTrip("Namespace of the order", XR.Config(XR.Record("Order", null, "urn:po")));
        RLOrderRecordList ign = new RLOrderRecordList();
        RLRecordXmlConfigRecordList ns = XR.Config(XR.Record("Order", null, "urn:po"));
        XR.Ext.MssXmlToRecordList(XR.ToXml(TD.Order(), ns, XR.Raw()), ign, ns, true, "", XR.Raw(), true, false, false);
        TestLog.Eq("round trip, Namespace of the order, read with IgnoreXMLDeclarationAndNS", "1:{" + order + "}", TD.Show(ign));

        RLRecordXmlConfigRecordList blobless = TD.WithoutBlob();
        RLAllTypesRecordList all = new RLAllTypesRecordList();
        XR.FromXml(XR.ToXml(TD.AllTypes(), blobless, XR.Defaults()), all, blobless, XR.Defaults());
        STAllTypesStructure a = all[0].ssSTAllTypes, e = TD.AllTypes().ssSTAllTypes;
        TestLog.Check("round trip of every basic type but Binary Data, platform defaults",
            a.ssText == e.ssText && a.ssNumber == e.ssNumber && a.ssBigNumber == e.ssBigNumber && a.ssAmount == e.ssAmount && a.ssFlag == e.ssFlag
            && a.ssDay == e.ssDay && a.ssClock == e.ssClock && a.ssMoment == e.ssMoment,
            a.ssText + "|" + a.ssNumber + "|" + a.ssBigNumber + "|" + TestLog.Show(a.ssAmount) + "|" + a.ssFlag + "|" + TestLog.Show(a.ssDay) + "|" + TestLog.Show(a.ssClock) + "|" + TestLog.Show(a.ssMoment));
    }

    private static void RoundTrip(string name, RLRecordXmlConfigRecordList config)
    {
        string xml = XR.ToXml(TD.Order(), config, XR.Raw());
        string got;
        try { got = TD.Show(TD.ReadOrders(xml, config, XR.Raw())); }
        catch (Exception ex) { got = TestLog.Describe(ex); }
        TestLog.Check("round trip, " + name, got == "1:{" + TD.Show(TD.Order().ssSTOrder) + "}", got + " from " + TestLog.Lit(xml));
    }

    private static RLTestRecordList Read(string xml, bool ignoreDeclaration, string attributePrefix, bool bypassRoot, bool bypassComments, bool showXml)
    {
        RLTestRecordList l = new RLTestRecordList();
        XR.Ext.MssXmlToRecordList(xml, l, XR.NoConfig(), ignoreDeclaration, attributePrefix, XR.Defaults(), bypassRoot, bypassComments, showXml);
        return l;
    }

    private static RLTestRecordList ReadWith(string xml, RLRecordXmlConfigRecordList config, string attributePrefix, RCDefaultXmlConfigRecord defaults)
    {
        RLTestRecordList l = new RLTestRecordList();
        XR.Ext.MssXmlToRecordList(xml, l, config, false, attributePrefix, defaults, true, false, false);
        return l;
    }

    private static string ShowH(string xml, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults, bool ignore)
    {
        RLTestHierarchicalRecordList l = new RLTestHierarchicalRecordList();
        XR.Ext.MssXmlToRecordList(xml, l, config, ignore, "", defaults, true, false, false);
        return TD.Show(l);
    }

    /// <summary>The read fails with ApplicationException "Unable to convert xml value ... Element: X" and the given inner exception.</summary>
    private static void Fails(string name, string xml, RCDefaultXmlConfigRecord defaults, string element, Type inner)
    {
        Exception ex = XR.Throws(delegate { TD.ReadAllTypes(xml, defaults); });
        TestLog.Check(name, ex is ApplicationException && ex.Message.StartsWith("Unable to convert xml value to structure type. Element: " + element + "; Value: ", StringComparison.Ordinal)
            && ex.InnerException != null && ex.InnerException.GetType() == inner, TestLog.Describe(ex));
    }
}
