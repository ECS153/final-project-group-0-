using AutoMapper;
using System;
using System.Threading.Tasks;
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
    public class BrowserController : ControllerBase
    {
        private IRequestSwapService _service;
        private IMapper _mapper;
        public BrowserController(IRequestSwapService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
  
        [HttpPost]
        public IActionResult Index ([FromBody] BrowserRequestSwapModel model)
        {
            var ReqSwap = _mapper.Map<RequestSwap>(model);
            ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ReqSwap.UserId = int.Parse(User.Identity.Name); 
  
            _service.Create(ReqSwap);
            
            return Ok();
        }

    }
}
