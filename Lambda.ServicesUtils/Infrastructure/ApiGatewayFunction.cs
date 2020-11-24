using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Infrastructure
{
    public abstract class ApiGatewayFunction
    {
        protected ApiGatewayResponseHelper _apiGatewayResponseHelper;

        public ApiGatewayFunction()
        {
            _apiGatewayResponseHelper = new ApiGatewayResponseHelper();
        }
    }
}