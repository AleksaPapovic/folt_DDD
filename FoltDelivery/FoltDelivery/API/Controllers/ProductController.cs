using AutoMapper;
using FoltDelivery.Infrastructure.Authorization;
using FoltDelivery.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoltDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        private readonly IConfiguration _configuration;

        private readonly IJwtUtils _iJwtUtils;

        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IConfiguration configuration, IMapper mapper, IJwtUtils iJwtUtils)
        {
            _productService = productService;
            _configuration = configuration;
            _mapper = mapper;
            _iJwtUtils = iJwtUtils;
        }

    }
}
