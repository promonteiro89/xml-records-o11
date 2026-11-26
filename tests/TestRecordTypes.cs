// Test-only OutSystems record types, written the way Integration Studio generates them in
// XmlRecords/Source/NET/Structures.cs, Records.cs and RecordLists.cs (ISimpleRecord structures with
// ss-prefixed fields, RC*Record wrappers with an ssST/ssEN field, RL*RecordList lists, OptimizedAttributes,
// Duplicate, the string[] constructor). The extension finds attributes by reflection, so these types
// exercise it exactly like an app's own structures, entities and aggregates do.
// Generated once by a script; edit by hand like generated code (keep every member in step). C# 5.

using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using OutSystems.ObjectKeys;
using OutSystems.RuntimeCommon;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.HubEdition.RuntimePlatform.Db;
using OutSystems.Internal.Db;
using OutSystems.HubEdition.RuntimePlatform.NewRuntime;

namespace XmlRecordsTestTypes {

	/// <summary>
	/// Structure <code>STAllTypesStructure</code> that represents the Service Studio structure
	///  <code>AllTypes</code> <p> Description: One attribute of every basic OutSystems data type.</p>
	/// </summary>
	[Serializable()]
	public partial struct STAllTypesStructure: ISerializable, ITypedRecord<STAllTypesStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdText = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*yP57YD91LbOLngnKkXdG_w");
		internal static readonly GlobalObjectKey IdNumber = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*pBvQR4h1xeOx3SM1W_Zt7g");
		internal static readonly GlobalObjectKey IdBigNumber = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*39QFQCPwwWRCaNp9hbX9Qg");
		internal static readonly GlobalObjectKey IdAmount = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*di7FTn9wDPGd8sNnpkUDGQ");
		internal static readonly GlobalObjectKey IdFlag = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*e1v1nBvsl1JuZSl0PkqNjQ");
		internal static readonly GlobalObjectKey IdDay = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*cDKQVsyPlXVmSrZokKV2Gg");
		internal static readonly GlobalObjectKey IdClock = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*KW5nN+M6dCSKE5tN+_49QQ");
		internal static readonly GlobalObjectKey IdMoment = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*cI1llxtf1VzjxAJ9B8gF7A");
		internal static readonly GlobalObjectKey IdBlob = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*r5OlD2YPQ4XWWTit_3+0GA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Text")]
		public string ssText;

		[System.Xml.Serialization.XmlElement("Number")]
		public int ssNumber;

		[System.Xml.Serialization.XmlElement("BigNumber")]
		public long ssBigNumber;

		[System.Xml.Serialization.XmlElement("Amount")]
		public decimal ssAmount;

		[System.Xml.Serialization.XmlElement("Flag")]
		public bool ssFlag;

		[System.Xml.Serialization.XmlElement("Day")]
		public DateTime ssDay;

		[System.Xml.Serialization.XmlElement("Clock")]
		public DateTime ssClock;

		[System.Xml.Serialization.XmlElement("Moment")]
		public DateTime ssMoment;

		[System.Xml.Serialization.XmlElement("Blob")]
		public byte[] ssBlob;


		public BitArray OptimizedAttributes;

