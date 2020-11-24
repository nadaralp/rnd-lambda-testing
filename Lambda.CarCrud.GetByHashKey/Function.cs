using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

using Lambda.ServicesUtils.Dynamo;
using Lambda.ServicesUtils.Infrastructure;
using Lambda.ServicesUtils.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CarCrud.GetByHashKey
{
    public class Function : CarOperationsFunction
    {
        public Function()
        {
        }

        /// <summary>
        /// Function to retrieve a car model by hash key.
        /// </summary>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request.PathParameters.TryGetValue("id", out string idHashKey))
            {
                Car carByHashKey = await _carService.GetCarByIdAsync(idHashKey);
                if (carByHashKey is null)
                {
                    return _apiGatewayResponseHelper.BadRequest("car doesn't exist with specified id");
                }
                context.Logger.Log($"found car - {carByHashKey}");
                return _apiGatewayResponseHelper.CreateResponse(JsonSerializer.Serialize(carByHashKey));
            }

            return _apiGatewayResponseHelper.BadRequest("bad path parameter");
        }
    }
}