using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.SQS
{
    public class SQSService : ISQSService
    {
        private AmazonSQSClient _sQSClient = new AmazonSQSClient();

        public async Task SendMessageAsync(string queueUrl, string messagePayload)
        {
            SendMessageResponse response = await _sQSClient.SendMessageAsync(queueUrl, messagePayload);
        }

        public async Task DeleteMessageAsync(string queueUrl, string receiptHandle)
        {
            DeleteMessageResponse response = await _sQSClient.DeleteMessageAsync(queueUrl, receiptHandle);
        }
    }
}