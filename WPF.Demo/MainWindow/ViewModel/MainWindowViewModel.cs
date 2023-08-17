using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPF.Common.Mvvm;

namespace WPF.Demo.MainWindow.ViewModel
{
    /// <summary>
    /// ViewModel class for the main window.
    /// </summary>
    class MainWindowViewModel : ObservableObject
    {
        #region Commands

        public ICommand? CreateCommand { get; set; }
        public ICommand? ReadCommand { get; set; }
        public ICommand? UpdateCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public ICommand? SubmitCommand { get; set; }

        /// <summary>
        /// Command to execute when a product is selected from the popup.
        /// </summary>
        public ICommand SelectProductCommand { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        private string? aName;

        public string? Name
        {
            get => aName;
            set => SetProperty(ref aName, value);
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        private string? aGender;

        public string? Gender
        {
            get => aGender;
            set => SetProperty(ref aGender, value);
        }

        /// <summary>
        /// Gets or sets the filtered products.
        /// </summary>
        private ObservableCollection<string>? _filteredProducts;
        public ObservableCollection<string>? FilteredProducts
        {
            get => _filteredProducts;
            set => SetProperty(ref _filteredProducts, value);
        }

        /// <summary>
        /// Gets or sets the search product text.
        /// </summary>
        private string? aProduct;
        public string? Product
        {
            get => aProduct;
            set
            {
                SetProperty(ref aProduct, value);
                UpdateFilteredProducts();
            }
        }

        /// <summary>
        /// Gets or sets whether the popup is open.
        /// </summary>
        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        /// <summary>
        /// Gets or sets the collection of products.
        /// </summary>
        public ObservableCollection<string> Products { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets or sets the collection of Genders.
        /// </summary>
        public ObservableCollection<string> Genders { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            SelectProductCommand = new RelayCommand<string>(OnProductedSelected);

            FilteredProducts = new ObservableCollection<string>();

            Name = string.Empty;

            Genders = new ObservableCollection<string>()
            {
                "Select", "Male", "Female", "Others"
            };
            Gender = Genders.FirstOrDefault();

            Products = new ObservableCollection<string>()
            {
                "Cement", "Rod", "Bali", "Pathor", "Taar"
            };
        }

        /// <summary>
        /// Updates the filtered products in the popup.
        /// </summary>
        private void UpdateFilteredProducts()
        {
            if (Product != null)
            {
                var filteredSuggestions = Products.Where(s => s.Contains(Product, System.StringComparison.OrdinalIgnoreCase));
                FilteredProducts = new ObservableCollection<string>(filteredSuggestions);
                IsPopupOpen = FilteredProducts.Any();
            }
        }

        /// <summary>
        /// Executes when a product is selcted in the popup.
        /// </summary>
        /// <param name="suggestion"></param>
        private void OnProductedSelected(string suggestion)
        {
            Product = suggestion;
            FilteredProducts?.Clear();
            IsPopupOpen = false; // Close the popup
        }
    }
}
