using Btg.Messages;
using Btg.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using WinRT.Interop;
#endif

namespace Btg.Views;

public partial class ClienteFormPage : ContentPage
{
    public ClienteFormPage(ClienteFormViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
      
        WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (_, msg) =>
        {
            #if WINDOWS
                        if (this.Window?.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
                        {
                            nativeWindow.Close();
                        }
            #endif
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}