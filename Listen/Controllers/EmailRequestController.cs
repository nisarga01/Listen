using Listen.IServices;
using Listen.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Listen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailRequestController : ControllerBase
    {
        private readonly IEmailRequestServices mailService;
        public EmailRequestController(IEmailRequestServices mailService)
        {
            this.mailService = mailService;
        }

        [EnableCors("CORSPolicy")]
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            var Result = await mailService.SendEmailAsync(request);
            if (Result.Success)
                return Ok(Result); 
            return BadRequest(Result);
        }
    }
}
