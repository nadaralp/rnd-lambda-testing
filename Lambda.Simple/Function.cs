using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.Simple
{
    public class Person
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
    }

    public class Function
    {
        /// <summary>
        /// Example function for an API Gateway request
        /// </summary>
        public string FunctionHandler(Person person, ILambdaContext context)
        {
            //context.Logger.Log("Test log from lambda");
            //if (apiGatewayProxyRequest != null)
            //{
            //    context.Logger.Log(JsonSerializer.Serialize(apiGatewayProxyRequest));
            //}
            //else
            //{
            //    context.Logger.Log("apiGatewayProxyRequest is null");
            //}

            //var person = JsonSerializer.Deserialize<Person>(apiGatewayProxyRequest?.Body);
            //if (string.IsNullOrEmpty(person?.FirstName) && string.IsNullOrEmpty(person?.LastName))
            //{
            //    return "Couldn't identify a person from the request";
            //}

            return $"Greeting, {person.FirstName}, {person.LastName}. How are you doing today?";
        }
    }
}