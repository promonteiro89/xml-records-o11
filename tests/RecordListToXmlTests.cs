// RecordListToXml: a record list to XML, with or without the <RecordList> root node. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class RecordListToXmlTests
{
    public static void Run()
    {
        DefaultConfig();
        RootNode();
        Layout();
        InvalidInputs();
    }

    private static RLTestRecordList TwoTests()
    {
        return TD.Tests(TD.Test("a", "", "", ""), TD.Test("b", "B", "", ""));
    }

    // The configuration every app starts with: DefaultConfig left empty (the structure's defaults).
    private static void DefaultConfig()
    {
        RLTestRecordList one = TD.Tests(TD.Test("a", "", "", ""));
        TestLog.Eq("one record, platform defaults, no root node",
            XR.Lines("<Test>", "  <Attribute1>a</Attribute1>", "</Test>"),
            XR.ListToXml(one, XR.NoConfig(), XR.Defaults(), false));

        // KI-01: with ExcludeIfNull True the clean-up parses the result as one XML document, which two
        // records side by side are not: XmlException "There are multiple root elements"
        string two = null;
        Exception ex = XR.Throws(delegate { two = XR.ListToXml(TwoTests(), XR.NoConfig(), XR.Defaults(), false); });
        TestLog.NoThrow("two records, platform defaults, no root node: no exception", ex);
        TestLog.Check("two records, platform defaults, no root node: both records written",
            two != null && Count(two, "<Test>") == 2 && two.Contains("<Attribute1>b</Attribute1>"), two == null ? TestLog.Describe(ex) : TestLog.Lit(two));

        TestLog.Eq("two records, platform defaults, root node",
            XR.Lines("<RecordList>", "  <Test>", "    <Attribute1>a</Attribute1>", "  </Test>", "  <Test>", "    <Attribute1>b</Attribute1>",
                "    <Attribute2>B</Attribute2>", "  </Test>", "</RecordList>"),
            XR.ListToXml(TwoTests(), XR.NoConfig(), XR.Defaults(), true));

        TestLog.Eq("two records, ExcludeIfNull False, no root node: records one after the other",
            "<Test>\r\n\t<Attribute1>a</Attribute1>\r\n\t<Attribute2></Attribute2>\r\n\t<Attribute3></Attribute3>\r\n\t<Attribute4></Attribute4>\r\n</Test>\r\n"
            + "<Test>\r\n\t<Attribute1>b</Attribute1>\r\n\t<Attribute2>B</Attribute2>\r\n\t<Attribute3></Attribute3>\r\n\t<Attribute4></Attribute4>\r\n</Test>\r\n",
            XR.ListToXml(TwoTests(), XR.NoConfig(), XR.Raw(), false));

        TestLog.Eq("two records, zero-initialized config (new RCDefaultXmlConfigRecord()), no root node",
            "<Test><Attribute1>a</Attribute1><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test>"
            + "<Test><Attribute1>b</Attribute1><Attribute2>B</Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test>",
            XR.ListToXml(TwoTests(), XR.NoConfig(), XR.Zero(), false));

        RLOrderRecordList orders = new RLOrderRecordList();
        orders.Append(TD.Order());
        RCOrderRecord second = TD.Order();
        second.ssSTOrder.ssAttr_Id = 8;
        orders.Append(second);
        TestLog.Eq("two orders with nested records and lists, platform defaults, root node",
            XR.Lines("<RecordList>",
                "  <Order Id=\"7\">", "    <Number>N1</Number>", "    <Customer Code=\"C9\">", "      <Name>Ann</Name>", "    </Customer>", "    <Lines>",
                "      <OrderLine>", "        <Product>P</Product>", "        <Quantity>2</Quantity>", "        <Price>3.5</Price>", "      </OrderLine>",
                "      <OrderLine>", "        <Product>Q</Product>", "        <Quantity>2</Quantity>", "        <Price>3.5</Price>", "      </OrderLine>",
                "    </Lines>", "  </Order>",
                "  <Order Id=\"8\">", "    <Number>N1</Number>", "    <Customer Code=\"C9\">", "      <Name>Ann</Name>", "    </Customer>", "    <Lines>",
                "      <OrderLine>", "        <Product>P</Product>", "        <Quantity>2</Quantity>", "        <Price>3.5</Price>", "      </OrderLine>",
                "      <OrderLine>", "        <Product>Q</Product>", "        <Quantity>2</Quantity>", "        <Price>3.5</Price>", "      </OrderLine>",
                "    </Lines>", "  </Order>", "</RecordList>"),
            XR.ListToXml(orders, XR.NoConfig(), XR.Defaults(), true));
    }

    private static void RootNode()
    {
        RLTestRecordList one = TD.Tests(TD.Test("a", "", "", ""));
        // the root node's lines end with Environment.NewLine, the records' with CRLF
        TestLog.Eq("root node, ExcludeIfNull False: records indented one tab inside <RecordList>",
            "<RecordList>" + XR.NL + "\t<Test>\r\n\t\t<Attribute1>a</Attribute1>\r\n\t\t<Attribute2></Attribute2>\r\n\t\t<Attribute3></Attribute3>\r\n"
            + "\t\t<Attribute4></Attribute4>\r\n\t</Test>\r\n</RecordList>" + XR.NL,
            XR.ListToXml(one, XR.NoConfig(), XR.Raw(), true));

        RCDefaultXmlConfigRecord decl = XR.Defaults();
        decl.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssAddDeclaration = true;
        decl.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssEncoding = "ISO-8859-1";
        decl.ssSTDefaultXmlConfig.ssXmlDeclaration.ssSTXmlDeclaration.ssStandalone = "yes";
        TestLog.Eq("root node with an XML declaration (encoding and standalone set), platform defaults",
            "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" standalone=\"yes\"?>" + XR.NL
            + XR.Lines("<RecordList>", "  <Test>", "    <Attribute1>a</Attribute1>", "  </Test>", "</RecordList>"),
            XR.ListToXml(one, XR.NoConfig(), decl, true));

        RLRecordXmlConfigRecordList cfg = XR.Config(XR.Record("Test", "Item", "urn:i"));
        TestLog.Eq("root node with EffectiveName and Namespace on the records, zero-initialized config",
            "<RecordList>" + XR.NL + "<Item xmlns=\"urn:i\"><Attribute1>a</Attribute1><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Item>"
            + "<Item xmlns=\"urn:i\"><Attribute1>b</Attribute1><Attribute2>B</Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Item></RecordList>" + XR.NL,
            XR.ListToXml(TwoTests(), cfg, XR.Zero(), true));
    }

    private static void Layout()
    {
        // KI-04: Indentation False still puts a line break after <RecordList> and </RecordList>
        TestLog.Eq("root node, Indentation False, ExcludeIfNull False: one line",
            "<RecordList><Test><Attribute1>a</Attribute1><Attribute2></Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test>"
            + "<Test><Attribute1>b</Attribute1><Attribute2>B</Attribute2><Attribute3></Attribute3><Attribute4></Attribute4></Test></RecordList>",
            XR.ListToXml(TwoTests(), XR.NoConfig(), TD.Compact(), true));
        RCDefaultXmlConfigRecord flat = XR.Defaults();
        flat.ssSTDefaultXmlConfig.ssIndentation = false;
        // KI-04: and with ExcludeIfNull True the clean-up indents it again
        TestLog.Eq("root node, Indentation False, platform defaults otherwise: one line",
            "<RecordList><Test><Attribute1>a</Attribute1></Test><Test><Attribute1>b</Attribute1><Attribute2>B</Attribute2></Test></RecordList>",
            XR.ListToXml(TwoTests(), XR.NoConfig(), flat, true));
    }

    private static void InvalidInputs()
    {
        TestLog.Throws("empty list: ApplicationException \"RecordList has no records.\"",
            XR.Throws(delegate { XR.ListToXml(new RLTestRecordList(), XR.NoConfig(), XR.Defaults(), true); }),
            typeof(ApplicationException), "RecordList has no records.");
        TestLog.Throws("RecordList null: ApplicationException",
            XR.Throws(delegate { XR.ListToXml(null, XR.NoConfig(), XR.Defaults(), false); }),
            typeof(ApplicationException), "Parameter 'RecordList' must be a valid RecordList");
        TestLog.Throws("RecordList that is a record: ApplicationException",
            XR.Throws(delegate { XR.ListToXml(TD.Test("a", "", "", ""), XR.NoConfig(), XR.Defaults(), false); }),
            typeof(ApplicationException), "Parameter 'RecordList' must be a valid RecordList");
        RLTestRecordList list = TwoTests();
        list.Advance();
        TestLog.Eq("a list whose iterator is not at the start is still written from its first record",
            XR.ListToXml(TwoTests(), XR.NoConfig(), XR.Zero(), false), XR.ListToXml(list, XR.NoConfig(), XR.Zero(), false));
    }

    private static int Count(string s, string part)
    {
        int n = 0, i = 0;
        while ((i = s.IndexOf(part, i, StringComparison.Ordinal)) >= 0) { n++; i += part.Length; }
        return n;
    }
}
