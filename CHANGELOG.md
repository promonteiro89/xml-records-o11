# Changelog

Releases of this repository, newest first. The `.xif` of each release is attached to its [GitHub release](https://github.com/promonteiro89/xml-records-o11/releases). KI numbers refer to [tests/KNOWN-ISSUES.md](tests/KNOWN-ISSUES.md).

The Forge's 1.6.16 and 1.6.17 packages are earlier uploads with the same DLL, from before this repository. Release v1.6.16 here is newer than both.

## 1.6.16 (2026-11-26)

Dates and times are read in the formats XML providers actually send, and no longer depend on the server's regional settings.

- `XmlToRecordList` threw "Unable to convert xml value to structure type" for any date/time that wasn't exactly `yyyy-MM-ddTHH:mm:ss`: fractional seconds (`.123`), `Z` or an offset (`+01:00`), a space instead of `T`, or no seconds. These are now read. A value with a zone is converted to the server's local time, and a zone on a date-only value is ignored. A null date written with a zone (`1900-01-01T00:00:00Z`) still reads as the null date (KI-10).
- Dates were written and read with the server's culture. On a fi-FI server the default Time was written `13.45.30`. On th-TH a Date was written with the Buddhist year (`2567-01-15`), and `2024-01-15` was read as year 1481. The invariant culture is now used everywhere (KI-11).
- A value read with TypeTimeFormat got today's date, so the stored value changed from day to day. Times are now placed on 1900-01-01 (KI-12).
- Dates before 1900-01-01 were written empty, losing birth dates and historical data. Only 1900-01-01 00:00:00 is now treated as the null date (KI-13).
- The format used to read a value depended on its length and on whether it contained `:`. A TypeDateFormat like `dd MMMM yyyy` could never be read, and each setting took only one format. Each setting now takes a `|`-separated list: the first format is used to write, and every format is tried to read (KI-14).
- Date text with spaces around it failed to read, and a blank value threw. The text is now trimmed, and a blank value reads as the null date (KI-15).
- A custom date format with a literal `&` or `<` wrote invalid XML. The formatted text is now escaped (KI-16).
- An unreadable date said only "String was not recognized as a valid DateTime." The message now lists the formats that were tried (KI-17).
- The extension's project referenced NUnit and compiled two test files, and the `.xif` didn't contain NUnit's build files. Compiling it in Integration Studio failed with "This project references NuGet package(s) that are missing". The NUnit reference and the test files are now out of the build.

Behaviour changes: check these before upgrading.

- Custom date formats now always use the invariant culture. On a pt-PT server, `dd MMM yyyy` used to write `mar` and now writes `Mar`.
- A Date Time value with an offset read into a **Date** attribute can land on the previous or next day, because it is converted to server time and keeps that time. For example `2024-07-01T00:00:00+02:00` reads as 2024-06-30 23:00:00 on a server in Lisbon, which shows as 2024-06-30.
- Dates before 1900-01-01 used to be written as empty text, and are now written as dates.
- On servers whose culture doesn't write ISO dates (fi-FI, th-TH and others), dates are now written in ISO form.

For dates from 1900-01-01 on, on a server whose culture already wrote ISO dates (en-US, pt-PT and most others), the XML written with the default configuration is unchanged.
