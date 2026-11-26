#!/bin/sh
# Runs the XmlRecords test suite under mono (macOS/Linux), against one of two targets:
#   TARGET=baseline (default)  tests/lib/baseline/OutSystems.NssXmlRecords.dll, the published extension
#                              (Forge 1.6.17), unchanged
#   TARGET=source              the extension built from XmlRecords/Source/NET
# Known issues (KnownIssues.cs) carry a status: an open one is KNOWN on both targets, one fixed in the
# source is KNOWN on baseline and must pass on source.
#
# Needs: mono + Roslyn csc and the .NET Framework 4.8 reference assemblies. On Windows use run-tests.ps1.
#   OS_PLATFORM_BIN  folder with OutSystems.HubEdition.RuntimePlatform.dll, OutSystems.RuntimeCommon.dll
#                    and OutSystems.HubEdition.DatabaseAbstractionLayer.dll (default: tests/lib if they are
#                    there, else ../XmlRecords/Source/NET/Bin, where Integration Studio puts them)
#   EXT_SRC          extension source folder for TARGET=source (default: ../XmlRecords/Source/NET)
#   BASELINE_DLL     extension DLL for TARGET=baseline (default: tests/lib/baseline/OutSystems.NssXmlRecords.dll)
#   NET48_API        reference assemblies (default: /opt/homebrew/lib/mono/4.8-api)
#
# Usage: [TARGET=baseline|source] tests/run-tests.sh [suite-filter]
#        exit code 0 = no unexpected result, 1 = failures, 2 = build error
set -eu

HERE=$(cd "$(dirname "$0")" && pwd)
TARGET=${TARGET:-baseline}
EXT_SRC=${EXT_SRC:-$HERE/../XmlRecords/Source/NET}
BASELINE_DLL=${BASELINE_DLL:-$HERE/lib/baseline/OutSystems.NssXmlRecords.dll}
NET48_API=${NET48_API:-/opt/homebrew/lib/mono/4.8-api}
if [ -z "${OS_PLATFORM_BIN:-}" ]; then
  if [ -f "$HERE/lib/OutSystems.HubEdition.RuntimePlatform.dll" ]; then OS_PLATFORM_BIN=$HERE/lib
  else OS_PLATFORM_BIN=$HERE/../XmlRecords/Source/NET/Bin; fi
fi
OUT=$HERE/.build

case "$TARGET" in baseline|source) ;; *) echo "TARGET must be baseline or source, not '$TARGET'"; exit 2 ;; esac

for f in OutSystems.HubEdition.RuntimePlatform.dll OutSystems.RuntimeCommon.dll OutSystems.HubEdition.DatabaseAbstractionLayer.dll; do
  [ -f "$OS_PLATFORM_BIN/$f" ] || { echo "missing $OS_PLATFORM_BIN/$f (set OS_PLATFORM_BIN)"; exit 2; }
done

rm -rf "$OUT" && mkdir -p "$OUT"
cp "$OS_PLATFORM_BIN"/OutSystems.*.dll "$OUT/"

FW="-r:$NET48_API/mscorlib.dll -r:$NET48_API/System.dll -r:$NET48_API/System.Core.dll -r:$NET48_API/System.Data.dll \
 -r:$NET48_API/System.Xml.dll -r:$NET48_API/System.Xml.Linq.dll -r:$NET48_API/System.Runtime.Serialization.dll \
 -r:$NET48_API/Facades/netstandard.dll"
OS="-r:$OUT/OutSystems.HubEdition.RuntimePlatform.dll -r:$OUT/OutSystems.RuntimeCommon.dll \
 -r:$OUT/OutSystems.HubEdition.DatabaseAbstractionLayer.dll"

if [ "$TARGET" = baseline ]; then
  [ -f "$BASELINE_DLL" ] || { echo "missing $BASELINE_DLL (set BASELINE_DLL)"; exit 2; }
  cp "$BASELINE_DLL" "$OUT/OutSystems.NssXmlRecords.dll"
else
  # the extension: the source files Integration Studio compiles (not unittests/, which needs NUnit)
  EXT_FILES=$(find "$EXT_SRC" -maxdepth 1 -name '*.cs' ! -name '%Name%*')
  # shellcheck disable=SC2086
  csc -nologo -nowarn:1591,0168,0219,0162 -langversion:7.3 -target:library -nostdlib -noconfig \
    -out:"$OUT/OutSystems.NssXmlRecords.dll" $FW $OS $EXT_FILES >"$OUT/build-ext.log" 2>&1 \
    || { grep -v warning "$OUT/build-ext.log"; echo "extension build failed"; exit 2; }
fi

# the tests: C# 5 only, so they can also run under Windows PowerShell 5.1 Add-Type
# shellcheck disable=SC2086
csc -nologo -langversion:5 -target:exe -nostdlib -noconfig -out:"$OUT/XmlRecords.Tests.exe" \
  $FW $OS -r:"$OUT/OutSystems.NssXmlRecords.dll" "$HERE"/*.cs >"$OUT/build-tests.log" 2>&1 \
  || { grep -v warning "$OUT/build-tests.log"; echo "test build failed"; exit 2; }

# fixed time zone (dates with Z or an offset convert to server local time) and target for KnownIssues.cs;
# the culture is pinned to en-US inside the test program
export TZ=Europe/Lisbon
export XR_TARGET="$TARGET"
cd "$OUT"
set +e
mono XmlRecords.Tests.exe "$@" >"$OUT/test-output.txt"
code=$?
cat "$OUT/test-output.txt"
exit $code
