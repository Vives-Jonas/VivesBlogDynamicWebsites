using Microsoft.EntityFrameworkCore;
using VivesBlog.Model;

namespace VivesBlog.Repository
{
    public class VivesBlogDbContext(DbContextOptions<VivesBlogDbContext> options) : DbContext(options)
    {
        public DbSet<Article> Articles => Set<Article>();
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
            var sophia = People.FirstOrDefault(p => p.FirstName == "Sophia");

            var random = new Random();

            DateTime RandomDate(DateTime start, DateTime end)
            {
                var range = (end - start).Days;
                return start.AddDays(random.Next(range)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60));
            }


            Articles.AddRange(new List<Article>()
            {
                new() {
        Title = "The Future of AI in Healthcare",
        Content = "Exploring the latest advancements in artificial intelligence and its impact on the medical field.",
        Author = emily,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    },
    new() {
        Title = "Mastering C# for Beginners",
        Content = "A step-by-step guide to understanding the fundamentals of C# programming.",
        Author = sophia,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "The Psychology of Habit Formation",
        Content = "How habits are formed and how to use psychology to build better ones.",
        Author = sarah,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    },
    new() {
        Title = "Top 10 JavaScript Frameworks in 2025",
        Content = "A detailed comparison of the most popular JavaScript frameworks this year.",
        Author = sophia,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "Mindfulness and Mental Health",
        Content = "Techniques to incorporate mindfulness into your daily life to improve mental well-being.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "Understanding SQL Indexes",
        Content = "How indexes work in SQL and how to optimize your database queries.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    },
    new() {
        Title = "A Beginner's Guide to Express.js",
        Content = "Learn how to set up and build web applications using Express.js.",
        Author = emily,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "Why You Should Start a Side Project",
        Content = "The benefits of working on personal projects and how they can boost your career.",
        Author = emily,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    },
    new() {
        Title = "The Evolution of Web Development",
        Content = "A look at how web development has changed over the years.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "Cybersecurity Trends in 2025",
        Content = "What to expect in the world of cybersecurity and how to stay protected.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "The Role of UX in Modern Web Design",
        Content = "Why user experience is crucial in designing successful web applications.",
        Author = sarah,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "Exploring the World of Game Development",
        Content = "An introduction to game development and how to get started.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    },
    new() {
        Title = "Best Practices for Writing Clean Code",
        Content = "Tips and techniques to make your code more readable and maintainable.",
        Author = sarah,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "How to Stay Motivated as a Developer",
        Content = "Overcoming burnout and staying passionate about coding.",
        Author = sophia,
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now)
    },
    new() {
        Title = "The Science Behind Sleep and Productivity",
        Content = "How sleep affects your brain and ways to improve your productivity.",
        CreatedDate = RandomDate(DateTime.Now.AddMonths(-12), DateTime.Now),
        UpdatedDate = RandomDate(DateTime.Now.AddMonths(-6), DateTime.Now)
    }});

            await SaveChangesAsync();
        }
    }
}
