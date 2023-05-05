using GeoChat.Chat.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Infra.DbAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Core.Models.Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
