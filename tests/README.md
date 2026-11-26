# XmlRecords tests

Tests for the three actions the extension exposes to OutSystems apps (`RecordToXml`, `RecordListToXml`,
`XmlToRecordList`, the `Mss*` methods of `CssXmlRecords`). They call the actions the way the platform
does: records and record lists passed as `object`, and the `XmlConfig` / `DefaultConfig` records the
actions take. The expected values are exact XML strings and exact values read back.

## Two targets

| TARGET | Runs against |
|---|---|
| `baseline` (default) | `tests/lib/baseline/OutSystems.NssXmlRecords.dll`, the extension as published on the Forge (1.6.17), unchanged |
| `source` | the extension built from `XmlRecords/Source/NET` (every `.cs` there, not `unittests/`) |

The checks state the specified behaviour. Where the published extension already behaves correctly the
check simply passes, so the baseline run pins what OutSystems apps rely on today. Where it is wrong the
check is registered as a known issue (below). Run both before and after any change to the source.

## Running them

The tests also need three OutSystems platform assemblies (OutSystems.HubEdition.RuntimePlatform.dll,
OutSystems.RuntimeCommon.dll, OutSystems.HubEdition.DatabaseAbstractionLayer.dll). They are OutSystems'
binaries, so they are not in this repository: open `XmlRecords.xif` in Integration Studio and run
Update Source Code once, which puts them in `XmlRecords/Source/NET/Bin`, where the scripts look by
default. `OS_PLATFORM_BIN` points them at another folder. See `tests/lib/README.md`.

Windows, the runtime OutSystems 11 uses (Visual Studio or its Build Tools, .NET Framework 4.8 targeting pack):

    tests\run-tests.ps1                              # published DLL, every suite
    tests\run-tests.ps1 -Target source               # built from the source
    tests\run-tests.ps1 -Target source DateTime      # suites whose name contains the text

macOS or Linux with mono, Roslyn `csc` and the .NET Framework 4.8 reference assemblies (`brew install mono`):

    tests/run-tests.sh
    TARGET=source tests/run-tests.sh
    TARGET=source tests/run-tests.sh DateTime

Exit code 0 means no unexpected result, 1 failures, 2 a build error. Both scripts accept `EXT_SRC`,
`BASELINE_DLL`, `OS_PLATFORM_BIN` and `NET48_API` to point at other files. The test files are C# 5, so
they also compile under Windows PowerShell 5.1 `Add-Type`.

Results don't depend on the machine: the test program sets the thread culture to en-US (CultureTests
switches to pt-PT, fi-FI and th-TH and back), `run-tests.sh` sets `TZ=Europe/Lisbon`, and the checks that
read a value with a time zone compute the expected local time from the machine's zone (Windows ignores
`TZ`). Line breaks written by .NET's XML classes, the XML declaration and AddRootNode are
`Environment.NewLine`; the checks build those with `XR.NL` / `XR.Lines`, so the same expectations hold on
Windows (CRLF) and mono (LF).

There is no CI: the platform assemblies can't be published with the repository. Run both targets
before opening a pull request and paste the two totals lines into its description.

## Suites

| File | Covers |
|---|---|
| RecordToXmlTests | every basic type (Text, Integer, Long Integer, Decimal, Boolean, Date, Time, Date Time, Binary Data), platform defaults vs ExcludeIfNull False vs a zero-initialized config, escaping, nested record and list, Attr_ / Cont_ prefixes, structures, entities, a record with two structures, invalid inputs |
| RecordListToXmlTests | lists with the platform defaults, AddRootNode, XML declaration, Indentation, empty list and invalid inputs |
| ConfigTests | AttributeName, EffectiveName, Namespace, hierarchical keys (`Parent/Child`, three levels), RecordMode / RecordListMode Child and Sequence, ExcludeIfNull and NullableValue, XML declaration options, Indentation, duplicate config |
| XmlToRecordListTests | reading every type, decimal culture, value errors, IgnoreXMLDeclarationAndNS, BypassRootRecordListNode, BypassComments, ShowXMLInLogs, the AttributePrefix input, invalid XML, round trips with each configuration |
| DateTimeTests | Date, Time and Date Time written and read: defaults, null date, before 1900, milliseconds, `Z` and offsets, whitespace, TypeDateTimeFormat / TypeDateFormat / TypeTimeFormat, error messages |
| CultureTests | the same date and number checks with the thread culture pt-PT, fi-FI and th-TH |

