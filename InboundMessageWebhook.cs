using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace WebhookExample
{
    /// <summary>
    /// This is the class that is run as an Azure Function with a HTTP trigger. An Azure Function is Azure's serverless function offering, much like
    /// AWS Lambda functions. Each time the HTTP endpoint is hit, Azure will spin up a copy of this function and execute it. 
    /// </summary>
    public static class SendMessageWebhook
    {
        /// <summary>
        /// The actual HTTP endpoint function that's run each time there's a request. 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("InboundMessageWebhook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //re-hydrate the request based on the body. This should be a webhook from Text Request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Message msg = JsonConvert.DeserializeObject<Message>(requestBody);
            if (msg == null)
            {
                return new BadRequestObjectResult("We couldn't understand your request.");
            }

            //grab the incoming message text. Is this a keyword that we're looking for? If so, send a message with the appropriate response. 
            //You could replace this switch statement with a query to your database to match a product name. 
            string keyword = msg.Conversation.Message.ToLower().Trim(); 
            switch (keyword)
            {
                case "laura":
                    await SendMessage("Zoya in Laura can best be described as soft pink toned ivory cream. https://www.zoya.com/content/item/Zoya/Zoya-Nail-Polish-Laura-ZP1030.html",
                        "https://storagetrdevelop.blob.core.windows.net/mms/laura.jpg", msg.Conversation.ConsumerPhoneNumber);
                    break;
                case "colleen":
                    await SendMessage("Zoya in Colleen can best be described as a delicate pink cream with a warm undertone. https://www.zoya.com/content/item/Zoya/Zoya-Nail-Polish-Colleen-ZP1025.html",
                        "https://storagetrdevelop.blob.core.windows.net/mms/colleen.jpg", msg.Conversation.ConsumerPhoneNumber);
                    break; 
            }

            return new OkResult(); 
        }

        /// <summary>
        /// Called to send a message in response to a keyword. 
        /// </summary>
        /// <param name="msgText">The message text to send.</param>
        /// <param name="imageUrl">The URL to a publicly addressable image file.</param>
        /// <param name="recipientPhone">The eleven digit phone number of who we're sending this message to.</param>
        /// <returns></returns>
        private static async Task SendMessage(string msgText, string imageUrl, string recipientPhone)
        {
            OutboundMessage msg = new OutboundMessage();
            msg.Message = msgText;
            msg.MMSMedia = new ImageAttachment(); 
            msg.MMSMedia.MediaUrl = imageUrl;
            msg.MMSMedia.MimeType = "image/jpeg";
            msg.RecipientPhoneNumber = recipientPhone;
            msg.SenderName = "Nail Polish Bot";
            msg.YourPhoneNumber = "14232056522"; //change this to your Text Request phone number that you want to send the message from. 

            string text = JsonConvert.SerializeObject(msg);
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://app.textrequest.com/api/v2/messages");
            request.Method = HttpMethod.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "api-key yourAPIKeyHere"); //replace yourAPIKeyHere with your Text Request API key
            request.Headers.Add(HttpRequestHeader.ContentType.ToString(), "application/json");
            request.Content = new StringContent(JsonConvert.SerializeObject(msg), System.Text.Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(request);
        }
    }
}
