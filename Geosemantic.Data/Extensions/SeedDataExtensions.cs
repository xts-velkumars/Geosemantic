using System;
using System.Linq;
using Geosemantic.Common.Constants;
using Geosemantic.Common.Enums;
using Geosemantic.Domain.Entities;
 
using Xen.Common.Enums;
using Xen.Extensions;

namespace Geosemantic.Data.Extensions
{

    public static class SeedDataExtensions
    {
        public static void EnsureSeeded(this GeosemanticEntities context)
        {
            PageSeedData(context);
            RoleSeedData(context);
            UserSeedData(context);
            RolePageMappingData(context);
        }


        private static void PageSeedData(GeosemanticEntities context)
        {

            if (context.Page.Any())
                return;

            context.Page.AddRange(
                new Page
                {
                   
                    Name = "Dashboard",
                    ShortName = "DB",
                    Sequence = 1,
                    Url = "/dashboard",
                    ParentId = 1,
                    Icon = "",
                    Label = "dashboard",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                    
                    Name = "Forms",
                    ShortName = "FM",
                    Sequence = 2,
                    Url = "/forms",
                    ParentId = 2,
                    Icon = "",
                    Label = "Forms",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                  
                    Name = "Questions",
                    ShortName = "QU",
                    Sequence = 3,
                    Url = "/questions",
                    ParentId = 3,
                    Icon = "",
                    Label = "Questions",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                 
                    Name = "Chats",
                    ShortName = "CH",
                    Sequence = 4,
                    Url = "/chats",
                    ParentId = 4,
                    Icon = "",
                    Label = "Chats",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                  
                    Name = "Users",
                    ShortName = "USE",
                    Sequence = 5,
                    Url = "/users",
                    ParentId = 5,
                    Icon = "",
                    Label = "Users",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                    
                    Name = "Contacts",
                    ShortName = "CON",
                    Sequence = 6,
                    Url = "/contacts",
                    ParentId = 6,
                    Icon = "",
                    Label = "Contacts",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                    
                    Name = "Reports",
                    ShortName = "REP",
                    Sequence = 7,
                    Url = "/reports",
                    ParentId = 7,
                    Icon = "",
                    Label = "Reports",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, 
                new Page
                {
                  
                    Name = "Setting",
                    ShortName = "SE",
                    Sequence = 8,
                    Url = "/setting",
                    ParentId = 8,
                    Icon = "",
                    Label = "Setting",
                    HasBadge = "false",
                    BadgeText = "0",
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }
            );
            context.SaveChanges();
        }

        private static void RoleSeedData(GeosemanticEntities context)
        {
            if (context.Role.Any())
            {
                return;
            }

            context.Role.AddRange(
                new Role
                {
                    Name = RoleConstants.Admin,
                    RoleSystemType = RoleSystemType.System,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "System",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "System",
                    StatusType = StatusType.Enabled
                }, new Role
                {
                    Name = RoleConstants.User,
                    RoleSystemType = RoleSystemType.System,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "System",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "System",
                    StatusType = StatusType.Enabled
                }
            );
            context.SaveChanges();
        }

        private static void UserSeedData(GeosemanticEntities context)
        {
            if (context.User.Any())
            {
                return;
            }

            context.User.AddRange(new User
            {
                EmailAddress = "velkumar.s@xenovex.com",
                FirstName = "Velkumar",
                LastName = "Santhanaraj",
                MobileNumber = "9600155567",
                Password = "reset@123".ToPasswordHash(),
                UserSystemType = UserSystemType.System,
                CreationTs = DateTime.UtcNow,
                CreationUserId = "System",
                LastChangeTs = DateTime.UtcNow,
                LastChangeUserId = "System",
                StatusType = StatusType.Enabled,
                RoleId = 1
                //Role = new Role
                //{
                //    Name = RoleConstants.Admin,
                //    RoleSystemType = RoleSystemType.System,
                //    CreationTs = DateTime.UtcNow,
                //    CreationUserId = "System",
                //    LastChangeTs = DateTime.UtcNow,
                //    LastChangeUserId = "System",
                //    StatusType = StatusType.Enabled
                //}
            });
            context.SaveChanges();
        }

        private static void RolePageMappingData(GeosemanticEntities context)
        {
            if (context.RolePageMapping.Any())
                return;

            context.RolePageMapping.AddRange(
                new RolePageMapping
                {
                    PageId = 1,
                    RoleId = 1,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 2,
                    RoleId = 1,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 3,
                    RoleId = 1,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 4,
                    RoleId = 1,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 5,
                    RoleId = 2,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                   
                    PageId = 6,
                    RoleId = 2,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 7,
                    RoleId = 2,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }, new RolePageMapping
                {
                    PageId = 8,
                    RoleId = 2,
                    CreationTs = DateTime.UtcNow,
                    CreationUserId = "0",
                    LastChangeTs = DateTime.UtcNow,
                    LastChangeUserId = "0",
                    StatusType = StatusType.Enabled
                }
            );
            context.SaveChanges();
        }
    }
}
