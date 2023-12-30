using DigiQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiQuiz.Infrastructure.Data.DbContexts;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Player> Player { get; set; }
}