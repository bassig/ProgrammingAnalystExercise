namespace ProgrammingAnalystExercise
{
    public class Library
    {
        private readonly List<Book> books = [];
        private readonly List<Card> cards = [];

        public Library()
        {
            books.Add(new(books.Count, "The Great Gatsby"));
            books.Add(new(books.Count, "1984"));
            books.Add(new(books.Count, "To Kill a Mockingbird"));
            cards.Add(new(0, "Ploni"));
        }

        public Book AddBook(string title)
        {
            var book = new Book(books.Max(book => book.Id) + 1, title);
            books.Add(book);
            Console.WriteLine($"The book {book.Title} has been added to the Library");
            return book;
        }

        /// <returns>First the book that was borrowed (null if it failed), and second a status representing the result of an attempt to borrow a specific title</returns>
        public (Book?, BorrowBookResult) BorrowBook(string title, Card borrower)
        {
            var bookFind = FindBook(title);
            if (bookFind.TotalCoppies > 0)
            {
                if (bookFind.Availible.Length > 0)
                {
                    var book = bookFind.Availible.First();
                    // Check to see if borrower already has another copy of this book
                    if (!bookFind.Borrowed.Any(book => book.BorrowedBy?.Equals(borrower) ?? false))
                    {
                        book.Borrow(borrower);
                        return (book, BorrowBookResult.Success);
                    }
                    else
                    {
                        Console.WriteLine($"Customer {borrower} already has borrowed a copy of the book {title}");
                        return (null, BorrowBookResult.CustomerAlreadyBorrowed);
                    }
                }
                else
                {
                    Console.WriteLine($"All coppies of '{title}' are currently checked out");
                    return (null, BorrowBookResult.NoCopiesAvailible);
                }
            }
            else
            {
                Console.WriteLine($"The book '{title}' was not found in the library.");
                return (null, BorrowBookResult.BookNotFound);
            }
        }

        /// <returns>True if successful or false otherwise</returns>
        public bool ReturnBook(Book book) => ReturnBook(book.Id);
        /// <returns>True if successful or false otherwise</returns>
        public bool ReturnBook(int bookId)
        {
            var book = books.FirstOrDefault(book => book.Id == bookId);
            if (book != null)
            {
                book.Return();
                return true;
            }
            else
            {
                Console.WriteLine($"The book of ID '{bookId}' was not found in the library.");
                return false;
            }
        }

        /// <returns>True if successful or false otherwise</returns>
        public bool RemoveBook(Book book) => RemoveBook(book.Id);
        /// <returns>True if successful or false otherwise</returns>
        public bool RemoveBook(int bookId)
        {
            var book = books.FirstOrDefault(book => book.Id == bookId);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"The book of ID '{book.Id}' of title '{book.Title}' has been removed from the library.");
                return true;
            }
            else
            {
                Console.WriteLine($"The book of ID '{bookId}' was not found in the library.");
                return false;
            }
        }

        public Book? GetBook(int id) => books.FirstOrDefault(book => book.Id == id);


        public FindResult FindBook(string? title = null)
        {
            var booksWithTitle = !string.IsNullOrWhiteSpace(title)
                ? books.Where(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                : books;

            return new FindResult
            {
                Borrowed = booksWithTitle.Where(book => book.BorrowedBy != null).ToArray(),
                Availible = booksWithTitle.Where(book => book.BorrowedBy == null).ToArray()
            };
        }

        public Card AddCard(string holderName)
        {
            var card = new Card(cards.Max(book => book.Id) + 1, holderName);
            cards.Add(card);
            Console.WriteLine($"A new library card has been issued to '{card.Holder}'");
            return card;
        }

        public Card? GetCard(int id) => cards.FirstOrDefault(card => card.Id == id);

        /// <returns>True if successful or false otherwise</returns>
        public bool RemoveCard(Card card) => RemoveCard(card.Id);
        public bool RemoveCard(int id)
        {
            var card = cards.FirstOrDefault(card => card.Id == id);
            if (card != null)
            {
                cards.Remove(card);
                Console.WriteLine($"The card of ID '{card.Id}' belonging to '{card.Holder}' has been removed from the library.");
                return true;
            }
            else
            {
                Console.WriteLine($"The book of ID '{id}' has not been issued by the library.");
                return false;
            }
        }

        // There's no method to find by name since library card holder could have the same name
        public Book[] FindBooksBorrowedBy(Card card) => FindBooksBorrowedBy(card.Id);
        public Book[] FindBooksBorrowedBy(int cardId) => books.Where(book => book.BorrowedBy?.Id == cardId).ToArray();

        public string[] GetCatalogue(bool onlyAvailible = false)
        {
            var titles = new List<string>();

            foreach (var book in books)
            {
                if (!titles.Any(title => title.Equals(book.Title, StringComparison.OrdinalIgnoreCase)))
                {
                    if (onlyAvailible && book.BorrowedBy != null) continue;
                    titles.Add(book.Title);
                }
            }

            return titles.ToArray();
        }  
    }
}
