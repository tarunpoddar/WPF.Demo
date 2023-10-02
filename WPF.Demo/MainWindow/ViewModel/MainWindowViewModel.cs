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
        /// Gets or sets wether the name is entered or not.
        /// </summary>
        private bool aIsTextEntered;

        public bool IsTextEntered
        {
            get => aIsTextEntered;
            set => SetProperty(ref aIsTextEntered, value);
        }

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        private string? aName;

        public string? Name
        {
            get => aName;
            set
            {
                if (aName != null)
                {
                    IsTextEntered = true;
                }

                SetProperty(ref aName, value);
            }
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
        /// Gets or sets wether the payment mode error text is visible.
        /// </summary>
        private bool aIsPaymentModeErrorTextVisible;
        public bool IsPaymentModeErrorTextVisible
        {
            get => aIsPaymentModeErrorTextVisible;
            set => SetProperty(ref aIsPaymentModeErrorTextVisible, value);
        }

        /// <summary>
        /// Gets or sets the payment mode error text.
        /// </summary>
        private string? aPaymentModeErrorText;
        public string? PaymentModeErrorText
        {
            get => aPaymentModeErrorText;
            set => SetProperty(ref aPaymentModeErrorText, value);
        }

        /// <summary>
        /// Gets or sets wether the online radio button is checked.
        /// </summary>
        private bool aIsOnlineRadioButtonChecked;
        public bool IsOnlineRadioButtonChecked
        {
            get => aIsOnlineRadioButtonChecked;
            set
            {
                SetProperty(ref aIsOnlineRadioButtonChecked, value);
                IsPaymentModeErrorTextVisible = false;
            }
        }

        /// <summary>
        /// Gets or sets wether the cash radio button is checked.
        /// </summary>
        private bool aIsCashRadioButtonChecked;
        public bool IsCashRadioButtonChecked
        {
            get => aIsCashRadioButtonChecked;
            set
            {
                SetProperty(ref aIsCashRadioButtonChecked, value);
                IsPaymentModeErrorTextVisible = false;
            }
        }

        /// <summary>
        /// Gets or sets wether the submit button is checked.
        /// </summary>
        private bool aSubmitButtonClicked;
        public bool SubmitButtonClicked
        {
            get => aSubmitButtonClicked;
            set => SetProperty(ref aSubmitButtonClicked, value);
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

        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            SelectProductCommand = new RelayCommand<string>(OnProductSelected);

            DecreaseQuantityCommand = new RelayCommand(OnDecreaseQuantity, CanDecreaseQuantity);

            IncreaseQuantityCommand = new RelayCommand(OnincreaseQuantity);

            PaymentModeSelectedCommand = new RelayCommand<string>(PaymentModeSelected);

            SubmitCommand = new RelayCommand(OnSubmit);

            FilteredProducts = new ObservableCollection<string>();

            Name = string.Empty;

            Quantity = 1;

            DateOfPurchase = DateTime.Today;

            IsPaymentModeErrorTextVisible = false;

            PaymentModeErrorText = "Please select a payment mode.";

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

        #endregion

        #region Members

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
            return Quantity > 1;
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
        /// <param name="theProduct">The selected product.</param>
        private void OnProductSelected(string theProduct)
        {
            Product = theProduct;
            FilteredProducts?.Clear();
            IsPopupOpen = false; // Close the popup
        }

        /// <summary>
        /// Executes when submit button is clicked.
        /// </summary>
        private void OnSubmit()
        {
            SubmitButtonClicked = true;

            if (!IsOnlineRadioButtonChecked && !IsCashRadioButtonChecked)
            {
                IsPaymentModeErrorTextVisible = true;
                return;
            }

            MessageBox.Show($"Name: {Name}\n" +
                $"Gender: {Gender}\n" +
                $"Product: {Product}\n" +
                $"Quantity: {Quantity}\n" +
                $"Mode of Payment: {myPaymentMode}\n" +
                $"Date of Purchase: {DateOfPurchase.ToShortDateString()}");
        }

        #endregion
    }
}
