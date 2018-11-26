using Donatello.Infrastructure;
using Donatello.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Services
{
    public class BoardService
    {
        private readonly DonatelloContext dbContext;
        public BoardService(DonatelloContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BoardList ListBoards()
        {
            var model = new BoardList();

            foreach (var board in dbContext.Boards)
            {
                model.Boards.Add(new BoardList.Board
                {
                    Id = board.Id,
                    Title = board.Title
                });
            }
           
            return model;
        }

        public void AddCard(AddCard viewModel)
        {
            var board = dbContext.Boards
                            .Include(b => b.Columns)
                            .SingleOrDefault(x => x.Id == viewModel.Id);

            var firstColumn = board.Columns.FirstOrDefault();
            if(firstColumn == null)
            {
                firstColumn = new Models.Column { Title = "ToDo" };
                board.Columns.Add(firstColumn);
            }

            firstColumn.Cards.Add(new Models.Card
            {
                Contents = viewModel.Contents
            });

            dbContext.SaveChanges();
        }

        public BoardView GetBoard(int id)
        {
            var model = new BoardView();

            var board = dbContext.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .SingleOrDefault(x => x.Id == id);

            model.Id = board.Id;
            
            foreach (var column in board.Columns)
            {
                var modelColumn = new BoardView.Column();
                modelColumn.Title = column.Title;

                foreach (var card in column.Cards)
                {
                    var modelCard = new BoardView.Card();
                    modelCard.Id = card.Id;
                    modelCard.Content = card.Contents;
                    modelColumn.Cards.Add(modelCard);
                }
                model.Columns.Add(modelColumn);
            }           
            return model;
        }       

        internal void  AddBoard(NewBoard viewModel)
        {
            dbContext.Boards.Add(new Models.Board
            {
                Title = viewModel.Title
            });

            dbContext.SaveChangesAsync();                
        }
    }
}
