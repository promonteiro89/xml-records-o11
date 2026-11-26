// Test harness for the XmlRecords O11 extension. Tests call the extension's public Mss* actions
// exactly as OutSystems apps do (records and record lists passed as object, the config records the
// action takes); they never call the extension's internal classes.
// C# 5 only, so the same files can run under Windows PowerShell 5.1 Add-Type.

using System;
using System.Globalization;
using System.Text;
using System.Threading;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.HubEdition.RuntimePlatform.Db;
using OutSystems.NssXmlRecords;

/// <summary>Shared pass/fail reporter for every test file in the harness.</summary>
public static class TestLog
{
    private static readonly StringBuilder Log = new StringBuilder();
    public static int Passed, Failed, Skipped, Known;

    /// <summary>The suite running now; known issues are registered per suite and check name.</summary>
    public static string Suite;

    /// <summary>"baseline" (the published DLL) or "source" (built from XmlRecords/Source/NET).</summary>
    public static readonly string Target = (Environment.GetEnvironmentVariable("XR_TARGET") ?? "baseline").ToLowerInvariant();

    public static void Section(string title)
    {
        Suite = title;
        if (Log.Length > 0) Log.AppendLine();
        Log.AppendLine("=== " + title + " ===");
    }

    public static void Note(string text) { Log.AppendLine(text); }

    /// <summary>
    /// A check states the specified behaviour. When it fails and the check is registered in
    /// KnownIssues.cs it is reported as KNOWN (a documented defect, see tests/KNOWN-ISSUES.md) and does
    /// not fail the run. A registered check that passes fails the run (stale entry), except an issue
    /// marked fixed in the source when the target is the source: there it must pass, and a failure is a
    /// regression.
    /// </summary>
    public static void Check(string name, bool cond, string detail)
    {
        KnownIssues.Entry issue = KnownIssues.Lookup(Suite, name);
        bool expectFailure = issue != null && !(issue.FixedInSource && Target == "source");
        if (issue == null || !expectFailure)
        {
            string tag = issue == null ? "" : " (" + issue.Id + " fixed in source)";
            if (cond) { Passed++; Log.AppendLine("  PASS  " + name + tag); }
            else
            {
                Failed++;
                Log.AppendLine("  FAIL  " + name + (issue == null ? "" : " -> regression: " + issue.Id + " is marked fixed in source")
                    + (detail == null ? "" : " -> " + Trunc(detail, 600)));
                // XR_LIST_FAILURES=1 lists the exact suite and check names, for registering known issues
                if (Environment.GetEnvironmentVariable("XR_LIST_FAILURES") == "1")
                    Console.Error.WriteLine("FAILED\t" + Suite + "\t" + name);
            }
        }
        else if (cond)
        {
            Failed++;
            Log.AppendLine("  FAIL  " + name + " -> now passes on " + Target + ", but it is registered as " + issue.Id
                + (issue.FixedInSource ? " (fixed in source, so it must still fail on the published DLL)" : " (open)")
                + ": if the defect is fixed, mark it fixed in KnownIssues.cs and tests/KNOWN-ISSUES.md");
        }
        else
        {
            Known++;
            Log.AppendLine("  KNOWN " + issue.Id + "  " + name + (detail == null ? "" : " -> " + Trunc(detail, 600)));
        }
    }

    public static void Check(string name, bool cond) { Check(name, cond, null); }

    /// <summary>Exact string comparison; the detail shows both values as C# literals.</summary>
    public static void Eq(string name, string expected, string actual)
    {
        Check(name, expected == actual, "expected " + Lit(expected) + " but was " + Lit(actual));
    }

    public static void Eq(string name, object expected, object actual)
    {
        Check(name, Equals(expected, actual), "expected " + Show(expected) + " but was " + Show(actual));
    }

    /// <summary>The action threw an exception of exactly this type whose message contains the text.</summary>
    public static void Throws(string name, Exception ex, Type type, string messagePart)
    {
        bool ok = ex != null && ex.GetType() == type && (messagePart == null || ex.Message.IndexOf(messagePart, StringComparison.Ordinal) >= 0);
        Check(name, ok, "expected " + type.Name + (messagePart == null ? "" : " containing " + Lit(messagePart)) + " but got " + Describe(ex));
    }

    /// <summary>The action completed without an exception.</summary>
    public static void NoThrow(string name, Exception ex)
    {
        Check(name, ex == null, "threw " + Describe(ex));
    }

    public static string Describe(Exception ex)
    {
        if (ex == null) return "no exception";
        string s = ex.GetType().Name + ": " + Lit(ex.Message);
        if (ex.InnerException != null) s += " (inner " + ex.InnerException.GetType().Name + ": " + Lit(ex.InnerException.Message) + ")";
        return s;
    }

