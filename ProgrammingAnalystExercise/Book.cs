namespace ProgrammingAnalystExercise
{
    public class Book
    {
        public string Title { get; private set; }
        public string Barcode { get; private set; } //Added Barkode field
        public bool IsBorrowed { get; private set; }

        public Book(string title)
        {
            Title = title;
            Barcode = Guid.NewGuid().ToString(); // Generates a unique barkode
            IsBorrowed = false;
        }

        public void Borrow()
        {
            if (!IsBorrowed)
            {
                IsBorrowed = true;
                Console.WriteLine($"The book '{Title}', Barcode: '{Barcode}' has been checked out.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}', Barcode: '{Barcode}' is already checked out.");
            }
        }

        public void Return()
        {
            if (IsBorrowed)
            {
                IsBorrowed = false;
                Console.WriteLine($"The book '{Title}', Barcode: '{Barcode}' has been returned.");
            }
            else
            {
                Console.WriteLine($"The book '{Title}', Barcode: '{Barcode}' was not borrowed.");
            }
        }
    }
}
