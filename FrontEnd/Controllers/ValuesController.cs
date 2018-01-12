using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> GetAsync(int id)
        {
            string receiverUrl = "http://backendapp/api/Values/5";
            //string receiverUrl = "http://localhost:61165/api/Values/5";            

            //To See Traces IDs uncomment this and from below the output

            //string output = "\nRequestID: " + Request.Headers["x-request-id"];
            //output += "\nUserAgent: " + Request.Headers["user-agent"];
            //output += "\nTraceID: " + Request.Headers["x-b3-traceid"] + "\n";    

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(receiverUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return "Failed to get name";
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //return "Hello " + content + "\nFrontend:  " + output;
                    return "Hello " + content;

                }
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
