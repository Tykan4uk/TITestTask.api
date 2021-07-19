using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskApi.Models.Requests;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        private string AccountId => User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value;
        private string Role => User.Claims.Single(s => s.Type == ClaimTypes.Role).Value;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] GetRequest request)
        {
            if (Role != "admin")
            {
                if (AccountId != request.AccountId)
                {
                    return NotFound();
                }
            }

            var result = await _messageService.GetAsync(request.AccountId);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            if (AccountId != request.AccountId)
            {
                return NotFound();
            }

            var result = await _messageService.AddAsync(request.AccountId, request.Message);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]UpdateRequest request)
        {
            if (AccountId != request.AccountId)
            {
                return NotFound();
            }

            var result = await _messageService.UpdateAsync(request.AccountId, request.MessageId, request.Message);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove([FromBody]RemoveRequest request)
        {
            if (Role != "admin")
            {
                if (AccountId != request.AccountId)
                {
                    return NotFound();
                }
            }

            var result = await _messageService.RemoveAsync(request.AccountId, request.MessageId);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}
