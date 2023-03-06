using booklook.ViewModels;

namespace booklook.Models
{
    public class Book {
        public string Title { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public string Isbn13 { get; set; } = string.Empty;
        public string Isbn10 { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string BookLink { get; set; } = string.Empty;

        /// <summary>
        ///     Create a book from a google api response 
        /// </summary>
        /// <param name="book"></param>
        /// <returns>
        ///     Book with data from response
        /// </returns>
        public Book CreateFromResponse(Item book) {
            Title = book.VolumeInfo.Title;
            Authors = String.Join(", ", book.VolumeInfo.Authors.ToArray());
            ImageSource = $"https://covers.openlibrary.org/b/isbn/{book.VolumeInfo.IndustryIdentifiers.First().Identifier}-L.jpg";
            Isbn10 = book.VolumeInfo.IndustryIdentifiers.Last().Identifier;
            Isbn13 = book.VolumeInfo.IndustryIdentifiers.First().Identifier;
            ReleaseDate = book.VolumeInfo.PublishedDate;
            BookLink = book.VolumeInfo.InfoLink;

            return this;
        }

        /// <summary>
        ///     Create a book from the BookViewModel BindingContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        ///     Book with data from BindingContext
        /// </returns>
        public Book CreateFromContext(BookViewModel context) {
            Title = context.Title;
            Authors = context.Authors;
            ImageSource = context.ImageSource;
            Isbn13 = context.Isbn13;
            Isbn10 = context.Isbn10;
            ReleaseDate = context.ReleaseDate;
            BookLink = context.BookLink;

            return this;
        }
    }
}
