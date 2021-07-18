using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] GetRequest request)
        {
            var result = await _messageService.GetAsync(request.AccountId);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            var result = await _messageService.AddAsync(request.AccountId, request.Message);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateRequest request)
        {
            var result = await _messageService.UpdateAsync(request.MessageId, request.Message);
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromBody]RemoveRequest request)
        {
            var result = await _messageService.RemoveAsync(request.MessageId);
            return result != null ? Ok(result) : BadRequest(result);
        }
    }
}
