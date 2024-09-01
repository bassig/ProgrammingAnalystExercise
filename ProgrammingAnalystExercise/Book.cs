namespace ProgrammingAnalystExercise
{
    public class Book
    {
        public string Title { get; private set; }
        public string? BorrowedBy { get; private set; } = null;
        public int Id { get; private set; }

        public Book(string title, int bookId)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("The book title can't be null or empty");
            Title = title;
            Id = bookId;
        }

        public void Borrow(string borrower) // TODO library card system
        {
            if (BorrowedBy == null)
            {
                BorrowedBy = borrower;
                Console.WriteLine($"The book '{Title}' of ID '{Id}' has been checked out by {BorrowedBy}.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' of ID '{Id}' is currently checked out by {BorrowedBy}.");
            }
        }

        public void Return()
        {
            if (BorrowedBy != null)
            {
                BorrowedBy = null;
                Console.WriteLine($"The book '{Title}' of ID '{Id}' has been returned.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' of ID '{Id}' is not borrowed.");
            }
        }
    }
}
