using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostBin.Repository;
using PostBin.Services;

namespace PostBin.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController: Controller
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Api Running...");
        }
    }
}