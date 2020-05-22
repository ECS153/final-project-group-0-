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
        private ISwapService _service;
        private IMapper _mapper;
        public PiController(ISwapService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
  
        [HttpGet]
        public IActionResult Index ()
        {
            var userId = int.Parse(User.Identity.Name);
            var reqSwap = _service.GetTop(userId);
            return Ok(_mapper.Map<RequestSwapModel>(reqSwap));
        }
        [HttpPost]
        public IActionResult Submit([FromBody]SubmitSwapModel model)
        {
            if (ModelState.IsValid) {
            try {
                if (ModelState.IsValid)
                    Console.Write("hello: " + model.CredentialId);
               var userId = int.Parse(User.Identity.Name);
            _service.Swap(model, userId);
                return Ok();
            }
            catch (AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
            }
            return BadRequest(ModelState);
            
            
        }

    }
}
