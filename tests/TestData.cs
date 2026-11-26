// Sample records and readers shared by the test files. C# 5.

using System;
using System.Globalization;
using System.Text;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class TD
{
    /// <summary>An AllTypes record with a value in every attribute.</summary>
    public static RCAllTypesRecord AllTypes()
    {
        RCAllTypesRecord r = new RCAllTypesRecord(null);
        r.ssSTAllTypes.ssText = "Hello";
        r.ssSTAllTypes.ssNumber = 42;
        r.ssSTAllTypes.ssBigNumber = 9876543210L;
        r.ssSTAllTypes.ssAmount = 1234.50M;
        r.ssSTAllTypes.ssFlag = true;
        r.ssSTAllTypes.ssDay = XR.D(2024, 1, 15);
        r.ssSTAllTypes.ssClock = XR.T(13, 45, 30);
        r.ssSTAllTypes.ssMoment = XR.D(2024, 1, 15, 13, 45, 30);
        r.ssSTAllTypes.ssBlob = new byte[] { 1, 2, 3 };
        return r;
    }

    /// <summary>Order 7 "N1", customer C9 Ann, lines P and Q (2 x 3.5 each).</summary>
    public static RCOrderRecord Order()
    {
        RCOrderRecord o = new RCOrderRecord(null);
        o.ssSTOrder.ssAttr_Id = 7;
        o.ssSTOrder.ssNumber = "N1";
        o.ssSTOrder.ssCustomer.ssSTCustomer.ssAttr_Code = "C9";
        o.ssSTOrder.ssCustomer.ssSTCustomer.ssName = "Ann";
        o.ssSTOrder.ssLines.Append(Line("P", 2, 3.5M));
        o.ssSTOrder.ssLines.Append(Line("Q", 2, 3.5M));
        return o;
    }

    public static RCOrderLineRecord Line(string product, int quantity, decimal price)
    {
        RCOrderLineRecord l = new RCOrderLineRecord(null);
        l.ssSTOrderLine.ssProduct = product;
        l.ssSTOrderLine.ssQuantity = quantity;
        l.ssSTOrderLine.ssPrice = price;
        return l;
    }

    public static RCTestRecord Test(string a1, string a2, string a3, string a4)
    {
        RCTestRecord t = new RCTestRecord(null);
        t.ssSTTest.ssAttribute1 = a1;
        t.ssSTTest.ssAttribute2 = a2;
        t.ssSTTest.ssAttribute3 = a3;
        t.ssSTTest.ssAttribute4 = a4;
        return t;
    }

    public static RLTestRecordList Tests(params RCTestRecord[] items)
    {
        RLTestRecordList l = new RLTestRecordList();
        foreach (RCTestRecord t in items) l.Append(t);
        return l;
    }

    /// <summary>The extension's own TestHierarchical: aString "s", aRecord.Attribute1 "x", one item "a" in aRecordList.</summary>
    public static RCTestHierarchicalRecord Hierarchical()
    {
        RCTestHierarchicalRecord h = new RCTestHierarchicalRecord(null);
        h.ssSTTestHierarchical.ssaString = "s";
        h.ssSTTestHierarchical.ssaRecord.ssSTTest.ssAttribute1 = "x";
        h.ssSTTestHierarchical.ssaRecordList.Append(Test("a", "", "", ""));
        return h;
    }

    public static RCLevel1Record Levels()
    {
        RCLevel1Record l = new RCLevel1Record(null);
        l.ssSTLevel1.ssName = "one";
        l.ssSTLevel1.ssChild.ssSTLevel2.ssName = "two";
        l.ssSTLevel1.ssChild.ssSTLevel2.ssChild.ssSTLevel3.ssName = "three";
        return l;
    }

    // ------------------------------------------------------------------ readable summaries

    public static string Show(STTestStructure t)
    {
        return t.ssAttribute1 + "," + t.ssAttribute2 + "," + t.ssAttribute3 + "," + t.ssAttribute4;
    }

    public static string Show(RLTestRecordList l)
    {
        var sb = new StringBuilder(l.Length + ":");
        for (int i = 0; i < l.Length; i++) sb.Append("{" + Show(l[i].ssSTTest) + "}");
        return sb.ToString();
    }

    public static string Show(STOrderStructure o)
    {
        var sb = new StringBuilder();
        sb.Append(o.ssAttr_Id + "|" + o.ssNumber + "|" + o.ssCustomer.ssSTCustomer.ssAttr_Code + "|" + o.ssCustomer.ssSTCustomer.ssName + "|lines " + o.ssLines.Length);
        for (int i = 0; i < o.ssLines.Length; i++)
        {
            STOrderLineStructure l = o.ssLines[i].ssSTOrderLine;
            sb.Append("[" + l.ssProduct + "," + l.ssQuantity + "," + l.ssPrice.ToString(CultureInfo.InvariantCulture) + "]");
        }
        return sb.ToString();
    }

    public static string Show(RLOrderRecordList l)
    {
        var sb = new StringBuilder(l.Length + ":");
        for (int i = 0; i < l.Length; i++) sb.Append("{" + Show(l[i].ssSTOrder) + "}");
        return sb.ToString();
    }

    public static string Show(RLTestHierarchicalRecordList l)
    {
        var sb = new StringBuilder(l.Length + ":");
        for (int i = 0; i < l.Length; i++)
        {
            STTestHierarchicalStructure h = l[i].ssSTTestHierarchical;
            sb.Append("{" + h.ssaString + "|" + h.ssaInt + "|" + h.ssaDecimal.ToString(CultureInfo.InvariantCulture) + "|" + h.ssaBoolean
                + "|rec " + Show(h.ssaRecord.ssSTTest) + "|list " + Show(h.ssaRecordList) + "}");
        }
        return sb.ToString();
    }

    // ------------------------------------------------------------------ readers

    public static RLTestRecordList ReadTests(string xml, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults)
    {
        RLTestRecordList l = new RLTestRecordList();
        XR.FromXml(xml, l, config, defaults);
        return l;
    }

    public static RLOrderRecordList ReadOrders(string xml, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults)
    {
        RLOrderRecordList l = new RLOrderRecordList();
        XR.FromXml(xml, l, config, defaults);
        return l;
    }

    /// <summary>Reads one AllTypes record and returns it.</summary>
    public static STAllTypesStructure ReadAllTypes(string xml, RCDefaultXmlConfigRecord defaults)
    {
        RLAllTypesRecordList l = new RLAllTypesRecordList();
        XR.FromXml(xml, l, defaults);
        if (l.Length != 1) throw new InvalidOperationException("expected 1 record, got " + l.Length);
        return l[0].ssSTAllTypes;
    }

    /// <summary>Text of one element of the XML (between &lt;name&gt; and &lt;/name&gt;), or null.</summary>
    public static string Element(string xml, string name)
    {
        int a = xml.IndexOf("<" + name + ">", StringComparison.Ordinal);
        int b = xml.IndexOf("</" + name + ">", StringComparison.Ordinal);
        if (a < 0 || b < a) return null;
        return xml.Substring(a + name.Length + 2, b - a - name.Length - 2);
    }

    /// <summary>Defaults with ExcludeIfNull False and Indentation False: the serializer's text, compact.</summary>
    public static RCDefaultXmlConfigRecord Compact()
    {
        RCDefaultXmlConfigRecord c = XR.Raw();
        c.ssSTDefaultXmlConfig.ssIndentation = false;
        return c;
    }

    public static RCDefaultXmlConfigRecord Formats(string dateTime, string date, string time)
    {
        RCDefaultXmlConfigRecord c = Compact();
        c.ssSTDefaultXmlConfig.ssTypeDateTimeFormat = dateTime;
        c.ssSTDefaultXmlConfig.ssTypeDateFormat = date;
        c.ssSTDefaultXmlConfig.ssTypeTimeFormat = time;
        return c;
    }

    /// <summary>How a DateTime value is written in an AllTypes Moment element.</summary>
    public static string WriteMoment(DateTime value, RCDefaultXmlConfigRecord defaults)
    {
        RCAllTypesRecord r = new RCAllTypesRecord(null);
        r.ssSTAllTypes.ssMoment = value;
        return Element(XR.ToXml(r, defaults), "Moment");
    }

    /// <summary>Reads "&lt;AllTypes&gt;&lt;element&gt;text&lt;/element&gt;&lt;/AllTypes&gt;" and returns the DateTime attribute.</summary>
    public static DateTime ReadDate(string element, string text, RCDefaultXmlConfigRecord defaults)
    {
        STAllTypesStructure s = ReadAllTypes("<AllTypes><" + element + ">" + text + "</" + element + "></AllTypes>", defaults);
        if (element == "Day") return s.ssDay;
        if (element == "Clock") return s.ssClock;
        return s.ssMoment;
    }

    /// <summary>The value read for a date, or the exception's description.</summary>
    public static object TryReadDate(string element, string text, RCDefaultXmlConfigRecord defaults)
    {
        try { return ReadDate(element, text, defaults); }
        catch (Exception ex) { return TestLog.Describe(ex); }
    }

    /// <summary>A record config that leaves Blob out of the XML (so AllTypes XML can be read back, see KI-02).</summary>
    public static RLRecordXmlConfigRecordList WithoutBlob()
    {
        RCAttributeXmlConfigRecord blob = XR.Attr("Blob");
        blob.ssSTAttributeXmlConfig.ssNullableValue = "System.Byte[]";
        return XR.Config(XR.Record("AllTypes", null, null, blob));
    }
}
