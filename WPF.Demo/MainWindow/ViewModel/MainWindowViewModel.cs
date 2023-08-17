using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.Common.Mvvm;

namespace WPF.Demo.MainWindow.ViewModel
{
    /// <summary>
    /// View Model class for the main window.
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

        /// <summary>
        /// Command to execute when quantity is increased.
        /// </summary>
        public ICommand? IncreaseQuantityCommand { get; set; }

        /// <summary>
        /// Command to execute when quantity is decreased.
        /// </summary>
        public ICommand? DecreaseQuantityCommand { get; set; }

        /// <summary>
        ///  Command to execute when a payment mode is selected.
        /// </summary>
        public ICommand? PaymentModeSelectedCommand { get; set; }

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
        private bool aIsPopupOpen;
        public bool IsPopupOpen
        {
            get => aIsPopupOpen;
            set => SetProperty(ref aIsPopupOpen, value);
        }

        /// <summary>
        ///  Gets or sets the quantity of the product.
        /// </summary>
        private int aQuantity;
        public int Quantity
        {
            get => aQuantity;
            set => SetProperty(ref aQuantity, value);
        }

        /// <summary>
        /// Gets or sets the date of purchase.
        /// </summary>
        private DateTime aDateOfPurchase;
        public DateTime DateOfPurchase
        {
            get => aDateOfPurchase;
            set => SetProperty(ref aDateOfPurchase, value);
        }

        /// <summary>
        /// Gets or sets the collection of products.
        /// </summary>
        public ObservableCollection<string> Products { get; set; }

        /// <summary>
        /// Gets or sets the collection of Genders.
        /// </summary>
        public ObservableCollection<string> Genders { get; set; }

        #endregion

        #region Fields

        private string myPaymentMode = string.Empty;

        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            SelectProductCommand = new RelayCommand<string>(OnProductedSelected);

            DecreaseQuantityCommand = new RelayCommand(OnDecreaseQuantity, CanDecreaseQuantity);

            IncreaseQuantityCommand = new RelayCommand(OnincreaseQuantity);

            PaymentModeSelectedCommand = new RelayCommand<string>(PaymentModeSelected);

            SubmitCommand = new RelayCommand(OnSubmit);

            FilteredProducts = new ObservableCollection<string>();

            Name = string.Empty;

            DateOfPurchase = DateTime.Today;

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
        /// Handles when a payment mode radio is selected.
        /// </summary>
        /// <param name="thePaymentMode">The payment mode.</param>
        private void PaymentModeSelected(string thePaymentMode)
        {
            myPaymentMode = thePaymentMode;
        }

        /// <summary>
        /// Increases the quantity of the product.
        /// </summary>
        private void OnincreaseQuantity()
        {
            Quantity++;
        }

        /// <summary>
        /// Decreases the quantity of the product.
        /// </summary>
        private void OnDecreaseQuantity()
        {
            Quantity--;
        }

        /// <summary>
        /// Checks whether the quantity of the product can be decreased further.
        /// </summary>
        private bool CanDecreaseQuantity()
        {
            return Quantity > 0;
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

        private void OnSubmit()
        {
            MessageBox.Show($"Name: {Name}\n" +
                $"Gender: {Gender}\n" +
                $"Product: {Product}\n" +
                $"Quantity: {Quantity}\n" +
                $"Mode of Payment: {myPaymentMode}\n" +
                $"Date of Purchase: {DateOfPurchase.ToShortDateString()}");
        }
    }
}
