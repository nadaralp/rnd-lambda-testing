using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Threading.Tasks;

namespace SNS.SendingSMSConsole
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IAmazonSimpleNotificationService snsClient = new AmazonSimpleNotificationServiceClient();
            PublishRequest publishRequest = new PublishRequest
            {
                Message = "Hello Dalia, You won 50 Million dollars from your lottery last night. Congratulations !!, Sugar daddy will give you your gift",
                PhoneNumber = "+972545493976"
            };
            publishRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] =
                new MessageAttributeValue { StringValue = "Loterry", DataType = "String" };

            var pubResponse = await snsClient.PublishAsync(publishRequest);
            Console.WriteLine(pubResponse.HttpStatusCode);
            Console.WriteLine(pubResponse.MessageId);
        }
    }
}