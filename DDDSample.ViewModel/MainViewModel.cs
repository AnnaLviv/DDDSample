using DDDSample.Logic;
using MvvmHelpers.Commands;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace DDDSample.ViewModel
{
    public class MainViewModel
    {
        public SnackMachineViewModel SnackMachineViewModel { get; }

        public MainViewModel()
        {
            OpenAcknowledgementsFileCommand = new Command(OpenAcknowledgementsFile);
            SnackMachineViewModel = new SnackMachineViewModel(new SnackMachine());
        }

        public ICommand OpenAcknowledgementsFileCommand { get; }
        private void OpenAcknowledgementsFile()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            var directory = Directory.GetCurrentDirectory();
            var acknowledgementsFile = Path.Combine(directory, "Open_Source_Acknowledgements.txt");

            var process = new Process();
            process.StartInfo = new ProcessStartInfo(acknowledgementsFile)
            {
                UseShellExecute = true
            };
            process.Start();
        }
    }
}
