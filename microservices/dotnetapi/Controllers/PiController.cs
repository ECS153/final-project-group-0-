using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using dotnetapi.Services;
using dotnetapi.Helpers;
using dotnetapi.Models.Requests;


using System.Collections.Generic;

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
  
        [HttpGet]
        public IActionResult Index ()
        {
            var userId = int.Parse(User.Identity.Name);
            var reqSwap = _swapService.GetTop(userId);
            
            return Ok(_mapper.Map<PiRequestSwapModel>(reqSwap));
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
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
