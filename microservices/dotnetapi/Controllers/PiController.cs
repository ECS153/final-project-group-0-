using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using dotnetapi.Entities;
using dotnetapi.Models.RequestModels;
using dotnetapi.Services;

namespace dotnetapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PiController : ControllerBase
    {
        private IRequestSwapService _service;
        private IMapper _mapper;
        public PiController(IRequestSwapService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
  
        [HttpGet]
        public IActionResult Index ()
        {
           var userId = int.Parse(User.Identity.Name);
               
            
            
            
            
            return Ok();
        }

    }
}
