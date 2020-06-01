using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using dotnetapi.Entities;
using dotnetapi.Services;
using dotnetapi.Helpers;
using dotnetapi.Models.Requests;

namespace dotnetapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    
    public class PiController : ControllerBase
    {
        private ISwapService _swapService;
        private IMapper _mapper;
    
        public PiController(ISwapService service, IMapper mapper)
        {
            _swapService = service;
            _mapper = mapper;
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////// Request Methods //////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult Index([FromBody] BrowserSubmitRequestModel model)
        {
            // Grab BrowserRequestSwapModel model, and map it to a RequestSwapModel
            var ReqSwap = _mapper.Map<RequestSwap>(model);

            // Fill in user IP addr as well as UserId, and call the RequestModel Service
            ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ReqSwap.UserId = int.Parse(User.Identity.Name); 
            _swapService.Create(ReqSwap);
            
            return Ok();
        }

        [HttpGet]
        public IActionResult GetHead()
        {
            var userId = int.Parse(User.Identity.Name);
            var reqSwap = _swapService.GetTop(userId);
            return Ok(_mapper.Map<PiRequestSwapModel>(reqSwap));
        }

        [HttpDelete]
        public IActionResult Pop()
        {
            var userId = int.Parse(User.Identity.Name);
            try {
                _swapService.DeleteTop(userId);
                return Ok();
            }
            catch(AppException ex) {
                return BadRequest(new { Title = ex.Message });
            }
           
        }

        [HttpPost]
        public IActionResult Submit([FromBody]PiSubmitSwapModel model)
        {
            try {
                var userId = int.Parse(User.Identity.Name);
                _swapService.Swap(model, userId);
                return Ok();
            }
            catch (AppException ex) {
                return BadRequest(new { Title = ex.Message });
            }
        }

    }
}
