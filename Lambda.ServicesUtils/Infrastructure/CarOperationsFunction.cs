using Lambda.ServicesUtils.Dynamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Infrastructure
{
    public abstract class CarOperationsFunction : ApiGatewayFunction
    {
        protected ICarService _carService;

        public CarOperationsFunction()
        {
            _carService = new CarSeriveDynamoDb();
        }
    }
}