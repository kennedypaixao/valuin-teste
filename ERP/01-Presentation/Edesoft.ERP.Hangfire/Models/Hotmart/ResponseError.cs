using Newtonsoft.Json;

namespace Edesoft.ERP.Hangfire.Models.Hotmart
{
    public class ResponseError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErroDescription { get; set; }
        [JsonProperty("error_uri")]
        public string ErrorUri { get; set; }
    }
}