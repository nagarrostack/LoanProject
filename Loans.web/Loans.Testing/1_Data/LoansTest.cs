using Loans.Data.Entities;

namespace Loans.Testing._1_Data
{
    [TestClass]
    public class LoansTest : BaseTest
    {
        [TestMethod]
        public void GetAllLoans_Test()
        {
            var result = context.ClientLoans.Count();
            Assert.AreNotEqual(0, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(10, 1)]
        public void GetLoanById_Test(int id, int expectedCount)
        {
            var result = context.ClientLoans.Count(l => l.Id == id);
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(11)]
        public void GetLoanByClientId_Test(int clientId)
        {
            var result = context.ClientLoans.Count(l => l.ClientId == clientId);
            Assert.AreNotEqual(0, result);
        }

        [TestMethod,
            DataTestMethod,
            DataRow(14, 12_000, 12, 6, 625, 0, 2022, 01, 01, 4_000)]
        public void AddLoan_Test(int clientId, int amount, int months, int aRP, int rating, int lateLoans, int year, int month, int day, int outstanding)
        {
            var count = context.ClientLoans.Count(l => l.ClientId == clientId);
            var loan = new ClientLoan { 
                Id = context.ClientLoans.Max(l => l.Id) + 1,
                ClientId = clientId,
                AmountRequest = amount,
                QtyMonthsPayment = months,
                APR = (byte)aRP,
                Rating = rating,
                LateLoans = lateLoans,
                LoanDate = new DateTime(year, month, day),
                OutstandingDebt = outstanding
            };
            context.ClientLoans.Add(loan);
            context.SaveChanges();
            Assert.AreNotEqual(count, context.ClientLoans.Count(l => l.ClientId == clientId));
            Assert.AreEqual(count + 1, context.ClientLoans.Count(l => l.ClientId == clientId));

        }

        [TestMethod,
            DataTestMethod,
            DataRow(14)]
        public void RemoveFromLoan_Test(int id)
        {
            var count = context.ClientLoans.Count();
            var loan = context.ClientLoans.FirstOrDefault(l => l.Id == id);
            context.ClientLoans.Remove(loan);
            context.SaveChanges();

            loan = context.ClientLoans.FirstOrDefault(l => l.Id == id);

            Assert.AreNotEqual(count, context.ClientLoans.Count());
            Assert.IsNull(loan);
        }
    }
}
