using Donatello.Infrastructure;
using Donatello.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Donatello.Services
{
    public class BoardService
    {
        private readonly DonatelloContext dbContext;
        private readonly EmailService emailService;

        public BoardService(DonatelloContext dbContext, EmailService emailService)
        {
            this.dbContext = dbContext;
            this.emailService = emailService;
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
            if (firstColumn == null)
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

        public NotificationSettings GetNotificationPreferences(int boardId, int columnId)
        {
            var column = dbContext.Columns.SingleOrDefault(x => x.Id == columnId);
            return new NotificationSettings
            {
                BoardId = boardId,
                ColumnId = columnId,
                EmailAddress = column.NotificationEmail
            };
        }

        public void SaveNotificationPreference(NotificationSettings viewModel)
        {
            var column = dbContext.Columns.SingleOrDefault(x => x.Id == viewModel.ColumnId);
            column.NotificationEmail = viewModel.EmailAddress;
            dbContext.SaveChanges();
        }

        public void SetColor(SetColorCommand command)
        {
            var board = dbContext.Boards.SingleOrDefault(x => x.Id == command.BoardId);
            board.BackgroundColor = command.Color;
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
            model.Color = board.BackgroundColor;

            foreach (var column in board.Columns)
            {
                var modelColumn = new BoardView.Column();
                modelColumn.Title = column.Title;
                modelColumn.Id = column.Id;

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

        internal void AddBoard(NewBoard viewModel)
        {
            dbContext.Boards.Add(new Models.Board
            {
                Title = viewModel.Title
            });

            dbContext.SaveChangesAsync();
        }

        public void Move(MoveCardCommand command)
        {
            var card = dbContext.Cards.SingleOrDefault(x => x.Id == command.CardId);
            card.ColumnId = command.ColumnId;
            dbContext.SaveChanges();

            var column = dbContext.Columns.SingleOrDefault(x => x.Id == command.ColumnId);
            emailService.SendCardMovedNotification(card, column);
        }
    }
}
