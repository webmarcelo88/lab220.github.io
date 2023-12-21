using System;
using Lab200.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Context;

public class LabContext : DbContext
{
    public LabContext() { }
    public LabContext(DbContextOptions<LabContext> options) : base(options) { }

    #region Props
    public DbSet<Category> Categories { get; set; }
    public DbSet<Colours> Colours { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeItems> EmployeeItems { get; set; }
    public DbSet<Function> Functions { get; set; }
    public DbSet<Grid> Grids { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductsTypes { get; set; }
    public DbSet<Scale> Scales { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<VmUser> VmUsers { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grid>().HasKey(g => new { g.ClientId, g.ProductId, g.Sku, g.ScaleId, g.ColorsId, g.Price });

        base.OnModelCreating(modelBuilder);
    }
}

