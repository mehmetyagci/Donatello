using Donatello.Infrastructure;
using Donatello.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donatello.Services
{
    public class CardService
    {
        private readonly DonatelloContext dbContext;
        public CardService(DonatelloContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CardDetails GetDetails(int id)
        {
            var card = dbContext
                .Cards
                .Include(c => c.Column)
                .SingleOrDefault(x => x.Id == id);
            if (card == null)
                return new CardDetails();

            var board = dbContext
                .Boards
                .Include(b => b.Columns)
                .SingleOrDefault(b => b.Id == card.Column.BoardId);

            var availableColumns = board
                .Columns
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                });
            return new CardDetails
            {
                Id = card.Id,
                Contents = card.Contents,
                Notes = card.Notes,
                Columns = availableColumns.ToList(),
                Column = card.ColumnId
            };
        }

        public void Update(CardDetails cardDetails)
        {
            var card = dbContext
                .Cards
                .SingleOrDefault(x => x.Id == cardDetails.Id);
            card.Contents = cardDetails.Contents;
            card.Notes = cardDetails.Notes;
            card.ColumnId = cardDetails.Column;

            dbContext.SaveChanges();
        }
    }
}
