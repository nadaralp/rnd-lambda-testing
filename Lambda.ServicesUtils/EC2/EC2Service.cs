using Amazon.EC2;
using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.EC2
{
    public class EC2Service : IEC2Service
    {
        private AmazonEC2Client _eC2Client = new AmazonEC2Client();

        public async Task StartInstanceAsync(string instanceId)
        {
            //var instanceIds = GetInstanceIdsFromConfig();
            List<string> instanceIds = new List<string>()
            {
                instanceId
            };

            StartInstancesRequest startInstancesRequest = new StartInstancesRequest(instanceIds);
            await _eC2Client.StartInstancesAsync(startInstancesRequest);
        }

        public async Task StopInstanceAsync(string instanceId)
        {
            //var instanceIds = GetInstanceIdsFromConfig();

            List<string> instanceIds = new List<string>()
            {
                instanceId
            };

            StopInstancesRequest stopInstancesRequest = new StopInstancesRequest(instanceIds);
            var response = await _eC2Client.StopInstancesAsync(stopInstancesRequest);
        }

        //public async Task<bool> IsInstanceRunning()
        //{
        //    // todo: debug this to see instance statuses
        //    DescribeInstanceStatusRequest describeInstanceStatusRequest = new DescribeInstanceStatusRequest()
        //    {
        //        IncludeAllInstances = true
        //    };
        //    DescribeInstanceStatusResponse describeStatus = await _eC2Client.DescribeInstanceStatusAsync(describeInstanceStatusRequest);
        //    foreach (var instance in describeStatus.InstanceStatuses)
        //    {
        //        Console.WriteLine(instance);
        //    }
        //    return false;
        //}

        public async Task ModifyInstanceType(string instanceId, InstanceTypeOptions instanceType)
        {
            ModifyInstanceAttributeRequest modifyInstanceAttributeRequest = new ModifyInstanceAttributeRequest
            {
                InstanceId = instanceId,
                InstanceType = InstanceType.T2Micro /*GetInstanceTypeBasedOnOptions(instanceType)*/,
            };

            ModifyInstanceAttributeResponse res = await _eC2Client.ModifyInstanceAttributeAsync(modifyInstanceAttributeRequest);
        }

        private InstanceType GetInstanceTypeBasedOnOptions(InstanceTypeOptions instanceTypeOptions)
        {
            switch (instanceTypeOptions)
            {
                case InstanceTypeOptions.Weak:
                    return InstanceType.T2Micro;

                case InstanceTypeOptions.Medium:
                    return InstanceType.T2Medium;

                case InstanceTypeOptions.Strong:
                    return InstanceType.T2Large;

                default:
                    return InstanceType.T2Micro;
            }
        }

        private List<string> GetInstanceIdsFromConfig()
        {
            // read config and return instance ids
            string configJson = File.ReadAllText(@"D:\Test Projects\Lambda-testing\Lambda\Lambda.ServicesUtils\config.json");
            JsonConfig jsonConfig = JsonSerializer.Deserialize<JsonConfig>(configJson);

            return jsonConfig.InstancesToModify;
        }
    }
}