# Known issues

Defects found while writing the test suite. Each check keeps the expected (specified) behaviour; while
the defect is there, the check is reported as `KNOWN KI-xx` and the run stays green. `KnownIssues.cs`
lists the checks of each issue with a status:

- **Open**: the defect is in the published extension and in the source. KNOWN on both targets.
- **Fixed**: the source fixes it; the baseline DLL (Forge 1.6.17) doesn't have the fix yet. KNOWN on `TARGET=baseline` (the published DLL),
  and it must pass on `TARGET=source`: a failure there is a regression.

A registered check that passes where it should fail fails the run until the entry is updated, and so
does a registered check that no longer runs. When a fix is released, the baseline DLL is replaced by the
new release, its entries are removed from `KnownIssues.cs` and the issue gets its version below.

The baseline is XML Records 1.6.17 as published on the Forge (`tests/lib/baseline`). The Forge's 1.6.16 and 1.6.17 packages are earlier uploads with the same DLL; this repository's v1.6.16 is newer than both. Line numbers
refer to `XmlRecords/Source/NET/` unless another file is named; for the date issues the published code is
the old `TypeMapper` (decompiled from the baseline DLL), which the source replaced.
Severity: **High** loses or corrupts data, or does the wrong thing silently; **Medium** wrong result
or crash in a plausible use; **Low** edge input, unclear error, cosmetic.

## Open

### Writing XML (RecordToXml, RecordListToXml)

