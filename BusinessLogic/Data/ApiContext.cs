using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace BusinessLogic.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<UsersInformation> Users { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }
    }
}
