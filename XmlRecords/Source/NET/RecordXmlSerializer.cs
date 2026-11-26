using System;
using System.Text;
using OutSystems.HubEdition.RuntimePlatform.Db;
using System.Reflection;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssXmlRecords
{
    class RecordXmlSerializer
    {
        XmlSerializerConfig _config = null;
        RCDefaultXmlConfigRecord _defaultConfig = new RCDefaultXmlConfigRecord();

        private int identLevel = 0;

        public int IdentLevel
        {
            get { return identLevel; }
            set { identLevel = value; }
        }

        private string TAB
        {
            get
            {
                if(_defaultConfig.ssSTDefaultXmlConfig.ssIndentation == false)
                {
                    identLevel = 0;
                }
                return string.Empty.PadLeft(identLevel, '\t');
            }
        }

        private string NewLine
        {
            get
            {
                if (_defaultConfig.ssSTDefaultXmlConfig.ssIndentation == false)
                {
                    return "";
                } else
                {
                    return "\r\n";
                }
            }
        }


        public RecordXmlSerializer(XmlSerializerConfig config)
        {
            _config = config;
            _defaultConfig = config.GetDefaultConfig();
        }

        public string Serialize(IRecord rec)
        {
            return this.Serialize(rec, string.Empty, false, null);
        }

        private string Serialize(IRecord rec, string parentHierarchy, bool excludeRecordDefinition, StringBuilder parentSB)
        {
            // Required for database entities
            rec = rec.Duplicate();

            StringBuilder sb = new StringBuilder();
            _config.TypeMapper.GetSimpleRecordInfo(rec, out string recName, out ISimpleRecord structValue);

            FieldInfo[] attributes = structValue.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            // Get the configuration
            XmlSerializerAttributeConfig config = _config.GetConfig(parentHierarchy + "/" + recName);
            string regEffectiveName = string.IsNullOrEmpty(config.EffectiveName) ? recName : config.EffectiveName;


            if (!excludeRecordDefinition)
            {
                sb.Append(TAB + "<" + regEffectiveName);
                if (!string.IsNullOrEmpty(config.Namespace))
                    sb.Append(" xmlns=\"" + config.Namespace + "\"");
                sb.Append(">" + NewLine);
                identLevel++;
            } //TODO: Add namespace to parentSB if excludeRecordDefinition?

            foreach (FieldInfo curField in attributes)
            {
                if (!curField.FieldType.IsSerializable || !curField.Name.TrimStart('_').StartsWith("ss"))
                    continue;

                bool isRecordList = typeof(RecordList).IsAssignableFrom(curField.FieldType);
                bool isRecord = curField.FieldType.GetInterface("IRecord") != null;

                // Remove platform ss prefix
                string attName = curField.Name.TrimStart('_');
                if (attName.StartsWith("ss"))
                    attName = attName.Substring(2);

                string tagName = config.AttributeName(attName);

                if (isRecordList)
                {
                    RecordList rl = curField.GetValue(structValue) as RecordList;

                    if (rl.Empty)
                        if (config.ExcludeIfNull(attName))
                            continue;

                    ERecordListMode rlMode = config.RecordListMode(attName);
                    if (rlMode == ERecordListMode.Child)
                    {
                        sb.Append(TAB + "<" + tagName + ">" + NewLine);
                        identLevel++;
                    }

                    rl.Reset();
                    rl.StartIteration();

                    if (!(rl.Current is IRecord)) {
                        throw new NotImplementedException("Lists of basic types are not supported. Use lists of structures instead.");
                    }

                    while (!rl.Eof)
                    {
                        sb.Append(this.Serialize((IRecord)rl.Current, parentHierarchy + "/" + recName, false, null));
                        rl.Advance();
                    }
                    rl.EndIteration();

                    if (rlMode == ERecordListMode.Child)
                    {
                        identLevel--;
                        sb.Append(TAB + "</" + tagName + ">" + NewLine);
                    }
                }
                else if (isRecord)
                {
                    sb.Append(TAB + "<" + tagName + ">" + NewLine);
                    identLevel++;

                    ERecordMode rMode = config.RecordMode(attName);

                    IRecord r = curField.GetValue(structValue) as IRecord;
                    sb.Append(this.Serialize(r, parentHierarchy + "/" + recName, rMode == ERecordMode.Sequence, sb));

                    identLevel--;
                    sb.Append(TAB + "</" + tagName + ">" + NewLine);
                }
                else
                {
                    object val = curField.GetValue(structValue);
                    string attValue = val == null ? string.Empty : val.ToString();
                    string attMappedValue = _config.TypeMapper.TypeToString(val);

                    if (curField.FieldType.Name == "Boolean")
                        val = val.ToString().ToLower();

                    // Exclude null?
                    if (attValue.ToString() == config.NullValue(attName) || attMappedValue == config.NullValue(attName))
                        if (config.ExcludeIfNull(attName))
                            continue;


                    if (!string.IsNullOrEmpty(config.ContentPrefix)
                       && tagName.StartsWith(config.ContentPrefix))
                    {
                        sb.Append(TAB + _config.TypeMapper.TypeToString(val) + NewLine);
                    }
                    else if (!string.IsNullOrEmpty(config.AttributePrefix)
                       && tagName.StartsWith(config.AttributePrefix))
                    {
                        //remove attribute prefix
                        tagName = tagName.Substring(config.AttributePrefix.Length);

                        StringBuilder sbAtt = sb;
                        string curXml = sb.ToString();
                        int parentTagEnd = curXml.IndexOf(">");
                        int namespaceEnd = curXml.IndexOf(" xmlns=\"");
                        if (excludeRecordDefinition)
                        {
                            sbAtt = parentSB;
                            curXml = parentSB.ToString();
                            parentTagEnd = curXml.LastIndexOf(">");
                            namespaceEnd = curXml.IndexOf(" xmlns=\"", curXml.LastIndexOf("<"));
                        }
                        if (namespaceEnd > 0) parentTagEnd = namespaceEnd;
                        sbAtt.Insert(parentTagEnd, " " + tagName + "=\"" + _config.TypeMapper.TypeToString(val) + "\"");
                    }
                    else
                    {
                        sb.Append(TAB + "<" + tagName + ">" + _config.TypeMapper.TypeToString(val) + "</" + tagName + ">" + NewLine);
                    }
                }
            }

            if (!excludeRecordDefinition)
            {
                identLevel--;
                sb.Append(TAB + "</" + regEffectiveName + ">" + NewLine);
            }

            return sb.ToString();
        }
    }
}
