namespace Core
{

    public class Setting : IEntity
    {   
        public string Name { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}