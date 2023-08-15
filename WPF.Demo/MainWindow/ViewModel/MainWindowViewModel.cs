using System.Windows.Input;
using WPF.Common.Mvvm;

namespace WPF.Demo.MainWindow.ViewModel
{
    class MainWindowViewModel: ObservableObject
    {
        public ICommand? CreateCommand { get; set; }
        public ICommand? ReadCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }

        public MainWindowViewModel()
        {
           // CreateCommand = new RelayCommand
        }
    }
}
