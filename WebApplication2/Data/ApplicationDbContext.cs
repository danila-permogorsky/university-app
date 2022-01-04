using Final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Entities;

namespace Final.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.LogTo(Console.WriteLine);
		optionsBuilder.EnableSensitiveDataLogging();
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ItemProduct>()
			.HasKey(ip => new {ip.ItemId, ip.ProductId});

		modelBuilder.Entity<ItemProduct>()
			.HasOne(e => e.Item)
			.WithMany(p => p.ItemProducts)
			.HasForeignKey(e => e.ItemId);

		modelBuilder.Entity<ItemProduct>()
			.HasOne(e => e.Product)
			.WithMany(p => p.ItemProducts)
			.HasForeignKey(e => e.ProductId);
		
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }
	
	public DbSet<Item> Items { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ItemProduct> ItemProducts { get; set; }
} 