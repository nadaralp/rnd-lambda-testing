using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Lambda.ServicesUtils.EC2;
using Lambda.ServicesUtils.SQS;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ModifyEc2
{
    public class Function
    {
        private IEC2Service _eC2Service;
        private ISQSService _sQSService;
        private const string bootUpInstanceQueueUrl = "https://sqs.us-east-1.amazonaws.com/925941540878/InstanceModification-StartQueue";

        public Function()
        {
            _eC2Service = new EC2Service();
            _sQSService = new SQSService();
        }

        public async Task<string> FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
        {
            try
            {
                foreach (var message in sqsEvent.Records)
                {
                    await ProcessMessage(message);
                }
                return "Everything went successfully";
            }
            catch (Exception e)
            {
                context.Logger.Log(e.Message);
                return e.Message;
            }
        }

        private async Task ProcessMessage(SQSEvent.SQSMessage message)
        {
            string instanceToModifyId = message.Body;
            // list of instances -- maybe heavy operation to fetch.

            await _eC2Service.ModifyInstanceType(instanceToModifyId, InstanceTypeOptions.Medium);
            await _sQSService.SendMessageAsync(bootUpInstanceQueueUrl, instanceToModifyId);
            // ack / delete?
        }
    }
}