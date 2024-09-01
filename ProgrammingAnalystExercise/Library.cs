namespace ProgrammingAnalystExercise
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public Library()
        {
            books.Add(new Book("The Great Gatsby", books.Count));
            books.Add(new Book("1984", books.Count));
            books.Add(new Book("To Kill a Mockingbird", books.Count));
        }

        public Book AddBook(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("Book title can't be null or empty");
            var book = new Book(title, books.Max(book => book.Id) + 1);
            books.Add(book);
            Console.WriteLine($"The book {book.Title} has been added to the Library");
            return book;
        }

        public void BorrowBook(string title, string borrower)
        {
            var bookFind = FindBook(title);
            if (bookFind.TotalCoppies > 0)
            {
                if (bookFind.Availible.Length > 0)
                {
                    var book = bookFind.Availible.First();
                    // Check to see if borrower already has another copy of this book
                    if (!bookFind.Borrowed.Any(book => book.BorrowedBy?.Equals(borrower, StringComparison.OrdinalIgnoreCase) ?? false))
                    {
                        book.Borrow(borrower);
                    }
                    else
                    {
                        Console.WriteLine($"Customer {borrower} already has borrowed a copy of the book {title}");
                    }
                }
                else
                {
                    Console.WriteLine($"All coppies of '{title}' are currently checked out");
                }
            }
            else
            {
                Console.WriteLine($"The book '{title}' was not found in the library.");
            }
        }

        public void ReturnBook(Book book) => ReturnBook(book.Id);
        public void ReturnBook(int bookId)
        {
            var book = books.FirstOrDefault(book => book.Id == bookId);
            if (book != null)
            {
                book.Return();
            }
            else
            {
                Console.WriteLine($"The book of ID '{bookId}' was not found in the library.");
            }
        }

        public void RemoveBook(Book book) => RemoveBook(book.Id);
        public void RemoveBook(int bookId)
        {
            var book = books.FirstOrDefault(book => book.Id == bookId);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"The book of ID '{book.Id}' of title '{book.Title}' has been removed from the library.");
            }
            else
            {
                Console.WriteLine($"The book of ID '{bookId}' was not found in the library.");
            }
        }

        public FindResult FindBook(string title)
        {
            var booksWithTitle = books.Where(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            return new FindResult
            {
                Borrowed = booksWithTitle.Where(book => book.BorrowedBy != null).ToArray(),
                Availible = booksWithTitle.Where(book => book.BorrowedBy == null).ToArray()
            };
        }
    }
}
