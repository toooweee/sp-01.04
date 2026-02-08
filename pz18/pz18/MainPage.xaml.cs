using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pz18
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

        private void TogglePaneClick(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;

            if (mySplitView.IsPaneOpen)
            {
                statusText.Text = "успешная активация Pane";
            }
            else
            {
                statusText.Text = "";
            }
        }
    }
}
