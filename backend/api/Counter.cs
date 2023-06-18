using Newtonsoft.Json;

namespace Company.Function
{
    public class Counter
    {
        [JsonProperty(PropertyName="id")]
        public string id { get; set; }
            
        [JsonProperty(PropertyName="count")]
        public string count { get; set; }
    }
}