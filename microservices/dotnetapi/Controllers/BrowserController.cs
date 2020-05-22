using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using dotnetapi.Entities;
using dotnetapi.Models.Requests;
using dotnetapi.Services;

namespace dotnetapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BrowserController : ControllerBase
    {
        private ISwapService _swapService;
        private IMapper _mapper;
        public BrowserController(ISwapService service, IMapper mapper)
        {
            _swapService = service;
            _mapper = mapper;
        }
  
        [HttpPost]
        public IActionResult Index ([FromBody] BrowserSubmitRequestModel model)
        {
            // Grab BrowserRequestSwapModel model, and map it to a RequestSwapModel
            var ReqSwap = _mapper.Map<RequestSwap>(model);

            // Fill in user IP addr as well as UserId, and call the RequestModel Service
            ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ReqSwap.UserId = int.Parse(User.Identity.Name); 
            _swapService.Create(ReqSwap);
            
            return Ok();
        }

    }
}
