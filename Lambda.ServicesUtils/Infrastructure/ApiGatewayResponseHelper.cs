using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Infrastructure
{
    public class ApiGatewayResponseHelper
    {
        public APIGatewayProxyResponse CreateResponse(string body, int statusCode = 200)
        {
            var response = new APIGatewayProxyResponse();
            response.StatusCode = statusCode;
            response.IsBase64Encoded = false;
            response.Body = body;

            // add default headers?

            return response;
        }

        public APIGatewayProxyResponse BadRequest(string message)
        {
            var response = new APIGatewayProxyResponse();
            response.StatusCode = 400;
            response.IsBase64Encoded = false;
            response.Body = message;

            return response;
        }

        public APIGatewayProxyResponse OkResponse<T>(T payload)
        {
            var response = new APIGatewayProxyResponse();
            response.StatusCode = 200;
            response.IsBase64Encoded = false;
            response.Body = JsonSerializer.Serialize(payload);

            return response;
        }

        public string CreateBodyWithObject<T>(T obj, object bodyMessage)
        {
            object bodyObject = new
            {
                message = bodyMessage,
                data = obj,
            };

            return JsonSerializer.Serialize(bodyObject);
        }
    }
}