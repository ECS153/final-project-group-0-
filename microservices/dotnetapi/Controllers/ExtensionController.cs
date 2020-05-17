using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetapi.Data;
using dotnetapi.Models;

namespace dotnetapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtensionController : ControllerBase
    {
        private readonly ProxyReplaceContext _context;
        public ExtensionController(ProxyReplaceContext context)
        {
            _context = context;
        }
  
        [HttpPost]
        public async Task<IActionResult> Index ([FromBody] ProxyReplace proxyReplace)
        {
            _context.ProxyReplaces.Add(proxyReplace);
            Console.Write(proxyReplace.Credential);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
