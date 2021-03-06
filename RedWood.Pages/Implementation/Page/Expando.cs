﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RedWood.Pages.Implementation.Page
{
    internal static class SerializationUtils
    {
        public static bool SerializeObject(object instance, string fileName, bool binarySerialization)
        {
            var retVal = true;

            if (!binarySerialization)
            {
                XmlTextWriter writer = null;
                try
                {
                    var serializer =
                        new XmlSerializer(instance.GetType());

                    Stream fs = new FileStream(fileName, FileMode.Create);

                    writer = new XmlTextWriter(fs, new UTF8Encoding());

                    writer.Formatting = Formatting.Indented;

                    writer.IndentChar = ' ';

                    writer.Indentation = 3;

                    serializer.Serialize(writer, instance);
                }
                catch (Exception ex)
                {
                    Debug.Write("SerializeObject failed with : " + ex.Message);

                    retVal = false;
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
            else
            {
                Stream fs = null;

                try
                {
                    var serializer = new BinaryFormatter();

                    fs = new FileStream(fileName, FileMode.Create);

                    serializer.Serialize(fs, instance);
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return retVal;
        }

        public static bool SerializeObject(object instance, XmlTextWriter writer, bool throwExceptions)
        {
            var retVal = true;

            try
            {
                var serializer =
                    new XmlSerializer(instance.GetType());


                writer.Formatting = Formatting.Indented;

                writer.IndentChar = ' ';

                writer.Indentation = 3;

                serializer.Serialize(writer, instance);
            }
            catch (Exception ex)
            {
                Debug.Write("SerializeObject failed with : " + ex.GetBaseException().Message + "\r\n" +
                            (ex.InnerException != null ? ex.InnerException.Message : ""));

                if (throwExceptions)
                    throw;

                retVal = false;
            }

            return retVal;
        }

        public static bool SerializeObject(object instance, out string xmlResultString)
        {
            return SerializeObject(instance, out xmlResultString, false);
        }

        public static bool SerializeObject(object instance, out string xmlResultString, bool throwExceptions)
        {
            xmlResultString = string.Empty;

            var ms = new MemoryStream();

            var writer = new XmlTextWriter(ms, new UTF8Encoding());

            if (!SerializeObject(instance, writer, throwExceptions))
            {
                ms.Close();
                return false;
            }

            xmlResultString = Encoding.UTF8.GetString(ms.ToArray(), 0, (int) ms.Length);

            ms.Close();
            writer.Close();

            return true;
        }

        public static bool SerializeObject(object instance, out byte[] resultBuffer, bool throwExceptions = false)
        {
            var retVal = true;

            MemoryStream ms = null;
            try
            {
                var serializer = new BinaryFormatter();

                ms = new MemoryStream();

                serializer.Serialize(ms, instance);
            }
            catch (Exception ex)
            {
                Debug.Write("SerializeObject failed with : " + ex.GetBaseException().Message);

                retVal = false;

                if (throwExceptions)
                    throw;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }

            resultBuffer = ms.ToArray();

            return retVal;
        }

        public static string SerializeObjectToString(object instance, bool throwExceptions = false)
        {
            var xmlResultString = string.Empty;

            if (!SerializeObject(instance, out xmlResultString, throwExceptions))
                return null;

            return xmlResultString;
        }

        public static byte[] SerializeObjectToByteArray(object instance, bool throwExceptions = false)
        {
            byte[] byteResult = null;

            if (!SerializeObject(instance, out byteResult))
                return null;

            return byteResult;
        }

        public static object DeSerializeObject(string fileName, Type objectType, bool binarySerialization)
        {
            return DeSerializeObject(fileName, objectType, binarySerialization, false);
        }

        public static object DeSerializeObject(string fileName, Type objectType, bool binarySerialization,
            bool throwExceptions)
        {
            object instance = null;

            if (!binarySerialization)
            {
                XmlReader reader = null;

                XmlSerializer serializer = null;

                FileStream fs = null;
                try
                {
                    serializer = new XmlSerializer(objectType);

                    fs = new FileStream(fileName, FileMode.Open);

                    reader = new XmlTextReader(fs);

                    instance = serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    if (throwExceptions)
                        throw;

                    var message = ex.Message;

                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();

                    if (reader != null)
                        reader.Close();
                }
            }
            else
            {
                BinaryFormatter serializer = null;

                FileStream fs = null;

                try
                {
                    serializer = new BinaryFormatter();

                    fs = new FileStream(fileName, FileMode.Open);

                    instance = serializer.Deserialize(fs);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return instance;
        }

        public static object DeSerializeObject(XmlReader reader, Type objectType)
        {
            var serializer = new XmlSerializer(objectType);

            var Instance = serializer.Deserialize(reader);

            reader.Close();

            return Instance;
        }

        public static object DeSerializeObject(string xml, Type objectType)
        {
            var reader = new XmlTextReader(xml, XmlNodeType.Document, null);

            return DeSerializeObject(reader, objectType);
        }

        public static object DeSerializeObject(byte[] buffer, Type objectType, bool throwExceptions = false)
        {
            BinaryFormatter serializer = null;

            MemoryStream ms = null;

            object Instance = null;

            try
            {
                serializer = new BinaryFormatter();

                ms = new MemoryStream(buffer);

                Instance = serializer.Deserialize(ms);
            }
            catch
            {
                if (throwExceptions)
                    throw;

                return null;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }

            return Instance;
        }

        public static string ObjectToString(object instanc, string separator, ObjectToStringTypes type)
        {
            var fi = instanc.GetType().GetFields();

            var output = string.Empty;

            if (type == ObjectToStringTypes.Properties || type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var property in instanc.GetType().GetProperties())
                {
                    try
                    {
                        output += property.Name + ":" + property.GetValue(instanc, null) + separator;
                    }
                    catch
                    {
                        output += property.Name + ": n/a" + separator;
                    }
                }
            }

            if (type == ObjectToStringTypes.Fields || type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (var field in fi)
                {
                    try
                    {
                        output = output + field.Name + ": " + field.GetValue(instanc) + separator;
                    }
                    catch
                    {
                        output = output + field.Name + ": n/a" + separator;
                    }
                }
            }
            return output;
        }
    }

    public enum ObjectToStringTypes
    {
        Properties,
        PropertiesAndFields,
        Fields
    }

    internal static class Utilities
    {
        public static Type GetTypeFromName(string typeName)
        {
            Type type = null;

            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = ass.GetType(typeName, false);

                if (type != null)
                    break;
            }
            return type;
        }

        public static string MapTypeToXmlType(Type type)
        {
            if (type == typeof (string) || type == typeof (char))
                return "string";
            if (type == typeof (int) || type == typeof (int))
                return "integer";
            if (type == typeof (long) || type == typeof (long))
                return "long";
            if (type == typeof (bool))
                return "boolean";
            if (type == typeof (DateTime))
                return "datetime";

            if (type == typeof (float))
                return "float";
            if (type == typeof (decimal))
                return "decimal";
            if (type == typeof (double))
                return "double";
            if (type == typeof (float))
                return "single";

            if (type == typeof (byte))
                return "byte";

            if (type == typeof (byte[]))
                return "base64Binary";

            return null;
        }

        public static Type MapXmlTypeToType(string xmlType)
        {
            xmlType = xmlType.ToLower();

            if (xmlType == "string")
                return typeof (string);
            if (xmlType == "integer")
                return typeof (int);
            if (xmlType == "long")
                return typeof (long);
            if (xmlType == "boolean")
                return typeof (bool);
            if (xmlType == "datetime")
                return typeof (DateTime);
            if (xmlType == "float")
                return typeof (float);
            if (xmlType == "decimal")
                return typeof (decimal);
            if (xmlType == "double")
                return typeof (double);
            if (xmlType == "single")
                return typeof (float);

            if (xmlType == "byte")
                return typeof (byte);
            if (xmlType == "base64binary")
                return typeof (byte[]);

            return null;
        }
    }

    [XmlRoot("properties")]
    public class PropertyBag : PropertyBag<object>
    {
        public new static PropertyBag CreateFromXml(string xml)
        {
            var bag = new PropertyBag();
            bag.FromXml(xml);
            return bag;
        }
    }

    [XmlRoot("properties")]
    public class PropertyBag<TValue> : Dictionary<string, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var key in Keys)
            {
                var value = this[key];

                Type type = null;
                if (value != null)
                    type = value.GetType();

                writer.WriteStartElement("item");

                writer.WriteStartElement("key");

                writer.WriteString(key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");

                var xmlType = Utilities.MapTypeToXmlType(type);

                var isCustom = false;

                if (value == null)
                {
                    writer.WriteAttributeString("type", "nil");
                }
                else if (!string.IsNullOrEmpty(xmlType))
                {
                    if (xmlType != "string")
                    {
                        writer.WriteStartAttribute("type");
                        writer.WriteString(xmlType);
                        writer.WriteEndAttribute();
                    }
                }
                else
                {
                    isCustom = true;
                    xmlType = "___" + value.GetType().FullName;
                    writer.WriteStartAttribute("type");
                    writer.WriteString(xmlType);
                    writer.WriteEndAttribute();
                }

                if (!isCustom)
                {
                    if (value != null)
                        writer.WriteValue(value);
                }
                else
                {
                    var ser = new XmlSerializer(value.GetType());

                    ser.Serialize(writer, value);
                }
                writer.WriteEndElement(); // value

                writer.WriteEndElement(); // item
            }
        }

        public void ReadXml(XmlReader reader)
        {
            Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "key")
                {
                    string xmlType = null;

                    var name = reader.ReadElementContentAsString();

                    reader.ReadToNextSibling("value");

                    if (reader.MoveToNextAttribute())
                        xmlType = reader.Value;
                    if (string.IsNullOrEmpty(xmlType))
                        xmlType = "string";

                    reader.MoveToContent();

                    TValue value;

                    var strval = string.Empty;

                    if (xmlType == "nil")
                        value = default(TValue); // null

                    else if (xmlType.StartsWith("___"))
                    {
                        while (reader.Read() && reader.NodeType != XmlNodeType.Element)
                        {
                        }

                        var type = Utilities.GetTypeFromName(xmlType.Substring(3));

                        var ser = new XmlSerializer(type);

                        value = (TValue) ser.Deserialize(reader);
                    }
                    else
                        value = (TValue) reader.ReadElementContentAs(Utilities.MapXmlTypeToType(xmlType), null);

                    Add(name, value);
                }
            }
        }

        public string ToXml()
        {
            string xml = null;

            SerializationUtils.SerializeObject(this, out xml);

            return xml;
        }

        public bool FromXml(string xml)
        {
            Clear();

            if (string.IsNullOrEmpty(xml))
                return true;

            var result = SerializationUtils.DeSerializeObject(xml,
                GetType()) as PropertyBag<TValue>;
            if (result != null)
            {
                foreach (var item in result)
                {
                    Add(item.Key, item.Value);
                }
            }
            else

                return false;

            return true;
        }

        public static PropertyBag<TValue> CreateFromXml(string xml)
        {
            var bag = new PropertyBag<TValue>();

            bag.FromXml(xml);

            return bag;
        }
    }

    [Serializable]
    public class Expando : DynamicObject, IDynamicMetaObjectProvider
    {
        private PropertyInfo[] _InstancePropertyInfo;
        private object Instance;
        private Type InstanceType;
        public PropertyBag Properties = new PropertyBag();

        public Expando()
        {
            Initialize(this);
        }

        public Expando(object instance)
        {
            Initialize(instance);
        }

        private PropertyInfo[] InstancePropertyInfo
        {
            get
            {
                if (_InstancePropertyInfo == null && Instance != null)
                    _InstancePropertyInfo = Instance.GetType().
                        GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                return _InstancePropertyInfo;
            }
        }

        public object this[string key]
        {
            get
            {
                try
                {
                    return Properties[key];
                }
                catch (KeyNotFoundException ex)
                {
                    object result = null;
                    if (GetProperty(Instance, key, out result))
                        return result;

                    throw;
                }
            }
            set
            {
                if (Properties.ContainsKey(key))
                {
                    Properties[key] = value;
                    return;
                }

                var miArray = InstanceType.GetMember(key, BindingFlags.Public | BindingFlags.GetProperty);
                if (miArray != null && miArray.Length > 0)
                    SetProperty(Instance, key, value);
                else
                    Properties[key] = value;
            }
        }

        protected virtual void Initialize(object instance)
        {
            Instance = instance;
            if (instance != null)
                InstanceType = instance.GetType();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            if (Properties.Keys.Contains(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            }

            if (Instance != null)
            {
                try
                {
                    return GetProperty(Instance, binder.Name, out result);
                }
                catch
                {
                }
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Instance != null)
            {
                try
                {
                    var result = SetProperty(Instance, binder.Name, value);
                    if (result)
                        return true;
                }
                catch
                {
                }
            }


            Properties[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (Instance != null)
            {
                try
                {
                    if (InvokeMethod(Instance, binder.Name, args, out result))
                        return true;
                }
                catch
                {
                }
            }

            result = null;
            return false;
        }

        protected bool GetProperty(object instance, string name, out object result)
        {
            if (instance == null)
                instance = this;

            var miArray = InstanceType.GetMember(name,
                BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    result = ((PropertyInfo) mi).GetValue(instance, null);
                    return true;
                }
            }

            result = null;
            return false;
        }

        protected bool SetProperty(object instance, string name, object value)
        {
            if (instance == null)
                instance = this;

            var miArray = InstanceType.GetMember(name,
                BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];

                if (mi.MemberType == MemberTypes.Property)
                {
                    ((PropertyInfo) mi).SetValue(Instance, value, null);
                    return true;
                }
            }
            return false;
        }

        protected bool InvokeMethod(object instance, string name, object[] args, out object result)
        {
            if (instance == null)
                instance = this;

            var miArray = InstanceType.GetMember(name,
                BindingFlags.InvokeMethod |
                BindingFlags.Public | BindingFlags.Instance);

            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0] as MethodInfo;

                result = mi.Invoke(Instance, args);

                return true;
            }

            result = null;
            return false;
        }

        public IEnumerable<KeyValuePair<string, object>> GetProperties(bool includeInstanceProperties = false)
        {
            if (includeInstanceProperties && Instance != null)
            {
                foreach (var prop in InstancePropertyInfo)
                    yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(Instance, null));
            }

            foreach (var key in Properties.Keys)
                yield return new KeyValuePair<string, object>(key, Properties[key]);
        }

        public bool Contains(KeyValuePair<string, object> item, bool includeInstanceProperties = false)
        {
            var res = Properties.ContainsKey(item.Key);
            if (res)
                return true;

            if (includeInstanceProperties && Instance != null)
            {
                foreach (var prop in InstancePropertyInfo)
                {
                    if (prop.Name == item.Key)
                        return true;
                }
            }

            return false;
        }
    }
}