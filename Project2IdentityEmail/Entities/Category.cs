namespace Project2IdentityEmail.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }

        public virtual ICollection<UserMessage> Messages { get; set; }
    }
}