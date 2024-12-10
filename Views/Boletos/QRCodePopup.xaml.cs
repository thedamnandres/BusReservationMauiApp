using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Views.Boletos;

public partial class QRCodePopup : ContentPage
{
    public QRCodePopup(string qrCodePng)
    {
        InitializeComponent();
        QRCodeImage.Source = qrCodePng;

    }

    private async void OnClosePopupClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
