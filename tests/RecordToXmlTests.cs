// RecordToXml: one record (a structure, an entity, or a record of an aggregate) to XML.
// The expected XML strings are what the published extension writes today, checked by hand against the
// action's description; where it is wrong the check states the right result and is registered in
// KnownIssues.cs. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class RecordToXmlTests
{
    public static void Run()
    {
        BasicTypes();
        DefaultsAndEmptyValues();
        SpecialCharacters();
        NestedRecordAndList();
        AttributeAndContentPrefixes();
        RecordShapes();
        InvalidInputs();
    }

    // Text, Integer, Long Integer, Decimal, Boolean, Date, Time, Date Time and Binary Data.
    private static void BasicTypes()
    {
        TestLog.Eq("every basic type, ExcludeIfNull False: tab-indented elements, CRLF after each",
            "<AllTypes>\r\n\t<Text>Hello</Text>\r\n\t<Number>42</Number>\r\n\t<BigNumber>9876543210</BigNumber>\r\n"
            + "\t<Amount>1234.50</Amount>\r\n\t<Flag>true</Flag>\r\n\t<Day>2024-01-15</Day>\r\n\t<Clock>13:45:30</Clock>\r\n"
            + "\t<Moment>2024-01-15T13:45:30</Moment>\r\n</AllTypes>\r\n",
            NoBlob(TD.AllTypes(), XR.Raw()));

        TestLog.Eq("Decimal keeps its scale (1234.50 stays 1234.50)", "1234.50", TD.Element(Write(TD.AllTypes(), TD.Compact()), "Amount"));
        TestLog.Eq("Boolean is written lower case (true)", "true", TD.Element(Write(TD.AllTypes(), TD.Compact()), "Flag"));
        TestLog.Eq("Long Integer beyond Integer range", "9876543210", TD.Element(Write(TD.AllTypes(), TD.Compact()), "BigNumber"));

        // KI-02: Binary Data is written as the .NET type name instead of its content (xs:base64Binary)
        TestLog.Eq("Binary Data is written as base64 (01 02 03 -> AQID)", "AQID", TD.Element(Write(TD.AllTypes(), TD.Compact()), "Blob"));
        RCAllTypesRecord empty = new RCAllTypesRecord(null);
        TestLog.Eq("empty Binary Data is written as an empty element", "", TD.Element(Write(empty, TD.Compact()), "Blob"));

        RCAllTypesRecord neg = new RCAllTypesRecord(null);
        neg.ssSTAllTypes.ssNumber = -5;
        neg.ssSTAllTypes.ssBigNumber = long.MinValue;
        neg.ssSTAllTypes.ssAmount = -0.001M;
        string x = Write(neg, TD.Compact());
        TestLog.Eq("negative Integer", "-5", TD.Element(x, "Number"));
        TestLog.Eq("Long Integer minimum", "-9223372036854775808", TD.Element(x, "BigNumber"));
        TestLog.Eq("negative Decimal with three places", "-0.001", TD.Element(x, "Amount"));

        RCAllTypesRecord big = new RCAllTypesRecord(null);
        big.ssSTAllTypes.ssAmount = 1234567.891M;
        TestLog.Eq("Decimal has no thousands separator (en-US default culture)", "1234567.891", TD.Element(Write(big, TD.Compact()), "Amount"));
    }

    // With the platform defaults (ExcludeIfNull True) empty values are left out and the XML is
    // re-formatted by an XDocument pass: two-space indentation, Environment.NewLine, no trailing line break.
    private static void DefaultsAndEmptyValues()
    {
        TestLog.Eq("every basic type, platform defaults: re-formatted with two spaces",
            XR.Lines("<AllTypes>", "  <Text>Hello</Text>", "  <Number>42</Number>", "  <BigNumber>9876543210</BigNumber>",
                "  <Amount>1234.50</Amount>", "  <Flag>true</Flag>", "  <Day>2024-01-15</Day>", "  <Clock>13:45:30</Clock>",
                "  <Moment>2024-01-15T13:45:30</Moment>", "</AllTypes>"),
            NoBlob(TD.AllTypes(), XR.Defaults()));

        // the zero-initialized config: ExcludeIfNull False, Indentation False, no prefixes
        TestLog.Eq("every basic type, zero-initialized config: one line, no indentation",
            "<AllTypes><Text>Hello</Text><Number>42</Number><BigNumber>9876543210</BigNumber><Amount>1234.50</Amount>"
            + "<Flag>true</Flag><Day>2024-01-15</Day><Clock>13:45:30</Clock><Moment>2024-01-15T13:45:30</Moment>"
            + "</AllTypes>",
            NoBlob(TD.AllTypes(), XR.Zero()));

        RCAllTypesRecord empty = new RCAllTypesRecord(null);
        TestLog.Eq("default values, ExcludeIfNull False: empty Text and dates as empty elements, 0, 0.0, false kept",
            "<AllTypes>\r\n\t<Text></Text>\r\n\t<Number>0</Number>\r\n\t<BigNumber>0</BigNumber>\r\n\t<Amount>0.0</Amount>\r\n"
            + "\t<Flag>false</Flag>\r\n\t<Day></Day>\r\n\t<Clock></Clock>\r\n\t<Moment></Moment>\r\n</AllTypes>\r\n",
            NoBlob(empty, XR.Raw()));
        TestLog.Eq("default values, platform defaults: empty Text and null dates left out; 0, 0.0 and false kept",
            XR.Lines("<AllTypes>", "  <Number>0</Number>", "  <BigNumber>0</BigNumber>", "  <Amount>0.0</Amount>", "  <Flag>false</Flag>", "</AllTypes>"),
            NoBlob(empty, XR.Defaults()));
        TestLog.Check("empty Binary Data is left out with the platform defaults", !Write(empty, XR.Defaults()).Contains("Blob"), Write(empty, XR.Defaults()));

        // KI-03: the ExcludeIfNull clean-up also removes the record's own element when every value is empty
        TestLog.Eq("a record whose values are all empty, platform defaults: an empty record element",
            "<Test />", Write(new RCTestRecord(null), XR.Defaults()));

        TestLog.Eq("Text of only spaces is left out with the platform defaults (treated as empty)",
            XR.Lines("<Test>", "  <Attribute2> x </Attribute2>", "</Test>"),
            Write(TD.Test("  ", " x ", "", ""), XR.Defaults()));
    }

    private static void SpecialCharacters()
    {
        RCTestRecord t = TD.Test("<&>\"'", "a&amp;b", "", "");
        TestLog.Eq("the five XML special characters are escaped (ExcludeIfNull False)",
            "&lt;&amp;&gt;&quot;&apos;", TD.Element(Write(t, TD.Compact()), "Attribute1"));
        TestLog.Eq("an existing entity is escaped again (a&amp;b -> a&amp;amp;b)",
            "a&amp;amp;b", TD.Element(Write(t, TD.Compact()), "Attribute2"));
        TestLog.Eq("special characters with the platform defaults: the clean-up re-escapes, quotes stay literal",
            XR.Lines("<Test>", "  <Attribute1>&lt;&amp;&gt;\"'</Attribute1>", "  <Attribute2>a&amp;amp;b</Attribute2>", "</Test>"),
            Write(t, XR.Defaults()));

        RCTestRecord u = TD.Test("Olá € 日本 😀", "line1\r\nline2\ttab", "", "");
        TestLog.Eq("non-ASCII text and line breaks are written as is (ExcludeIfNull False)",
            "<Test>\r\n\t<Attribute1>Olá € 日本 😀</Attribute1>\r\n\t<Attribute2>line1\r\nline2\ttab</Attribute2>\r\n"
            + "\t<Attribute3></Attribute3>\r\n\t<Attribute4></Attribute4>\r\n</Test>\r\n",
            Write(u, XR.Raw()));
        TestLog.Eq("a CRLF inside Text becomes a new line with the platform defaults (XML end-of-line handling)",
            XR.Lines("<Test>", "  <Attribute1>Olá € 日本 😀</Attribute1>", "  <Attribute2>line1" + XR.NL + "line2\ttab</Attribute2>", "</Test>"),
            Write(u, XR.Defaults()));

        // KI-09: a character XML 1.0 cannot hold (U+0001) is written raw: the output is not XML
        RCTestRecord ctrl = TD.Test("a\u0001b", "", "", "");
        string raw = null;
        Exception ex = XR.Throws(delegate { raw = Write(ctrl, XR.Raw()); });
        TestLog.Check("a control character gives well-formed XML or a clear error (ExcludeIfNull False)",
            ex is ApplicationException || (ex == null && IsWellFormed(raw)), ex != null ? TestLog.Describe(ex) : TestLog.Lit(raw));
        ex = XR.Throws(delegate { Write(ctrl, XR.Defaults()); });
        TestLog.Check("a control character gives well-formed XML or a clear error (platform defaults)",
            ex == null || ex is ApplicationException, TestLog.Describe(ex));
    }

    private static void NestedRecordAndList()
    {
        TestLog.Eq("nested record (RecordMode S: its attributes directly in the element) and list (RecordListMode C: items in a wrapper)",
            "<Order Id=\"7\">\r\n\t<Number>N1</Number>\r\n\t<Customer Code=\"C9\">\r\n\t\t<Name>Ann</Name>\r\n\t</Customer>\r\n"
            + "\t<Lines>\r\n\t\t<OrderLine>\r\n\t\t\t<Product>P</Product>\r\n\t\t\t<Quantity>2</Quantity>\r\n\t\t\t<Price>3.5</Price>\r\n\t\t</OrderLine>\r\n"
            + "\t\t<OrderLine>\r\n\t\t\t<Product>Q</Product>\r\n\t\t\t<Quantity>2</Quantity>\r\n\t\t\t<Price>3.5</Price>\r\n\t\t</OrderLine>\r\n"
            + "\t</Lines>\r\n</Order>\r\n",
            Write(TD.Order(), XR.Raw()));
        TestLog.Eq("nested record and list, platform defaults",
            XR.Lines("<Order Id=\"7\">", "  <Number>N1</Number>", "  <Customer Code=\"C9\">", "    <Name>Ann</Name>", "  </Customer>",
                "  <Lines>", "    <OrderLine>", "      <Product>P</Product>", "      <Quantity>2</Quantity>", "      <Price>3.5</Price>", "    </OrderLine>",
                "    <OrderLine>", "      <Product>Q</Product>", "      <Quantity>2</Quantity>", "      <Price>3.5</Price>", "    </OrderLine>",
                "  </Lines>", "</Order>"),
            Write(TD.Order(), XR.Defaults()));
        // zero-initialized config: RecordMode "" means Child (record element inside the attribute element)
        // and AttributePrefix "" means Attr_ attributes are plain elements
        TestLog.Eq("nested record and list, zero-initialized config: Child mode and no attribute prefix",
            "<Order><Attr_Id>7</Attr_Id><Number>N1</Number><Customer><Customer><Attr_Code>C9</Attr_Code><Name>Ann</Name></Customer></Customer>"
            + "<Lines><OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            Write(TD.Order(), XR.Zero()));

        RCOrderRecord noLines = TD.Order();
        noLines.ssSTOrder.ssLines = new RLOrderLineRecordList();
        TestLog.Eq("empty nested list, ExcludeIfNull False: an empty wrapper with a line break",
            "<Order Id=\"7\">\r\n\t<Number>N1</Number>\r\n\t<Customer Code=\"C9\">\r\n\t\t<Name>Ann</Name>\r\n\t</Customer>\r\n\t<Lines>\r\n\t</Lines>\r\n</Order>\r\n",
            Write(noLines, XR.Raw()));
        TestLog.Eq("empty nested list, platform defaults: left out",
            XR.Lines("<Order Id=\"7\">", "  <Number>N1</Number>", "  <Customer Code=\"C9\">", "    <Name>Ann</Name>", "  </Customer>", "</Order>"),
            Write(noLines, XR.Defaults()));

        TestLog.Eq("the extension's TestHierarchical structure, ExcludeIfNull False",
            "<TestHierarchical>\r\n\t<aString>s</aString>\r\n\t<aInt>0</aInt>\r\n\t<aDecimal>0.0</aDecimal>\r\n\t<aBoolean>false</aBoolean>\r\n"
            + "\t<aDate></aDate>\r\n\t<aDateTime></aDateTime>\r\n\t<aTime></aTime>\r\n\t<aRecordList>\r\n\t\t<Test>\r\n\t\t\t<Attribute1>a</Attribute1>\r\n"
            + "\t\t\t<Attribute2></Attribute2>\r\n\t\t\t<Attribute3></Attribute3>\r\n\t\t\t<Attribute4></Attribute4>\r\n\t\t</Test>\r\n\t</aRecordList>\r\n"
            + "\t<aRecord>\r\n\t\t<Attribute1>x</Attribute1>\r\n\t\t<Attribute2></Attribute2>\r\n\t\t<Attribute3></Attribute3>\r\n\t\t<Attribute4></Attribute4>\r\n"
            + "\t</aRecord>\r\n</TestHierarchical>\r\n",
            Write(TD.Hierarchical(), XR.Raw()));
        TestLog.Eq("the extension's TestHierarchical structure, platform defaults",
            XR.Lines("<TestHierarchical>", "  <aString>s</aString>", "  <aInt>0</aInt>", "  <aDecimal>0.0</aDecimal>", "  <aBoolean>false</aBoolean>",
                "  <aRecordList>", "    <Test>", "      <Attribute1>a</Attribute1>", "    </Test>", "  </aRecordList>",
                "  <aRecord>", "    <Attribute1>x</Attribute1>", "  </aRecord>", "</TestHierarchical>"),
            Write(TD.Hierarchical(), XR.Defaults()));
        TestLog.Eq("three nesting levels, zero-initialized config",
            "<Level1><Name>one</Name><Child><Level2><Name>two</Name><Child><Level3><Name>three</Name></Level3></Child></Level2></Child></Level1>",
            Write(TD.Levels(), XR.Zero()));
    }

    private static void AttributeAndContentPrefixes()
    {
        RCNoteRecord n = new RCNoteRecord(null);
        n.ssSTNote.ssAttr_Lang = "en";
        n.ssSTNote.ssCont_Text = "hi there";
        TestLog.Eq("Attr_ attribute becomes an XML attribute and Cont_ attribute the element's text (ExcludeIfNull False)",
            "<Note Lang=\"en\">\r\n\thi there\r\n</Note>\r\n", Write(n, XR.Raw()));
        TestLog.Eq("Attr_ and Cont_, platform defaults: mixed content is kept as written (not re-indented)",
            "<Note Lang=\"en\">" + XR.NL + "\thi there" + XR.NL + "</Note>", Write(n, XR.Defaults()));
        TestLog.Eq("Attr_ and Cont_, Indentation False: content directly in the element",
            "<Note Lang=\"en\">hi there</Note>", Write(n, TD.Compact()));

        RCNoteRecord quote = new RCNoteRecord(null);
        quote.ssSTNote.ssAttr_Lang = "a\"b<c";
        TestLog.Eq("an XML attribute value is escaped", "<Note Lang=\"a&quot;b&lt;c\"></Note>", Write(quote, TD.Compact()));

        // attributes are inserted before the end of the start tag, and before xmlns when there is one
        RLRecordXmlConfigRecordList cfg = XR.Config(XR.Record("Test", null, "urn:t", XR.Attr("Attribute1", "Attr_A"), XR.Attr("Attribute2", "Attr_B")));
        TestLog.Eq("two XML attributes and a namespace: attributes in attribute order, before xmlns",
            "<Test A=\"1\" B=\"2\" xmlns=\"urn:t\"><Attribute3>3</Attribute3><Attribute4></Attribute4></Test>",
            XR.ToXml(TD.Test("1", "2", "3", ""), cfg, TD.Compact()));

        RLRecordXmlConfigRecordList nested = XR.Config(XR.Record("TestHierarchical/Test", null, null, XR.Attr("Attribute1", "Attr_A")));
        TestLog.Eq("an XML attribute of a nested record in Sequence mode goes on the attribute's element",
            "<TestHierarchical><aString>s</aString><aInt>0</aInt><aDecimal>0.0</aDecimal><aBoolean>false</aBoolean><aDate></aDate><aDateTime></aDateTime><aTime></aTime>"
            + "<aRecordList><Test A=\"a\"><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test></aRecordList>"
            + "<aRecord A=\"x\"><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></aRecord></TestHierarchical>",
            XR.ToXml(TD.Hierarchical(), nested, TD.Compact()));
    }

    private static void RecordShapes()
    {
        RCAllTypesRecord r = TD.AllTypes();
        TestLog.Eq("a structure passed directly (ST, not its RC record) gives the same XML",
            NoBlob(r, XR.Raw()), NoBlob(r.ssSTAllTypes, XR.Raw()));

        RCProductRecord p = new RCProductRecord(null);
        p.ssENProduct.ssId = 5;
        p.ssENProduct.ssName = "Pen";
        TestLog.Eq("an entity record: element named after the entity (EN prefix and Entity suffix removed)",
            "<Product><Id>5</Id><Name>Pen</Name></Product>", Write(p, TD.Compact()));
        TestLog.Eq("an entity passed directly (ENProductEntity)",
            "<Product><Id>5</Id><Name>Pen</Name></Product>", Write(p.ssENProduct, TD.Compact()));

        // KI-08: a record of an aggregate that joins two sources has two structures; only the first is written
        RCOrderCustomerRecord oc = new RCOrderCustomerRecord(null);
        oc.ssSTOrder = TD.Order().ssSTOrder;
        oc.ssSTCustomer.ssName = "Zed";
        string x = Write(oc, TD.Compact());
        TestLog.Check("a record with two structures writes the first one (Order)", x.StartsWith("<Order Id=\"7\">", StringComparison.Ordinal), TestLog.Lit(x));
        TestLog.Check("a record with two structures also writes the second one (Customer Zed)", x.Contains("Zed"), TestLog.Lit(x));
    }

    private static void InvalidInputs()
    {
        TestLog.Throws("Record null: ApplicationException", XR.Throws(delegate { Write(null, XR.Defaults()); }),
            typeof(ApplicationException), "Parameter 'Record' must be a valid IRecord.");
        TestLog.Throws("Record that is a Text: ApplicationException", XR.Throws(delegate { Write("x", XR.Defaults()); }),
            typeof(ApplicationException), "Parameter 'Record' must be a valid IRecord.");
        TestLog.Throws("Record that is a record list: ApplicationException", XR.Throws(delegate { Write(new RLTestRecordList(), XR.Defaults()); }),
            typeof(ApplicationException), "Parameter 'Record' must be a valid IRecord.");
        TestLog.Eq("XmlConfig null works like an empty list",
            Write(TD.Order(), XR.Raw()), XR.ToXml(TD.Order(), null, XR.Raw()));
        RCAllTypesRecord bad = new RCAllTypesRecord(null);
        RCDefaultXmlConfigRecord badCulture = XR.Raw();
        badCulture.ssSTDefaultXmlConfig.ssTypeDecimalCulture = "xx-INVALID";
        TestLog.Throws("an unknown TypeDecimalCulture fails with CultureNotFoundException", XR.Throws(delegate { Write(bad, badCulture); }),
            typeof(System.Globalization.CultureNotFoundException), "xx-INVALID");
    }

    // Binary Data has its own checks (KI-02); the whole-record checks leave it out so they show everything else
    private static string NoBlob(object record, RCDefaultXmlConfigRecord defaults)
    {
        return XR.ToXml(record, TD.WithoutBlob(), defaults);
    }

    private static string Write(object record, RCDefaultXmlConfigRecord defaults)
    {
        return XR.ToXml(record, defaults);
    }

    internal static bool IsWellFormed(string xml)
    {
        try { new System.Xml.XmlDocument().LoadXml(xml); return true; }
        catch (System.Xml.XmlException) { return false; }
    }
}
