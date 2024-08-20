namespace ProgrammingAnalystExercise
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public Library()
        {
            books.Add(new Book("The Great Gatsby"));
            books.Add(new Book("1984"));
            books.Add(new Book("To Kill a Mockingbird"));
        }

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"The book {book.Title}, Barcode: {book.Barcode} has been added to the Library");
        }

        public void BorrowBook(string barcode)
        {
            var book = FindBookByBarcode(barcode);
            if (book != null)
            {
                book.Borrow();
            }
        }

        public void ReturnBook(string barcode)
        {
            var foundbook = FindBookByBarcode(barcode);
            if (foundbook != null)
            {
                foundbook.Return();
            }
        }

        public void RemoveBook(string barcode)
        {
            var book = FindBookByBarcode(barcode);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"The book '{book.Title}' Barcode: '{book.Barcode}' has been removed from the library.");
            }
        }

        private Book? FindBookByBarcode(string barcode)
        {
            var foundBook =  books.FirstOrDefault(book => book.Barcode == barcode);
            if (foundBook != null)
            {
                return foundBook;
            }
            else
            {
                Console.WriteLine($"The book with Barcode: '{barcode}' was not found in the library.");
                return null;
            }
        }

        public string? FindAvailableBookBarcodeByTitle(string title)
        {
            var foundBook = books.FirstOrDefault( book => book.Title == title && book.IsBorrowed);
            if (foundBook != null)
            {
                return foundBook.Barcode;
            }
            return null;
        }
    }
}
