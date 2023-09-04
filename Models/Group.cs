using Microsoft.EntityFrameworkCore;

namespace KingPriceUserManagementWebApp.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public List<UserGroup>? UserGroups { get; set; }
        public List<GroupPermission>? GroupPermissions { get; set; }
    }
}
