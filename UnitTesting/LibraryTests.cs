using ProgrammingAnalystExercise;

namespace UnitTesting
{
    public class LibraryTests
    {
        [Fact]
        public void AddBook()
        {
            var library = new Library();

            var newBook = library.AddBook("Moby Dick");

            Assert.Equal(3, newBook.Id);
            Assert.Equal(4, library.GetCatalogue().Length);
        }

        [Fact]
        public void AddBookTitleNullOrWhitespace()
        {
            var library = new Library();

            Assert.Throws<ArgumentNullException>(() => library.AddBook(null));
            Assert.Throws<ArgumentNullException>(() => library.AddBook(""));
            Assert.Throws<ArgumentNullException>(() => library.AddBook(" "));
        }

        [Fact]
        public void RemoveBook()
        {
            var library = new Library();
            var book = library.GetBook(0);

            library.RemoveBook(0);

            Assert.Equal(2, library.GetCatalogue().Length);
        }

        [Fact]
        public void BorrowBook()
        {
            var library = new Library();
            var card = library.GetCard(0);

            var (book, result) = library.BorrowBook("The Great Gatsby", card);

            Assert.Equal(card.Holder, book?.BorrowedBy?.Holder);
            Assert.Equal(BorrowBookResult.Success, result);
        }

        [Fact]
        public void BorrowBookNullCard()
        {
            var library = new Library();

            Assert.Throws<ArgumentNullException>(() => library.BorrowBook("The Great Gatsby", null));
        }

        [Fact]
        public void BorrowBookNonExistant()
        {
            var library = new Library();
            var card = library.GetCard(0);

            var (book, result) = library.BorrowBook("The Illiad", card);

            Assert.Null(book);
            Assert.Equal(BorrowBookResult.BookNotFound, result);
        }

        [Fact]
        public void BorrowBookAllRentedOut_1()
        {
            var library = new Library();
            var card1 = library.GetCard(0);
            var card2 = new Card(1, "Agmoni");

            var (book1, result1) = library.BorrowBook("The Great Gatsby", card1);
            var (book2, result2) = library.BorrowBook("The Great Gatsby", card2);

            Assert.Equal(BorrowBookResult.Success, result1);
            Assert.Equal(BorrowBookResult.NoCopiesAvailible, result2);
            Assert.NotNull(book1);
            Assert.Null(book2);
        }

        [Fact]
        public void BorrowBookAllRentedOut_2()
        {
            var library = new Library();
            library.AddBook("The Great Gatsby");
            var card1 = library.GetCard(0);
            var card2 = new Card(1, "Agmoni");
            var card3 = new Card(1, "Shmoni");

            var (book1, result1) = library.BorrowBook("The Great Gatsby", card1);
            var (book2, result2) = library.BorrowBook("The Great Gatsby", card2);
            var (book3, result3) = library.BorrowBook("The Great Gatsby", card3);

            Assert.Equal(BorrowBookResult.Success, result1);
            Assert.Equal(BorrowBookResult.Success, result2);
            Assert.Equal(BorrowBookResult.NoCopiesAvailible, result3);
            Assert.NotNull(book1);
            Assert.NotNull(book2);
            Assert.Null(book3);
        }

        [Fact]
        public void BorrowBookReturned()
        {
            var library = new Library();
            var card1 = library.GetCard(0);
            var card2 = new Card(1, "Agmoni");

            var (book1, result1) = library.BorrowBook("The Great Gatsby", card1);
            var returnResult = library.ReturnBook(book1);
            var (book2, result2) = library.BorrowBook("The Great Gatsby", card2);

            Assert.Equal(BorrowBookResult.Success, result1);
            Assert.True(returnResult);
            Assert.Equal(BorrowBookResult.Success, result2);
            Assert.NotNull(book1);
            Assert.Equal(book1, book2);
        }

        [Fact]
        public void BorrowBookAlreadyBorrowed()
        {
            var library = new Library();
            library.AddBook("The Great Gatsby");
            var card1 = library.GetCard(0);

            var (book1, result1) = library.BorrowBook("The Great Gatsby", card1);
            var (book2, result2) = library.BorrowBook("The Great Gatsby", card1);

            Assert.Equal(BorrowBookResult.Success, result1);
            Assert.Equal(BorrowBookResult.CustomerAlreadyBorrowed, result2);
            Assert.NotNull(book1);
            Assert.Null(book2);
        }

        [Fact]
        public void ReturnBook()
        {
            var library = new Library();
            var card = library.GetCard(0);
            var book = library.GetBook(0);

            library.BorrowBook("The Great Gatsby", card);
            var result = library.ReturnBook(book);

            Assert.Null(book?.BorrowedBy?.Holder);
            Assert.True(result);
        }

        [Fact]
        public void ReturnBookNotExistant()
        {
            var library = new Library();

            var book = new Book(5, "The Illiad");
            var result = library.ReturnBook(book);

            Assert.Null(book?.BorrowedBy?.Holder);
            Assert.False(result);
        }

        [Fact]
        public void FindBook()
        {
            var library = new Library();
            library.AddBook("The Great Gatsby");
            var card = library.GetCard(0);
            library.BorrowBook("The Great Gatsby", card);

            var findResult = library.FindBook("The Great Gatsby");

            Assert.Equal(2, findResult.TotalCoppies);
            Assert.Single(findResult.Availible);
            Assert.Single(findResult.Borrowed);
        }

        [Fact]
        public void FindBookAll()
        {
            var library = new Library();
            library.AddBook("The Great Gatsby");

            var findResult = library.FindBook();

            Assert.Equal(4, findResult.TotalCoppies);
            Assert.Equal(4, findResult.Availible.Length);
            Assert.Empty(findResult.Borrowed);
        }

        [Fact]
        public void FindBookDoesntExists()
        {
            var library = new Library();

            var findResult = library.FindBook("The Illiad");

            Assert.Equal(0, findResult.TotalCoppies);
            Assert.Empty(findResult.Availible);
            Assert.Empty(findResult.Borrowed);
        }

        [Fact]
        public void FindBooksBorrowedBy()
        {
            var library = new Library();
            var card = library.GetCard(0);

            var (book, borrowResult) = library.BorrowBook("The Great Gatsby", card);
            var result = library.FindBooksBorrowedBy(card.Id);

            Assert.Equal(BorrowBookResult.Success, borrowResult);
            Assert.NotNull(book);
            Assert.Single(result);
        }

        [Fact]
        public void GetCatalogue()
        {
            var library = new Library();

            var result = library.GetCatalogue();

            Assert.Equal(["The Great Gatsby", "1984", "To Kill a Mockingbird"], result);
        }

        [Fact]
        public void GetCatalogueAvailible()
        {
            var library = new Library();
            var (book, borrowResult) = library.BorrowBook("The Great Gatsby", library.GetCard(0));

            var result = library.GetCatalogue(true);

            Assert.Equal(["1984", "To Kill a Mockingbird"], result);
            Assert.Equal(BorrowBookResult.Success, borrowResult);
        }
    }
}