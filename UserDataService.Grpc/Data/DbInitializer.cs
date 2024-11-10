namespace UserDataService.Grpc.Data
{
    public class DbInitializer
    {
        public static void Initialize(UserDataContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
