using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donatello.Services;
using Donatello.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Donatello.Controllers
{
    public class BoardController : Controller
    {
        private readonly BoardService boardService;

        public BoardController(BoardService boardService)
        {
            this.boardService = boardService;
        }

        public IActionResult Index(int id)
        {
            BoardView model = boardService.GetBoard(id);

            return View(model);
        }

        public IActionResult AddCard(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCard(AddCard viewModel)
        {
            if (ModelState.IsValid)
            {
                boardService.AddCard(viewModel);
                return RedirectToAction(nameof(Index), 
                    new { id = viewModel.Id });
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Notifications(int boardId, int columnId)
        {
            var settings = boardService.GetNotificationPreferences(boardId, columnId);
            return View(settings);
        }

        [HttpPost]
        public IActionResult Notifications(NotificationSettings viewModel)
        {
            boardService.SaveNotificationPreference(viewModel);
            return RedirectToAction(nameof(Index), new { id = viewModel.BoardId });
        }      
    }
}