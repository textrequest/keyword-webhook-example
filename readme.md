#Text Request Webhook Example on Azure

This example shows how to use a Text Request webhook for inbound messages. Each incoming message is checked against a list of keywords that we'd like to respond to. If a match is found, a message is sent out via the Text Request API. 

## What you'll need

- Your Text Request API key.
- The Text Request phone number you'd like to process messages from. 
- An Azure Account to publish the Azure Function.