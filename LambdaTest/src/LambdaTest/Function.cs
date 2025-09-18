using System.Net;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaTest;

public class Function
{
    private const string tableName = "CustomerTable";
    private AmazonDynamoDBClient? client;
    Responses result = new Responses();

    public async Task<APIGatewayProxyResponse> FunctionHandler(Customer input, ILambdaContext context)
    {
        // You can't find this file because it contains secret keys.
        AccessDetails account = new AccessDetails();
        client = new AmazonDynamoDBClient(account.accessKey, account.secretKey, account.sessionToken, RegionEndpoint.USEast1);

        try
        {
            Dictionary<string, AttributeValue> documentRecord = new Dictionary<string, AttributeValue>();
            documentRecord["CustomerID"] = new AttributeValue { S = Guid.NewGuid().ToString() };
            documentRecord["FirstName"] = new AttributeValue { S = input.FirstName };
            documentRecord["LastName"] = new AttributeValue { S = input.LastName };
            documentRecord["Address"] = new AttributeValue { S = input.Address };

            PutItemRequest request = new PutItemRequest
            {
                TableName = tableName,
                Item = documentRecord
            };

            PutItemResponse response = await client.PutItemAsync(request);

            context.Logger.LogInformation("Data added to DynamoDB");
            result.status = 200;
            result.returnMessage = "Data added to DynamoDB";
            result.returnRequestID = response.ResponseMetadata.RequestId;

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(result)
            };
        }
        catch (AmazonDynamoDBException ex)
        {
            context.Logger.LogInformation(ex.Message);
            result.status = 400;
            result.returnMessage = "ERROR: " + ex.Message;

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = JsonConvert.SerializeObject(result)
            };
        }
    }
}
