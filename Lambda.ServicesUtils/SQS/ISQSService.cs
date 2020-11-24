using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.SQS
{
    public interface ISQSService
    {
        Task DeleteMessageAsync(string queueUrl, string receiptHandle);

        Task SendMessageAsync(string queueUrl, string messagePayload);
    }
}