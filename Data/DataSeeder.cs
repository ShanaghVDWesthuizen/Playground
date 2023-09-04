using System;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using KingPriceUserManagementWebApp.Data;
using KingPriceUserManagementWebApp.Models;
using Group = KingPriceUserManagementWebApp.Models.Group;

public class DataSeeder
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if there's already data in the database
        if (context.Users.Any() || context.Groups.Any() || context.Permissions.Any())
        {
            return; // Data has been seeded
        }

        // Create and add users
        var user1 = new User { Username = "User1" };
        var user2 = new User { Username = "User2" };
        context.Users.AddRange(user1, user2);

        // Create and add groups
        var group1 = new Group { GroupName = "Group1" };
        var group2 = new Group { GroupName = "Group2" };
        context.Groups.AddRange(group1, group2);

        // Create and add permissions
        var permission1 = new Permission { PermissionName = "Level1" };
        var permission2 = new Permission { PermissionName = "Level2" };
        context.Permissions.AddRange(permission1, permission2);

        // Create and add user-group relationships
        context.UserGroups.Add(new UserGroup { User = user1, Group = group1 });
        context.UserGroups.Add(new UserGroup { User = user1, Group = group2 });
        context.UserGroups.Add(new UserGroup { User = user2, Group = group1 });

        // Create and add group-permission relationships
        context.GroupPermissions.Add(new GroupPermission { Group = group1, Permission = permission1 });
        context.GroupPermissions.Add(new GroupPermission { Group = group2, Permission = permission2 });

        context.SaveChanges();
    }
}

