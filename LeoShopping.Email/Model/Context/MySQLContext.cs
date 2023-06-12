using Microsoft.EntityFrameworkCore;

namespace LeoShopping.Email.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
            
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
            
        }

        public DbSet<EmailLog> EmailLogs { get; set; }


    }
}
