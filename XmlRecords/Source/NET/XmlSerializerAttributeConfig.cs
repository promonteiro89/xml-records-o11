using System.Collections.Generic;

namespace OutSystems.NssXmlRecords
{
    public enum ERecordListMode
    {
        Child,
        Sequence
    }
    public enum ERecordMode
    {
        Child,
        Sequence
    }

    class XmlSerializerAttributeConfig
    {
        private Dictionary<string, STAttributeXmlConfigStructure> _config = new Dictionary<string, STAttributeXmlConfigStructure>();
        private STDefaultXmlConfigStructure _defaultConfig;
        private string _namespace = null;
        private string _effectiveName = null;

        public XmlSerializerAttributeConfig(RLAttributeXmlConfigRecordList attributesConfig, STDefaultXmlConfigStructure defaultConfig) : this(attributesConfig, defaultConfig, null, null) { }
        public XmlSerializerAttributeConfig(RLAttributeXmlConfigRecordList attributesConfig, STDefaultXmlConfigStructure defaultConfig, string namespaceXmlns, string effectiveName)
        {
            _defaultConfig = defaultConfig;
            attributesConfig.Reset();
            attributesConfig.StartIteration();

            while (!attributesConfig.Eof)
            {
                _config.Add(
                      attributesConfig.CurrentRec.ssSTAttributeXmlConfig.ssName,
                      attributesConfig.CurrentRec.ssSTAttributeXmlConfig);

                attributesConfig.Advance();
            }
            attributesConfig.EndIteration();

            this._namespace = namespaceXmlns;
            this._effectiveName = effectiveName;
        }

        public bool ExcludeIfNull(string attribute)
        {
            if(!_config.ContainsKey(attribute)) 
                return _defaultConfig.ssExcludeIfNull;

            return _config[attribute].ssExcludeIfNull;
        }

        public ERecordListMode RecordListMode(string attribute)
        {
            if (!_config.ContainsKey(attribute))
                return _defaultConfig.ssRecordListMode == "S" ? ERecordListMode.Sequence : ERecordListMode.Child;

            return _config[attribute].ssRecordListMode == "S" ? ERecordListMode.Sequence : ERecordListMode.Child;
        }

        public ERecordMode RecordMode(string attribute)
        {
            if (!_config.ContainsKey(attribute))
                return _defaultConfig.ssRecordMode == "S" ? ERecordMode.Sequence : ERecordMode.Child;

            return _config[attribute].ssRecordMode == "S" ? ERecordMode.Sequence : ERecordMode.Child;
        }

        public string NullValue(string attribute)
        {
            if (!_config.ContainsKey(attribute))
                return string.Empty;

            return _config[attribute].ssNullableValue;
        }

        public string AttributeName(string attribute)
        {
            if (!_config.ContainsKey(attribute))
                return attribute;

            return string.IsNullOrEmpty(_config[attribute].ssAttributeName) ? attribute : _config[attribute].ssAttributeName;
        }

        public string Namespace
        {
            get
            {
                return this._namespace;
            }
        }
        public string EffectiveName
        {
            get
            {
                return this._effectiveName;
            }
        }

        public string AttributePrefix
        {
            get
            {
                return _defaultConfig.ssAttributePrefix;
            }
        }

        public string ContentPrefix
        {
            get
            {
                return _defaultConfig.ssContentPrefix;
            }
        }
    }
}
