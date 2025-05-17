using Btg.Services;
using Btg.Views;
using Btg.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace Btg
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windowsLifecycleBuilder =>
                    {
                        windowsLifecycleBuilder.OnWindowCreated(window =>
                        {
                            window.ExtendsContentIntoTitleBar = true;
                            var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                            switch (appWindow.Presenter)
                            {
                                case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                                    overlappedPresenter.SetBorderAndTitleBar(true, true);
                                    overlappedPresenter.Maximize();
                                    break;
                            }
                        });
                    });
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IClienteService, ClienteService>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<ClienteFormViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ClienteFormPage>();

            return builder.Build();
        }
    }
}
