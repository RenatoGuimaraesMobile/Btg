#if WINDOWS
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
#endif

namespace Btg.Helpers
{
    public static class WindowHelper
    {
        public static void OpenCenteredWindow(Page page, string title = "Nova Janela", int width = 850, int height = 1000)
        {
            var newWindow = new Microsoft.Maui.Controls.Window
            {
                Page = page,
                Title = title
            };

            Application.Current.OpenWindow(newWindow);

            #if WINDOWS
                // Aguarda a criação da janela para centralizar
                Task.Delay(10).ContinueWith(_ =>
                {
                    var nativeWindow = newWindow.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
                    if (nativeWindow == null)
                        return;

                    var hWnd = WindowNative.GetWindowHandle(nativeWindow);
                    var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                    var appWindow = AppWindow.GetFromWindowId(windowId);

                    var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
                    var workArea = displayArea.WorkArea;

                    int x = workArea.X + (workArea.Width - width) / 2;
                    int y = workArea.Y + (workArea.Height - height) / 2;

                    appWindow.MoveAndResize(new RectInt32(x, y, width, height));
                }, TaskScheduler.FromCurrentSynchronizationContext());
            #endif
        }
    }
}
