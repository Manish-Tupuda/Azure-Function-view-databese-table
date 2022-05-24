using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace sqlfunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string userid = req.Query["userid"];
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "woodruff-db.database.windows.net";
            builder.UserID = "azurequadrantit";
            builder.Password = "4-v3ry-53cr37-p455w0rd";
            builder.InitialCatalog = "moonshot";
            int f;

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");

            String sql = "SELECT * FROM dbo.NewTable where USERID =" +userid;

            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            //Console.WriteLine(reader[1]);

            while (reader.Read())
            {
                 Console.WriteLine("{0} {1} {2} {3} {4} ", reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(4));
                //Console.WriteLine();
               
            }
            //f = reader.GetInt32(0);

            connection.Close();
            /*
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
*/
            return new OkObjectResult("Sucessful");
            }
    }
}