| ID | Sev | Action(s) | Symptom | Cause |
|---|---|---|---|---|
| KI-01 | High | RecordListToXml | With the platform's DefaultConfig (ExcludeIfNull True) and AddRootNode False, a list of two or more records throws `XmlException: There are multiple root elements`. Only one record or AddRootNode True works. | The ExcludeIfNull clean-up parses the whole output as one XML document (`XDocument.Parse`, XmlRecords.cs:75 and :198). Records side by side have no single root. |
| KI-02 | High | RecordToXml, RecordListToXml, XmlToRecordList | A Binary Data attribute is written as the text `System.Byte[]` (also when empty, so ExcludeIfNull never leaves it out), and reading one throws `NotSupportedException` wrapped in "Unable to convert xml value". The data is lost without an error on write. | `TypeToString` falls back to `value.ToString()` (TypeMapper.cs:45); `StringToType` has no `byte[]` case (TypeMapper.cs:78). Expected: xs:base64Binary. |
| KI-03 | Medium | RecordToXml, RecordListToXml | With DefaultConfig ExcludeIfNull True every empty element is removed after writing, so an attribute configured with ExcludeIfNull False loses its empty element anyway, and a record whose values are all empty comes back as `""` (not even `<Test />`). | `ClearXmlEmptyElements` (XmlRecords.cs:195-204) removes every element whose value is blank, including the record's root element, regardless of the attribute config. |
| KI-04 | Low | RecordToXml, RecordListToXml | Indentation False is ignored when ExcludeIfNull is True (the clean-up's `XDocument.ToString()` indents again with two spaces), and AddRootNode always writes a line break after `<RecordList>` and `</RecordList>`. | XmlRecords.cs:204 (`ToString()` without `SaveOptions.DisableFormatting`); XmlRecords.cs:49 and :67 use `AppendLine`. |
| KI-07 | Medium | RecordToXml, RecordListToXml, XmlToRecordList | A RecordXmlConfig for the third level of nesting under the key `Level2/Level3` is never used; `Level1/Level2/Level3`, `Level1/Level3` and `Level3` are. | The lookup fallback (XmlSerializerConfig.cs:69) drops the parents nearest the record first: `Level1/Level2/Level3` -> `Level1/Level3` -> `Level3`. |
| KI-08 | High | RecordToXml, RecordListToXml, XmlToRecordList | A record with more than one structure or entity (an aggregate's record joining two sources) writes and reads only the first one; the others are silently dropped. | `GetSimpleRecordInfo` takes the first `ISimpleRecord` field (TypeMapper.cs:120). |
| KI-09 | Low | RecordToXml, RecordListToXml | Text with a character XML 1.0 can't hold (e.g. U+0001) is written raw, which gives XML that no parser accepts; with ExcludeIfNull True the clean-up throws a bare `XmlException` instead. | `SecurityElement.Escape` only escapes `< > & " '` (TypeMapper.cs:45). |

### Reading XML (XmlToRecordList)

| ID | Sev | Action(s) | Symptom | Cause |
|---|---|---|---|---|
| KI-05 | High | XmlToRecordList | XML with a default namespace (as RecordToXml writes it when a RecordXmlConfig has Namespace) is read without its nested list items: the list comes back empty, no error. Works only with IgnoreXMLDeclarationAndNS True. | The list items are looked up with an XPath without namespace (`SelectNodes("./" + name)`, RecordXmlDeserializer.cs:164 and :168). |
| KI-06 | High | XmlToRecordList | An EffectiveName configured with a hierarchical key (`Order/OrderLine`, `Order/Customer`) is used when writing but not when reading: a list's items come back as one blank item, a Child-mode record comes back empty, no error. | The reader looks the child's config up without the parent's name (`GetConfig(parentHierarchy + "/" + childRecName)`, RecordXmlDeserializer.cs:142 and :161) while the writer includes it. |

### Without a failing check

These are documented behaviour of the published extension that the tests pin as they are; they are
listed because they surprise users.

| Note |
|---|
| The XmlToRecordList input AttributePrefix is used only when DefaultConfig.AttributePrefix is empty, and the structure's default is `Attr_`, so with the platform defaults the input has no effect. |
| An XML declaration or a top-level comment in the input is taken as a record (`The current node (xml) does not match ...`) unless IgnoreXMLDeclarationAndNS / BypassComments are True. |
| A record element that holds only text (`<Test>abc</Test>`) gives that text to every attribute of the record. |
| With EffectiveName set, the "does not match" error names the structure, not the EffectiveName expected. |
| With the platform defaults the output is re-formatted by the ExcludeIfNull clean-up: two-space indentation and Environment.NewLine; with ExcludeIfNull False it is tab-indented with CRLF. The XML declaration and the `<RecordList>` lines always end with Environment.NewLine. |
| A Decimal keeps its scale (`1234.50`, default `0.0`), and TypeDecimalCulture also accepts that culture's thousands separators when reading. |

## Fixed in v1.6.16 of this repository (not in the Forge 1.6.17 baseline)

The date/time converter (`XmlDateTimeConverter` in TypeMapper.cs) replaces the published code's
date handling. Its checks are KNOWN on the published DLL and pass on the source.

| ID | Sev | Action(s) | Defect in the published extension | Fix |
|---|---|---|---|---|
| KI-10 | High | XmlToRecordList | ISO 8601 / xs:dateTime values other than exactly `yyyy-MM-ddTHH:mm:ss` failed: fractional seconds (`.123`), `Z` or an offset, a space instead of `T`, no seconds; also `yyyy-MM-ddZ` dates and `HH:mm` / fractional times. Cause: `ParseExact` with the single pattern `s` / `HH:mm:ss` / `yyyy-MM-dd`. | Default format lists accept them; a value with a zone is converted to the server's local time (Kind Unspecified), a zone on a date-only value is ignored. |
| KI-11 | High | RecordToXml, RecordListToXml, XmlToRecordList | Dates were formatted and parsed with the server's culture: on fi-FI the default Time was written `13.45.30` and a custom `dd/MM/yyyy HH:mm:ss` as `15.01.2024 13.45.30`; on th-TH a Date was written with the Buddhist year (`2567-01-15`) and `2024-01-15` was read as year 1481. | InvariantCulture everywhere. |
| KI-12 | Medium | XmlToRecordList | A value read with TypeTimeFormat kept today's date instead of 1900-01-01 (the OutSystems Time convention), so the value changed every day. | A Time match is anchored on 1900-01-01, also for custom formats. |
| KI-13 | High | RecordToXml, RecordListToXml | Every value up to 1900-01-01 was written empty, so real dates before 1900 (birth dates, historical data) were lost; they were read correctly. | Only 1900-01-01 00:00:00 and DateTime.MinValue are the null date. |
| KI-14 | Medium | XmlToRecordList, RecordToXml | Which format was used to read depended on the text (longer than 10 characters: DateTime format; has `:`: Time format; else Date format), so a TypeDateFormat like `dd MMMM yyyy` or a TypeTimeFormat like `HH.mm` could never read; only one format per setting. | Each setting is a `|`-separated list; every list is tried in order (DateTime, Date, Time), the first entry is the output format. |
| KI-15 | Low | XmlToRecordList | Date text with surrounding spaces (CDATA, an XML attribute) failed, and a blank value (`<![CDATA[ ]]>`, `Day=" "`) threw instead of giving the null date. | The text is trimmed; blank is 1900-01-01. |
| KI-16 | Low | RecordToXml, RecordListToXml | A custom date format with a literal `&` or `<` wrote unescaped text (invalid XML). | The formatted text is escaped like every other value. |
| KI-17 | Low | XmlToRecordList | An unreadable date said only "String was not recognized as a valid DateTime." | The message lists the formats that were tried. |
