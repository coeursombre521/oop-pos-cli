using System;
using System.Xml;
using System.Xml.Serialization;

namespace entitati {
    public class ListOfPackageable : List<IPackageable>, IXmlSerializable {
        public ListOfPackageable() : base() { }

        public System.Xml.Schema.XmlSchema GetSchema() => null!;

        public void ReadXml(XmlReader reader) {
            reader.ReadStartElement("Packageables");
            while (reader.IsStartElement("IPackageable")) {
                Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName")!)!;
                XmlSerializer serial = new(type);

                reader.ReadStartElement("IPackageable");
                this.Add((IPackageable)serial.Deserialize(reader)!);
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer) {
            foreach (IPackageable packageable in this) {
                writer.WriteStartElement("IPackageable");
                writer.WriteAttributeString("AssemblyQualifiedName", packageable.GetType().AssemblyQualifiedName);
                XmlSerializer xmlSerializer = new(packageable.GetType());
                xmlSerializer.Serialize(writer, packageable);
                writer.WriteEndElement();
            }
        }
    }
}