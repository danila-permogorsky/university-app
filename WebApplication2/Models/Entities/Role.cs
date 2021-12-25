using Final.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models.Entities;

public class Role : IdentityRole<int>, IWithId
{
}