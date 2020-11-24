using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Lambda.ServicesUtils.Infrastructure;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CarCrud.GetAllCarsFilter
{
    public class Function : CarOperationsFunction
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            request.PathParameters.TryGetValue("manufacturer", out string manufacturer);
            request.PathParameters.TryGetValue("price", out string price);

            var filteredCars = await _carService.FilterCars(manufacturer, Convert.ToDouble(price));
            return _apiGatewayResponseHelper.CreateResponse(JsonSerializer.Serialize(filteredCars));
        }
    }
}