`TestHarness.cs` holds the reporter (`TestLog`), the action helpers (`XR`) and `Main`; `TestData.cs` the
sample records and readers; `TestRecordTypes.cs` test-only record types written like Integration
Studio's generated code (an `ISimpleRecord` structure with `ss` fields, an `RC...Record` wrapper with an
`ssST...`/`ssEN...` field, an `RL...RecordList`, `OptimizedAttributes`, `Duplicate`, the `string[]`
constructor): AllTypes, Customer, OrderLine, Order (nested record and list, `Attr_Id`), Note (`Attr_` and
`Cont_`), Level1-3, the entity Product and OrderCustomer (a record with two structures). The extension's
own `Test` and `TestHierarchical` structures are used too.

## Adding a test

1. Add a check to the suite that covers the action, or a new file `<Name>Tests.cs` with a
   `public static class <Name>Tests` and a `public static void Run()`; it is found by reflection, no
   registration needed.
2. Call the actions through `XR` (`XR.ToXml`, `XR.ListToXml`, `XR.FromXml`, `XR.Ext.Mss...`) with
   `XR.Defaults()` (what the platform passes for an empty DefaultConfig), `XR.Raw()` (ExcludeIfNull False,
   the serializer's text unchanged), `TD.Compact()` (also Indentation False) or `XR.Zero()`.
3. Assert with `TestLog.Eq` (exact strings show as C# literals on failure, ready to paste),
   `TestLog.Check`, `TestLog.Throws` (exception type and a part of the message) or `TestLog.NoThrow`.
   Assert the extension's own messages; for a .NET exception assert its type, not its wording.
4. The expected value is the specified behaviour (the action's and the structures' descriptions, the
   XML and XML Schema specifications, the OutSystems date conventions). Run it on both targets: if the
   published DLL already does it, the check passes; if not, register a known issue. Check names must be
   unique within a suite (they are the key of the register).
5. A new record type goes in `TestRecordTypes.cs`, written like the generated code next to it.

## Known issues

Defects are listed in `KNOWN-ISSUES.md`. Their checks keep the specified behaviour and are registered in
`KnownIssues.cs` as `{ id, status, suite, check name }`:

- **Open**: still in the source. The check is `KNOWN KI-xx` on both targets.
- **Fixed**: fixed in the source and not yet in the baseline DLL. `KNOWN` on baseline; on source it must pass, and a
  failure is reported as a regression.

A registered check that passes where it is expected to fail fails the run (the entry is stale), and so
does a registered check that no longer runs (renamed or removed).

To register an issue or an intended change of behaviour:

1. Get the exact names of the failing checks:

       XR_LIST_FAILURES=1 tests/run-tests.sh 2>&1 >/dev/null
       XR_LIST_FAILURES=1 TARGET=source tests/run-tests.sh 2>&1 >/dev/null

2. Add one line per check to `KnownIssues.cs` with the next free id and its status, and describe the
   issue in `KNOWN-ISSUES.md` (symptom, cause with file:line, impact).
3. A change to the source that fixes an issue: mark its entries `Fixed` and move it to "Fixed" in
   `KNOWN-ISSUES.md`. Any other check that starts failing on `TARGET=source` is a regression; don't
   register it.
4. When a new version is published, replace `tests/lib/baseline/` with its DLL and remove the entries of
   the issues it fixed (they now pass on both targets); note the version in `KNOWN-ISSUES.md`.

Don't change an expected value to make a failing check pass.
