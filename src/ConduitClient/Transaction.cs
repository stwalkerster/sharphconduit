namespace Stwalkerster.ConduitClient
{
    using Newtonsoft.Json;

    public class Transaction
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}