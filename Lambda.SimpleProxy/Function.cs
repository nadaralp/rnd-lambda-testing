using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Http;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.SimpleProxy
{
    public class Response
    {
        // The http method as is.
        public string HttpMethod { get; set; }

        //key=value of the query params (querystring)
        public IDictionary<string, string> QueryParams { get; set; }

        // key=value of the path params. eg. "proxy"="nadar"
        // or proxy = "nadar/text" if you give more than one.
        public IDictionary<string, string> PathParams { get; set; }

        // Gives you the JSON body
        public string Body { get; set; }

        // Gives you the full specified path. e.g
        // /proxied/nadar
        public string Path { get; set; }

        // The stage of the API Gateway
        public string Stage { get; set; }

        // Multi stage variables
        public IDictionary<string, string> StageVariables { get; set; }

        public Response()
        {
        }

        public Response(APIGatewayProxyRequest request)
        {
            Body = request.Body;
            HttpMethod = request.HttpMethod;
            PathParams = request.PathParameters;
            QueryParams = request.QueryStringParameters;
            Path = request.Path;
            Stage = request.RequestContext.Stage;
            StageVariables = request.StageVariables;
        }
    }

    /// <summary>
    /// Function to help retrieve all metadata
    /// </summary>
    public class Function
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.Log("LAMBDA CONTEXT======\n" + JsonSerializer.Serialize(context));

            APIGatewayProxyResponse response = new APIGatewayProxyResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Headers = new Dictionary<string, string> { { "X-My-Header", "Nadar Alpenidze" } },
                IsBase64Encoded = false,
                Body = JsonSerializer.Serialize(new Response(request))
            };

            return response;
        }
    }
}