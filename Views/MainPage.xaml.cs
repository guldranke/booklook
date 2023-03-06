using booklook.Helpers;
using booklook.Models;
using booklook.ViewModels;
using ZXing.Net.Maui;

namespace booklook.Views;

public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = false,
        };
    }

    /// <summary>
    ///     On barcode detected, try to fetch the book details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BarcodeDetected(object sender, BarcodeDetectionEventArgs e) {
        MainViewModel context = (MainViewModel)BindingContext;
        context.IsScanning = false;

        MainThread.BeginInvokeOnMainThread(async () => {
            string barcode = e.Results.First().Value;

            RestService httpClient = new();

            try {
                GoogleBookResponse response = await httpClient.GetBook(barcode);

                if (response == null || response.TotalItems == 0) {
                    await DisplayAlert("Error", "Book not available on server", "Close");
                    context.IsScanning = true;
                    return;
                }

                Book book = new Book().CreateFromResponse(response.Items.First());

                BookViewModel bookViewModel = new() {
                    Title = book.Title ?? "Unknown",
                    Authors = book.Authors ?? "Unknown",
                    ImageSource = book.ImageSource ?? "Unknown",
                    Isbn10 = book.Isbn10 ?? "Unknown",
                    Isbn13 = book.Isbn13 ?? "Unknown",
                    ReleaseDate = book.ReleaseDate ?? "Unknown",
                    BookLink = book.BookLink ?? "Unknown"
                };

                await Navigation.PushAsync(new BookPage { BindingContext = bookViewModel });
            } catch {
                await DisplayAlert("Error", "Couldn't connect to server", "Close");
                context.IsScanning = true;
            }
        });
    }

    protected override void OnAppearing() {
        base.OnAppearing();

        MainThread.BeginInvokeOnMainThread(() => {
            MainViewModel mainViewModel = new() { IsScanning = true };
            BindingContext = mainViewModel;
        });
    }
}