    public static string Show(object o)
    {
        if (o == null) return "(null)";
        if (o is string) return Lit((string)o);
        if (o is DateTime) return ((DateTime)o).ToString("yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture) + " (" + ((DateTime)o).Kind + ")";
        if (o is decimal) return ((decimal)o).ToString(CultureInfo.InvariantCulture) + "M";
        return Convert.ToString(o, CultureInfo.InvariantCulture);
    }

    /// <summary>A string as a C# literal, so a failing value can be read exactly (and pasted).</summary>
    public static string Lit(string s)
    {
        if (s == null) return "null";
        var sb = new StringBuilder("\"");
        foreach (char c in s)
        {
            switch (c)
            {
                case '\r': sb.Append("\\r"); break;
                case '\n': sb.Append("\\n"); break;
                case '\t': sb.Append("\\t"); break;
                case '"': sb.Append("\\\""); break;
                case '\\': sb.Append("\\\\"); break;
                default:
                    if (c < ' ' || c > '~') sb.Append("\\u" + ((int)c).ToString("x4")); else sb.Append(c);
                    break;
            }
        }
        return sb.Append('"').ToString();
    }

    public static void Skip(string name, string reason)
    {
        Skipped++;
        Log.AppendLine("  SKIP  " + name + " -> " + reason);
    }

    public static string Text { get { return Log.ToString(); } }

    public static string Trunc(string s, int len)
    {
        if (s == null) return "(null)";
        return s.Length <= len ? s : s.Substring(0, len) + "...";
    }
}

/// <summary>Helpers shared by the test files. Everything goes through the extension's actions.</summary>
public static class XR
{
    public static readonly CssXmlRecords Ext = new CssXmlRecords();

    /// <summary>Environment.NewLine: the XML declaration, AddRootNode and the ExcludeIfNull clean-up use it.</summary>
    public static readonly string NL = Environment.NewLine;

    /// <summary>DefaultConfig as an app passes it when the input is left empty: the structure's defaults
    /// (ExcludeIfNull True, RecordListMode "C", RecordMode "S", AttributePrefix "Attr_", ContentPrefix
    /// "Cont_", Indentation True, no XML declaration).</summary>
    public static RCDefaultXmlConfigRecord Defaults()
    {
        return new RCDefaultXmlConfigRecord(null);
    }

    /// <summary>A zero-initialized config (every Text null, every Boolean False), as C# code gets with
    /// `new RCDefaultXmlConfigRecord()`: not what the platform passes, but a call the extension accepts.</summary>
    public static RCDefaultXmlConfigRecord Zero()
    {
        return new RCDefaultXmlConfigRecord();
    }

    /// <summary>The defaults with ExcludeIfNull False, so the serializer's own text is returned
    /// unchanged (no clean-up pass that re-formats the XML).</summary>
    public static RCDefaultXmlConfigRecord Raw()
    {
        RCDefaultXmlConfigRecord c = Defaults();
        c.ssSTDefaultXmlConfig.ssExcludeIfNull = false;
        return c;
    }

    public static RLRecordXmlConfigRecordList NoConfig()
    {
        return new RLRecordXmlConfigRecordList();
    }

    /// <summary>An AttributeXmlConfig with the structure's defaults (ExcludeIfNull True,
    /// RecordListMode "C", RecordMode "S") and the attribute's name.</summary>
    public static RCAttributeXmlConfigRecord Attr(string name)
    {
        RCAttributeXmlConfigRecord a = new RCAttributeXmlConfigRecord(null);
        a.ssSTAttributeXmlConfig.ssName = name;
        return a;
    }

    public static RCAttributeXmlConfigRecord Attr(string name, string attributeName)
    {
        RCAttributeXmlConfigRecord a = Attr(name);
        a.ssSTAttributeXmlConfig.ssAttributeName = attributeName;
        return a;
    }

    public static RCRecordXmlConfigRecord Record(string recordName, string effectiveName, string ns, params RCAttributeXmlConfigRecord[] attributes)
    {
        RCRecordXmlConfigRecord r = new RCRecordXmlConfigRecord(null);
        r.ssSTRecordXmlConfig.ssRecordName = recordName;
        r.ssSTRecordXmlConfig.ssEffectiveName = effectiveName ?? "";
        r.ssSTRecordXmlConfig.ssNamespace = ns ?? "";
        foreach (RCAttributeXmlConfigRecord a in attributes) r.ssSTRecordXmlConfig.ssAttributesConfig.Append(a);
        return r;
    }

    public static RLRecordXmlConfigRecordList Config(params RCRecordXmlConfigRecord[] records)
    {
        RLRecordXmlConfigRecordList list = new RLRecordXmlConfigRecordList();
        foreach (RCRecordXmlConfigRecord r in records) list.Append(r);
        return list;
    }

