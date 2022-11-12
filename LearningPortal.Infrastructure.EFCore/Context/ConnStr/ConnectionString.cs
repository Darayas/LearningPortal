namespace LearningPortal.Infrastructure.EFCore.Context.ConnStr
{
    public static class ConnectionString
    {
        public static string GetConnectionString()
        {
            //----- Client Connection String -----
            return @"Server=.;Database=LearningPortalDb;Trusted_Connection=True;";

            //----- Server Connection String -----
            //return "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        }
    }
}
