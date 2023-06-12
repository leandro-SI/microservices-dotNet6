using LeoShopping.Email.Messages;
using LeoShopping.Email.Model;
using LeoShopping.Email.Model.Context;
using LeoShopping.Email.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeoShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySQLContext> _context = null;

        public EmailRepository(DbContextOptions<MySQLContext> context)
        {
            _context = context;
        }

        public async Task LogEmail(UpdatePaymetResultMessage message)
        {
            EmailLog email = new EmailLog()
            {
                Email = message.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {message.OrderId} criada com sucesso!"
            };

            await using var _db = new MySQLContext(_context);

            _db.EmailLogs.Add(email);
            await _db.SaveChangesAsync();
        }
    }
}
