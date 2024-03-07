using GameManagerApp.View;
using Microsoft.EntityFrameworkCore;




namespace GameManagerApp.Data
{
    public class GameContext : DbContext
    {
        public DbSet<GameInfo> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=games.db");
    }
}
