using System;
using System.Linq;
using static DDDSample.Logic.Money;

namespace DDDSample.Logic
{
    /// <summary>
    /// Entity.
    /// Indentifier Equality and Reference Equality apply.
    /// Almost always mutable.
    /// Prefer Value object to Entites.
    /// Entites act as Wrappers.
    /// 
    /// </summary>
    public sealed class SnackMachine: Entity
    { 
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney( Money money)
        {
            Money[] coins = { FiveCent, TenCent, TwentyCent, FiftyCent, OneEuro, TwoEuro };
            if (!coins.Contains(money))
                throw new InvalidOperationException("You are allowed to insert only one coit at the time.");

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }
    }   
}
