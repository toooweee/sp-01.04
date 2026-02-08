using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pz26
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GoPage1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page1));
        }

        private void GoPage2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }

        private void GoPage3_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page3));
        }
    }
}
