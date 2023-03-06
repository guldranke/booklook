using booklook.Helpers;
using booklook.Models;
using booklook.ViewModels;

namespace booklook.Views;

public partial class BookPage : ContentPage {
    public BookPage() {
        InitializeComponent();
    }


    /// <summary>
    ///     On button click, try to upload a book to the server with a set location 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>    
    private async void UploadBook(object sender, EventArgs e) {
        BookViewModel context = (BookViewModel)BindingContext;

        string location = await DisplayActionSheet("Choose a location", "Cancel", null, "Næstved", "Odense");

        if (location == "Cancel") {
            return;
        }

        context.IsLoading = true;
        BindingContext = context;

        Book book = new Book().CreateFromContext(context);
        RestService httpClient = new();

        try {
            HttpResponseMessage response = await httpClient.PostBook(book, location);

            if (!response.IsSuccessStatusCode) {
                await DisplayAlert("Error", "Couldn't upload the book", "Close");
                return;
            }

            context.IsLoading = false;
            BindingContext = context;

            await DisplayAlert("Success", "Book has been uploaded", "OK");
            await Navigation.PopAsync();
        } catch {
            context.IsLoading = false;
            BindingContext = context;

            await DisplayAlert("Error", "Couldn't connect to server", "Close");
        }
    }
}