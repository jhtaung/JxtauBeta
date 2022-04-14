using System.Xml.Serialization;

namespace Server.DTOs
{
    public class AddressDto
    {
        public string Address1 { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip5 { get; set; } = "";
        public string Zip4 { get; set; } = "";
    }

    [XmlRoot(ElementName="AddressValidateResponse")]
	public class AddressValidateResponse {
        [XmlElement(ElementName="Address")]
		public Address? Address { get; set; }

    }

    [XmlRoot(ElementName="Address")]
	public class Address {
		[XmlElement(ElementName="Address1")]
		public string Address1 { get; set; } = "";
		
        [XmlElement(ElementName="Address2")]
		public string Address2 { get; set; } = "";
		
        [XmlElement(ElementName="City")]
		public string City { get; set; } = "";
		
        [XmlElement(ElementName="State")]
		public string State { get; set; } = "";
		
        [XmlElement(ElementName="Zip5")]
		public string Zip5 { get; set; } = "";
		
        [XmlElement(ElementName="Zip4")]
		public string Zip4 { get; set; } = "";
		
        [XmlAttribute(AttributeName="ID")]
		public string ID { get; set; } = "";
	}
}