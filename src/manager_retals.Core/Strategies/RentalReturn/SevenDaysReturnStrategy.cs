using manager_retals.Core.Enums;

namespace manager_retals.Core.Strategies.RentalReturn
{
    public class SevenDaysReturnStrategy : IRentalReturnStrategy
    {
        public RentalPlan Plan => RentalPlan.SevenDays;

        private const decimal DailyPrice = 30.00m;
        private const decimal PenaltyRate = 0.20m;

        public decimal CalculateFinalPrice(DateTime actualReturn, DateTime expectedReturnDate, decimal totalPrice)
        {
            if (actualReturn < expectedReturnDate)
            {
                int unusedDays = (expectedReturnDate - actualReturn).Days;
                decimal unusedValue = unusedDays * DailyPrice;
                decimal penalty = unusedValue * PenaltyRate;

                return totalPrice - unusedValue + penalty;
            }
            else if (actualReturn > expectedReturnDate)
            {
                int extraDays = (actualReturn - expectedReturnDate).Days;
                decimal extraFee = extraDays * 50m;

                return totalPrice + extraFee;
            }

            return totalPrice;
        }
    }
}
