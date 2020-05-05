using DDDSample.Logic;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Windows.Input;

using static DDDSample.Logic.Money;

namespace DDDSample.ViewModel
{
    public class SnackMachineViewModel : BaseViewModel
    {
        private readonly SnackMachine snackMachine;

        public ICommand InsertFiveCentCommand { get; }
        public ICommand InsertTenCentCommand { get; }
        public ICommand InsertTwentyCentCommand { get; }
        public ICommand InsertFiftyCentCommand { get; }
        public ICommand InsertOneEuroCommand { get; }
        public ICommand InsertTwoEuroCommand { get; }
        public ICommand BuySnackCommand { get; }
        public ICommand ReturnMoneyCommand { get; }

        public SnackMachineViewModel(SnackMachine snackMachine)
        {
            this.snackMachine = snackMachine;
            Title = "Snack Machine";

            InsertFiveCentCommand = new Command(execute: () => InsertMoneyAction(FiveCent));
            InsertTenCentCommand = new Command(execute: () => InsertMoneyAction(TenCent));
            InsertTwentyCentCommand = new Command(execute: () => InsertMoneyAction(TwentyCent));
            InsertFiftyCentCommand = new Command(execute: () => InsertMoneyAction(FiftyCent));
            InsertOneEuroCommand = new Command(execute: () => InsertMoneyAction(OneEuro));
            InsertTwoEuroCommand = new Command(execute: () => InsertMoneyAction(TwoEuro));

            BuySnackCommand = new Command(execute: BuySnackAction);
            ReturnMoneyCommand = new Command(execute: ReturnMoneyAction);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        private string message = string.Empty;

        private void InsertMoneyAction(Money money)
        {
            Message = $"You have inserted {money} €";
            snackMachine.InsertMoney(money);
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(AllMoneyInside));
        }

        private void ReturnMoneyAction()
        {
            Message = $"You have got {snackMachine.MoneyInTransaction} € returned";
            snackMachine.ReturnMoney();
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(AllMoneyInside));
        }

        private void BuySnackAction()
        {
            Message = $"You bought a snack for {snackMachine.MoneyInTransaction} €";
            snackMachine.BuySnack();
            OnPropertyChanged(nameof(MoneyInTransaction));
            OnPropertyChanged(nameof(AllMoneyInside));
        }

        public string AllMoneyInside
        {
            get
            {
                var allMoney = snackMachine.MoneyInTransaction + snackMachine.MoneyInside;
                return $"All Money inside: {allMoney} €";
            }
        }

        public string MoneyInTransaction => $"Money inserted: {snackMachine.MoneyInTransaction} €";

    }
}
