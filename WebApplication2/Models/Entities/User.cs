using System.ComponentModel.DataAnnotations.Schema;
using Final.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models.Entities;

public enum ServiceRoles
{
	Admin,
	Worker, 
	AccepptanceEngineer,
	AccountantEngineer
}

// public static class ServiceRoles
// {
// 	public const string Admin = "Admin";
// 	public const string Worker = "Worker";
// 	public const string AcceptanceEngineer = "AcceptanceEngineer";
// 	public const string AccountantEngineer = "AccountantEngineer";
//
// 	public static List<string> Roles { get; set; } = new()
// 	{
// 		"Admin",
// 		"Worker",
// 		"AcceptanceEngineer",
// 		"AccountantEngineer"
// 	};
// }

public class User : IdentityUser<int>, IWithId
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	
	[NotMapped]
	public string RoleName { get; set; }
	
	[NotMapped]
	public string FullName => FirstName + " " + LastName;
}