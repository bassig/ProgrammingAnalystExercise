namespace ProgrammingAnalystExercise
{
    public class Book
    {
        public string Title { get; private set; }
        public string? BorrowedBy { get; private set; } = null;

        public Book(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new NullReferenceException("The book must have a title");
            Title = title;
        }

        public void Borrow(string borrower) // TODO library card system
        {
            if (BorrowedBy == null)
            {
                BorrowedBy = borrower;
                Console.WriteLine($"The book '{Title}' has been checked out by {BorrowedBy}.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' is currently checked out by {BorrowedBy}.");
            }
        }

        public void Return()
        {
            if (BorrowedBy != null)
            {
                BorrowedBy = null;
                Console.WriteLine($"The book '{Title}' has been returned.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}' was not borrowed.");
            }
        }
    }
}
