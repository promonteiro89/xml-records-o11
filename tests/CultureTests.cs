// The server's culture must not change the XML: the same checks run with the thread culture set to
// pt-PT, fi-FI (time separator ".") and th-TH (Buddhist calendar), then back to en-US. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class CultureTests
{
    public static void Run()
    {
        foreach (string culture in new string[] { "pt-PT", "fi-FI", "th-TH" })
        {
            string name = culture;
            XR.InCulture(name, delegate { Checks(name); });
        }
    }

    private static void Checks(string culture)
    {
        string p = culture + ": ";
        RCDefaultXmlConfigRecord c = TD.Compact();
        // KI-11: the date text is formatted with the thread culture (fi-FI writes 13.45.30, th-TH year 2567)
        TestLog.Eq(p + "Time is written HH:mm:ss", "13:45:30", TD.WriteMoment(XR.T(13, 45, 30), c));
        TestLog.Eq(p + "Date is written yyyy-MM-dd", "2024-01-15", TD.WriteMoment(XR.D(2024, 1, 15), c));
        TestLog.Eq(p + "Date Time is written yyyy-MM-ddTHH:mm:ss", "2024-01-15T13:45:30", TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), c));
        TestLog.Eq(p + "TypeDateTimeFormat dd/MM/yyyy HH:mm:ss keeps / and :", "15/01/2024 13:45:30",
            TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), TD.Formats("dd/MM/yyyy HH:mm:ss", "", "")));
        TestLog.Eq(p + "TypeTimeFormat HH:mm keeps :", "13:45", TD.WriteMoment(XR.T(13, 45, 30), TD.Formats("", "", "HH:mm")));

        TestLog.Eq(p + "Date Time is read", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Moment", "2024-01-15T13:45:30", XR.Defaults()));
        TestLog.Eq(p + "Date is read", XR.D(2024, 1, 15), TD.TryReadDate("Day", "2024-01-15", XR.Defaults()));
        TestLog.Eq(p + "Time is read", XR.T(13, 45, 30), TD.TryReadDate("Clock", "13:45:30", XR.Defaults()));

        RCAllTypesRecord r = new RCAllTypesRecord(null);
        r.ssSTAllTypes.ssNumber = -5;
        r.ssSTAllTypes.ssBigNumber = -7;
        r.ssSTAllTypes.ssAmount = 1234.5M;
        r.ssSTAllTypes.ssFlag = true;
        string x = XR.ToXml(r, c);
        TestLog.Check(p + "numbers and Boolean are written as with en-US (-5, -7, 1234.5, true)",
            TD.Element(x, "Number") == "-5" && TD.Element(x, "BigNumber") == "-7" && TD.Element(x, "Amount") == "1234.5" && TD.Element(x, "Flag") == "true", x);
        STAllTypesStructure s = TD.ReadAllTypes("<AllTypes><Number>-5</Number><BigNumber>-7</BigNumber><Amount>2.5</Amount></AllTypes>", XR.Defaults());
        TestLog.Check(p + "numbers are read as with en-US (Decimal 2.5)", s.ssNumber == -5 && s.ssBigNumber == -7 && s.ssAmount == 2.5M,
            s.ssNumber + " " + s.ssBigNumber + " " + TestLog.Show(s.ssAmount));
    }
}
