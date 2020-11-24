using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Lambda.ServicesUtils.Dynamo;
using Lambda.ServicesUtils.Infrastructure;
using Lambda.ServicesUtils.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.CarCrud.Create
{
    public class Function
    {
        private ICarService _carService;
        private ApiGatewayResponseHelper _apiGatewayResponseHelper;

        public Function()
        {
            _carService = new CarSeriveDynamoDb();
            _apiGatewayResponseHelper = new ApiGatewayResponseHelper();
        }

        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var carFromRequest = JsonSerializer.Deserialize<Car>(request.Body);
            if (carFromRequest is null)
            {
                context.Logger.Log("carFromRequest is null");
                return _apiGatewayResponseHelper.BadRequest("carFromRequest was null");
            }

            await _carService.AddCarAsync(carFromRequest);
            return _apiGatewayResponseHelper.CreateResponse(
                _apiGatewayResponseHelper.CreateBodyWithObject(carFromRequest, "New car was created"));
        }
    }
}