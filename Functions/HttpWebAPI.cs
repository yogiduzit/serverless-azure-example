using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
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

        [FunctionName("GetStudentById")]
        public async Task<IActionResult> GetStudentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "students/{id}")] HttpRequest req, 
        string id, ILogger log)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request.");

            Student student = await _context.Students.FirstOrDefaultAsync(i => i.StudentId == id);
            if (student == null) {
                return new NotFoundObjectResult(new { id });
            }

            return new OkObjectResult(student);
        }

        [FunctionName("CreateStudent")]
        public async Task<IActionResult> CreateStudent([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "students")] HttpRequest req, 
        ILogger log)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Student student = JsonConvert.DeserializeObject<Student>(requestBody);
            student.StudentId = Guid.NewGuid().ToString();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new CreatedResult(new Uri($"/students/{student.StudentId}", UriKind.Relative), student);
        }
    }
}
