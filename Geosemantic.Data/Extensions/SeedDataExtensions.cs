namespace Geosemantic.Data.Extensions
{

    public static class SeedDataExtensions
    {
        public static void EnsureSeeded(this SteEntities context)
        {
            //if (!context.User.Any())
            //{
            //    context.User.Add(new User
            //    {

            //        FirstName = "Velkumar",
            //        LastName = "Santhanaraj",
            //        EmailAddress = "velkumar.s@xenovex.com",
            //        Password = "reset@123".ToPasswordHash(),
            //        CreationTs = DateTime.UtcNow,
            //        CreationUserId = "System",
            //        LastChangeTs = DateTime.UtcNow,
            //        LastChangeUserId = "System",
            //        StatusType = StatusType.Enabled
            //    });
            //    context.SaveChanges();
            //}
        }
    }
     
}
