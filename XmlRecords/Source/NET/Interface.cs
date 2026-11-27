using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssXmlRecords {

	public interface IssXmlRecords {

		/// <summary>
		/// Reads XML into a Record List. The list&apos;s record type decides which elements are read.
		/// </summary>
		/// <param name="ssXml">The XML to read. Its top elements must have the name of the list&apos;s record type, or the EffectiveName set for it in XmlConfig.</param>
		/// <param name="ssRecordList">The Record List variable to fill. Each record read from the XML is appended to it.</param>
		/// <param name="ssXmlConfig">Settings per record type. Leave it empty to use the record and attribute names.</param>
		/// <param name="ssIgnoreXMLDeclarationAndNS">When True, the XML declaration and the xmlns and xsi attributes are removed before reading, so elements with a namespace are matched by name. Default True.</param>
		/// <param name="ssAttributePrefix">Used only when DefaultConfig.AttributePrefix is empty. Attributes whose name starts with this prefix are read from XML attributes.</param>
		/// <param name="ssDefaultConfig">Defaults for every record, such as the prefixes and the number and date formats.</param>
		/// <param name="ssBypassRootRecordListNode">When True and the root element is &lt;RecordList&gt;, its child elements are read as the records. Default True.</param>
		/// <param name="ssBypassComments">When True, XML comments are removed before reading. Default False.</param>
		/// <param name="ssShowXMLInLogs">When True, the error raised for XML that can&apos;t be parsed includes the XML. Set it to False when the XML has sensitive data. Default True.</param>
		void MssXmlToRecordList(string ssXml, object ssRecordList, RLRecordXmlConfigRecordList ssXmlConfig, bool ssIgnoreXMLDeclarationAndNS, string ssAttributePrefix, RCDefaultXmlConfigRecord ssDefaultConfig, bool ssBypassRootRecordListNode, bool ssBypassComments, bool ssShowXMLInLogs);

		/// <summary>
		/// Writes a Record as XML, with its nested records and lists.
		/// </summary>
		/// <param name="ssRecord">The Record to write. Pass the record variable itself.</param>
		/// <param name="ssXmlConfig">Settings per record type. Leave it empty to use the record and attribute names.</param>
		/// <param name="ssDefaultConfig">Defaults for every record, such as null handling, indentation, the XML declaration and the number and date formats.</param>
		/// <param name="ssXml">The XML written.</param>
		void MssRecordToXml(object ssRecord, RLRecordXmlConfigRecordList ssXmlConfig, RCDefaultXmlConfigRecord ssDefaultConfig, out string ssXml);

		/// <summary>
		/// Writes each record of a Record List as XML, one after the other.
		/// </summary>
		/// <param name="ssRecordList">The Record List to write. It must have at least one record.</param>
		/// <param name="ssXmlConfig">Settings per record type. Leave it empty to use the record and attribute names.</param>
		/// <param name="ssDefaultConfig">Defaults for every record, such as null handling, indentation, the XML declaration and the number and date formats.</param>
		/// <param name="ssAddRootNode">When True, the records are wrapped in a &lt;RecordList&gt; element. Default False.</param>
		/// <param name="ssXml">The XML written.</param>
		void MssRecordListToXml(object ssRecordList, RLRecordXmlConfigRecordList ssXmlConfig, RCDefaultXmlConfigRecord ssDefaultConfig, bool ssAddRootNode, out string ssXml);

	} // IssXmlRecords

} // OutSystems.NssXmlRecords
