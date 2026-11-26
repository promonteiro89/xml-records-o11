# XML Records for OutSystems 11

[![Platform](https://img.shields.io/badge/Platform-OutSystems_11-red.svg)](https://www.outsystems.com/)
[![.NET](https://img.shields.io/badge/.NET_Framework-4.8-blue.svg)](https://dotnet.microsoft.com/download/dotnet-framework/net48)

An Integration Studio extension (.NET Framework 4.8) that converts OutSystems 11 Records and Record Lists to XML and back. It has three server actions, and the XML layout can be configured per record and per attribute: element or attribute, names, namespaces, null handling, and the formats used for numbers and dates.

The extension is published on the OutSystems Forge as [**XML Records**](https://www.outsystems.com/forge/component-overview/153/xml-records-o11). This repository is where changes are made and tested before a release.

**Version numbers:** the Forge's 1.6.16 and 1.6.17 packages are earlier uploads that contain the same DLL, from before this repository. This repository's v1.6.16 is newer than both and holds the fixes listed in the [changelog](CHANGELOG.md).

## Table of Contents

- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Actions](#actions)
- [Configuration](#configuration)
- [Dates and times](#dates-and-times)
- [Build and deployment](#build-and-deployment)
- [Testing](#testing)
- [Known issues](#known-issues)
- [Contributing](#contributing)
- [Changelog](#changelog)

---

## Architecture

```
XmlRecords.xif                      # The extension as Integration Studio opens and publishes it
XmlRecords/Source/NET/
├── XmlRecords.csproj / .sln        # Project definition (.NET Framework 4.8)
├── Interface.cs                    # Action signatures and descriptions (IssXmlRecords), generated
├── XmlRecords.cs                   # The actions (CssXmlRecords)
├── RecordXmlSerializer.cs          # Record -> XML
├── RecordXmlDeserializer.cs        # XML -> Record
├── XmlSerializerConfig.cs          # Looks up the RecordXmlConfig of each record
├── XmlSerializerAttributeConfig.cs # Looks up the AttributeXmlConfig of each attribute
├── TypeMapper.cs                   # Converts values to text and back (numbers, booleans, dates)
├── AssemblyInfo.cs
├── Structures.cs / Records.cs / RecordLists.cs / Entities.cs   # Generated OutSystems data types
└── Test.cs, unittests/             # Old tests kept in the .xif; not compiled
tests/                              # The regression suite and its known-issues register
```

The serializer walks the record by reflection. It reads the `ss`-prefixed fields that Integration Studio and Service Studio generate, so any structure or entity record works without extra code.

---

## Prerequisites

- OutSystems 11 with Integration Studio (matching your environment's version).
- To run the tests: mono with Roslyn `csc` on macOS or Linux (`brew install mono`), or Windows with the .NET Framework 4.8 targeting pack, plus the OutSystems platform assemblies (see [Testing](#testing)).

---

## Actions

| Action | What it does |
|---|---|
| `RecordToXml(Record, XmlConfig, DefaultConfig, out Xml)` | Writes one record, including its nested records and lists, as XML. |
| `RecordListToXml(RecordList, XmlConfig, DefaultConfig, AddRootNode, out Xml)` | Writes every record of a list. AddRootNode wraps them in `<RecordList>`. |
| `XmlToRecordList(Xml, RecordList, XmlConfig, IgnoreXMLDeclarationAndNS, AttributePrefix, DefaultConfig, BypassRootRecordListNode, BypassComments, ShowXMLInLogs)` | Reads XML into the record list passed in. Its record type decides which elements are expected. |

`Record` and `RecordList` are typed `Object` in the extension. Pass the record or list variable itself, and it is read or filled by reflection.

---

## Configuration

`XmlConfig` is a list of `RecordXmlConfig`, one per record type that needs settings:

| Attribute | Meaning |
|---|---|
| RecordName | The structure or entity name. `Parent/Child` applies the settings only to that child under that parent. |
| EffectiveName | Element name to use instead of RecordName. |
| Namespace | Default namespace written on the record's element. |
| AttributesConfig | List of `AttributeXmlConfig`: per attribute Name, AttributeName (element or XML attribute name), ExcludeIfNull, NullableValue, RecordMode and RecordListMode. |

`DefaultConfig` (`DefaultXmlConfig`) holds the defaults for every record: ExcludeIfNull, RecordMode / RecordListMode (`Child` or `Sequence`), AttributePrefix (default `Attr_`, attributes with this prefix are written as XML attributes), ContentPrefix (`Cont_`, written as the element's text), XmlDeclaration, Indentation, TypeDecimalCulture and the three date formats below.

---

## Dates and times

From 1.6.16 on, every date conversion uses the invariant culture, so the server's regional settings never change the result.

| Setting | Written as (default) | Read (default) |
|---|---|---|
| TypeDateTimeFormat | `yyyy-MM-ddTHH:mm:ss` | `yyyy-MM-dd` + `T` or a space + `HH:mm`, optional seconds, optional fraction, optional `Z` / `±hh:mm` |
| TypeDateFormat | `yyyy-MM-dd` | `yyyy-MM-dd`, optional `Z` / `±hh:mm` (ignored) |
| TypeTimeFormat | `HH:mm:ss` | `HH:mm`, optional seconds and fraction |

- Each setting can hold several .NET custom formats separated by `|`, for example `dd/MM/yyyy HH:mm:ss|yyyy-MM-ddTHH:mm:ss`. The first one is used when writing, and all of them are tried when reading.
- When reading, the DateTime, Date and Time lists are tried in that order, whatever the attribute's type. The first list that matches wins, so with `TypeDateFormat=yyMMdd` and `TypeTimeFormat=HHmmss` the text `121110` reads as the date 2012-11-10, not as a time.
- A Date Time value with `Z` or an offset is converted to the server's local time. The value keeps its time even when the attribute is a Date, so `2024-07-01T00:00:00+02:00` read into a Date attribute holds 2024-06-30 23:00:00 on a server in Lisbon, which shows as 2024-06-30.
- In a custom format, any zone specifier (`K`, `z`, `zz`, `zzz` or an unquoted `Z`) converts the value on read. An unquoted `Z` also writes the server's local time followed by a literal `Z`, so `yyyy-MM-ddTHH:mm:ssZ` shifts the value by the server's offset on every round trip. Quote it (`yyyy-MM-dd'T'HH:mm:ss'Z'`) to read and write the value as written. Time values with a zone (`13:45:30Z`) are rejected by the default formats.
- A zoned value on 1900-01-01 with a time is converted like any other, so `1900-01-01T03:00:00Z` reads as 1899-12-31 22:00:00 on a server in New York. `0001-01-01T00:00:00` with a positive offset is rejected.
- Fractions of a second longer than 7 digits are rejected.
- A Time value read is placed on 1900-01-01, the OutSystems convention.
- The null date is 1900-01-01 00:00:00 (and `DateTime.MinValue`). It is always written as empty text, and ExcludeIfNull decides whether the empty element is left out. A null date written with a zone (`1900-01-01T00:00:00Z`) reads as the null date. Dates before 1900 are written like any other.
- Text around a value is trimmed, and a blank value reads as the null date.

---

## Build and deployment

1. Open `XmlRecords.xif` in Integration Studio. It unpacks the source to `XmlRecords/Source/NET`.
2. Edit the hand-written files (`XmlRecords.cs`, the serializer files, `TypeMapper.cs`). Keep CRLF line endings, as Integration Studio writes them (`.gitattributes` checks the source out with CRLF).
3. In Integration Studio: Compile, then 1-Click Publish to your environment.

Integration Studio unpacks the `.xif` again every time it opens it, overwriting `XmlRecords/Source/NET`. Copy your edited files somewhere else before you reopen it.

---

## Testing

The suite in `tests/` calls the three actions the way the platform does and checks the exact XML and values. It runs against two targets:

| Target | Runs against |
|---|---|
| `baseline` (default) | `tests/lib/baseline/OutSystems.NssXmlRecords.dll`, the extension as published on the Forge (1.6.17), unchanged |
| `source` | the extension compiled from `XmlRecords/Source/NET` |

```
tests/run-tests.sh                    # macOS / Linux, published DLL
TARGET=source tests/run-tests.sh      # compiled from the source
tests\run-tests.ps1 -Target source    # Windows
```

The tests need OutSystems' platform assemblies, which are not redistributed here. Open `XmlRecords.xif` in Integration Studio and run Update Source Code (Extension menu) if `OutSystems.HubEdition.RuntimePlatform.dll` is not in `XmlRecords/Source/NET/Bin`. The scripts take the assemblies from there. Details are in [tests/README.md](tests/README.md).

---

## Known issues

[tests/KNOWN-ISSUES.md](tests/KNOWN-ISSUES.md) lists the defects found while writing the tests. Each one has the checks that show it, and its status: open, or fixed in the source.

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

---

## Changelog

See [CHANGELOG.md](CHANGELOG.md).
