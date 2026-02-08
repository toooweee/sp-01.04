using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;

namespace pz25
{
    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += OnResuming;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            string launchMessage = e.PreviousExecutionState switch
            {
                ApplicationExecutionState.NotRunning => "Первый запуск приложения или запуск после полного закрытия",
                ApplicationExecutionState.ClosedByUser => "Запуск после закрытия пользователем (Alt+F4 или крестик)",
                ApplicationExecutionState.Terminated => "Повторный запуск после завершения системой (Terminated). Можно загрузить сохранённые данные.",
                ApplicationExecutionState.Suspended => "Запуск после приостановки (редкий случай)",
                _ => "Запуск приложения"
            };

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();

                await ShowStateDialog("Запуск приложения", launchMessage);
            }
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load page '{e.SourcePageType.FullName}'.");
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            await ShowStateDialog("Приостановка приложения", "Приложение переходит в состояние Suspended. Сохраняем данные...");


            deferral.Complete();
        }

        private async void OnResuming(object sender, object e)
        {
            await ShowStateDialog("Возобновление приложения", "Приложение возобновлено после приостановки (Resuming)");
        }

        private async System.Threading.Tasks.Task ShowStateDialog(string title, string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }
}