using System.Net;

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

        public void BorrowBook(string title, Card borrower)
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

        public Book GetBook(int id) => books.First(book => book.Id == id);

        public FindResult FindBook(string title)
        {
            var booksWithTitle = books.Where(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

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

        public Card GetCard(int id) => cards.First(card => card.Id == id);

        public void RemoveCard(Card card) => RemoveCard(card.Id);
        public void RemoveCard(int id)
        {
            var card = cards.FirstOrDefault(card => card.Id == id);
            if (card != null)
            {
                cards.Remove(card);
                Console.WriteLine($"The card of ID '{card.Id}' belonging to '{card.Holder}' has been removed from the library.");
            }
            else
            {
                Console.WriteLine($"The book of ID '{id}' has not been issued by the library.");
            }
        }

        // There's no method to find by name since library card holder could have the same name
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
