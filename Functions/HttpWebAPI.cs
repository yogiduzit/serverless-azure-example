using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Snoopy.Function
{
    public class HttpWebAPI
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public HttpWebAPI(IHttpClientFactory httpClientFactory,
            ApplicationDbContext context)
        {
            _client = httpClientFactory.CreateClient();
            _context = context;
        }

        [FunctionName("GetStudents")]
        public IActionResult GetStudents([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students")] HttpRequest req, 
        ILogger log)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request.");

            var studentsArray = _context.Students.OrderBy(s => s.School).ToArray();

            return new OkObjectResult(studentsArray);
        }

        [FunctionName("HttpWebAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
