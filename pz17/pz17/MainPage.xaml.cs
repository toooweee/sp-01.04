using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pz17
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Student MyStudent { get; } = new Student();

        public MainPage()
        {
            InitializeComponent();

            Button button = new Button();
            button.Content = "из кода";
            button.Height = 200;
            button.Width = 600;

            container.Children.Add(button);
        }

        private void on_click(object sender, RoutedEventArgs e)
        {
            button.Content = "Изменне";
        }
    }
}
