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
