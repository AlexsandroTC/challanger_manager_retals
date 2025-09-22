using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalReturn
{
    public interface IRentalReturnStrategy
    {
        RentalPlan Plan { get; }
        decimal CalculateFinalPrice(DateTime actualReturn, DateTime expectedReturnDate, decimal totalPrice);
    }
}
