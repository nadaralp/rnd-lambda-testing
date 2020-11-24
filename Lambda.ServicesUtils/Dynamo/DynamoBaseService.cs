using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.Dynamo
{
    public abstract class DynamoBaseService
    {
        protected AmazonDynamoDBClient amazonDynamoClient;
        protected DynamoDBContext context;

        public DynamoBaseService()
        {
            amazonDynamoClient = new AmazonDynamoDBClient();
            context = new DynamoDBContext(amazonDynamoClient);
        }
    }
}