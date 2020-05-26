namespace Twins.Models
{
    public class Category
    {
        public int Id { get; }

        public string Name { get; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
