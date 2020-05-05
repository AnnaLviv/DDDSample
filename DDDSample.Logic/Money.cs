using System;

namespace DDDSample.Logic
{
    /// <summary>
    /// Value Object
    /// Reference Eqaulity and Structural Equality apply
    /// Has to be Immutable.
    /// Can not live on their own. They should belong to one or several entities.
    /// They can not have own table in the DB.
    /// Prefer Value object to Entites.
    /// Value Objects are light-weight, easy to work with.
    /// Put most ob business logic into Value objects
    /// </summary>
    public sealed class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);

        public static readonly Money FiveCent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money TwentyCent = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money FiftyCent = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money OneEuro = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwoEuro = new Money(0, 0, 0, 0, 0, 1);

        public int FiveCentCount { get; }
        public int TenCentCount { get; }
        public int TwentyCentCount { get; }
        public int FiftyCentCount { get; }
        public int OneEuroCount { get; }
        public int TwoEuroCount { get; }
        public decimal Amount => FiveCentCount * 0.05m +
                     TenCentCount * 0.1m +
                     TwentyCentCount * 0.2m +
                     FiftyCentCount * 0.5m +
                     OneEuroCount * 1m +
                     TwoEuroCount * 2m;

        public Money(int fiveCentCount, int tenCentCount, int twentyCentCount,
           int fiftyCentCount, int oneEuroCount, int twoEuroCount)
        {
            if (fiveCentCount < 0)
                throw new InvalidOperationException("Count of five cents can not be smaller than 0.");
            if (tenCentCount < 0)
                throw new InvalidOperationException("Count of ten cents can not be smaller than 0.");
            if (twentyCentCount < 0)
                throw new InvalidOperationException("Count of twenty cents can not be smaller than 0.");
            if (fiftyCentCount < 0)
                throw new InvalidOperationException("Count of fifty cents can not be smaller than 0.");
            if (oneEuroCount < 0)
                throw new InvalidOperationException("Count of one euro can not be smaller than 0.");
            if (twoEuroCount < 0)
                throw new InvalidOperationException("Count of two euro can not be smaller than 0.");

            FiveCentCount = fiveCentCount;
            TenCentCount = tenCentCount;
            TwentyCentCount = twentyCentCount;
            FiftyCentCount = fiftyCentCount;
            OneEuroCount = oneEuroCount;
            TwoEuroCount = twoEuroCount;
        }

        public static Money operator +(Money moneyOne, Money moneyTwo)
        {
            Money sum = new Money(
                            moneyOne.FiveCentCount + moneyTwo.FiveCentCount,
                            moneyOne.TenCentCount + moneyTwo.TenCentCount,
                            moneyOne.TwentyCentCount + moneyTwo.TwentyCentCount,
                            moneyOne.FiftyCentCount + moneyTwo.FiftyCentCount,
                            moneyOne.OneEuroCount + moneyTwo.OneEuroCount,
                            moneyOne.TwoEuroCount + moneyTwo.TwoEuroCount);
            return sum;            
        }

        public static Money operator -(Money moneyOne, Money moneyTwo)
        {
            Money sum = new Money(
                            moneyOne.FiveCentCount - moneyTwo.FiveCentCount,
                            moneyOne.TenCentCount - moneyTwo.TenCentCount,
                            moneyOne.TwentyCentCount - moneyTwo.TwentyCentCount,
                            moneyOne.FiftyCentCount - moneyTwo.FiftyCentCount,
                            moneyOne.OneEuroCount - moneyTwo.OneEuroCount,
                            moneyOne.TwoEuroCount - moneyTwo.TwoEuroCount);
            return sum;
        }


        protected override bool EqualsCore(Money valueObject)
        {
            return  FiveCentCount == valueObject.FiveCentCount
                  && TenCentCount == valueObject.TenCentCount 
                  && TwentyCentCount == valueObject.TwentyCentCount
                  && FiftyCentCount == valueObject.FiftyCentCount
                  && OneEuroCount == valueObject.OneEuroCount
                  && TwoEuroCount == valueObject.TwoEuroCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = FiveCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ TwentyCentCount;
                hashCode = (hashCode * 397) ^ FiftyCentCount;
                hashCode = (hashCode * 397) ^ OneEuroCount;
                hashCode = (hashCode * 397) ^ TwoEuroCount;

                return hashCode;
            }
        }

        public override string ToString()
        {
            return Amount.ToString();
        }
    }
}
