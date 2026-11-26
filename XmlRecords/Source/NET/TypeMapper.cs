using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OutSystems.HubEdition.RuntimePlatform.Db;
using System.Reflection;
using System.Security;
using System.Globalization;

using OutSystems.RuntimeCommon;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssXmlRecords
{
    
    class TypeMapper
    {

        public string _decimalCulture = "en-US";
        private readonly XmlDateTimeConverter _dateTimeConverter;

        public TypeMapper(STDefaultXmlConfigStructure defaultConfig)
        {
            if (!string.IsNullOrEmpty(defaultConfig.ssTypeDecimalCulture))
                this._decimalCulture = defaultConfig.ssTypeDecimalCulture;

            this._dateTimeConverter = new XmlDateTimeConverter(
                defaultConfig.ssTypeDateTimeFormat,
                defaultConfig.ssTypeDateFormat,
                defaultConfig.ssTypeTimeFormat);

        }
        public string TypeToString(object value)
        {
            if (value is DateTime)
            {
                return _dateTimeConverter.ToXml((DateTime)value);
            }
            else if (value is Decimal)
            {
                return SecurityElement.Escape(((Decimal)value).ToString(new CultureInfo(_decimalCulture)));
            }
            else
            {
                return SecurityElement.Escape(value.ToString());
            }
        }

        public object StringToType(string value, Type t)
        {
            if (t == typeof(String))
            {
                return value;
            }
            else if (t == typeof(Int32))
            {
                return Int32.Parse(value);
            }
            else if (t == typeof(Decimal))
            {
                return Decimal.Parse(value, new CultureInfo(_decimalCulture));
            }
            else if (t == typeof(Boolean))
            {
                value = value.Replace("1", "True").Replace("0", "False");
                return Boolean.Parse(value);
            }
            else if (t == typeof(Int64))
            {
                return Int64.Parse(value);
            }
            else if (t == typeof(DateTime))
            {
                return _dateTimeConverter.FromXml(value);
            }
            else
            {
                throw new NotSupportedException("Not able to deserialize type " + t.ToString());
            }
        }

        public IRecord GetEmptyRecord(RecordList rl)
        {
            return GetEmptyRecord(rl.Current.GetType());
        }
        public IRecord GetEmptyRecord(Type recordType)
        {
            ConstructorInfo ci = recordType.GetConstructor(new Type[] { typeof(string[]) });
            string[] argString = new string[] { "" };
            return ci.Invoke(new object[] { argString }) as IRecord;
        }

        public void GetSimpleRecordInfo(IRecord rec, out String recName, out ISimpleRecord structValue) {
            FieldInfo dummy;
            GetSimpleRecordInfo(rec, out recName, out structValue, out dummy);
        }

        public void GetSimpleRecordInfo(IRecord rec, out String recName, out ISimpleRecord structValue, out FieldInfo recordTypeSubfield) {
            recordTypeSubfield = null;

            if (rec is ISimpleRecord) {

                // Try to fix the type names to what they would be without prefixes and suffixes ... but still very dependent of internals
                recName = rec.GetType().Name;
                if (recName.StartsWith("ST")) {
                    recName = recName.LeftCut("ST".Length);
                    if (recName.EndsWith("Structure")) {
                        recName = recName.RightCut("Structure".Length);
                    }
                } else if (recName.StartsWith("EN")) {
                    recName = recName.LeftCut("EN".Length);
                    if (recName.EndsWith("Entity")) {
                        recName = recName.RightCut("Entity".Length);
                    }
                }

                structValue = (ISimpleRecord)rec;

            } else {
                recordTypeSubfield = rec.GetType().GetFields().First(f => typeof(ISimpleRecord).IsAssignableFrom(f.FieldType));
                structValue = (ISimpleRecord)recordTypeSubfield.GetValue(rec);

                // Get name and remove platform ssST prefix
                recName = recordTypeSubfield.Name.RemoveIfStartsWith("ssST").RemoveIfStartsWith("ssEN");
            }
        }
    }

    // BEGIN XmlDateTimeConverter
    /// <summary>
    /// Converts OutSystems Date / Time / DateTime values (all System.DateTime) to and from XML text.
    /// Has no dependency on OutSystems assemblies.
    /// </summary>
    internal sealed class XmlDateTimeConverter
    {
        public static readonly DateTime NullDate = new DateTime(1900, 1, 1);

        private const int MaxEchoedValueLength = 100;

        // Built-in parse lists. "ss.FFFFFFF" accepts no fraction (the '.' is then optional) or 1-7 digits;
        // "K" accepts no zone, "Z", or an "+hh:mm"/"-hh:mm" offset.
        private static readonly string[] DefaultDateTimeFormats = new string[]
        {
            "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK",
            "yyyy-MM-dd HH:mm:ss.FFFFFFFK",
            "yyyy-MM-dd'T'HH:mmK",
            "yyyy-MM-dd HH:mmK"
        };
        private static readonly string[] DefaultDateFormats = new string[] { "yyyy-MM-dd", "yyyy-MM-ddK" };
        private static readonly string[] DefaultTimeFormats = new string[] { "HH:mm:ss.FFFFFFF", "HH:mm" };

        // Built-in output formats (backward compatible: no fraction, no zone).
        private const string DefaultDateTimeOutputFormat = "yyyy-MM-ddTHH:mm:ss";
        private const string DefaultDateOutputFormat = "yyyy-MM-dd";
        private const string DefaultTimeOutputFormat = "HH:mm:ss";

        private const string DefaultDateTimeDescription = "ISO 8601 date/time: yyyy-MM-dd(T| )HH:mm[:ss[.fffffff]][Z|±hh:mm]";
        private const string DefaultDateDescription = "ISO 8601 date: yyyy-MM-dd[Z|±hh:mm]";
        private const string DefaultTimeDescription = "ISO 8601 time: HH:mm[:ss[.fffffff]]";

        private readonly FormatList _dateTimeFormats;
        private readonly FormatList _dateFormats;
        private readonly FormatList _timeFormats;

        public XmlDateTimeConverter(string dateTimeFormats, string dateFormats, string timeFormats)
        {
            _dateTimeFormats = FormatList.Create(dateTimeFormats, DefaultDateTimeFormats, DefaultDateTimeOutputFormat, DefaultDateTimeDescription);
            _dateFormats = FormatList.Create(dateFormats, DefaultDateFormats, DefaultDateOutputFormat, DefaultDateDescription);
            _timeFormats = FormatList.Create(timeFormats, DefaultTimeFormats, DefaultTimeOutputFormat, DefaultTimeDescription);
        }

        public string ToXml(DateTime value)
        {
            if (value == NullDate || value == DateTime.MinValue)
                return string.Empty;

            string format;
            if (value.Date == NullDate && value.TimeOfDay != TimeSpan.Zero)
                format = _timeFormats.OutputFormat;
            else if (value.TimeOfDay == TimeSpan.Zero)
                format = _dateFormats.OutputFormat;
            else
                format = _dateTimeFormats.OutputFormat;

            return System.Security.SecurityElement.Escape(value.ToString(format, CultureInfo.InvariantCulture));
        }

        public DateTime FromXml(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return NullDate;

            string text = value.Trim();
            DateTime clock;
            DateTimeOffset zoned;
            bool hasZone;

            if (TryParseAny(text, _dateTimeFormats.ParseFormats, out clock, out zoned, out hasZone))
            {
                // The null date stays null whatever zone it was written in; converting it would turn it
                // into a real date (1899-12-31 ...) on any server that isn't on UTC.
                if (hasZone && (zoned.DateTime == NullDate || zoned.DateTime == DateTime.MinValue))
                    return NullDate;
                if (hasZone)
                    return DateTime.SpecifyKind(zoned.ToLocalTime().DateTime, DateTimeKind.Unspecified);
                return DateTime.SpecifyKind(clock, DateTimeKind.Unspecified);
            }

            if (TryParseAny(text, _dateFormats.ParseFormats, out clock, out zoned, out hasZone))
            {
                // Zone suffix on a date-only value is ignored: keep the written calendar date.
                DateTime date = hasZone ? zoned.DateTime.Date : clock.Date;
                return DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
            }

            if (TryParseAny(text, _timeFormats.ParseFormats, out clock, out zoned, out hasZone))
            {
                TimeSpan timeOfDay = hasZone ? zoned.DateTime.TimeOfDay : clock.TimeOfDay;
                return DateTime.SpecifyKind(NullDate.Add(timeOfDay), DateTimeKind.Unspecified);
            }

            throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                "Unable to convert '{0}' to a Date/Time/DateTime value. Tried DateTime formats [{1}], Date formats [{2}], Time formats [{3}].",
                EchoValue(value),
                _dateTimeFormats.Description,
                _dateFormats.Description,
                _timeFormats.Description));
        }

        private static string EchoValue(string value)
        {
            if (value.Length <= MaxEchoedValueLength)
                return value;
            return value.Substring(0, MaxEchoedValueLength) + "… (" + value.Length.ToString(CultureInfo.InvariantCulture) + " chars)";
        }

        // Tries each format in order. 'clock' is the wall-clock value as written; when the input carried a
        // timezone (Z or an offset), 'hasZone' is true and 'zoned' holds the parsed instant with its offset.
        private static bool TryParseAny(string text, string[] formats, out DateTime clock, out DateTimeOffset zoned, out bool hasZone)
        {
            foreach (string format in formats)
            {
                DateTime parsed;
                if (!DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out parsed))
                    continue;

                if (parsed.Kind == DateTimeKind.Unspecified)
                {
                    clock = parsed;
                    zoned = default(DateTimeOffset);
                    hasZone = false;
                    return true;
                }

                // A zone designator was present: re-parse as DateTimeOffset to keep the exact offset.
                DateTimeOffset dto;
                if (DateTimeOffset.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dto))
                {
                    clock = dto.DateTime;
                    zoned = dto;
                    hasZone = true;
                    return true;
                }
            }

            clock = default(DateTime);
            zoned = default(DateTimeOffset);
            hasZone = false;
            return false;
        }

        private sealed class FormatList
        {
            public string[] ParseFormats;
            public string OutputFormat;
            public string Description;

            // A configured value ('|' separated, entries trimmed, empty entries dropped) replaces the defaults;
            // its first entry is the output format and its entries are echoed literally in error messages.
            public static FormatList Create(string configured, string[] defaults, string defaultOutput, string defaultDescription)
            {
                string[] formats = string.IsNullOrWhiteSpace(configured)
                    ? new string[0]
                    : configured.Split('|').Select(f => f.Trim()).Where(f => f.Length > 0).ToArray();

                FormatList list = new FormatList();
                if (formats.Length == 0)
                {
                    list.ParseFormats = defaults;
                    list.OutputFormat = defaultOutput;
                    list.Description = defaultDescription;
                }
                else
                {
                    list.ParseFormats = formats;
                    list.OutputFormat = formats[0];
                    list.Description = string.Join(" | ", formats);
                }
                return list;
            }
        }
    }
    // END XmlDateTimeConverter
}