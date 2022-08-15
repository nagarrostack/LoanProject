namespace Loans.Data.Initializers
{
    public interface IDBInitializer
    {
        Task Initialize();

        Task SeedConfiguration();
        Task SeedClient();
        Task SeedLoan();
        Task SeedBusiness();
    }
}