    public static string ToXml(object record, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults)
    {
        string xml;
        Ext.MssRecordToXml(record, config, defaults, out xml);
        return xml;
    }

    public static string ToXml(object record, RCDefaultXmlConfigRecord defaults)
    {
        return ToXml(record, NoConfig(), defaults);
    }

    public static string ListToXml(object list, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults, bool addRootNode)
    {
        string xml;
        Ext.MssRecordListToXml(list, config, defaults, addRootNode, out xml);
        return xml;
    }

    /// <summary>XmlToRecordList with the inputs' defaults: IgnoreXMLDeclarationAndNS False, AttributePrefix
    /// "", BypassRootRecordListNode True, BypassComments False, ShowXMLInLogs False.</summary>
    public static void FromXml(string xml, object list, RLRecordXmlConfigRecordList config, RCDefaultXmlConfigRecord defaults)
    {
        Ext.MssXmlToRecordList(xml, list, config, false, "", defaults, true, false, false);
    }

    public static void FromXml(string xml, object list, RCDefaultXmlConfigRecord defaults)
    {
        FromXml(xml, list, NoConfig(), defaults);
    }

    /// <summary>Runs the action and returns the exception it threw, or null when it completed.</summary>
    public static Exception Throws(Action action)
    {
        try { action(); return null; }
        catch (Exception ex) { return ex; }
    }

    /// <summary>Lines joined with Environment.NewLine, the way the ExcludeIfNull clean-up (XDocument) writes them.</summary>
    public static string Lines(params string[] lines)
    {
        return string.Join(NL, lines);
    }

    public static DateTime D(int y, int mo, int d) { return new DateTime(y, mo, d); }
    public static DateTime D(int y, int mo, int d, int h, int mi, int s) { return new DateTime(y, mo, d, h, mi, s); }
    public static DateTime T(int h, int mi, int s) { return new DateTime(1900, 1, 1, h, mi, s); }
    public static readonly DateTime NullDate = new DateTime(1900, 1, 1);

    /// <summary>Runs the action with this thread culture, then restores en-US.</summary>
    public static void InCulture(string name, Action action)
    {
        CultureInfo before = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = new CultureInfo(name);
        try { action(); }
        finally { Thread.CurrentThread.CurrentCulture = before; }
    }
}

public static class Program
{
    public static int Main(string[] args)
    {
        // results must not depend on the machine: en-US culture here, TZ=Europe/Lisbon from the script
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

        // Every public static class whose name ends in "Tests" and has a public static void Run()
        // is a test file; they run in name order, so new files need no registration here.
        var suites = new System.Collections.Generic.List<Type>();
        foreach (Type t in typeof(Program).Assembly.GetTypes())
            if (t.IsClass && t.IsAbstract && t.IsSealed && t.Name.EndsWith("Tests", StringComparison.Ordinal)
                && t.GetMethod("Run", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, Type.EmptyTypes, null) != null)
                suites.Add(t);
        suites.Sort(delegate(Type a, Type b) { return string.CompareOrdinal(a.Name, b.Name); });
        string only = args.Length > 0 ? args[0] : null;   // optional filter: run only suites whose name contains it
        var ran = new System.Collections.Generic.List<string>();
        Console.WriteLine("Target: " + TestLog.Target + "   TZ: " + TimeZoneInfo.Local.Id + "   culture: " + CultureInfo.CurrentCulture.Name);
        Console.WriteLine();
        foreach (Type t in suites)
        {
            if (only != null && t.Name.IndexOf(only, StringComparison.OrdinalIgnoreCase) < 0) continue;
            ran.Add(t.Name);
            System.Reflection.MethodInfo m = t.GetMethod("Run", Type.EmptyTypes);
            Run(t.Name, delegate { try { m.Invoke(null, null); } catch (System.Reflection.TargetInvocationException ex) { throw ex.InnerException; } });
        }

        // a registered known issue whose check no longer runs means a test was renamed or removed
        TestLog.Section("KnownIssues register");
        foreach (string entry in KnownIssues.Unmatched(ran))
            TestLog.Check("registered check still exists: " + entry, false, "no check with this name ran; update KnownIssues.cs");

        Console.WriteLine(TestLog.Text);
        Console.WriteLine();
        Console.WriteLine("Passed: " + TestLog.Passed + "  Known: " + TestLog.Known + "  Failed: " + TestLog.Failed + "  Skipped: " + TestLog.Skipped);
        return TestLog.Failed == 0 ? 0 : 1;
    }

    /// <summary>Runs one test file; an unexpected exception counts as a failure, not a crash.</summary>
    private static void Run(string title, Action suite)
    {
        TestLog.Section(title);
        try { suite(); }
        catch (Exception ex) { TestLog.Check(title + " (suite crashed)", false, ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace); }
    }
}
