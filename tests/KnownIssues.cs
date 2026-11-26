// Register of checks that fail on the published extension because of a documented defect
// (tests/KNOWN-ISSUES.md). Each check keeps its specified expectation; while it fails it is reported
// as KNOWN <id>.
//
// Status per issue:
//   Open   the defect is in the published DLL and in the source: KNOWN on both targets.
//   Fixed  the source fixes it, the baseline DLL doesn't: KNOWN on TARGET=baseline, must PASS on TARGET=source
//          (a failure there is a regression).
// A registered check that passes where it is expected to fail fails the run (stale entry), and so does
// a registered check that no longer runs (renamed or removed).
//
// To list the exact names of failing checks: XR_LIST_FAILURES=1 tests/run-tests.sh 2>&1 >/dev/null
// C# 5 only.

using System;
using System.Collections.Generic;

public static class KnownIssues
{
    public sealed class Entry
    {
        public string Id;
        public bool FixedInSource;
    }

    private const string Open = "open";
    private const string Fixed = "fixed";

    // { issue id, status (Open or Fixed), suite, exact check name }
    private static readonly string[][] Entries = {
        new[] { "KI-01", Open, "RecordListToXmlTests", "two records, platform defaults, no root node: no exception" },
        new[] { "KI-01", Open, "RecordListToXmlTests", "two records, platform defaults, no root node: both records written" },

        new[] { "KI-02", Open, "RecordToXmlTests", "Binary Data is written as base64 (01 02 03 -> AQID)" },
        new[] { "KI-02", Open, "RecordToXmlTests", "empty Binary Data is written as an empty element" },
        new[] { "KI-02", Open, "RecordToXmlTests", "empty Binary Data is left out with the platform defaults" },
        new[] { "KI-02", Open, "XmlToRecordListTests", "Binary Data is read from base64 (AQID -> 01 02 03)" },

        new[] { "KI-03", Open, "RecordToXmlTests", "a record whose values are all empty, platform defaults: an empty record element" },
        new[] { "KI-03", Open, "ConfigTests", "attribute ExcludeIfNull False keeps its empty element when DefaultConfig ExcludeIfNull is True" },

        new[] { "KI-04", Open, "ConfigTests", "Indentation False with the platform defaults otherwise: not formatted" },
        new[] { "KI-04", Open, "RecordListToXmlTests", "root node, Indentation False, ExcludeIfNull False: one line" },
        new[] { "KI-04", Open, "RecordListToXmlTests", "root node, Indentation False, platform defaults otherwise: one line" },

        new[] { "KI-05", Open, "XmlToRecordListTests", "round trip, Namespace of the order" },

        new[] { "KI-06", Open, "XmlToRecordListTests", "round trip, EffectiveName of the list items (key Order/OrderLine)" },
        new[] { "KI-06", Open, "XmlToRecordListTests", "round trip, EffectiveName of the customer in Child mode (key Order/Customer)" },

        new[] { "KI-07", Open, "ConfigTests", "three levels, key \"Level2/Level3\" configures the innermost record" },

        new[] { "KI-08", Open, "RecordToXmlTests", "a record with two structures also writes the second one (Customer Zed)" },

        new[] { "KI-09", Open, "RecordToXmlTests", "a control character gives well-formed XML or a clear error (ExcludeIfNull False)" },
        new[] { "KI-09", Open, "RecordToXmlTests", "a control character gives well-formed XML or a clear error (platform defaults)" },

        new[] { "KI-10", Fixed, "DateTimeTests", "the null date written with Z stays the null date" },
        new[] { "KI-10", Fixed, "DateTimeTests", "the null date written with +05:00 stays the null date" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with milliseconds (.123)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with one fractional digit (.5)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with a space instead of T" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time without seconds (yyyy-MM-ddTHH:mm)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date with Z (xs:date zone, ignored)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date with +01:00 (ignored)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Time without seconds (HH:mm)" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Time with fractional seconds" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with Z, winter" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with Z, summer" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with +02:00" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with +01:00 in summer" },
        new[] { "KI-10", Fixed, "DateTimeTests", "Date Time with seven fractional digits and -05:00" },
        new[] { "KI-10", Fixed, "DateTimeTests", "a Date Time read from a value with Z is DateTimeKind.Unspecified" },

        new[] { "KI-11", Fixed, "CultureTests", "fi-FI: Time is written HH:mm:ss" },
        new[] { "KI-11", Fixed, "CultureTests", "fi-FI: TypeDateTimeFormat dd/MM/yyyy HH:mm:ss keeps / and :" },
        new[] { "KI-11", Fixed, "CultureTests", "fi-FI: TypeTimeFormat HH:mm keeps :" },
        new[] { "KI-11", Fixed, "CultureTests", "th-TH: Date is written yyyy-MM-dd" },
        new[] { "KI-11", Fixed, "CultureTests", "th-TH: TypeDateTimeFormat dd/MM/yyyy HH:mm:ss keeps / and :" },
        new[] { "KI-11", Fixed, "CultureTests", "th-TH: Date is read" },

        new[] { "KI-12", Fixed, "DateTimeTests", "TypeTimeFormat hh:mm tt gives a Time on 1900-01-01" },
        new[] { "KI-12", Fixed, "DateTimeTests", "TypeTimeFormat HH:mm:ss (the default, set explicitly) gives a Time on 1900-01-01" },
        new[] { "KI-12", Fixed, "DateTimeTests", "round trip of a Time with TypeTimeFormat HH:mm (seconds dropped, still on 1900-01-01)" },

        new[] { "KI-13", Fixed, "DateTimeTests", "a date before 1900 is written (1850-03-01)" },
        new[] { "KI-13", Fixed, "DateTimeTests", "a date and time before 1900 is written (1899-12-31T23:00:00)" },
        new[] { "KI-13", Fixed, "DateTimeTests", "0001-01-02 is written" },
        new[] { "KI-13", Fixed, "DateTimeTests", "a Date before 1900 is not left out by the platform defaults' ExcludeIfNull" },
        new[] { "KI-13", Fixed, "DateTimeTests", "round trip of dates before 1900" },

        new[] { "KI-14", Fixed, "DateTimeTests", "TypeDateTimeFormat with two formats: the first is written" },
        new[] { "KI-14", Fixed, "DateTimeTests", "TypeDateFormat longer than 10 characters (dd MMMM yyyy)" },
        new[] { "KI-14", Fixed, "DateTimeTests", "TypeTimeFormat without a colon (HH.mm)" },
        new[] { "KI-14", Fixed, "DateTimeTests", "TypeDateTimeFormat with two formats: the second one reads" },

        new[] { "KI-15", Fixed, "DateTimeTests", "an empty CDATA section is the null date" },
        new[] { "KI-15", Fixed, "DateTimeTests", "a CDATA section of only spaces is the null date" },
        new[] { "KI-15", Fixed, "DateTimeTests", "spaces around a Date Time (CDATA) are ignored" },
        new[] { "KI-15", Fixed, "DateTimeTests", "an XML attribute of only a space is the null date" },

        new[] { "KI-16", Fixed, "DateTimeTests", "a literal & in a custom format is escaped" },

        new[] { "KI-17", Fixed, "DateTimeTests", "an unreadable date's message names the formats tried (default: yyyy-MM-dd)" },
        new[] { "KI-17", Fixed, "DateTimeTests", "an unreadable date's message names the configured format" },
    };

    private static Dictionary<string, Entry> index;
    private static readonly HashSet<string> Hit = new HashSet<string>();

    private static string Key(string suite, string name) { return suite + "\n" + name; }

    /// <summary>The issue registered for this check, or null.</summary>
    public static Entry Lookup(string suite, string name)
    {
        if (index == null)
        {
            index = new Dictionary<string, Entry>();
            foreach (string[] e in Entries)
            {
                if (e[1] != Open && e[1] != Fixed) throw new InvalidOperationException("KnownIssues: bad status '" + e[1] + "' for " + e[0]);
                if (index.ContainsKey(Key(e[2], e[3]))) throw new InvalidOperationException("KnownIssues: check registered twice: " + e[2] + ": " + e[3]);
                index[Key(e[2], e[3])] = new Entry { Id = e[0], FixedInSource = e[1] == Fixed };
            }
        }
        Entry entry;
        if (suite == null || !index.TryGetValue(Key(suite, name), out entry)) return null;
        Hit.Add(Key(suite, name));
        return entry;
    }

    /// <summary>Registered checks of the suites that ran which no check matched.</summary>
    public static List<string> Unmatched(List<string> suitesRan)
    {
        var missing = new List<string>();
        foreach (string[] e in Entries)
            if (suitesRan.Contains(e[2]) && !Hit.Contains(Key(e[2], e[3])))
                missing.Add(e[0] + " " + e[2] + ": " + e[3]);
        return missing;
    }
}
