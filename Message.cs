using System;
using System.Collections.Generic;
using Newtonsoft.Json; 

namespace WebhookExample
{
    class Message
    {
        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }

        [JsonProperty(PropertyName = "yourPhoneNumber")]
        public YourPhoneNumber YourPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "conversation")]
        public Conversation Conversation { get; set; }
    }

    class Account
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "externalAccountId")]
        public string ExternalAccountId { get; set; }
    }

    public class YourPhoneNumber
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "externalPhoneId")]
        public string ExternalPhoneId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class Conversation
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "consumerPhoneNumber")]
        public string ConsumerPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "messageDirection")]
        public string MessageDirection { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "consumerFriendlyName")]
        public string ConsumerFriendlyName { get; set; }

        [JsonProperty(PropertyName = "mmsAttachments")]
        public List<MMSAttachment> MMSAttachments { get; set; }

    }

    public class MMSAttachment
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }
    }
}
