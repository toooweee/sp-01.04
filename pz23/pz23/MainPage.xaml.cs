using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace pz23
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<string> bookmarks = new List<string>();
        public MainPage()
        {
            InitializeComponent();

            webView.NavigationStarting += WebView_NavigationStarting;
            webView.NavigationCompleted += WebView_NavigationCompleted;

            // стартовая страница
            Navigate("https://www.microsoft.com");
        }

        private void Navigate(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            try
            {
                Uri uri = new Uri(url);
                webView.Navigate(uri);
            }
            catch
            {
                UrlBox.Text = "Некорректный URL";
            }
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Navigate(UrlBox.Text);
        }

        private void UrlBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Navigate(UrlBox.Text);
            }
        }

        private void AddBookmark_Click(object sender, RoutedEventArgs e)
        {
            string url = UrlBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(url))
                return;

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            if (!bookmarks.Contains(url))
                bookmarks.Add(url);

            UpdateBookmarksMenu();
        }

        private void UpdateBookmarksMenu()
        {
            BookmarksFlyout.Items.Clear();

            if (bookmarks.Count == 0)
            {
                BookmarksFlyout.Items.Add(new MenuFlyoutItem
                {
                    Text = "(пока пусто)",
                    IsEnabled = false
                });
                return;
            }

            foreach (var url in bookmarks)
            {
                var item = new MenuFlyoutItem
                {
                    Text = url
                };

                item.Click += (s, e) =>
                {
                    UrlBox.Text = url;
                    Navigate(url);
                };

                BookmarksFlyout.Items.Add(item);
            }
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri != null && args.Uri.Host.Contains("yandex.ru"))
            {
                args.Cancel = true;
                UrlBox.Text = "Переход запрещен";
            }
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.Uri != null)
                UrlBox.Text = args.Uri.ToString();
        }
    }
}
