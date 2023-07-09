using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryCult.Identity.API.Data
{
    public class LivraryContext : IdentityDbContext
    {
        public LivraryContext(DbContextOptions<LivraryContext> options): base(options) { }
    }
}
