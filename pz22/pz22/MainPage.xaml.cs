using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pz22
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public OrderViewModel Order { get; set; } = new OrderViewModel();
        public MainPage()
        {
            InitializeComponent();

            DataContext = Order;

            Order.CoffeePrice = 2.0;
            Order.SizePrice = 0.0;
        }

        private void Americano_Checked(object sender, RoutedEventArgs e)
        {
            Order.CoffeePrice = 2.0;
        }

        private void Latte_Checked(object sender, RoutedEventArgs e)
        {
            Order.CoffeePrice = 3.0;
        }

        private void Cappuccino_Checked(object sender, RoutedEventArgs e)
        {
            Order.CoffeePrice = 3.5;
        }

        private void Small_Checked(object sender, RoutedEventArgs e)
        {
            Order.SizePrice = 0.0;
        }

        private void Medium_Checked(object sender, RoutedEventArgs e)
        {
            Order.SizePrice = 0.5;
        }

        private void Large_Checked(object sender, RoutedEventArgs e)
        {
            Order.SizePrice = 1.0;
        }
    }
}
