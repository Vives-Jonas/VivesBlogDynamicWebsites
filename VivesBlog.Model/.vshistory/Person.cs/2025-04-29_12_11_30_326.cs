namespace VivesBlog.Model
{
    public class Person
    {

        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }


        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();   
    }
}
