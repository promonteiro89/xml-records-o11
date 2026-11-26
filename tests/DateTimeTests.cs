// Date, Time and Date Time attributes, written and read, with the default formats (ISO 8601 / XML
// Schema, as the DefaultConfig descriptions say) and with TypeDateTimeFormat, TypeDateFormat and
// TypeTimeFormat. OutSystems conventions: the null date is 1900-01-01 00:00:00, a Time is 1900-01-01 plus
// the time of day, values are server local time with DateTimeKind.Unspecified.
// Culture-dependent behaviour is in CultureTests. C# 5.

using System;
using OutSystems.NssXmlRecords;
using XmlRecordsTestTypes;

public static class DateTimeTests
{
    public static void Run()
    {
        WriteDefaults();
        WriteCustomFormats();
        ReadDefaults();
        ReadZones();
        ReadEmptyAndWhitespace();
        ReadCustomFormats();
        ReadErrors();
        RoundTrips();
    }

    private static void WriteDefaults()
    {
        RCDefaultXmlConfigRecord c = TD.Compact();
        TestLog.Eq("Date Time is written yyyy-MM-ddTHH:mm:ss", "2024-01-15T13:45:30", TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), c));
        TestLog.Eq("milliseconds are not written", "2024-01-15T13:45:30", TD.WriteMoment(new DateTime(2024, 1, 15, 13, 45, 30, 123), c));
        TestLog.Eq("a value at midnight is written as a Date (yyyy-MM-dd)", "2024-01-15", TD.WriteMoment(XR.D(2024, 1, 15), c));
        TestLog.Eq("a value on 1900-01-01 with a time is written as a Time (HH:mm:ss)", "13:45:30", TD.WriteMoment(XR.T(13, 45, 30), c));
        TestLog.Eq("1900-01-01 00:00:01 is a Time", "00:00:01", TD.WriteMoment(XR.T(0, 0, 1), c));
        TestLog.Eq("1900-01-02 is a Date", "1900-01-02", TD.WriteMoment(XR.D(1900, 1, 2), c));
        TestLog.Eq("the null date 1900-01-01 is written empty", "", TD.WriteMoment(XR.NullDate, c));
        TestLog.Eq("DateTime.MinValue is written empty", "", TD.WriteMoment(DateTime.MinValue, c));
        TestLog.Eq("DateTimeKind.Utc is written as is, without Z", "2024-01-15T13:45:30", TD.WriteMoment(new DateTime(2024, 1, 15, 13, 45, 30, DateTimeKind.Utc), c));
        TestLog.Eq("DateTimeKind.Local is written as is, without an offset", "2024-01-15T13:45:30", TD.WriteMoment(new DateTime(2024, 1, 15, 13, 45, 30, DateTimeKind.Local), c));
        TestLog.Eq("year 9999", "9999-12-31T23:59:59", TD.WriteMoment(new DateTime(9999, 12, 31, 23, 59, 59), c));

        // KI-13: every value up to 1900-01-01 is treated as the null date, so real dates before 1900 are lost
        TestLog.Eq("a date before 1900 is written (1850-03-01)", "1850-03-01", TD.WriteMoment(XR.D(1850, 3, 1), c));
        TestLog.Eq("a date and time before 1900 is written (1899-12-31T23:00:00)", "1899-12-31T23:00:00", TD.WriteMoment(XR.D(1899, 12, 31, 23, 0, 0), c));
        TestLog.Eq("0001-01-02 is written", "0001-01-02", TD.WriteMoment(new DateTime(1, 1, 2), c));
        RCAllTypesRecord old = new RCAllTypesRecord(null);
        old.ssSTAllTypes.ssDay = XR.D(1850, 3, 1);
        TestLog.Check("a Date before 1900 is not left out by the platform defaults' ExcludeIfNull",
            XR.ToXml(old, XR.Defaults()).Contains("<Day>1850-03-01</Day>"), XR.ToXml(old, XR.Defaults()));
    }

    private static void WriteCustomFormats()
    {
        TestLog.Eq("TypeDateTimeFormat dd/MM/yyyy HH:mm", "15/01/2024 13:45", TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), TD.Formats("dd/MM/yyyy HH:mm", "", "")));
        TestLog.Eq("TypeDateTimeFormat with milliseconds and zone (yyyy-MM-ddTHH:mm:ss.fffK, Unspecified kind: no zone)",
            "2024-01-15T13:45:30.123", TD.WriteMoment(new DateTime(2024, 1, 15, 13, 45, 30, 123), TD.Formats("yyyy-MM-ddTHH:mm:ss.fffK", "", "")));
        TestLog.Eq("TypeDateFormat dd/MM/yyyy", "15/01/2024", TD.WriteMoment(XR.D(2024, 1, 15), TD.Formats("", "dd/MM/yyyy", "")));
        TestLog.Eq("TypeTimeFormat hh:mm tt", "01:45 PM", TD.WriteMoment(XR.T(13, 45, 30), TD.Formats("", "", "hh:mm tt")));
        TestLog.Eq("TypeDateFormat is not used for a Date Time", "2024-01-15T13:45:30", TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), TD.Formats("", "dd/MM/yyyy", "")));
        TestLog.Eq("a custom format still writes the null date empty", "", TD.WriteMoment(XR.NullDate, TD.Formats("dd/MM/yyyy HH:mm", "dd/MM/yyyy", "HH:mm")));
        // KI-14: a setting can list several formats separated by |; the first one is used for writing
        TestLog.Eq("TypeDateTimeFormat with two formats: the first is written", "2024-01-15T13:45:30.000",
            TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), TD.Formats("yyyy-MM-ddTHH:mm:ss.fff | yyyy-MM-ddTHH:mm:ss", "", "")));
        // KI-16: the formatted text isn't escaped like every other value
        TestLog.Eq("a literal & in a custom format is escaped", "2024&amp;01", TD.WriteMoment(XR.D(2024, 1, 15, 13, 45, 30), TD.Formats("yyyy'&'MM", "", "")));
    }

    private static void ReadDefaults()
    {
        RCDefaultXmlConfigRecord d = XR.Defaults();
        TestLog.Eq("Date Time yyyy-MM-ddTHH:mm:ss", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Moment", "2024-01-15T13:45:30", d));
        TestLog.Eq("Date yyyy-MM-dd", XR.D(2024, 1, 15), TD.TryReadDate("Day", "2024-01-15", d));
        TestLog.Eq("Time HH:mm:ss on 1900-01-01", XR.T(13, 45, 30), TD.TryReadDate("Clock", "13:45:30", d));
        TestLog.Eq("a date written in a Date Time element", XR.D(2024, 1, 15), TD.TryReadDate("Moment", "2024-01-15", d));
        TestLog.Eq("a time written in a Date Time element", XR.T(13, 45, 30), TD.TryReadDate("Moment", "13:45:30", d));
        TestLog.Eq("a date and time written in a Date element is kept whole", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Day", "2024-01-15T13:45:30", d));
        TestLog.Eq("a Date Time before 1900 is read", XR.D(1850, 3, 1, 10, 0, 0), TD.TryReadDate("Moment", "1850-03-01T10:00:00", d));
        TestLog.Eq("a Date before 1900 is read", XR.D(1850, 3, 1), TD.TryReadDate("Day", "1850-03-01", d));
        TestLog.Eq("1900-01-01 is read as the null date", XR.NullDate, TD.TryReadDate("Day", "1900-01-01", d));
        STAllTypesStructure attrs = TD.ReadAllTypes("<AllTypes Moment=\"2024-01-15T10:00:00\" Day=\"2024-01-15\"/>", d);
        TestLog.Check("dates in XML attributes", attrs.ssMoment == XR.D(2024, 1, 15, 10, 0, 0) && attrs.ssDay == XR.D(2024, 1, 15), TestLog.Show(attrs.ssMoment) + " " + TestLog.Show(attrs.ssDay));
        object read = TD.TryReadDate("Moment", "2024-01-15T13:45:30", d);
        TestLog.Check("a Date Time read is DateTimeKind.Unspecified", read is DateTime && ((DateTime)read).Kind == DateTimeKind.Unspecified, TestLog.Show(read));

        // KI-10: ISO 8601 / xs:dateTime values with fractional seconds, a zone, a space instead of T, or no seconds
        TestLog.Eq("Date Time with milliseconds (.123)", new DateTime(2024, 1, 15, 13, 45, 30, 123), TD.TryReadDate("Moment", "2024-01-15T13:45:30.123", d));
        TestLog.Eq("Date Time with one fractional digit (.5)", new DateTime(2024, 1, 15, 13, 45, 30, 500), TD.TryReadDate("Moment", "2024-01-15T13:45:30.5", d));
        TestLog.Eq("Date Time with a space instead of T", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Moment", "2024-01-15 13:45:30", d));
        TestLog.Eq("Date Time without seconds (yyyy-MM-ddTHH:mm)", XR.D(2024, 1, 15, 13, 45, 0), TD.TryReadDate("Moment", "2024-01-15T13:45", d));
        TestLog.Eq("Date with Z (xs:date zone, ignored)", XR.D(2024, 1, 15), TD.TryReadDate("Day", "2024-01-15Z", d));
        TestLog.Eq("Date with +01:00 (ignored)", XR.D(2024, 1, 15), TD.TryReadDate("Day", "2024-01-15+01:00", d));
        TestLog.Eq("Time without seconds (HH:mm)", XR.T(13, 45, 0), TD.TryReadDate("Clock", "13:45", d));
        TestLog.Eq("Time with fractional seconds", XR.T(13, 45, 30).AddMilliseconds(500), TD.TryReadDate("Clock", "13:45:30.5", d));
    }

    // A zone means an instant: it is converted to the server's local time (TZ=Europe/Lisbon in
    // run-tests.sh; the expected values are computed with the machine's zone, so they hold anywhere).
    private static void ReadZones()
    {
        RCDefaultXmlConfigRecord d = XR.Defaults();
        // KI-10
        TestLog.Eq("Date Time with Z, winter", Local(2024, 1, 15, 13, 45, 30, 0, 0), TD.TryReadDate("Moment", "2024-01-15T13:45:30Z", d));
        TestLog.Eq("Date Time with Z, summer", Local(2024, 7, 15, 13, 45, 30, 0, 0), TD.TryReadDate("Moment", "2024-07-15T13:45:30Z", d));
        TestLog.Eq("Date Time with +02:00", Local(2024, 1, 15, 13, 45, 30, 0, 2), TD.TryReadDate("Moment", "2024-01-15T13:45:30+02:00", d));
        TestLog.Eq("Date Time with +01:00 in summer", Local(2024, 7, 15, 13, 45, 30, 0, 1), TD.TryReadDate("Moment", "2024-07-15T13:45:30+01:00", d));
        TestLog.Eq("Date Time with seven fractional digits and -05:00", Local(2024, 1, 15, 13, 45, 30, 0, -5).AddTicks(1234567),
            TD.TryReadDate("Moment", "2024-01-15T13:45:30.1234567-05:00", d));
        TestLog.Eq("the null date written with Z stays the null date", XR.NullDate, TD.TryReadDate("Moment", "1900-01-01T00:00:00Z", d));
        TestLog.Eq("the null date written with +05:00 stays the null date", XR.NullDate, TD.TryReadDate("Moment", "1900-01-01T00:00:00+05:00", d));
        object z = TD.TryReadDate("Moment", "2024-01-15T13:45:30Z", d);
        TestLog.Check("a Date Time read from a value with Z is DateTimeKind.Unspecified", z is DateTime && ((DateTime)z).Kind == DateTimeKind.Unspecified, TestLog.Show(z));
    }

    private static void ReadEmptyAndWhitespace()
    {
        RCDefaultXmlConfigRecord d = XR.Defaults();
        TestLog.Eq("an empty element keeps the null date", XR.NullDate, TD.TryReadDate("Moment", "", d));
        TestLog.Eq("an element of only spaces keeps the null date", XR.NullDate, TD.TryReadDate("Moment", "   ", d));
        // KI-15: text that reaches the parser with spaces (CDATA, an XML attribute) is not trimmed
        TestLog.Eq("an empty CDATA section is the null date", XR.NullDate, TD.TryReadDate("Moment", "<![CDATA[]]>", d));
        TestLog.Eq("a CDATA section of only spaces is the null date", XR.NullDate, TD.TryReadDate("Moment", "<![CDATA[ ]]>", d));
        TestLog.Eq("spaces around a Date Time (CDATA) are ignored", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Moment", "<![CDATA[ 2024-01-15T13:45:30 ]]>", d));
        object attr;
        try { attr = TD.ReadAllTypes("<AllTypes Day=\" \"/>", d).ssDay; }
        catch (Exception ex) { attr = TestLog.Describe(ex); }
        TestLog.Eq("an XML attribute of only a space is the null date", XR.NullDate, attr);
    }

    private static void ReadCustomFormats()
    {
        TestLog.Eq("TypeDateTimeFormat dd.MM.yyyy HH:mm:ss", XR.D(2024, 1, 15, 13, 45, 30), TD.TryReadDate("Moment", "15.01.2024 13:45:30", TD.Formats("dd.MM.yyyy HH:mm:ss", "", "")));
        TestLog.Eq("TypeDateFormat dd/MM/yyyy", XR.D(2024, 1, 15), TD.TryReadDate("Day", "15/01/2024", TD.Formats("", "dd/MM/yyyy", "")));
        TestLog.Check("a configured TypeDateTimeFormat replaces the default: ISO text is no longer read",
            TD.TryReadDate("Moment", "2024-01-15T13:45:30", TD.Formats("dd.MM.yyyy HH:mm:ss", "", "")) is string);
        // KI-12: a value read with TypeTimeFormat keeps today's date instead of 1900-01-01
        TestLog.Eq("TypeTimeFormat hh:mm tt gives a Time on 1900-01-01", XR.T(13, 45, 0), TD.TryReadDate("Clock", "01:45 PM", TD.Formats("", "", "hh:mm tt")));
        TestLog.Eq("TypeTimeFormat HH:mm:ss (the default, set explicitly) gives a Time on 1900-01-01", XR.T(13, 45, 30), TD.TryReadDate("Clock", "13:45:30", TD.Formats("", "", "HH:mm:ss")));
        // KI-14: which format is tried depends on the text's length and on a ':', not on the setting
        TestLog.Eq("TypeDateFormat longer than 10 characters (dd MMMM yyyy)", XR.D(2024, 1, 15), TD.TryReadDate("Day", "15 January 2024", TD.Formats("", "dd MMMM yyyy", "")));
        TestLog.Eq("TypeTimeFormat without a colon (HH.mm)", XR.T(13, 45, 0), TD.TryReadDate("Clock", "13.45", TD.Formats("", "", "HH.mm")));
        TestLog.Eq("TypeDateTimeFormat with two formats: the second one reads", XR.D(2024, 1, 15, 13, 45, 0),
            TD.TryReadDate("Moment", "15.01.2024 13:45", TD.Formats("dd.MM.yyyy HH:mm:ss|dd.MM.yyyy HH:mm", "", "")));
    }

    private static void ReadErrors()
    {
        RCDefaultXmlConfigRecord d = XR.Defaults();
        foreach (string bad in new string[] { "garbage-value", "2024-1-5", "15/01/2024", "1:02:03", "25:00:00", "2024-02-30" })
        {
            string text = bad;
            Exception ex = XR.Throws(delegate { TD.ReadDate("Moment", text, d); });
            TestLog.Check("\"" + bad + "\": ApplicationException naming element and value, inner FormatException",
                ex is ApplicationException && ex.Message.StartsWith("Unable to convert xml value to structure type. Element: Moment; Value: " + text + "; Error:", StringComparison.Ordinal)
                && ex.InnerException is FormatException, TestLog.Describe(ex));
        }
        // KI-17: the message says only "String was not recognized as a valid DateTime."
        Exception e1 = XR.Throws(delegate { TD.ReadDate("Moment", "garbage-value", d); });
        TestLog.Check("an unreadable date's message names the formats tried (default: yyyy-MM-dd)",
            e1 != null && e1.Message.Contains("yyyy-MM-dd"), TestLog.Describe(e1));
        Exception e2 = XR.Throws(delegate { TD.ReadDate("Moment", "garbage-value", TD.Formats("dd.MM.yyyy HH:mm:ss", "", "")); });
        TestLog.Check("an unreadable date's message names the configured format",
            e2 != null && e2.Message.Contains("dd.MM.yyyy HH:mm:ss"), TestLog.Describe(e2));
    }

    private static void RoundTrips()
    {
        RLRecordXmlConfigRecordList cfg = TD.WithoutBlob();
        RCAllTypesRecord r = new RCAllTypesRecord(null);
        r.ssSTAllTypes.ssDay = XR.D(2024, 2, 29);
        r.ssSTAllTypes.ssClock = XR.T(23, 59, 59);
        r.ssSTAllTypes.ssMoment = XR.D(2024, 12, 31, 0, 0, 1);
        STAllTypesStructure a = RoundTrip(r, cfg, XR.Defaults());
        TestLog.Check("round trip of Date, Time and Date Time, platform defaults",
            a.ssDay == r.ssSTAllTypes.ssDay && a.ssClock == r.ssSTAllTypes.ssClock && a.ssMoment == r.ssSTAllTypes.ssMoment,
            TestLog.Show(a.ssDay) + " " + TestLog.Show(a.ssClock) + " " + TestLog.Show(a.ssMoment));

        RCDefaultXmlConfigRecord custom = XR.Defaults();
        custom.ssSTDefaultXmlConfig.ssTypeDateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        custom.ssSTDefaultXmlConfig.ssTypeDateFormat = "dd/MM/yyyy";
        a = RoundTrip(r, cfg, custom);
        TestLog.Check("round trip of Date and Date Time with custom formats",
            a.ssDay == r.ssSTAllTypes.ssDay && a.ssMoment == r.ssSTAllTypes.ssMoment, TestLog.Show(a.ssDay) + " " + TestLog.Show(a.ssMoment));

        // KI-13
        RCAllTypesRecord old = new RCAllTypesRecord(null);
        old.ssSTAllTypes.ssDay = XR.D(1850, 3, 1);
        old.ssSTAllTypes.ssMoment = XR.D(1899, 12, 31, 23, 0, 0);
        a = RoundTrip(old, cfg, XR.Defaults());
        TestLog.Check("round trip of dates before 1900", a.ssDay == old.ssSTAllTypes.ssDay && a.ssMoment == old.ssSTAllTypes.ssMoment,
            TestLog.Show(a.ssDay) + " " + TestLog.Show(a.ssMoment));
        // KI-12
        RCDefaultXmlConfigRecord time = XR.Defaults();
        time.ssSTDefaultXmlConfig.ssTypeTimeFormat = "HH:mm";
        a = RoundTrip(r, cfg, time);
        TestLog.Eq("round trip of a Time with TypeTimeFormat HH:mm (seconds dropped, still on 1900-01-01)", XR.T(23, 59, 0), a.ssClock);
    }

    private static STAllTypesStructure RoundTrip(RCAllTypesRecord r, RLRecordXmlConfigRecordList cfg, RCDefaultXmlConfigRecord defaults)
    {
        RLAllTypesRecordList l = new RLAllTypesRecordList();
        XR.FromXml(XR.ToXml(r, cfg, defaults), l, cfg, defaults);
        return l[0].ssSTAllTypes;
    }

    /// <summary>The server local wall-clock time of an instant given with an offset in hours.</summary>
    private static DateTime Local(int y, int mo, int d, int h, int mi, int s, int ms, int offsetHours)
    {
        DateTime local = new DateTimeOffset(y, mo, d, h, mi, s, ms, TimeSpan.FromHours(offsetHours)).ToLocalTime().DateTime;
        return DateTime.SpecifyKind(local, DateTimeKind.Unspecified);
    }
}
