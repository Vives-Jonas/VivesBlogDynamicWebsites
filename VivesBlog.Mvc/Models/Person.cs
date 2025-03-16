namespace VivesBlog.Mvc.Models
{
    public class Person
    {

        public int Id { get; set; }
        public required string Name { get; set; }

        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();   
    }
}
