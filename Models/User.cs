using Microsoft.EntityFrameworkCore;

namespace KingPriceUserManagementWebApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public List<UserGroup>? UserGroups { get; set; }
    }
}
