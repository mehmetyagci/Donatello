using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donatello.Services;
using Donatello.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Donatello.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoardService boardService;

        public HomeController(BoardService boardService)
        {
            this.boardService = boardService;
        }

        public IActionResult Index()
        {
            BoardList model = boardService.ListBoards();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewBoard viewModel)
        {
            boardService.AddBoard(viewModel);
            return RedirectToAction(nameof(Index));
        }      
    }
}