using Newtonsoft.Json;
using System.Collections.Generic;

namespace Edesoft.ERP.Hangfire.Models.Hotmart
{
    public class ResponseSubscriptions
    {
        [JsonProperty("items")]
        public List<Item> Itens { get; set; }
    }

    public class Item
    {
        [JsonProperty("subscription_id")]
        public int SubscriptionId { get; set; }
        [JsonProperty("subscriber_code")]
        public string SubscriberCode { get; set; }
        [JsonProperty("accession_date")]
        public string AccessionDate { get; set; }
        [JsonProperty("date_next_charge")]
        public string DateNextCharge { get; set; }
        [JsonProperty("subscriber")]
        public Subscriber Subscriber { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Subscriber
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ucode")]
        public string Ucode { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}