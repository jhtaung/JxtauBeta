using System.Xml.Serialization;

namespace Server.DTOs
{
    public class AddressZipDto
    {
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Zip5 { get; set; }
    }

    [XmlRoot(ElementName="CityStateLookupResponse")]
	public class CityStateLookupResponse {
        [XmlElement(ElementName="ZipCode")]
		public ZipCode? ZipCode { get; set; }

    }

    [XmlRoot(ElementName="ZipCode")]
	public class ZipCode {
        [XmlElement(ElementName="City")]
		public string City { get; set; } = "";
		
        [XmlElement(ElementName="State")]
		public string State { get; set; } = "";
		
        [XmlElement(ElementName="Zip5")]
		public string Zip5 { get; set; } = "";
		
        [XmlAttribute(AttributeName="ID")]
		public string ID { get; set; } = "";
	}
}