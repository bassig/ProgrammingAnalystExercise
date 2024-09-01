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
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title), "The book title can't be null or empty");
            Title = title;
            Id = id;
        }

        /// <returns>True if successful or false otherwise</returns>
        public bool Borrow(Card borrowerCard) 
        {
            ArgumentNullException.ThrowIfNull(borrowerCard);
            if (BorrowedBy == null)
            {
                BorrowedBy = borrowerCard;
                Console.WriteLine($"The book '{Title}' of ID '{Id}' has been checked out by {BorrowedBy}.");
                return true;
            }
            else
            {
                Console.WriteLine($"The book '{Title}' of ID '{Id}' is currently checked out by {BorrowedBy}.");
                return false;
            }
        }

        /// <returns>True if successful or false otherwise</returns>
        public bool Return()
        {
            if (BorrowedBy != null)
            {
                BorrowedBy = null;
                Console.WriteLine($"The book '{Title}' of ID '{Id}' has been returned.");
                return true;
            }
            else
            {
                Console.WriteLine($"The book '{Title}' of ID '{Id}' is not borrowed.");
                return false;
            }
        }
    }
}
