using System;
using Microsoft.AspNetCore.Mvc;

namespace Bereket.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public ObjectResult Get()
        {
            return Ok("Hello world!");
        }
    }
}