using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Models
{
    // each column in SQL would be called attribute in DynamoDB

    [DynamoDBTable("car")]
    public class Car
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("isEmailVerified")]
        public string IsEmailVerified { get; set; }
    }
}