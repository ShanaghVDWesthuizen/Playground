using Microsoft.EntityFrameworkCore;

namespace KingPriceUserManagementWebApp.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public List<GroupPermission>? GroupPermissions { get; set; }
    }
}
