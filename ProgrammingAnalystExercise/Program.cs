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
            
            var book = library.AddBook("Great Expectations");
            library.AddBook("gReAt eXpEcTaTiOnS");
            var ploni = library.GetCard(0);
            var agmoni = library.AddCard("Agmoni");

            library.BorrowBook("Great Expectations", ploni);
            library.BorrowBook("Great Expectations", ploni);
            library.BorrowBook("Great Expectations", agmoni);
            library.BorrowBook("Great Expectations", agmoni);
            library.ReturnBook(book.Id);

            library.RemoveBook(book);
        }
    }
}
