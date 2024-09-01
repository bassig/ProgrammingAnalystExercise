namespace ProgrammingAnalystExercise
{
    public class Card: IEquatable<Card>
    {
        public int Id { get; private set; }
        public string Holder { get; private set; }

        public Card(int id, string holder)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            if (string.IsNullOrWhiteSpace(holder)) throw new ArgumentNullException(nameof(holder), "The name of a card holder can't be null or empty");
            Id = id;
            Holder = holder;
        }

        public bool Equals(Card? other) => this.Id == other?.Id;
    }
}
