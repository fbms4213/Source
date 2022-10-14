using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WinFormsAppMVP.Models;

namespace WinFormsAppMVP.Repositories.Contexts;


public class EFContext : DbContext
{
    private readonly IConfiguration _configuration;

    public EFContext()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_configuration.GetConnectionString("DefaultConStr"));

        base.OnConfiguring(optionsBuilder);
    }


    public DbSet<Student>? Students { get; set; }

}