namespace ProgrammingAnalystExercise
{
    internal class Program
    {
        /*
         * The following program mimics a simple library that contains a collection of books
         * and methods for Adding, Borrowing, Returning, and Removing books from the collection.
         * 
         * Examples are shown below.
         * 
         * Assignment: How would you improve the code including debugging any errors you might find?
         */

        static void Main(string[] args)
        {
            var library = new Library();
            
            var newBook = new Book("Great Expectations");
            
            library.AddBook(newBook);
            library.BorrowBook("Great Expectations", "Ploni");
            library.ReturnBook("Great Expectations");
            library.RemoveBook("Great Expectations");
        }
    }
}
