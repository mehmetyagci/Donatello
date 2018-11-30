using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donatello.Services;
using Donatello.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Donatello.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly BoardService boardService;
        public BoardController(BoardService boardService)
        {
            this.boardService = boardService;
        }

        [HttpPost("movecard")]
        public IActionResult MoveCard([FromBody]MoveCardCommand command)
        {
            boardService.Move(command);
            return Ok(new { Moved = true });
        }

        [HttpPost("setColor")]
        public IActionResult SetColor([FromBody]SetColorCommand command)
        {
            this.boardService.SetColor(command);
            return Ok(new { Changed = true });
        }

        [HttpPost("setTitle")]
        public IActionResult SetTitle([FromBody] SetTitleCommand command)
        {
            this.boardService.SetTitle(command);
            return Ok(new { Changed = true });
        }
    }
}