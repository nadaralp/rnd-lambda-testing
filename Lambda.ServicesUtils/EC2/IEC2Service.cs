using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.EC2
{
    public interface IEC2Service
    {
        Task ModifyInstanceType(string instanceId, InstanceTypeOptions instanceType);

        Task StartInstanceAsync(string instanceId);

        Task StopInstanceAsync(string instanceId);
    }
}