
namespace EasyJetAssessment.Models
{
    public class Money : IMoney
    {
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }

        public string Currency { get; }
    }
}
