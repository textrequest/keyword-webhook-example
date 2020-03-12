using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; 

namespace WebhookExample
{
    public class OutboundMessage
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "yourPhoneNumber")]
        public string YourPhoneNumber { get; set; }

        [JsonProperty(PropertyName = "recipientPhoneNumber")]
        public string RecipientPhoneNumber { get; set;  }

        [JsonProperty(PropertyName = "senderName")]
        public string SenderName { get; set; }

        [JsonProperty(PropertyName = "mmsMedia")]
        public ImageAttachment MMSMedia { get; set; }
    }

    public class ImageAttachment
    {
        [JsonProperty(PropertyName = "mediaUrl")]
        public string MediaUrl { get; set; }

        [JsonProperty(PropertyName = "mimeType")]
        public string MimeType { get; set; }
    }
}
