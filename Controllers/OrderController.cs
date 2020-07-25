using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderItem.Controllers
{
    // [Route("api/[controller]")]
    //  [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        Cart obj = new Cart();

        static string GetToken(string url)
        {
            var user = new User { Name = "sahil", Password = "Hi123" };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                //updated
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }


        [HttpPost]
        [Route("api/Order")]
        public Cart PostOrder([FromBody] Cart c)
        {
             string token = GetToken("http://20.37.142.153/api/Token");
             //  string token = GetToken("https://localhost:44380/api/Token");

            obj.Id = 1;
            obj.userId = 1;
            obj.menuItemId = c.menuItemId;
            int id = obj.menuItemId;


            using (var client = new HttpClient())
            {
                // Setting Base address.  
                  client.BaseAddress = new Uri("http://20.37.142.153/");
              //  client.BaseAddress = new Uri("https://localhost:44380/");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                response = client.GetAsync("api/MenuItem/" + id).Result;

                string result = response.Content.ReadAsStringAsync().Result;
                obj.menuItemName = result;
                return obj;
            }
        }
    }
}

