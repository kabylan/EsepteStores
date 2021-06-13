using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EsepteStores.Controllers
{
    [Route("api/cookie")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       
        // GET api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            return HttpContext.Session.GetString("Cookie");
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(string value)
        {
            HttpContext.Session.SetString("Cookie", value);
        }

    }
}
