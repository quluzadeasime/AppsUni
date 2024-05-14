using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace MaximApp.Models;

public class User:IdentityUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsRemember { get; set; }
}
