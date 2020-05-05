using DDDSample.Logic;
using FluentAssertions;
using System;
using Xunit;

using static DDDSample.Logic.Money;

namespace DDDSample.Test
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void ReturnMoneyEmptiesMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(OneEuro);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [Fact]
        public void InsertedMoneyGoToMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(TenCent);
            snackMachine.InsertMoney(OneEuro);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.1m);
        }

        [Fact]
        public void CanNotInsertMoreThanOneCoinAtTheTime()
        {
            var snackMachine = new SnackMachine();
            var thirtyCent = TwentyCent + TenCent;

            Assert.Throws<InvalidOperationException>(() => snackMachine.InsertMoney(thirtyCent));
        }

        [Fact]
        public void MoneyInTransactionGoToMoneyInsideAfterPurchase()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(OneEuro);
            snackMachine.InsertMoney(OneEuro);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }
    }
}
