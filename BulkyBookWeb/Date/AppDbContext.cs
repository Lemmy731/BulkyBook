using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Date
{
    public class AppDbContext:IdentityDbContext<MyAppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<Category> Categories { get; set; }
    }
}
