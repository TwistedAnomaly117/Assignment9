using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using System.Dynamic;
using System.Net.Http;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment9
{
    public class Function
    {

        public static readonly HttpClient client = new HttpClient();
        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            dynamic data = new ExpandoObject();

            Dictionary<string, string> item = (Dictionary<string, string>)input.QueryStringParameters;

            string call = await client.GetStringAsync("https://api.nytimes.com/svc/books/v3/lists/current/" + item.First().Value + ".json?api-key=JUspjOwVF2OD9luGoZnHm6R8f1UcZHPg");

            dynamic objects = JsonConvert.DeserializeObject<ExpandoObject>(call);

            return objects;

        }
    }

}
