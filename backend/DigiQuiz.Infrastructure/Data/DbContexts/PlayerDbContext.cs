using DigiQuiz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigiQuiz.Infrastructure.Data.DbContexts;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Player> Player { get; set; }
}