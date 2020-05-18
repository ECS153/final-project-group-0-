using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using dotnetapi.Entities;

namespace dotnetapi.Controllers
{
    [Authorize]
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
