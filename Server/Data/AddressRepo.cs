using System.Xml.Linq;
using System.Xml.Serialization;
using Server.DTOs;
using Server.Interfaces;
using Server.Params;

namespace Server.Data
{
    public class AddressRepo : IAddressRepo
    {
        private readonly HttpClient _httpClient;
        public AddressRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://production.shippingapis.com/");
        }
        public async Task<AddressDto> GetAddressAsync(AddressParams addressParams)
        {
            // var properties = typeof(AddressParams).GetProperties();

            // construct query
            string query = "";
            query = "<AddressValidateRequest USERID=\"445000006481\">";
            query += "<Address ID=\"0\">";
            query += "<Address1>" + addressParams.Address1 + "</Address1>";
            query += "<Address2>" + addressParams.Address2 + "</Address2>";
            query += "<City>" + addressParams.City + "</City>";
            query += "<State>" + addressParams.State + "</State>";
            query += "<Zip5>" + addressParams.Zip5 + "</Zip5>";
            query += "<Zip4>" + addressParams.Zip4 + "</Zip4>";
            query += "</Address>";
            query += "</AddressValidateRequest>";

            // get results
            var response = await _httpClient.GetAsync("ShippingAPI.dll?API=Verify&XML=" + query);
            var result = await response.Content.ReadAsStringAsync();

            // parse xml
            var xml = XElement.Parse(result);
            StringReader reader = new StringReader(xml.ToString());
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AddressValidateResponse));
            var addressXml = (AddressValidateResponse)xmlSerializer.Deserialize(reader)!;

            // bind dto
            var addressDto = new AddressDto()
            {
                Address1 = addressXml.Address!.Address1,
                Address2 = addressXml.Address!.Address2,
                City = addressXml.Address!.City,
                State = addressXml.Address!.State,
                Zip5 = addressXml.Address!.Zip5,
                Zip4 = addressXml.Address!.Zip4
            };

            return addressDto;
        }
    }
}