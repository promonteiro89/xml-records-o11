// XmlConfig (RecordXmlConfig / AttributeXmlConfig) and DefaultConfig options, on the writing side.
// The reading side of the same options is in XmlToRecordListTests. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class ConfigTests
{
    public static void Run()
    {
        Names();
        HierarchicalKeys();
        Modes();
        ExcludeIfNullAndNullableValue();
        XmlDeclaration();
        Indentation();
        InvalidConfig();
    }

    private static void Names()
    {
        RLRecordXmlConfigRecordList cfg = XR.Config(
            XR.Record("Order", "PurchaseOrder", "urn:po", XR.Attr("Number", "OrderNo"), XR.Attr("Attr_Id", "Attr_Key")),
            XR.Record("Order/Customer", null, null, XR.Attr("Name", "FullName")),
            XR.Record("OrderLine", "Line", null));
        TestLog.Eq("EffectiveName, Namespace, AttributeName (also of an XML attribute and of a nested record's attribute)",
            "<PurchaseOrder Key=\"7\" xmlns=\"urn:po\">\r\n\t<OrderNo>N1</OrderNo>\r\n\t<Customer Code=\"C9\">\r\n\t\t<FullName>Ann</FullName>\r\n\t</Customer>\r\n"
            + "\t<Lines>\r\n\t\t<Line>\r\n\t\t\t<Product>P</Product>\r\n\t\t\t<Quantity>2</Quantity>\r\n\t\t\t<Price>3.5</Price>\r\n\t\t</Line>\r\n"
            + "\t\t<Line>\r\n\t\t\t<Product>Q</Product>\r\n\t\t\t<Quantity>2</Quantity>\r\n\t\t\t<Price>3.5</Price>\r\n\t\t</Line>\r\n\t</Lines>\r\n</PurchaseOrder>\r\n",
            XR.ToXml(TD.Order(), cfg, XR.Raw()));

        RLRecordXmlConfigRecordList prefixAway = XR.Config(XR.Record("Order", null, null, XR.Attr("Attr_Id", "OrderId"), XR.Attr("Number", "Attr_Number")));
        TestLog.Eq("AttributeName decides attribute or element: Attr_Id renamed OrderId is an element, Number renamed Attr_Number an XML attribute",
            "<Order Number=\"N1\"><OrderId>7</OrderId><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), prefixAway, TD.Compact()));

        RLRecordXmlConfigRecordList unknown = XR.Config(XR.Record("Nothing", "X", "urn:x", XR.Attr("Zzz", "Y")), XR.Record("Order", null, null, XR.Attr("Zzz", "Y")));
        TestLog.Eq("config for a record or attribute that doesn't exist is ignored",
            XR.ToXml(TD.Order(), XR.NoConfig(), TD.Compact()), XR.ToXml(TD.Order(), unknown, TD.Compact()));

        RLRecordXmlConfigRecordList ns = XR.Config(XR.Record("OrderLine", null, "urn:l"));
        TestLog.Eq("Namespace on the records of a nested list: xmlns on each item",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine xmlns=\"urn:l\"><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine xmlns=\"urn:l\"><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), ns, TD.Compact()));

        RLRecordXmlConfigRecordList customer = XR.Config(XR.Record("Customer", "Buyer", "urn:b"));
        TestLog.Eq("EffectiveName and Namespace of a nested record in Sequence mode are not used (no record element)",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), customer, TD.Compact()));
    }

    // RecordName may be a path: "Parent/Child" configures Child only inside Parent, and wins over "Child".
    private static void HierarchicalKeys()
    {
        RLRecordXmlConfigRecordList both = XR.Config(
            XR.Record("Test", null, null, XR.Attr("Attribute1", "Plain")),
            XR.Record("TestHierarchical/Test", null, null, XR.Attr("Attribute1", "Nested")));
        string x = XR.ToXml(TD.Hierarchical(), both, XR.Defaults());
        TestLog.Eq("\"TestHierarchical/Test\" wins over \"Test\" for the records inside TestHierarchical",
            XR.Lines("<TestHierarchical>", "  <aString>s</aString>", "  <aInt>0</aInt>", "  <aDecimal>0.0</aDecimal>", "  <aBoolean>false</aBoolean>",
                "  <aRecordList>", "    <Test>", "      <Nested>a</Nested>", "    </Test>", "  </aRecordList>",
                "  <aRecord>", "    <Nested>x</Nested>", "  </aRecord>", "</TestHierarchical>"), x);
        TestLog.Eq("\"Test\" still applies to a Test record on its own",
            XR.Lines("<Test>", "  <Plain>a</Plain>", "</Test>"), XR.ToXml(TD.Test("a", "", "", ""), both, XR.Defaults()));

        foreach (string key in new string[] { "Level3", "Level2/Level3", "Level1/Level2/Level3", "Level1/Level3" })
        {
            RLRecordXmlConfigRecordList cfg = XR.Config(XR.Record(key, null, null, XR.Attr("Name", "Renamed")));
            // KI-07: "Level2/Level3" is never tried for the third level: the lookup drops the middle level
            // first ("Level1/Level3") and then falls back to "Level3"
            TestLog.Eq("three levels, key \"" + key + "\" configures the innermost record",
                "<Level1><Name>one</Name><Child><Level2><Name>two</Name><Child><Level3><Renamed>three</Renamed></Level3></Child></Level2></Child></Level1>",
                XR.ToXml(TD.Levels(), cfg, XR.Zero()));
        }
        RLRecordXmlConfigRecordList level2 = XR.Config(XR.Record("Level1/Level2", null, null, XR.Attr("Name", "Renamed")));
        TestLog.Eq("three levels, key \"Level1/Level2\" configures the middle record only",
            "<Level1><Name>one</Name><Child><Level2><Renamed>two</Renamed><Child><Level3><Name>three</Name></Level3></Child></Level2></Child></Level1>",
            XR.ToXml(TD.Levels(), level2, XR.Zero()));
    }

    private static void Modes()
    {
        RCAttributeXmlConfigRecord lines = XR.Attr("Lines");
        lines.ssSTAttributeXmlConfig.ssRecordListMode = "S";
        TestLog.Eq("RecordListMode S: list items directly in the parent, no wrapper",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Order>",
            XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null, lines)), TD.Compact()));

        RCAttributeXmlConfigRecord cust = XR.Attr("Customer");
        cust.ssSTAttributeXmlConfig.ssRecordMode = "C";
        TestLog.Eq("RecordMode C: the record's element inside the attribute's element (XML attribute on the inner one)",
            "<Order Id=\"7\"><Number>N1</Number><Customer><Customer Code=\"C9\"><Name>Ann</Name></Customer></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null, cust)), TD.Compact()));

        RCDefaultXmlConfigRecord seq = TD.Compact();
        seq.ssSTDefaultXmlConfig.ssRecordListMode = "S";
        seq.ssSTDefaultXmlConfig.ssRecordMode = "C";
        TestLog.Eq("DefaultConfig RecordListMode S and RecordMode C apply to every attribute without its own config",
            "<Order Id=\"7\"><Number>N1</Number><Customer><Customer Code=\"C9\"><Name>Ann</Name></Customer></Customer>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Order>",
            XR.ToXml(TD.Order(), seq));

        RCDefaultXmlConfigRecord lower = TD.Compact();
        lower.ssSTDefaultXmlConfig.ssRecordListMode = "s";
        TestLog.Eq("modes are case sensitive: \"s\" means Child",
            XR.ToXml(TD.Order(), TD.Compact()), XR.ToXml(TD.Order(), lower));

        RCAttributeXmlConfigRecord named = XR.Attr("Lines", "Items");
        named.ssSTAttributeXmlConfig.ssRecordListMode = "C";
        TestLog.Eq("AttributeName renames a list's wrapper",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Items>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Items></Order>",
            XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null, named)), TD.Compact()));
    }

    private static void ExcludeIfNullAndNullableValue()
    {
        RCAttributeXmlConfigRecord number = XR.Attr("Number");
        number.ssSTAttributeXmlConfig.ssNullableValue = "N1";
        TestLog.Eq("NullableValue: an attribute equal to it is left out (ExcludeIfNull of the attribute True)",
            "<Order Id=\"7\"><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null, number)), TD.Compact()));

        RCAllTypesRecord empty = new RCAllTypesRecord(null);
        empty.ssSTAllTypes.ssText = "t";
        RCAttributeXmlConfigRecord zero = XR.Attr("Number");
        zero.ssSTAttributeXmlConfig.ssNullableValue = "0";
        RCAttributeXmlConfigRecord flag = XR.Attr("Flag");
        flag.ssSTAttributeXmlConfig.ssNullableValue = "False";
        RCAttributeXmlConfigRecord day = XR.Attr("Day");
        day.ssSTAttributeXmlConfig.ssNullableValue = "1900-01-01 00:00:00";
        string x = XR.ToXml(empty, XR.Config(XR.Record("AllTypes", null, null, zero, flag, day)), TD.Compact());
        TestLog.Check("NullableValue \"0\" leaves out an Integer 0", !x.Contains("<Number>"), x);
        TestLog.Check("NullableValue \"False\" (the .NET text) leaves out a Boolean false", !x.Contains("<Flag>"), x);
        TestLog.Check("NullableValue \"1900-01-01 00:00:00\" does not match a null Date (compared with the en-US text and with \"\")",
            x.Contains("<Day></Day>"), x);

        RCAttributeXmlConfigRecord keep = XR.Attr("Number");
        keep.ssSTAttributeXmlConfig.ssNullableValue = "0";
        keep.ssSTAttributeXmlConfig.ssExcludeIfNull = false;
        TestLog.Check("attribute ExcludeIfNull False keeps an attribute equal to its NullableValue",
            XR.ToXml(empty, XR.Config(XR.Record("AllTypes", null, null, keep)), TD.Compact()).Contains("<Number>0</Number>"));

        RCAttributeXmlConfigRecord drop = XR.Attr("Attribute3");
        TestLog.Eq("attribute ExcludeIfNull True with DefaultConfig ExcludeIfNull False leaves out only that empty attribute",
            "<Test><Attribute1></Attribute1><Attribute2></Attribute2><Attribute4></Attribute4></Test>",
            XR.ToXml(new RCTestRecord(null), XR.Config(XR.Record("Test", null, null, drop)), TD.Compact()));

        // KI-03: DefaultConfig ExcludeIfNull True runs a clean-up that removes every empty element, so an
        // attribute configured with ExcludeIfNull False is removed too
        RCAttributeXmlConfigRecord keep3 = XR.Attr("Attribute3");
        keep3.ssSTAttributeXmlConfig.ssExcludeIfNull = false;
        TestLog.Eq("attribute ExcludeIfNull False keeps its empty element when DefaultConfig ExcludeIfNull is True",
            XR.Lines("<Test>", "  <Attribute1>a</Attribute1>", "  <Attribute3></Attribute3>", "</Test>"),
            XR.ToXml(TD.Test("a", "", "", ""), XR.Config(XR.Record("Test", null, null, keep3)), XR.Defaults()));

        RCAttributeXmlConfigRecord list = XR.Attr("Lines");
        list.ssSTAttributeXmlConfig.ssExcludeIfNull = false;
        RCOrderRecord noLines = TD.Order();
        noLines.ssSTOrder.ssLines = new RLOrderLineRecordList();
        TestLog.Eq("an empty list with attribute ExcludeIfNull False keeps its wrapper (DefaultConfig ExcludeIfNull False)",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines></Lines></Order>",
            XR.ToXml(noLines, XR.Config(XR.Record("Order", null, null, list)), TD.Compact()));
        RCAttributeXmlConfigRecord listDrop = XR.Attr("Lines");
        TestLog.Eq("an empty list with attribute ExcludeIfNull True is left out (DefaultConfig ExcludeIfNull False)",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer></Order>",
            XR.ToXml(noLines, XR.Config(XR.Record("Order", null, null, listDrop)), TD.Compact()));
    }

    private static void XmlDeclaration()
    {
        RCTestRecord t = TD.Test("a", "", "", "");
        RCDefaultXmlConfigRecord d = TD.Compact();
        d.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssAddDeclaration = true;
        TestLog.Eq("AddDeclaration with the structure's defaults: version 1.0, UTF-8, standalone no, then Environment.NewLine",
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + XR.NL
            + "<Test><Attribute1>a</Attribute1><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test>",
            XR.ToXml(t, d));

        RCDefaultXmlConfigRecord custom = TD.Compact();
        custom.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssAddDeclaration = true;
        custom.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssVersion = "1.1";
        custom.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssEncoding = "UTF-16";
        custom.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssStandalone = "yes";
        TestLog.Check("AddDeclaration with Version 1.1, Encoding UTF-16, Standalone yes",
            XR.ToXml(t, custom).StartsWith("<?xml version=\"1.1\" encoding=\"UTF-16\" standalone=\"yes\"?>" + XR.NL + "<Test>", StringComparison.Ordinal));

        RCDefaultXmlConfigRecord zero = XR.Zero();
        zero.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssAddDeclaration = true;
        TestLog.Check("AddDeclaration on a zero-initialized config: empty version, encoding and standalone",
            XR.ToXml(t, zero).StartsWith("<?xml version=\"\" encoding=\"\" standalone=\"\"?>" + XR.NL + "<Test>", StringComparison.Ordinal));

        RCDefaultXmlConfigRecord withClean = XR.Defaults();
        withClean.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssAddDeclaration = true;
        TestLog.Eq("AddDeclaration with the platform defaults: declaration added after the clean-up",
            "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + XR.NL + XR.Lines("<Test>", "  <Attribute1>a</Attribute1>", "</Test>"),
            XR.ToXml(t, withClean));
        TestLog.Check("no declaration by default", !XR.ToXml(t, XR.Defaults()).StartsWith("<?xml", StringComparison.Ordinal));
    }

    private static void Indentation()
    {
        TestLog.Eq("Indentation False, ExcludeIfNull False: no tabs and no line breaks",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), TD.Compact()));
        RCDefaultXmlConfigRecord flat = XR.Defaults();
        flat.ssSTDefaultXmlConfig.ssIndentation = false;
        // KI-04: the ExcludeIfNull clean-up (XDocument.ToString) indents the XML again
        TestLog.Eq("Indentation False with the platform defaults otherwise: not formatted",
            "<Order Id=\"7\"><Number>N1</Number><Customer Code=\"C9\"><Name>Ann</Name></Customer><Lines>"
            + "<OrderLine><Product>P</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine>"
            + "<OrderLine><Product>Q</Product><Quantity>2</Quantity><Price>3.5</Price></OrderLine></Lines></Order>",
            XR.ToXml(TD.Order(), flat));
    }

    private static void InvalidConfig()
    {
        TestLog.Throws("the same RecordName twice: ArgumentException",
            XR.Throws(delegate { XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null), XR.Record("Order", null, null)), XR.Raw()); }),
            typeof(ArgumentException), null);
        TestLog.Throws("the same attribute Name twice in a record: ArgumentException",
            XR.Throws(delegate { XR.ToXml(TD.Order(), XR.Config(XR.Record("Order", null, null, XR.Attr("Number"), XR.Attr("Number"))), XR.Raw()); }),
            typeof(ArgumentException), null);
    }
}
