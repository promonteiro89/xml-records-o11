# Test dependencies

Assemblies the tests build against:

| File | From |
|---|---|
| OutSystems.HubEdition.RuntimePlatform.dll, OutSystems.RuntimeCommon.dll, OutSystems.HubEdition.DatabaseAbstractionLayer.dll | **not in the repository** (OutSystems' binaries). Integration Studio puts them in `XmlRecords/Source/NET/Bin` when you open `XmlRecords.xif` and run Update Source Code; the scripts use that folder unless these files are copied here or `OS_PLATFORM_BIN` is set |
| baseline/OutSystems.NssXmlRecords.dll | the published extension: the extension DLL of XML Records 1.6.17 on the Forge, SHA-1 `038b47d64d3f770fb39623b7153136fae5ea811d`, assembly version 11.16.0.35766 |

They are only used by the tests. `TARGET=baseline` (the default) runs the suite against
`baseline/OutSystems.NssXmlRecords.dll` unchanged, so the checks describe what OutSystems apps get today;
`TARGET=source` builds the extension from `XmlRecords/Source/NET` instead.

Replace the baseline DLL only when a new version is published to the Forge: take the DLL from that
release's `.xif`, update the SHA-1 above, and update `KnownIssues.cs` / `KNOWN-ISSUES.md` for the issues
that release fixed.
