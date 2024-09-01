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
            Console.WriteLine($"The book {book.Title} has been added to the Library");
        }

        public void BorrowBook(string title, string borrower)
        {
            var book = FindBook(title);
            if (book != null)
            {
                book.Borrow(borrower);
            }
            else
            {
                Console.WriteLine($"The book '{title}' was not found in the library.");
            }
        }

        public void ReturnBook(string title)
        {
            var book = FindBook(title);
            if (book != null)
            {
                book.Return();
            }
            else
            {
                Console.WriteLine($"The book '{title}' was not found in the library.");
            }
        }

        public void RemoveBook(string title)
        {
            var book = FindBook(title);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"The book '{title}' has been removed from the library.");
            }
            else
            {
                Console.WriteLine($"The book '{title}' was not found in the library.");
            }
        }

        private Book? FindBook(string title)
        {
            foreach (var book in books)
            {
                if (book.Title == title)
                {
                    return book;
                }
            }
            return new Book(title);
        }
    }
}
