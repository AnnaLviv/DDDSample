using DDDSample.Logic;
using FluentAssertions;
using System;
using Xunit;

using static DDDSample.Logic.Money;

namespace DDDSample.Test
{
    public class MoneySpecs
    {
        [Fact]
        public void SumOfTwoMoneysProducesCorrectResult()
        {
            // Arrange
            var moneyOne = new Money(1, 2, 3, 4, 5, 6);
            var moneyTwo = new Money(1, 2, 3, 4, 5, 6);

            //Act
            var sum = moneyOne + moneyTwo;

            //Assert
            sum.FiveCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.TwentyCentCount.Should().Be(6);
            sum.FiftyCentCount.Should().Be(8);
            sum.OneEuroCount.Should().Be(10);
            sum.TwoEuroCount.Should().Be(12);
        }

        [Fact]
        public void TwoMoneyInstancesEqualIfTheyContainTheSameAmount()
        {
            var moneyOne = new Money(1, 2, 3, 4, 5, 6);
            var moneyTwo = new Money(1, 2, 3, 4, 5, 6);

            moneyOne.Should().Be(moneyTwo);
            moneyOne.GetHashCode().Should().Be(moneyTwo.GetHashCode());

        }

        [Fact]
        public void TwoMoneyInstancesNoEqualIfTheyContainDifferentAmounts()
        {
            var oneEuro = OneEuro;
            var hundredCents = new Money(10, 0, 0, 0, 0, 0);

            oneEuro.Should().NotBe(hundredCents);
            oneEuro.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1,0,0,0,0,0)]
        [InlineData(0, -10, 0, 0, 0, 0)]
        [InlineData(0, 0, -20, 0, 0, 0)]
        [InlineData(0, 0, 0, -30, 0, 0)]
        [InlineData(0, 0, 0, 0, -40, 0)]
        [InlineData(0, 0, 0, 0, 0, -50)]
        public void CanNotCreateMoneyWithNegativeValue(int fiveCentCount, int tenCentCount, int twentyCentCount,
           int fiftyCentCount, int oneEuroCount, int twoEuroCount)
        {
            Action action = () => 
            new Money(fiveCentCount,
                      tenCentCount,
                      twentyCentCount,
                      fiftyCentCount,
                      oneEuroCount,
                      twoEuroCount);
            Assert.Throws<InvalidOperationException>(action);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 2, 0, 5, 0, 0, 2.75)]
        [InlineData(0, 1, 1, 1, 0, 1, 2.8)]
        [InlineData(10, 0, 0, 10, 0, 0, 5.5)]
        [InlineData(0, 0, 5, 0, 3, 10, 24)]
        [InlineData(1, 0, 0, 0, 0, 100, 200.05)]
        public void AmountIsCalculatedCorrectly(int fiveCentCount, int tenCentCount, int twentyCentCount,
           int fiftyCentCount, int oneEuroCount, int twoEuroCount, decimal expectedAmount)
        {
               var money = new Money(fiveCentCount,
                      tenCentCount,
                      twentyCentCount,
                      fiftyCentCount,
                      oneEuroCount,
                      twoEuroCount);
            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void SubtracionOfTwoMoneysProducesCorrectResult()
        {
            var moneyOne = new Money(1, 2, 3, 4, 5, 6);
            var moneyTwo = new Money(1, 0, 3, 0, 5, 0);

            var subtraction = moneyOne - moneyTwo;

            subtraction.FiveCentCount.Should().Be(0);
            subtraction.TenCentCount.Should().Be(2);
            subtraction.TwentyCentCount.Should().Be(0);
            subtraction.FiftyCentCount.Should().Be(4);
            subtraction.OneEuroCount.Should().Be(0);
            subtraction.TwoEuroCount.Should().Be(6);
        }

        [Fact]
        public void CanNotSubtractMoreThanExist()
        {
            var moneyOne = TwentyCent;
            var moneyTwo = TenCent;

            Assert.Throws<InvalidOperationException>(()=> moneyOne - moneyTwo);
        }
    }
}
