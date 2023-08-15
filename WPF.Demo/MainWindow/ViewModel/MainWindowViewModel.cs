using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPF.Common.Mvvm;

namespace WPF.Demo.MainWindow.ViewModel
{
    class MainWindowViewModel : ObservableObject
    {
        public ICommand? CreateCommand { get; set; }
        public ICommand? ReadCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? SubmitCommand { get; set; }
        public ICommand? MouseDownCommand { get; set; }

        /// <summary>
        /// Customer's name.
        /// </summary>
        private string? aName;

        public string? Name
        {
            get => aName;
            set => SetProperty(ref aName, value);
        }

        /// <summary>
        /// Collection of Genders.
        /// </summary>
        public ObservableCollection<string> Genders { get; set; }

        /// <summary>
        /// The selected gender.
        /// </summary>
        private string? aSelectedGender;

        public string? SelectedGender
        {
            get => aSelectedGender;
            set => SetProperty(ref aSelectedGender, value);
        }

        public ObservableCollection<string> Products { get; } = new ObservableCollection<string>();

        private ObservableCollection<string>? _filteredSuggestions;
        public ObservableCollection<string>? FilteredSuggestions
        {
            get => _filteredSuggestions;
            set => SetProperty(ref _filteredSuggestions, value);
        }

        private string? aSearchProduct;

        public string? SearchProduct
        {
            get => aSearchProduct;
            set
            {
                SetProperty(ref aSearchProduct, value);
                UpdateFilteredSuggestions();
            }
        }

        public ICommand SelectSuggestionCommand { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindowViewModel()
        {
            SelectSuggestionCommand = new RelayCommand<string>(SelectSuggestion);
            FilteredSuggestions = new ObservableCollection<string>();

            // CreateCommand = new RelayCommand
            Name = string.Empty;
            Genders = new ObservableCollection<string>()
            {
                "Select", "Male", "Female", "Others"
            };
            SelectedGender = Genders.FirstOrDefault();

            Products = new ObservableCollection<string>()
            {
                "Cement", "Rod", "Bali", "Pathor", "Taar"
            };
        }

        private void UpdateFilteredSuggestions()
        {
            if (SearchProduct != null)
            {
                var filteredSuggestions = Products.Where(s => s.Contains(SearchProduct, System.StringComparison.OrdinalIgnoreCase));
                FilteredSuggestions = new ObservableCollection<string>(filteredSuggestions);
            }
        }

        /// <summary>
        /// This is called on selection of a text in the popup.
        /// </summary>
        /// <param name="suggestion"></param>
        private void SelectSuggestion(string suggestion)
        {
            SearchProduct = suggestion;
            FilteredSuggestions?.Clear();
        }
    }
}
