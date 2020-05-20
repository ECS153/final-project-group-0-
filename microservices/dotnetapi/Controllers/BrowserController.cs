using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using dotnetapi.Entities;
using dotnetapi.Models.Browsers;
using dotnetapi.Services;

namespace dotnetapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrowserController : ControllerBase
    {
        private IBrowserService BrowserService;
        public BrowserController(IBrowserService service)
        {
            BrowserService = service;
        }
  
        [HttpPost]
        public IActionResult Index ([FromBody] RequestSwap ReqSwap)
        {
            BrowserService.Create(ReqSwap);
            
            
            
            return Ok();
        }

    }
}
