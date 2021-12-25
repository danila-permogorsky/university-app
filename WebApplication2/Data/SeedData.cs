using Final.Data;
using Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Entities;

namespace WebApplication2.Data;

public static class SeedData
{
	public static async Task Initialize(IServiceProvider serviceProvider)
	{
		try
		{
			await using var context = new ApplicationDbContext(
				serviceProvider.GetService<DbContextOptions<ApplicationDbContext>>() ??
				throw new Exception());

			var itemsNotEmpty = !await context.Items.AnyAsync();
			var productsNotEmpty = !await context.Products.AnyAsync();

			var items = new List<Item>
			{
				new()
				{
					Name = "Block",
					Material = "Iron",
					Uin = 47570,
					InstallStatus = InstallStatus.PendingInstallation,
					WarehouseStatus = WarehouseStatus.InStock
				}, new()
				{
					Name = "Carcass",
					Material = "Plastic",
					Uin = 47371,
					InstallStatus = InstallStatus.PendingInstallation,
					WarehouseStatus = WarehouseStatus.Missing
				}, new()
				{
					Name = "Wheel",
					Material = "Iron and Rubber",
					Uin = 57576,
					InstallStatus = InstallStatus.PendingInstallation,
					WarehouseStatus = WarehouseStatus.NeedToOrder
				}
			};

			var products = new List<Product>
			{
				new()
				{
					Name = "Bus",
					SerialNumber = 325,
					AssemblyStatus = AssemblyStatus.InProgress,
				}, new()
				{
					Name = "Tower Crane",
					SerialNumber = 427,
					AssemblyStatus = AssemblyStatus.Waiting
				}, new()
				{
					Name = "Cargo Trailer",
					SerialNumber = 193,
					AssemblyStatus = AssemblyStatus.Done
				}, new Product
				{
					Name = "Rocket",
					SerialNumber = 763,
					AssemblyStatus = AssemblyStatus.Paused
				}
			};

			var itemProducts = new List<ItemProduct>();

			foreach (var product in products)
			{
				foreach (var item in items)
				{
					itemProducts.Add(new ItemProduct
					{
						Item = item,
						Product = product
					});
				}
			}
			
			await context.AddRangeAsync(itemProducts);
			
			if (itemsNotEmpty)
			{
				await context.Items.AddRangeAsync(items);

				await context.SaveChangesAsync();
			}

			if (productsNotEmpty)
			{
				await context.Products.AddRangeAsync(products);
				
				await context.SaveChangesAsync();
			}

			var userManager = serviceProvider.GetService<UserManager<User>>();
			var roleManager = serviceProvider.GetService<RoleManager<Role>>();

			var roles = new List<string>
			{
				"Admin",
				"Worker",
				"AcceptanceEngineer",
				"AccountantEngineer"
			};

			foreach (var role in roles)
			{
				var doesntExists = !await roleManager.RoleExistsAsync(role);

				if (doesntExists)
					await roleManager.CreateAsync(new Role {Name = role});
			}

			var emails = new List<string>
			{
				"admin@example.com",
				"worker@example.com",
				"acceptance_engineer@example.com",
				"accountant_engineer@example.com"
			};

			var users = new List<User>
			{
				new()
				{
					FirstName = "Super",
					LastName = "Admin",
					RoleName = "Admin",
					Email = "admin@example.com",
					UserName = "admin@example.com"
				},
				new()
				{
					FirstName = "Factory",
					LastName = "Worker",
					RoleName = "Worker",
					Email = "worker@example.com",
					UserName = "worker@example.com"
				},
				new()
				{
					FirstName = "Acceptance",
					LastName = "Engineer",
					RoleName = "AcceptanceEngineer",
					Email = "acceptance_engineer@example.com",
					UserName = "acceptance_engineer@example.com"
				},
				new()
				{
					FirstName = "Accountant",
					LastName = "Engineer",
					RoleName = "AccountantEngineer",
					Email = "accountant_engineer@example.com",
					UserName = "accountant_engineer@example.com"
				}
			};

			foreach (var user in users)
			{
				var doesntExists = await userManager.FindByEmailAsync(user.Email) == null;

				IdentityResult result = null;
				
				if(doesntExists)
				{
					result = await userManager.CreateAsync(user, "P@ssw0rd");
				}

				if (result.Succeeded)
					await userManager.AddToRoleAsync(user, user.RoleName);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}