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
    
    public class SwapController : ControllerBase
    {
        private ISwapService _swapService;
        private IMapper _mapper;
    
        public SwapController(ISwapService service, IMapper mapper)
        {
            _swapService = service;
            _mapper = mapper;
        }
        
        ////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////// Request Methods //////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        [HttpPost("new")]
        public IActionResult New([FromBody] SubmitRequestModel model)
        {
            int userId = int.Parse(User.Identity.Name);
            var ReqSwap = _mapper.Map<RequestSwap>(model);
            
            ReqSwap.UserId = userId; 
            ReqSwap.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            
            _swapService.Enqueue(ReqSwap);
            
            return Ok();
        }

        [HttpGet]
        public IActionResult GetHead()
        {
            var userId = int.Parse(User.Identity.Name);
            var reqSwap = _swapService.Front(userId);
            return Ok(_mapper.Map<RequestSwapModel>(reqSwap));
        }

        [HttpDelete]
        public IActionResult Pop()
        {
            var userId = int.Parse(User.Identity.Name);
            try {
                _swapService.Dequeue(userId);
                return Ok();
            }
            catch(AppException e) {
                return BadRequest(new { Title = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Submit([FromBody]SubmitSwapModel model)
        {
            try {
                var userId = int.Parse(User.Identity.Name);
                _swapService.Swap(model.CredentialId, userId);
                return Ok();
            }
            catch (AppException e) {
                return BadRequest(new { Title = e.Message });
            }
        }

    }
}
