namespace ProgrammingAnalystExercise
{
    public class Book
    {
        public string Title { get; private set; }
        public Card? BorrowedBy { get; private set; } = null;
        public int Id { get; private set; }

        public Book(int id, string title)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title), "The book title can't be null or empty");
            Title = title;
            Id = id;
        }

        public void Borrow(Card borrowerCard) 
        {
            if (BorrowedBy == null)
            {
                BorrowedBy = borrowerCard;
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
