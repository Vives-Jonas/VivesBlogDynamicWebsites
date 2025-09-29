using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;

namespace VivesBlog.Repository
{
    public class VivesBlogDbContext(DbContextOptions<VivesBlogDbContext> options) : DbContext(options)
    {
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Person> People => Set<Person>();

        public async Task Seed()
        {
            People.AddRange(new List<Person>
            {
                new() {FirstName = "Dr. Emily", LastName=" Carter" },
                new() {FirstName = "Sarah", LastName = "Thompson",  Email="Sarah.Thompson@example.com" },
                new() {FirstName = "Dr. Rachel", LastName = "Green" },
                new() {FirstName = "Sophia", LastName = "Williams", Email = "Sophia.Williams@example.com"}

            });

            await SaveChangesAsync();


            var emily = People.FirstOrDefault(p => p.FirstName == "Dr. Emily");
            var sarah = People.FirstOrDefault(p => p.FirstName == "Sarah");

            BlogPosts.AddRange(new List<BlogPost>()
            {
                new() {  Title = "The Future of AI in Healthcare", Content = "Exploring the latest advancements in artificial intelligence and its impact on the medical field.",Author = emily},
                new() {  Title = "Mastering C# for Beginners", Content = "A step-by-step guide to understanding the fundamentals of C# programming." },
                new() {  Title = "The Psychology of Habit Formation", Content = "How habits are formed and how to use psychology to build better ones.",Author = sarah  },
                new() {  Title = "Top 10 JavaScript Frameworks in 2025", Content = "A detailed comparison of the most popular JavaScript frameworks this year." },
                new() {  Title = "Mindfulness and Mental Health", Content = "Techniques to incorporate mindfulness into your daily life to improve mental well-being." },
                new() {  Title = "Understanding SQL Indexes", Content = "How indexes work in SQL and how to optimize your database queries." },
                new() {  Title = "A Beginner's Guide to Express.js", Content = "Learn how to set up and build web applications using Express.js.", Author = emily },
                new() {  Title = "Why You Should Start a Side Project", Content = "The benefits of working on personal projects and how they can boost your career.", Author = emily },
                new() {  Title = "The Evolution of Web Development", Content = "A look at how web development has changed over the years." },
                new() {  Title = "Cybersecurity Trends in 2025", Content = "What to expect in the world of cybersecurity and how to stay protected." },
                new() {  Title = "The Role of UX in Modern Web Design", Content = "Why user experience is crucial in designing successful web applications.",Author = sarah },
                new() {  Title = "Exploring the World of Game Development", Content = "An introduction to game development and how to get started." },
                new() {  Title = "Best Practices for Writing Clean Code", Content = "Tips and techniques to make your code more readable and maintainable.",Author = sarah},
                new() {  Title = "How to Stay Motivated as a Developer", Content = "Overcoming burnout and staying passionate about coding." },
                new() {  Title = "The Science Behind Sleep and Productivity", Content = "How sleep affects your brain and ways to improve your productivity." }
            });

            await SaveChangesAsync();
        }
    }
}
