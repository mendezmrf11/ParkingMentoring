using CupiParqueadero.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ParkingMentoring.Models;

namespace CupiParqueadero.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; } 

        public DbSet<User> Users { get; set; }




    }
}
