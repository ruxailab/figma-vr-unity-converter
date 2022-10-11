using Newtonsoft.Json;

namespace Teste
{
    public class ContaResponse
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("handle")]
        public string handle { get; set; }
        [JsonProperty("img_url")]
        public string img_url { get; set; }
    }
}