		public STAllTypesStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssText = "";
			ssNumber = 0;
			ssBigNumber = 0L;
			ssAmount = 0.0M;
			ssFlag = false;
			ssDay = new DateTime(1900, 1, 1, 0, 0, 0);
			ssClock = new DateTime(1900, 1, 1, 0, 0, 0);
			ssMoment = new DateTime(1900, 1, 1, 0, 0, 0);
			ssBlob = new byte[] {};
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssText = r.ReadText(index++, "AllTypes.Text", "");
			ssNumber = r.ReadInteger(index++, "AllTypes.Number", 0);
			ssBigNumber = r.ReadLongInteger(index++, "AllTypes.BigNumber", 0L);
			ssAmount = r.ReadDecimal(index++, "AllTypes.Amount", 0.0M);
			ssFlag = r.ReadBoolean(index++, "AllTypes.Flag", false);
			ssDay = r.ReadDate(index++, "AllTypes.Day", new DateTime(1900, 1, 1, 0, 0, 0));
			ssClock = r.ReadTime(index++, "AllTypes.Clock", new DateTime(1900, 1, 1, 0, 0, 0));
			ssMoment = r.ReadDateTime(index++, "AllTypes.Moment", new DateTime(1900, 1, 1, 0, 0, 0));
			ssBlob = r.ReadBinaryData(index++, "AllTypes.Blob", new byte[] {});
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STAllTypesStructure r) {
			this = r;
		}


		public static bool operator == (STAllTypesStructure a, STAllTypesStructure b) {
			if (a.ssText != b.ssText) return false;
			if (a.ssNumber != b.ssNumber) return false;
			if (a.ssBigNumber != b.ssBigNumber) return false;
			if (a.ssAmount != b.ssAmount) return false;
			if (a.ssFlag != b.ssFlag) return false;
			if (a.ssDay != b.ssDay) return false;
			if (a.ssClock != b.ssClock) return false;
			if (a.ssMoment != b.ssMoment) return false;
			if (a.ssBlob != b.ssBlob) return false;
			return true;
		}

		public static bool operator != (STAllTypesStructure a, STAllTypesStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STAllTypesStructure)) return false;
			return (this == (STAllTypesStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssText.GetHashCode()
				^ ssNumber.GetHashCode()
				^ ssBigNumber.GetHashCode()
				^ ssAmount.GetHashCode()
				^ ssFlag.GetHashCode()
				^ ssDay.GetHashCode()
				^ ssClock.GetHashCode()
				^ ssMoment.GetHashCode()
				^ ssBlob.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STAllTypesStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssText = "";
			ssNumber = 0;
			ssBigNumber = 0L;
			ssAmount = 0.0M;
			ssFlag = false;
			ssDay = new DateTime(1900, 1, 1, 0, 0, 0);
			ssClock = new DateTime(1900, 1, 1, 0, 0, 0);
			ssMoment = new DateTime(1900, 1, 1, 0, 0, 0);
			ssBlob = new byte[] {};
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssText", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssText' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssText = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssNumber", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssNumber' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssNumber = (int) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssBigNumber", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssBigNumber' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssBigNumber = (long) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssAmount", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssAmount' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssAmount = (decimal) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssFlag", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssFlag' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssFlag = (bool) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssDay", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssDay' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssDay = (DateTime) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssClock", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssClock' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssClock = (DateTime) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssMoment", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssMoment' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssMoment = (DateTime) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssBlob", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssBlob' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssBlob = (byte[]) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STAllTypesStructure Duplicate() {
			STAllTypesStructure t;
			t.ssText = this.ssText;
			t.ssNumber = this.ssNumber;
			t.ssBigNumber = this.ssBigNumber;
			t.ssAmount = this.ssAmount;
			t.ssFlag = this.ssFlag;
			t.ssDay = this.ssDay;
			t.ssClock = this.ssClock;
			t.ssMoment = this.ssMoment;
			t.ssBlob = this.ssBlob;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Text")) VarValue.AppendAttribute(recordElem, "Text", ssText, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Text");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Number")) VarValue.AppendAttribute(recordElem, "Number", ssNumber, detailLevel, TypeKind.Integer); else VarValue.AppendOptimizedAttribute(recordElem, "Number");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".BigNumber")) VarValue.AppendAttribute(recordElem, "BigNumber", ssBigNumber, detailLevel, TypeKind.LongInteger); else VarValue.AppendOptimizedAttribute(recordElem, "BigNumber");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Amount")) VarValue.AppendAttribute(recordElem, "Amount", ssAmount, detailLevel, TypeKind.Decimal); else VarValue.AppendOptimizedAttribute(recordElem, "Amount");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Flag")) VarValue.AppendAttribute(recordElem, "Flag", ssFlag, detailLevel, TypeKind.Boolean); else VarValue.AppendOptimizedAttribute(recordElem, "Flag");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Day")) VarValue.AppendAttribute(recordElem, "Day", ssDay, detailLevel, TypeKind.Date); else VarValue.AppendOptimizedAttribute(recordElem, "Day");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Clock")) VarValue.AppendAttribute(recordElem, "Clock", ssClock, detailLevel, TypeKind.Time); else VarValue.AppendOptimizedAttribute(recordElem, "Clock");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Moment")) VarValue.AppendAttribute(recordElem, "Moment", ssMoment, detailLevel, TypeKind.DateTime); else VarValue.AppendOptimizedAttribute(recordElem, "Moment");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Blob")) VarValue.AppendAttribute(recordElem, "Blob", ssBlob, detailLevel, TypeKind.BinaryData); else VarValue.AppendOptimizedAttribute(recordElem, "Blob");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "text") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Text")) variable.Value = ssText; else variable.Optimized = true;
			} else if (head == "number") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Number")) variable.Value = ssNumber; else variable.Optimized = true;
			} else if (head == "bignumber") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".BigNumber")) variable.Value = ssBigNumber; else variable.Optimized = true;
			} else if (head == "amount") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Amount")) variable.Value = ssAmount; else variable.Optimized = true;
			} else if (head == "flag") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Flag")) variable.Value = ssFlag; else variable.Optimized = true;
			} else if (head == "day") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Day")) variable.Value = ssDay; else variable.Optimized = true;
			} else if (head == "clock") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Clock")) variable.Value = ssClock; else variable.Optimized = true;
			} else if (head == "moment") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Moment")) variable.Value = ssMoment; else variable.Optimized = true;
			} else if (head == "blob") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Blob")) variable.Value = ssBlob; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdText) {
				return ssText;
			} else if (key == IdNumber) {
				return ssNumber;
			} else if (key == IdBigNumber) {
				return ssBigNumber;
			} else if (key == IdAmount) {
				return ssAmount;
			} else if (key == IdFlag) {
				return ssFlag;
			} else if (key == IdDay) {
				return ssDay;
			} else if (key == IdClock) {
				return ssClock;
			} else if (key == IdMoment) {
				return ssMoment;
			} else if (key == IdBlob) {
				return ssBlob;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssText = (string) other.AttributeGet(IdText);
			ssNumber = (int) other.AttributeGet(IdNumber);
			ssBigNumber = (long) other.AttributeGet(IdBigNumber);
			ssAmount = (decimal) other.AttributeGet(IdAmount);
			ssFlag = (bool) other.AttributeGet(IdFlag);
			ssDay = (DateTime) other.AttributeGet(IdDay);
			ssClock = (DateTime) other.AttributeGet(IdClock);
			ssMoment = (DateTime) other.AttributeGet(IdMoment);
			ssBlob = (byte[]) other.AttributeGet(IdBlob);
		}
		public bool IsDefault() {
			STAllTypesStructure defaultStruct = new STAllTypesStructure(null);
			if (this.ssText != defaultStruct.ssText) return false;
			if (this.ssNumber != defaultStruct.ssNumber) return false;
			if (this.ssBigNumber != defaultStruct.ssBigNumber) return false;
			if (this.ssAmount != defaultStruct.ssAmount) return false;
			if (this.ssFlag != defaultStruct.ssFlag) return false;
			if (this.ssDay != defaultStruct.ssDay) return false;
			if (this.ssClock != defaultStruct.ssClock) return false;
			if (this.ssMoment != defaultStruct.ssMoment) return false;
			if (this.ssBlob != defaultStruct.ssBlob) return false;
			return true;
		}
	} // STAllTypesStructure

	/// <summary>
	/// Structure <code>STCustomerStructure</code> that represents the Service Studio structure
	///  <code>Customer</code> <p> Description: A customer; Code is written as an XML attribute (Attr_ prefix).</p>
	/// </summary>
	[Serializable()]
	public partial struct STCustomerStructure: ISerializable, ITypedRecord<STCustomerStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdAttr_Code = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*LrUULhMdeIoukx0Nx2jseg");
		internal static readonly GlobalObjectKey IdName = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*hUDYEzRn9F4gilt64xYtSA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Attr_Code")]
		public string ssAttr_Code;

		[System.Xml.Serialization.XmlElement("Name")]
		public string ssName;


		public BitArray OptimizedAttributes;

		public STCustomerStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssAttr_Code = "";
			ssName = "";
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssAttr_Code = r.ReadText(index++, "Customer.Attr_Code", "");
			ssName = r.ReadText(index++, "Customer.Name", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STCustomerStructure r) {
			this = r;
		}


		public static bool operator == (STCustomerStructure a, STCustomerStructure b) {
			if (a.ssAttr_Code != b.ssAttr_Code) return false;
			if (a.ssName != b.ssName) return false;
			return true;
		}

		public static bool operator != (STCustomerStructure a, STCustomerStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STCustomerStructure)) return false;
			return (this == (STCustomerStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssAttr_Code.GetHashCode()
				^ ssName.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STCustomerStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssAttr_Code = "";
			ssName = "";
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssAttr_Code", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssAttr_Code' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssAttr_Code = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssName' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssName = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STCustomerStructure Duplicate() {
			STCustomerStructure t;
			t.ssAttr_Code = this.ssAttr_Code;
			t.ssName = this.ssName;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Attr_Code")) VarValue.AppendAttribute(recordElem, "Attr_Code", ssAttr_Code, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Attr_Code");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Name")) VarValue.AppendAttribute(recordElem, "Name", ssName, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Name");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "attr_code") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Attr_Code")) variable.Value = ssAttr_Code; else variable.Optimized = true;
			} else if (head == "name") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Name")) variable.Value = ssName; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdAttr_Code) {
				return ssAttr_Code;
			} else if (key == IdName) {
				return ssName;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssAttr_Code = (string) other.AttributeGet(IdAttr_Code);
			ssName = (string) other.AttributeGet(IdName);
		}
		public bool IsDefault() {
			STCustomerStructure defaultStruct = new STCustomerStructure(null);
			if (this.ssAttr_Code != defaultStruct.ssAttr_Code) return false;
			if (this.ssName != defaultStruct.ssName) return false;
			return true;
		}
	} // STCustomerStructure

	/// <summary>
	/// Structure <code>STOrderLineStructure</code> that represents the Service Studio structure
	///  <code>OrderLine</code> <p> Description: An order line.</p>
	/// </summary>
	[Serializable()]
	public partial struct STOrderLineStructure: ISerializable, ITypedRecord<STOrderLineStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdProduct = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*FtYmNtLS5FVAUeVVtNVnRw");
		internal static readonly GlobalObjectKey IdQuantity = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*UCqWY6D0wZJSfCsw_e3PJQ");
		internal static readonly GlobalObjectKey IdPrice = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*vWGqI9xkXtyv79Lbfbec_g");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Product")]
		public string ssProduct;

		[System.Xml.Serialization.XmlElement("Quantity")]
		public int ssQuantity;

		[System.Xml.Serialization.XmlElement("Price")]
		public decimal ssPrice;


		public BitArray OptimizedAttributes;

		public STOrderLineStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssProduct = "";
			ssQuantity = 0;
			ssPrice = 0.0M;
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssProduct = r.ReadText(index++, "OrderLine.Product", "");
			ssQuantity = r.ReadInteger(index++, "OrderLine.Quantity", 0);
			ssPrice = r.ReadDecimal(index++, "OrderLine.Price", 0.0M);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STOrderLineStructure r) {
			this = r;
		}


		public static bool operator == (STOrderLineStructure a, STOrderLineStructure b) {
			if (a.ssProduct != b.ssProduct) return false;
			if (a.ssQuantity != b.ssQuantity) return false;
			if (a.ssPrice != b.ssPrice) return false;
			return true;
		}

		public static bool operator != (STOrderLineStructure a, STOrderLineStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STOrderLineStructure)) return false;
			return (this == (STOrderLineStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssProduct.GetHashCode()
				^ ssQuantity.GetHashCode()
				^ ssPrice.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STOrderLineStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssProduct = "";
			ssQuantity = 0;
			ssPrice = 0.0M;
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssProduct", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssProduct' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssProduct = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssQuantity", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssQuantity' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssQuantity = (int) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssPrice", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssPrice' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssPrice = (decimal) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STOrderLineStructure Duplicate() {
			STOrderLineStructure t;
			t.ssProduct = this.ssProduct;
			t.ssQuantity = this.ssQuantity;
			t.ssPrice = this.ssPrice;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Product")) VarValue.AppendAttribute(recordElem, "Product", ssProduct, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Product");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Quantity")) VarValue.AppendAttribute(recordElem, "Quantity", ssQuantity, detailLevel, TypeKind.Integer); else VarValue.AppendOptimizedAttribute(recordElem, "Quantity");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Price")) VarValue.AppendAttribute(recordElem, "Price", ssPrice, detailLevel, TypeKind.Decimal); else VarValue.AppendOptimizedAttribute(recordElem, "Price");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "product") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Product")) variable.Value = ssProduct; else variable.Optimized = true;
			} else if (head == "quantity") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Quantity")) variable.Value = ssQuantity; else variable.Optimized = true;
			} else if (head == "price") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Price")) variable.Value = ssPrice; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdProduct) {
				return ssProduct;
			} else if (key == IdQuantity) {
				return ssQuantity;
			} else if (key == IdPrice) {
				return ssPrice;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssProduct = (string) other.AttributeGet(IdProduct);
			ssQuantity = (int) other.AttributeGet(IdQuantity);
			ssPrice = (decimal) other.AttributeGet(IdPrice);
		}
		public bool IsDefault() {
			STOrderLineStructure defaultStruct = new STOrderLineStructure(null);
			if (this.ssProduct != defaultStruct.ssProduct) return false;
			if (this.ssQuantity != defaultStruct.ssQuantity) return false;
			if (this.ssPrice != defaultStruct.ssPrice) return false;
			return true;
		}
	} // STOrderLineStructure

	/// <summary>
	/// Structure <code>STOrderStructure</code> that represents the Service Studio structure
	///  <code>Order</code> <p> Description: An order with a nested record and a nested record list; Id is an XML attribute.</p>
	/// </summary>
	[Serializable()]
	public partial struct STOrderStructure: ISerializable, ITypedRecord<STOrderStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdAttr_Id = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*Lj2ZaaxpaFIKa24H7DNCHg");
		internal static readonly GlobalObjectKey IdNumber = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*hA0r_3u4qNxDkURAOK69eg");
		internal static readonly GlobalObjectKey IdCustomer = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*X4BiCLgyR2n8HB2BeVt6hQ");
		internal static readonly GlobalObjectKey IdLines = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*NZJHYKM6+bY1cwTL3q6MKw");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Attr_Id")]
		public long ssAttr_Id;

		[System.Xml.Serialization.XmlElement("Number")]
		public string ssNumber;

		[System.Xml.Serialization.XmlElement("Customer")]
		public RCCustomerRecord ssCustomer;

		[System.Xml.Serialization.XmlElement("Lines")]
		public RLOrderLineRecordList ssLines;


		public BitArray OptimizedAttributes;

		public STOrderStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssAttr_Id = 0L;
			ssNumber = "";
			ssCustomer = new RCCustomerRecord(null);
			ssLines = new RLOrderLineRecordList();
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssCustomer.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssAttr_Id = r.ReadLongInteger(index++, "Order.Attr_Id", 0L);
			ssNumber = r.ReadText(index++, "Order.Number", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STOrderStructure r) {
			this = r;
		}


		public static bool operator == (STOrderStructure a, STOrderStructure b) {
			if (a.ssAttr_Id != b.ssAttr_Id) return false;
			if (a.ssNumber != b.ssNumber) return false;
			if (a.ssCustomer != b.ssCustomer) return false;
			if (a.ssLines != b.ssLines) return false;
			return true;
		}

		public static bool operator != (STOrderStructure a, STOrderStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STOrderStructure)) return false;
			return (this == (STOrderStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssAttr_Id.GetHashCode()
				^ ssNumber.GetHashCode()
				^ ssCustomer.GetHashCode()
				^ ssLines.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STOrderStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssAttr_Id = 0L;
			ssNumber = "";
			ssCustomer = new RCCustomerRecord(null);
			ssLines = new RLOrderLineRecordList();
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssAttr_Id", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssAttr_Id' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssAttr_Id = (long) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssNumber", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssNumber' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssNumber = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssCustomer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssCustomer' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssCustomer = (RCCustomerRecord) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssLines", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssLines' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssLines = (RLOrderLineRecordList) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssCustomer.RecursiveReset();
			ssLines.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssCustomer.InternalRecursiveSave();
			ssLines.InternalRecursiveSave();
		}


		public STOrderStructure Duplicate() {
			STOrderStructure t;
			t.ssAttr_Id = this.ssAttr_Id;
			t.ssNumber = this.ssNumber;
			t.ssCustomer = (RCCustomerRecord) this.ssCustomer.Duplicate();
			t.ssLines = (RLOrderLineRecordList) this.ssLines.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Attr_Id")) VarValue.AppendAttribute(recordElem, "Attr_Id", ssAttr_Id, detailLevel, TypeKind.LongInteger); else VarValue.AppendOptimizedAttribute(recordElem, "Attr_Id");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Number")) VarValue.AppendAttribute(recordElem, "Number", ssNumber, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Number");
				ssCustomer.ToXml(this, recordElem, "Customer", detailLevel - 1);
				ssLines.ToXml(this, recordElem, "Lines", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "attr_id") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Attr_Id")) variable.Value = ssAttr_Id; else variable.Optimized = true;
			} else if (head == "number") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Number")) variable.Value = ssNumber; else variable.Optimized = true;
			} else if (head == "customer") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Customer")) variable.Value = ssCustomer; else variable.Optimized = true;
				variable.SetFieldName("customer");
			} else if (head == "lines") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Lines")) variable.Value = ssLines; else variable.Optimized = true;
				variable.SetFieldName("lines");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdAttr_Id) {
				return ssAttr_Id;
			} else if (key == IdNumber) {
				return ssNumber;
			} else if (key == IdCustomer) {
				return ssCustomer;
			} else if (key == IdLines) {
				return ssLines;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssAttr_Id = (long) other.AttributeGet(IdAttr_Id);
			ssNumber = (string) other.AttributeGet(IdNumber);
			ssCustomer.FillFromOther((IRecord) other.AttributeGet(IdCustomer));
			ssLines = new RLOrderLineRecordList();
			ssLines.FillFromOther((IOSList) other.AttributeGet(IdLines));
		}
		public bool IsDefault() {
			STOrderStructure defaultStruct = new STOrderStructure(null);
			if (this.ssAttr_Id != defaultStruct.ssAttr_Id) return false;
			if (this.ssNumber != defaultStruct.ssNumber) return false;
			if (this.ssCustomer != defaultStruct.ssCustomer) return false;
			if (this.ssLines != null && this.ssLines.Length != 0) return false;
			return true;
		}
	} // STOrderStructure

	/// <summary>
	/// Structure <code>STNoteStructure</code> that represents the Service Studio structure
	///  <code>Note</code> <p> Description: An element with an attribute (Attr_ prefix) and text content (Cont_ prefix).</p>
	/// </summary>
	[Serializable()]
	public partial struct STNoteStructure: ISerializable, ITypedRecord<STNoteStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdAttr_Lang = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*XCnxZDBOI9+K_0szZQB4vw");
		internal static readonly GlobalObjectKey IdCont_Text = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*XztAZNHu5bEoxJM+8IYszg");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Attr_Lang")]
		public string ssAttr_Lang;

		[System.Xml.Serialization.XmlElement("Cont_Text")]
		public string ssCont_Text;


		public BitArray OptimizedAttributes;

		public STNoteStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssAttr_Lang = "";
			ssCont_Text = "";
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssAttr_Lang = r.ReadText(index++, "Note.Attr_Lang", "");
			ssCont_Text = r.ReadText(index++, "Note.Cont_Text", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STNoteStructure r) {
			this = r;
		}


		public static bool operator == (STNoteStructure a, STNoteStructure b) {
			if (a.ssAttr_Lang != b.ssAttr_Lang) return false;
			if (a.ssCont_Text != b.ssCont_Text) return false;
			return true;
		}

		public static bool operator != (STNoteStructure a, STNoteStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STNoteStructure)) return false;
			return (this == (STNoteStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssAttr_Lang.GetHashCode()
				^ ssCont_Text.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STNoteStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssAttr_Lang = "";
			ssCont_Text = "";
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssAttr_Lang", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssAttr_Lang' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssAttr_Lang = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssCont_Text", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssCont_Text' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssCont_Text = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STNoteStructure Duplicate() {
			STNoteStructure t;
			t.ssAttr_Lang = this.ssAttr_Lang;
			t.ssCont_Text = this.ssCont_Text;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Attr_Lang")) VarValue.AppendAttribute(recordElem, "Attr_Lang", ssAttr_Lang, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Attr_Lang");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Cont_Text")) VarValue.AppendAttribute(recordElem, "Cont_Text", ssCont_Text, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Cont_Text");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "attr_lang") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Attr_Lang")) variable.Value = ssAttr_Lang; else variable.Optimized = true;
			} else if (head == "cont_text") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Cont_Text")) variable.Value = ssCont_Text; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdAttr_Lang) {
				return ssAttr_Lang;
			} else if (key == IdCont_Text) {
				return ssCont_Text;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssAttr_Lang = (string) other.AttributeGet(IdAttr_Lang);
			ssCont_Text = (string) other.AttributeGet(IdCont_Text);
		}
		public bool IsDefault() {
			STNoteStructure defaultStruct = new STNoteStructure(null);
			if (this.ssAttr_Lang != defaultStruct.ssAttr_Lang) return false;
			if (this.ssCont_Text != defaultStruct.ssCont_Text) return false;
			return true;
		}
	} // STNoteStructure

	/// <summary>
	/// Structure <code>STLevel3Structure</code> that represents the Service Studio structure
	///  <code>Level3</code> <p> Description: Innermost of three nesting levels.</p>
	/// </summary>
	[Serializable()]
	public partial struct STLevel3Structure: ISerializable, ITypedRecord<STLevel3Structure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdName = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*3LYo4qrWRFpNoIYCrYzuTA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Name")]
		public string ssName;


		public BitArray OptimizedAttributes;

		public STLevel3Structure(params string[] dummy) {
			OptimizedAttributes = null;
			ssName = "";
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssName = r.ReadText(index++, "Level3.Name", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STLevel3Structure r) {
			this = r;
		}


		public static bool operator == (STLevel3Structure a, STLevel3Structure b) {
			if (a.ssName != b.ssName) return false;
			return true;
		}

		public static bool operator != (STLevel3Structure a, STLevel3Structure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STLevel3Structure)) return false;
			return (this == (STLevel3Structure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssName.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STLevel3Structure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssName = "";
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssName' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssName = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STLevel3Structure Duplicate() {
			STLevel3Structure t;
			t.ssName = this.ssName;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Name")) VarValue.AppendAttribute(recordElem, "Name", ssName, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Name");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "name") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Name")) variable.Value = ssName; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdName) {
				return ssName;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssName = (string) other.AttributeGet(IdName);
		}
		public bool IsDefault() {
			STLevel3Structure defaultStruct = new STLevel3Structure(null);
			if (this.ssName != defaultStruct.ssName) return false;
			return true;
		}
	} // STLevel3Structure

	/// <summary>
	/// Structure <code>STLevel2Structure</code> that represents the Service Studio structure
	///  <code>Level2</code> <p> Description: Middle of three nesting levels.</p>
	/// </summary>
	[Serializable()]
	public partial struct STLevel2Structure: ISerializable, ITypedRecord<STLevel2Structure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdName = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*I5EHDiCRonq98SZkLBi7fA");
		internal static readonly GlobalObjectKey IdChild = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*s8Be3B1bkv1lGZZWl8knFg");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Name")]
		public string ssName;

		[System.Xml.Serialization.XmlElement("Child")]
		public RCLevel3Record ssChild;


		public BitArray OptimizedAttributes;

		public STLevel2Structure(params string[] dummy) {
			OptimizedAttributes = null;
			ssName = "";
			ssChild = new RCLevel3Record(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssChild.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssName = r.ReadText(index++, "Level2.Name", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STLevel2Structure r) {
			this = r;
		}


		public static bool operator == (STLevel2Structure a, STLevel2Structure b) {
			if (a.ssName != b.ssName) return false;
			if (a.ssChild != b.ssChild) return false;
			return true;
		}

		public static bool operator != (STLevel2Structure a, STLevel2Structure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STLevel2Structure)) return false;
			return (this == (STLevel2Structure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssName.GetHashCode()
				^ ssChild.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STLevel2Structure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssName = "";
			ssChild = new RCLevel3Record(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssName' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssName = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssChild", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssChild' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssChild = (RCLevel3Record) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssChild.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssChild.InternalRecursiveSave();
		}


		public STLevel2Structure Duplicate() {
			STLevel2Structure t;
			t.ssName = this.ssName;
			t.ssChild = (RCLevel3Record) this.ssChild.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Name")) VarValue.AppendAttribute(recordElem, "Name", ssName, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Name");
				ssChild.ToXml(this, recordElem, "Child", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "name") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Name")) variable.Value = ssName; else variable.Optimized = true;
			} else if (head == "child") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Child")) variable.Value = ssChild; else variable.Optimized = true;
				variable.SetFieldName("child");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdName) {
				return ssName;
			} else if (key == IdChild) {
				return ssChild;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssName = (string) other.AttributeGet(IdName);
			ssChild.FillFromOther((IRecord) other.AttributeGet(IdChild));
		}
		public bool IsDefault() {
			STLevel2Structure defaultStruct = new STLevel2Structure(null);
			if (this.ssName != defaultStruct.ssName) return false;
			if (this.ssChild != defaultStruct.ssChild) return false;
			return true;
		}
	} // STLevel2Structure

	/// <summary>
	/// Structure <code>STLevel1Structure</code> that represents the Service Studio structure
	///  <code>Level1</code> <p> Description: Outermost of three nesting levels.</p>
	/// </summary>
	[Serializable()]
	public partial struct STLevel1Structure: ISerializable, ITypedRecord<STLevel1Structure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdName = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*D8Mnwzbt6PFmzDgSgc6ZpQ");
		internal static readonly GlobalObjectKey IdChild = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*daEZZ2R++5CAscTl2XfquA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Name")]
		public string ssName;

		[System.Xml.Serialization.XmlElement("Child")]
		public RCLevel2Record ssChild;


		public BitArray OptimizedAttributes;

		public STLevel1Structure(params string[] dummy) {
			OptimizedAttributes = null;
			ssName = "";
			ssChild = new RCLevel2Record(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssChild.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssName = r.ReadText(index++, "Level1.Name", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(STLevel1Structure r) {
			this = r;
		}


		public static bool operator == (STLevel1Structure a, STLevel1Structure b) {
			if (a.ssName != b.ssName) return false;
			if (a.ssChild != b.ssChild) return false;
			return true;
		}

		public static bool operator != (STLevel1Structure a, STLevel1Structure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STLevel1Structure)) return false;
			return (this == (STLevel1Structure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssName.GetHashCode()
				^ ssChild.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public STLevel1Structure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssName = "";
			ssChild = new RCLevel2Record(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssName' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssName = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssChild", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssChild' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssChild = (RCLevel2Record) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssChild.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssChild.InternalRecursiveSave();
		}


		public STLevel1Structure Duplicate() {
			STLevel1Structure t;
			t.ssName = this.ssName;
			t.ssChild = (RCLevel2Record) this.ssChild.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Name")) VarValue.AppendAttribute(recordElem, "Name", ssName, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Name");
				ssChild.ToXml(this, recordElem, "Child", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "name") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Name")) variable.Value = ssName; else variable.Optimized = true;
			} else if (head == "child") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Child")) variable.Value = ssChild; else variable.Optimized = true;
				variable.SetFieldName("child");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdName) {
				return ssName;
			} else if (key == IdChild) {
				return ssChild;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssName = (string) other.AttributeGet(IdName);
			ssChild.FillFromOther((IRecord) other.AttributeGet(IdChild));
		}
		public bool IsDefault() {
			STLevel1Structure defaultStruct = new STLevel1Structure(null);
			if (this.ssName != defaultStruct.ssName) return false;
			if (this.ssChild != defaultStruct.ssChild) return false;
			return true;
		}
	} // STLevel1Structure

	/// <summary>
	/// Entity <code>ENProductEntity</code> that represents the Service Studio entity
	///  <code>Product</code> <p> Description: An entity (EN prefix in the generated code).</p>
	/// </summary>
	[Serializable()]
	public partial struct ENProductEntity: ISerializable, ITypedRecord<ENProductEntity>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdId = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*dCLZs4u+cXX0Cfrj+n5RNw");
		internal static readonly GlobalObjectKey IdName = GlobalObjectKey.Parse("lrHFp2zdWe3cgPrRVvVkyw*HA6VInmoiX6uss1yjHPR0Q");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Id")]
		public long ssId;

		[System.Xml.Serialization.XmlElement("Name")]
		public string ssName;


		public BitArray OptimizedAttributes;

		public ENProductEntity(params string[] dummy) {
			OptimizedAttributes = null;
			ssId = 0L;
			ssName = "";
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssId = r.ReadLongInteger(index++, "Product.Id", 0L);
			ssName = r.ReadText(index++, "Product.Name", "");
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(ENProductEntity r) {
			this = r;
		}


		public static bool operator == (ENProductEntity a, ENProductEntity b) {
			if (a.ssId != b.ssId) return false;
			if (a.ssName != b.ssName) return false;
			return true;
		}

		public static bool operator != (ENProductEntity a, ENProductEntity b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(ENProductEntity)) return false;
			return (this == (ENProductEntity) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssId.GetHashCode()
				^ ssName.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public ENProductEntity(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssId = 0L;
			ssName = "";
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssId", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssId' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssId = (long) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssName' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssName = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public ENProductEntity Duplicate() {
			ENProductEntity t;
			t.ssId = this.ssId;
			t.ssName = this.ssName;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Entity");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Id")) VarValue.AppendAttribute(recordElem, "Id", ssId, detailLevel, TypeKind.LongInteger); else VarValue.AppendOptimizedAttribute(recordElem, "Id");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".Name")) VarValue.AppendAttribute(recordElem, "Name", ssName, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "Name");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "id") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Id")) variable.Value = ssId; else variable.Optimized = true;
			} else if (head == "name") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Name")) variable.Value = ssName; else variable.Optimized = true;
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdId) {
				return ssId;
			} else if (key == IdName) {
				return ssName;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssId = (long) other.AttributeGet(IdId);
			ssName = (string) other.AttributeGet(IdName);
		}
		public bool IsDefault() {
			ENProductEntity defaultStruct = new ENProductEntity(null);
			if (this.ssId != defaultStruct.ssId) return false;
			if (this.ssName != defaultStruct.ssName) return false;
			return true;
		}
	} // ENProductEntity

	/// <summary>
	/// Structure <code>RCAllTypesRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCAllTypesRecord: ISerializable, ITypedRecord<RCAllTypesRecord> {
		internal static readonly GlobalObjectKey IdAllTypes = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*53MHHo_H3eBsIjPo+sFM3Q");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("AllTypes")]
		public STAllTypesStructure ssSTAllTypes;


		public static implicit operator STAllTypesStructure(RCAllTypesRecord r) {
			return r.ssSTAllTypes;
		}

		public static implicit operator RCAllTypesRecord(STAllTypesStructure r) {
			RCAllTypesRecord res = new RCAllTypesRecord(null);
			res.ssSTAllTypes = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCAllTypesRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTAllTypes = new STAllTypesStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTAllTypes.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTAllTypes.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCAllTypesRecord r) {
			this = r;
		}


		public static bool operator == (RCAllTypesRecord a, RCAllTypesRecord b) {
			if (a.ssSTAllTypes != b.ssSTAllTypes) return false;
			return true;
		}

		public static bool operator != (RCAllTypesRecord a, RCAllTypesRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCAllTypesRecord)) return false;
			return (this == (RCAllTypesRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTAllTypes.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCAllTypesRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTAllTypes = new STAllTypesStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTAllTypes", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTAllTypes' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTAllTypes = (STAllTypesStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTAllTypes.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTAllTypes.InternalRecursiveSave();
		}


		public RCAllTypesRecord Duplicate() {
			RCAllTypesRecord t;
			t.ssSTAllTypes = (STAllTypesStructure) this.ssSTAllTypes.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTAllTypes.ToXml(this, recordElem, "AllTypes", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "alltypes") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".AllTypes")) variable.Value = ssSTAllTypes; else variable.Optimized = true;
				variable.SetFieldName("alltypes");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdAllTypes) {
				return ssSTAllTypes;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTAllTypes.FillFromOther((IRecord) other.AttributeGet(IdAllTypes));
		}
		public bool IsDefault() {
			RCAllTypesRecord defaultStruct = new RCAllTypesRecord(null);
			if (this.ssSTAllTypes != defaultStruct.ssSTAllTypes) return false;
			return true;
		}
	} // RCAllTypesRecord

	/// <summary>
	/// Structure <code>RCCustomerRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCCustomerRecord: ISerializable, ITypedRecord<RCCustomerRecord> {
		internal static readonly GlobalObjectKey IdCustomer = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*RRrIq0daUh4W5UPhEtHLRA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Customer")]
		public STCustomerStructure ssSTCustomer;


		public static implicit operator STCustomerStructure(RCCustomerRecord r) {
			return r.ssSTCustomer;
		}

		public static implicit operator RCCustomerRecord(STCustomerStructure r) {
			RCCustomerRecord res = new RCCustomerRecord(null);
			res.ssSTCustomer = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCCustomerRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTCustomer = new STCustomerStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTCustomer.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTCustomer.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCCustomerRecord r) {
			this = r;
		}


		public static bool operator == (RCCustomerRecord a, RCCustomerRecord b) {
			if (a.ssSTCustomer != b.ssSTCustomer) return false;
			return true;
		}

		public static bool operator != (RCCustomerRecord a, RCCustomerRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCCustomerRecord)) return false;
			return (this == (RCCustomerRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTCustomer.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCCustomerRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTCustomer = new STCustomerStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTCustomer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTCustomer' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTCustomer = (STCustomerStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTCustomer.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTCustomer.InternalRecursiveSave();
		}


		public RCCustomerRecord Duplicate() {
			RCCustomerRecord t;
			t.ssSTCustomer = (STCustomerStructure) this.ssSTCustomer.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTCustomer.ToXml(this, recordElem, "Customer", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "customer") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Customer")) variable.Value = ssSTCustomer; else variable.Optimized = true;
				variable.SetFieldName("customer");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdCustomer) {
				return ssSTCustomer;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTCustomer.FillFromOther((IRecord) other.AttributeGet(IdCustomer));
		}
		public bool IsDefault() {
			RCCustomerRecord defaultStruct = new RCCustomerRecord(null);
			if (this.ssSTCustomer != defaultStruct.ssSTCustomer) return false;
			return true;
		}
	} // RCCustomerRecord

	/// <summary>
	/// Structure <code>RCOrderLineRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCOrderLineRecord: ISerializable, ITypedRecord<RCOrderLineRecord> {
		internal static readonly GlobalObjectKey IdOrderLine = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*Y72qu_v0gRenfEGTOaMePQ");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("OrderLine")]
		public STOrderLineStructure ssSTOrderLine;


		public static implicit operator STOrderLineStructure(RCOrderLineRecord r) {
			return r.ssSTOrderLine;
		}

		public static implicit operator RCOrderLineRecord(STOrderLineStructure r) {
			RCOrderLineRecord res = new RCOrderLineRecord(null);
			res.ssSTOrderLine = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCOrderLineRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTOrderLine = new STOrderLineStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTOrderLine.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTOrderLine.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCOrderLineRecord r) {
			this = r;
		}


		public static bool operator == (RCOrderLineRecord a, RCOrderLineRecord b) {
			if (a.ssSTOrderLine != b.ssSTOrderLine) return false;
			return true;
		}

		public static bool operator != (RCOrderLineRecord a, RCOrderLineRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCOrderLineRecord)) return false;
			return (this == (RCOrderLineRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTOrderLine.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCOrderLineRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTOrderLine = new STOrderLineStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTOrderLine", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTOrderLine' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTOrderLine = (STOrderLineStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTOrderLine.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTOrderLine.InternalRecursiveSave();
		}


		public RCOrderLineRecord Duplicate() {
			RCOrderLineRecord t;
			t.ssSTOrderLine = (STOrderLineStructure) this.ssSTOrderLine.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTOrderLine.ToXml(this, recordElem, "OrderLine", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "orderline") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".OrderLine")) variable.Value = ssSTOrderLine; else variable.Optimized = true;
				variable.SetFieldName("orderline");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdOrderLine) {
				return ssSTOrderLine;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTOrderLine.FillFromOther((IRecord) other.AttributeGet(IdOrderLine));
		}
		public bool IsDefault() {
			RCOrderLineRecord defaultStruct = new RCOrderLineRecord(null);
			if (this.ssSTOrderLine != defaultStruct.ssSTOrderLine) return false;
			return true;
		}
	} // RCOrderLineRecord

	/// <summary>
	/// Structure <code>RCOrderRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCOrderRecord: ISerializable, ITypedRecord<RCOrderRecord> {
		internal static readonly GlobalObjectKey IdOrder = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*SgDm9gPRnAPCGf3TIIqHVg");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Order")]
		public STOrderStructure ssSTOrder;


		public static implicit operator STOrderStructure(RCOrderRecord r) {
			return r.ssSTOrder;
		}

		public static implicit operator RCOrderRecord(STOrderStructure r) {
			RCOrderRecord res = new RCOrderRecord(null);
			res.ssSTOrder = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCOrderRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTOrder = new STOrderStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTOrder.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTOrder.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCOrderRecord r) {
			this = r;
		}


		public static bool operator == (RCOrderRecord a, RCOrderRecord b) {
			if (a.ssSTOrder != b.ssSTOrder) return false;
			return true;
		}

		public static bool operator != (RCOrderRecord a, RCOrderRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCOrderRecord)) return false;
			return (this == (RCOrderRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTOrder.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCOrderRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTOrder = new STOrderStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTOrder", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTOrder' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTOrder = (STOrderStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTOrder.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTOrder.InternalRecursiveSave();
		}


		public RCOrderRecord Duplicate() {
			RCOrderRecord t;
			t.ssSTOrder = (STOrderStructure) this.ssSTOrder.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTOrder.ToXml(this, recordElem, "Order", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "order") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Order")) variable.Value = ssSTOrder; else variable.Optimized = true;
				variable.SetFieldName("order");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdOrder) {
				return ssSTOrder;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTOrder.FillFromOther((IRecord) other.AttributeGet(IdOrder));
		}
		public bool IsDefault() {
			RCOrderRecord defaultStruct = new RCOrderRecord(null);
			if (this.ssSTOrder != defaultStruct.ssSTOrder) return false;
			return true;
		}
	} // RCOrderRecord

	/// <summary>
	/// Structure <code>RCNoteRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCNoteRecord: ISerializable, ITypedRecord<RCNoteRecord> {
		internal static readonly GlobalObjectKey IdNote = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*uSrewpvqaZbaAG73c0d0Kg");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Note")]
		public STNoteStructure ssSTNote;


		public static implicit operator STNoteStructure(RCNoteRecord r) {
			return r.ssSTNote;
		}

		public static implicit operator RCNoteRecord(STNoteStructure r) {
			RCNoteRecord res = new RCNoteRecord(null);
			res.ssSTNote = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCNoteRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTNote = new STNoteStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTNote.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTNote.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCNoteRecord r) {
			this = r;
		}


		public static bool operator == (RCNoteRecord a, RCNoteRecord b) {
			if (a.ssSTNote != b.ssSTNote) return false;
			return true;
		}

		public static bool operator != (RCNoteRecord a, RCNoteRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCNoteRecord)) return false;
			return (this == (RCNoteRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTNote.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCNoteRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTNote = new STNoteStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTNote", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTNote' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTNote = (STNoteStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTNote.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTNote.InternalRecursiveSave();
		}


		public RCNoteRecord Duplicate() {
			RCNoteRecord t;
			t.ssSTNote = (STNoteStructure) this.ssSTNote.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTNote.ToXml(this, recordElem, "Note", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "note") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Note")) variable.Value = ssSTNote; else variable.Optimized = true;
				variable.SetFieldName("note");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdNote) {
				return ssSTNote;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTNote.FillFromOther((IRecord) other.AttributeGet(IdNote));
		}
		public bool IsDefault() {
			RCNoteRecord defaultStruct = new RCNoteRecord(null);
			if (this.ssSTNote != defaultStruct.ssSTNote) return false;
			return true;
		}
	} // RCNoteRecord

	/// <summary>
	/// Structure <code>RCLevel3Record</code>
	/// </summary>
	[Serializable()]
	public partial struct RCLevel3Record: ISerializable, ITypedRecord<RCLevel3Record> {
		internal static readonly GlobalObjectKey IdLevel3 = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*GvSyIBeeIR8TLkY1hhtALQ");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Level3")]
		public STLevel3Structure ssSTLevel3;


		public static implicit operator STLevel3Structure(RCLevel3Record r) {
			return r.ssSTLevel3;
		}

		public static implicit operator RCLevel3Record(STLevel3Structure r) {
			RCLevel3Record res = new RCLevel3Record(null);
			res.ssSTLevel3 = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCLevel3Record(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTLevel3 = new STLevel3Structure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTLevel3.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTLevel3.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCLevel3Record r) {
			this = r;
		}


		public static bool operator == (RCLevel3Record a, RCLevel3Record b) {
			if (a.ssSTLevel3 != b.ssSTLevel3) return false;
			return true;
		}

		public static bool operator != (RCLevel3Record a, RCLevel3Record b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCLevel3Record)) return false;
			return (this == (RCLevel3Record) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTLevel3.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCLevel3Record(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTLevel3 = new STLevel3Structure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTLevel3", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTLevel3' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTLevel3 = (STLevel3Structure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTLevel3.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTLevel3.InternalRecursiveSave();
		}


		public RCLevel3Record Duplicate() {
			RCLevel3Record t;
			t.ssSTLevel3 = (STLevel3Structure) this.ssSTLevel3.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTLevel3.ToXml(this, recordElem, "Level3", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "level3") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Level3")) variable.Value = ssSTLevel3; else variable.Optimized = true;
				variable.SetFieldName("level3");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdLevel3) {
				return ssSTLevel3;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTLevel3.FillFromOther((IRecord) other.AttributeGet(IdLevel3));
		}
		public bool IsDefault() {
			RCLevel3Record defaultStruct = new RCLevel3Record(null);
			if (this.ssSTLevel3 != defaultStruct.ssSTLevel3) return false;
			return true;
		}
	} // RCLevel3Record

	/// <summary>
	/// Structure <code>RCLevel2Record</code>
	/// </summary>
	[Serializable()]
	public partial struct RCLevel2Record: ISerializable, ITypedRecord<RCLevel2Record> {
		internal static readonly GlobalObjectKey IdLevel2 = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*Ac5KrM+DZiIWG5Xg8vRRcQ");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Level2")]
		public STLevel2Structure ssSTLevel2;


		public static implicit operator STLevel2Structure(RCLevel2Record r) {
			return r.ssSTLevel2;
		}

		public static implicit operator RCLevel2Record(STLevel2Structure r) {
			RCLevel2Record res = new RCLevel2Record(null);
			res.ssSTLevel2 = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCLevel2Record(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTLevel2 = new STLevel2Structure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTLevel2.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTLevel2.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCLevel2Record r) {
			this = r;
		}


		public static bool operator == (RCLevel2Record a, RCLevel2Record b) {
			if (a.ssSTLevel2 != b.ssSTLevel2) return false;
			return true;
		}

		public static bool operator != (RCLevel2Record a, RCLevel2Record b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCLevel2Record)) return false;
			return (this == (RCLevel2Record) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTLevel2.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCLevel2Record(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTLevel2 = new STLevel2Structure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTLevel2", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTLevel2' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTLevel2 = (STLevel2Structure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTLevel2.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTLevel2.InternalRecursiveSave();
		}


		public RCLevel2Record Duplicate() {
			RCLevel2Record t;
			t.ssSTLevel2 = (STLevel2Structure) this.ssSTLevel2.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTLevel2.ToXml(this, recordElem, "Level2", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "level2") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Level2")) variable.Value = ssSTLevel2; else variable.Optimized = true;
				variable.SetFieldName("level2");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdLevel2) {
				return ssSTLevel2;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTLevel2.FillFromOther((IRecord) other.AttributeGet(IdLevel2));
		}
		public bool IsDefault() {
			RCLevel2Record defaultStruct = new RCLevel2Record(null);
			if (this.ssSTLevel2 != defaultStruct.ssSTLevel2) return false;
			return true;
		}
	} // RCLevel2Record

	/// <summary>
	/// Structure <code>RCLevel1Record</code>
	/// </summary>
	[Serializable()]
	public partial struct RCLevel1Record: ISerializable, ITypedRecord<RCLevel1Record> {
		internal static readonly GlobalObjectKey IdLevel1 = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*l8_wuGkBl3J7U6DNY96POw");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Level1")]
		public STLevel1Structure ssSTLevel1;


		public static implicit operator STLevel1Structure(RCLevel1Record r) {
			return r.ssSTLevel1;
		}

		public static implicit operator RCLevel1Record(STLevel1Structure r) {
			RCLevel1Record res = new RCLevel1Record(null);
			res.ssSTLevel1 = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCLevel1Record(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTLevel1 = new STLevel1Structure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTLevel1.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTLevel1.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCLevel1Record r) {
			this = r;
		}


		public static bool operator == (RCLevel1Record a, RCLevel1Record b) {
			if (a.ssSTLevel1 != b.ssSTLevel1) return false;
			return true;
		}

		public static bool operator != (RCLevel1Record a, RCLevel1Record b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCLevel1Record)) return false;
			return (this == (RCLevel1Record) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTLevel1.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCLevel1Record(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTLevel1 = new STLevel1Structure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTLevel1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTLevel1' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTLevel1 = (STLevel1Structure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTLevel1.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTLevel1.InternalRecursiveSave();
		}


		public RCLevel1Record Duplicate() {
			RCLevel1Record t;
			t.ssSTLevel1 = (STLevel1Structure) this.ssSTLevel1.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTLevel1.ToXml(this, recordElem, "Level1", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "level1") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Level1")) variable.Value = ssSTLevel1; else variable.Optimized = true;
				variable.SetFieldName("level1");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdLevel1) {
				return ssSTLevel1;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTLevel1.FillFromOther((IRecord) other.AttributeGet(IdLevel1));
		}
		public bool IsDefault() {
			RCLevel1Record defaultStruct = new RCLevel1Record(null);
			if (this.ssSTLevel1 != defaultStruct.ssSTLevel1) return false;
			return true;
		}
	} // RCLevel1Record

	/// <summary>
	/// Structure <code>RCProductRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCProductRecord: ISerializable, ITypedRecord<RCProductRecord> {
		internal static readonly GlobalObjectKey IdProduct = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*CsVHiEYGoVPxmk56oZzfQg");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Product")]
		public ENProductEntity ssENProduct;


		public static implicit operator ENProductEntity(RCProductRecord r) {
			return r.ssENProduct;
		}

		public static implicit operator RCProductRecord(ENProductEntity r) {
			RCProductRecord res = new RCProductRecord(null);
			res.ssENProduct = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCProductRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssENProduct = new ENProductEntity(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssENProduct.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssENProduct.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCProductRecord r) {
			this = r;
		}


		public static bool operator == (RCProductRecord a, RCProductRecord b) {
			if (a.ssENProduct != b.ssENProduct) return false;
			return true;
		}

		public static bool operator != (RCProductRecord a, RCProductRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCProductRecord)) return false;
			return (this == (RCProductRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssENProduct.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCProductRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssENProduct = new ENProductEntity(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssENProduct", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssENProduct' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssENProduct = (ENProductEntity) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssENProduct.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssENProduct.InternalRecursiveSave();
		}


		public RCProductRecord Duplicate() {
			RCProductRecord t;
			t.ssENProduct = (ENProductEntity) this.ssENProduct.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssENProduct.ToXml(this, recordElem, "Product", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "product") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Product")) variable.Value = ssENProduct; else variable.Optimized = true;
				variable.SetFieldName("product");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdProduct) {
				return ssENProduct;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssENProduct.FillFromOther((IRecord) other.AttributeGet(IdProduct));
		}
		public bool IsDefault() {
			RCProductRecord defaultStruct = new RCProductRecord(null);
			if (this.ssENProduct != defaultStruct.ssENProduct) return false;
			return true;
		}
	} // RCProductRecord

	/// <summary>
	/// Structure <code>RCOrderCustomerRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCOrderCustomerRecord: ISerializable, ITypedRecord<RCOrderCustomerRecord> {
		internal static readonly GlobalObjectKey IdOrder = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*2vML6YuTj7dlwg4UZOLQow");
		internal static readonly GlobalObjectKey IdCustomer = GlobalObjectKey.Parse("ZeUn+SVFyflTkf74Qg0FUw*idcOTHMQH+OzlyM1igUfcw");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Order")]
		public STOrderStructure ssSTOrder;

		[System.Xml.Serialization.XmlElement("Customer")]
		public STCustomerStructure ssSTCustomer;


		public BitArray OptimizedAttributes;

		public RCOrderCustomerRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTOrder = new STOrderStructure(null);
			ssSTCustomer = new STCustomerStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[2];
			all[0] = null;
			all[1] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTOrder.OptimizedAttributes = value[0];
					ssSTCustomer.OptimizedAttributes = value[1];
				}
			}
			get {
				BitArray[] all = new BitArray[2];
				all[0] = null;
				all[1] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTOrder.Read(r, ref index);
			ssSTCustomer.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCOrderCustomerRecord r) {
			this = r;
		}


		public static bool operator == (RCOrderCustomerRecord a, RCOrderCustomerRecord b) {
			if (a.ssSTOrder != b.ssSTOrder) return false;
			if (a.ssSTCustomer != b.ssSTCustomer) return false;
			return true;
		}

		public static bool operator != (RCOrderCustomerRecord a, RCOrderCustomerRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCOrderCustomerRecord)) return false;
			return (this == (RCOrderCustomerRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTOrder.GetHashCode()
				^ ssSTCustomer.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCOrderCustomerRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTOrder = new STOrderStructure(null);
			ssSTCustomer = new STCustomerStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTOrder", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTOrder' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTOrder = (STOrderStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssSTCustomer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTCustomer' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTCustomer = (STCustomerStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTOrder.RecursiveReset();
			ssSTCustomer.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTOrder.InternalRecursiveSave();
			ssSTCustomer.InternalRecursiveSave();
		}


		public RCOrderCustomerRecord Duplicate() {
			RCOrderCustomerRecord t;
			t.ssSTOrder = (STOrderStructure) this.ssSTOrder.Duplicate();
			t.ssSTCustomer = (STCustomerStructure) this.ssSTCustomer.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTOrder.ToXml(this, recordElem, "Order", detailLevel - 1);
				ssSTCustomer.ToXml(this, recordElem, "Customer", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "order") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Order")) variable.Value = ssSTOrder; else variable.Optimized = true;
				variable.SetFieldName("order");
			} else if (head == "customer") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Customer")) variable.Value = ssSTCustomer; else variable.Optimized = true;
				variable.SetFieldName("customer");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdOrder) {
				return ssSTOrder;
			} else if (key == IdCustomer) {
				return ssSTCustomer;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTOrder.FillFromOther((IRecord) other.AttributeGet(IdOrder));
			ssSTCustomer.FillFromOther((IRecord) other.AttributeGet(IdCustomer));
		}
		public bool IsDefault() {
			RCOrderCustomerRecord defaultStruct = new RCOrderCustomerRecord(null);
			if (this.ssSTOrder != defaultStruct.ssSTOrder) return false;
			if (this.ssSTCustomer != defaultStruct.ssSTCustomer) return false;
			return true;
		}
	} // RCOrderCustomerRecord

	/// <summary>
	/// RecordList type <code>RLAllTypesRecordList</code> that represents a record list of
	///  <code>AllTypes</code>
	/// </summary>
	[Serializable()]
	public partial class RLAllTypesRecordList: GenericRecordList<RCAllTypesRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCAllTypesRecord GetElementDefaultValue() {
			return new RCAllTypesRecord("");
		}

		public T[] ToArray<T>(Func<RCAllTypesRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLAllTypesRecordList recordlist, Func<RCAllTypesRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLAllTypesRecordList(RCAllTypesRecord[] array) {
			RLAllTypesRecordList result = new RLAllTypesRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLAllTypesRecordList ToList<T>(T[] array, Func <T, RCAllTypesRecord> converter) {
			RLAllTypesRecordList result = new RLAllTypesRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLAllTypesRecordList FromRestList<T>(RestList<T> restList, Func <T, RCAllTypesRecord> converter) {
			RLAllTypesRecordList result = new RLAllTypesRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLAllTypesRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLAllTypesRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLAllTypesRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLAllTypesRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCAllTypesRecord> NewList() {
			return new RLAllTypesRecordList();
		}


	} // RLAllTypesRecordList

	/// <summary>
	/// RecordList type <code>RLCustomerRecordList</code> that represents a record list of
	///  <code>Customer</code>
	/// </summary>
	[Serializable()]
	public partial class RLCustomerRecordList: GenericRecordList<RCCustomerRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCCustomerRecord GetElementDefaultValue() {
			return new RCCustomerRecord("");
		}

		public T[] ToArray<T>(Func<RCCustomerRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLCustomerRecordList recordlist, Func<RCCustomerRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLCustomerRecordList(RCCustomerRecord[] array) {
			RLCustomerRecordList result = new RLCustomerRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLCustomerRecordList ToList<T>(T[] array, Func <T, RCCustomerRecord> converter) {
			RLCustomerRecordList result = new RLCustomerRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLCustomerRecordList FromRestList<T>(RestList<T> restList, Func <T, RCCustomerRecord> converter) {
			RLCustomerRecordList result = new RLCustomerRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLCustomerRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLCustomerRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLCustomerRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLCustomerRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCCustomerRecord> NewList() {
			return new RLCustomerRecordList();
		}


	} // RLCustomerRecordList

	/// <summary>
	/// RecordList type <code>RLOrderLineRecordList</code> that represents a record list of
	///  <code>OrderLine</code>
	/// </summary>
	[Serializable()]
	public partial class RLOrderLineRecordList: GenericRecordList<RCOrderLineRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCOrderLineRecord GetElementDefaultValue() {
			return new RCOrderLineRecord("");
		}

		public T[] ToArray<T>(Func<RCOrderLineRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLOrderLineRecordList recordlist, Func<RCOrderLineRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLOrderLineRecordList(RCOrderLineRecord[] array) {
			RLOrderLineRecordList result = new RLOrderLineRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLOrderLineRecordList ToList<T>(T[] array, Func <T, RCOrderLineRecord> converter) {
			RLOrderLineRecordList result = new RLOrderLineRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLOrderLineRecordList FromRestList<T>(RestList<T> restList, Func <T, RCOrderLineRecord> converter) {
			RLOrderLineRecordList result = new RLOrderLineRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLOrderLineRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderLineRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderLineRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLOrderLineRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCOrderLineRecord> NewList() {
			return new RLOrderLineRecordList();
		}


	} // RLOrderLineRecordList

	/// <summary>
	/// RecordList type <code>RLOrderRecordList</code> that represents a record list of
	///  <code>Order</code>
	/// </summary>
	[Serializable()]
	public partial class RLOrderRecordList: GenericRecordList<RCOrderRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCOrderRecord GetElementDefaultValue() {
			return new RCOrderRecord("");
		}

		public T[] ToArray<T>(Func<RCOrderRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLOrderRecordList recordlist, Func<RCOrderRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLOrderRecordList(RCOrderRecord[] array) {
			RLOrderRecordList result = new RLOrderRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLOrderRecordList ToList<T>(T[] array, Func <T, RCOrderRecord> converter) {
			RLOrderRecordList result = new RLOrderRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLOrderRecordList FromRestList<T>(RestList<T> restList, Func <T, RCOrderRecord> converter) {
			RLOrderRecordList result = new RLOrderRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLOrderRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLOrderRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCOrderRecord> NewList() {
			return new RLOrderRecordList();
		}


	} // RLOrderRecordList

	/// <summary>
	/// RecordList type <code>RLNoteRecordList</code> that represents a record list of
	///  <code>Note</code>
	/// </summary>
	[Serializable()]
	public partial class RLNoteRecordList: GenericRecordList<RCNoteRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCNoteRecord GetElementDefaultValue() {
			return new RCNoteRecord("");
		}

		public T[] ToArray<T>(Func<RCNoteRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLNoteRecordList recordlist, Func<RCNoteRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLNoteRecordList(RCNoteRecord[] array) {
			RLNoteRecordList result = new RLNoteRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLNoteRecordList ToList<T>(T[] array, Func <T, RCNoteRecord> converter) {
			RLNoteRecordList result = new RLNoteRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLNoteRecordList FromRestList<T>(RestList<T> restList, Func <T, RCNoteRecord> converter) {
			RLNoteRecordList result = new RLNoteRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLNoteRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLNoteRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLNoteRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLNoteRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCNoteRecord> NewList() {
			return new RLNoteRecordList();
		}


	} // RLNoteRecordList

	/// <summary>
	/// RecordList type <code>RLLevel3RecordList</code> that represents a record list of
	///  <code>Level3</code>
	/// </summary>
	[Serializable()]
	public partial class RLLevel3RecordList: GenericRecordList<RCLevel3Record>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCLevel3Record GetElementDefaultValue() {
			return new RCLevel3Record("");
		}

		public T[] ToArray<T>(Func<RCLevel3Record, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLLevel3RecordList recordlist, Func<RCLevel3Record, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLLevel3RecordList(RCLevel3Record[] array) {
			RLLevel3RecordList result = new RLLevel3RecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLLevel3RecordList ToList<T>(T[] array, Func <T, RCLevel3Record> converter) {
			RLLevel3RecordList result = new RLLevel3RecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLLevel3RecordList FromRestList<T>(RestList<T> restList, Func <T, RCLevel3Record> converter) {
			RLLevel3RecordList result = new RLLevel3RecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLLevel3RecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel3RecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel3RecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLLevel3RecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCLevel3Record> NewList() {
			return new RLLevel3RecordList();
		}


	} // RLLevel3RecordList

	/// <summary>
	/// RecordList type <code>RLLevel2RecordList</code> that represents a record list of
	///  <code>Level2</code>
	/// </summary>
	[Serializable()]
	public partial class RLLevel2RecordList: GenericRecordList<RCLevel2Record>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCLevel2Record GetElementDefaultValue() {
			return new RCLevel2Record("");
		}

		public T[] ToArray<T>(Func<RCLevel2Record, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLLevel2RecordList recordlist, Func<RCLevel2Record, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLLevel2RecordList(RCLevel2Record[] array) {
			RLLevel2RecordList result = new RLLevel2RecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLLevel2RecordList ToList<T>(T[] array, Func <T, RCLevel2Record> converter) {
			RLLevel2RecordList result = new RLLevel2RecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLLevel2RecordList FromRestList<T>(RestList<T> restList, Func <T, RCLevel2Record> converter) {
			RLLevel2RecordList result = new RLLevel2RecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLLevel2RecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel2RecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel2RecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLLevel2RecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCLevel2Record> NewList() {
			return new RLLevel2RecordList();
		}


	} // RLLevel2RecordList

	/// <summary>
	/// RecordList type <code>RLLevel1RecordList</code> that represents a record list of
	///  <code>Level1</code>
	/// </summary>
	[Serializable()]
	public partial class RLLevel1RecordList: GenericRecordList<RCLevel1Record>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCLevel1Record GetElementDefaultValue() {
			return new RCLevel1Record("");
		}

		public T[] ToArray<T>(Func<RCLevel1Record, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLLevel1RecordList recordlist, Func<RCLevel1Record, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLLevel1RecordList(RCLevel1Record[] array) {
			RLLevel1RecordList result = new RLLevel1RecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLLevel1RecordList ToList<T>(T[] array, Func <T, RCLevel1Record> converter) {
			RLLevel1RecordList result = new RLLevel1RecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLLevel1RecordList FromRestList<T>(RestList<T> restList, Func <T, RCLevel1Record> converter) {
			RLLevel1RecordList result = new RLLevel1RecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLLevel1RecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel1RecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLLevel1RecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLLevel1RecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCLevel1Record> NewList() {
			return new RLLevel1RecordList();
		}


	} // RLLevel1RecordList

	/// <summary>
	/// RecordList type <code>RLProductRecordList</code> that represents a record list of
	///  <code>Product</code>
	/// </summary>
	[Serializable()]
	public partial class RLProductRecordList: GenericRecordList<RCProductRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCProductRecord GetElementDefaultValue() {
			return new RCProductRecord("");
		}

		public T[] ToArray<T>(Func<RCProductRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLProductRecordList recordlist, Func<RCProductRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLProductRecordList(RCProductRecord[] array) {
			RLProductRecordList result = new RLProductRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLProductRecordList ToList<T>(T[] array, Func <T, RCProductRecord> converter) {
			RLProductRecordList result = new RLProductRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLProductRecordList FromRestList<T>(RestList<T> restList, Func <T, RCProductRecord> converter) {
			RLProductRecordList result = new RLProductRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLProductRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLProductRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLProductRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLProductRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCProductRecord> NewList() {
			return new RLProductRecordList();
		}


	} // RLProductRecordList

	/// <summary>
	/// RecordList type <code>RLOrderCustomerRecordList</code> that represents a record list of
	///  <code>OrderCustomer</code>
	/// </summary>
	[Serializable()]
	public partial class RLOrderCustomerRecordList: GenericRecordList<RCOrderCustomerRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCOrderCustomerRecord GetElementDefaultValue() {
			return new RCOrderCustomerRecord("");
		}

		public T[] ToArray<T>(Func<RCOrderCustomerRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLOrderCustomerRecordList recordlist, Func<RCOrderCustomerRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLOrderCustomerRecordList(RCOrderCustomerRecord[] array) {
			RLOrderCustomerRecordList result = new RLOrderCustomerRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLOrderCustomerRecordList ToList<T>(T[] array, Func <T, RCOrderCustomerRecord> converter) {
			RLOrderCustomerRecordList result = new RLOrderCustomerRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLOrderCustomerRecordList FromRestList<T>(RestList<T> restList, Func <T, RCOrderCustomerRecord> converter) {
			RLOrderCustomerRecordList result = new RLOrderCustomerRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLOrderCustomerRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderCustomerRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLOrderCustomerRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLOrderCustomerRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCOrderCustomerRecord> NewList() {
			return new RLOrderCustomerRecordList();
		}


	} // RLOrderCustomerRecordList

} // XmlRecordsTestTypes
