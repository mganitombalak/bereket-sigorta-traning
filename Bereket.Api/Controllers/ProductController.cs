using System;
using Bereket.Domain.Entity;
using Bereket.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bereket.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IBaseCrudRepository<Product, int> productRepository;
        public ProductController(IBaseCrudRepository<Product, int> productRepo)
        {
            productRepository=productRepo;
        }

        [HttpGet]
        public ObjectResult Get()
        {
            return Ok(productRepository.Find());
        }
    }
}