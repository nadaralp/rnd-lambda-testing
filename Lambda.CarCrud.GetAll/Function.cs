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

namespace Lambda.CarCrud.GetAll
{
    public class Function : CarOperationsFunction
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request.QueryStringParameters == null || !request.QueryStringParameters.Any())
            {
                // ask yourself, is it a heavy query? can I cache?..
                var allCarsFromDb = await _carService.GetCarsAsync();
                return _apiGatewayResponseHelper.CreateResponse(JsonSerializer.Serialize(allCarsFromDb));
            }

            // filter
            request.QueryStringParameters.TryGetValue("manufacturer", out string manufacturer);
            request.QueryStringParameters.TryGetValue("price", out string price);

            var filteredCars = await _carService.FilterCars(manufacturer, Convert.ToDouble(price));
            return _apiGatewayResponseHelper.OkResponse(filteredCars);

            // paginate

            // sort
            return null;
        }
    }
}