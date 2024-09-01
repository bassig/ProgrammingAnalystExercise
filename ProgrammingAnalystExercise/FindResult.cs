namespace ProgrammingAnalystExercise
{
    public class FindResult
    {
        public int TotalCoppies { get { return Borrowed.Length + Availible.Length; } }
        public Book[] Borrowed { get; set; }
        public Book[] Availible { get; set; }
    }
}
