using VivesBlog.Mvc.Models;

namespace VivesBlog.Mvc.Core
{
    public class BlogPostDatabase
    {
        public IList<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        public void Seed()
        {
            BlogPosts = new List<BlogPost>()
            {
                new() { ID=1, Title = "The Future of AI in Healthcare", Content = "Exploring the latest advancements in artificial intelligence and its impact on the medical field.", Author = "Dr. Emily Carter" },
                new() { ID=2, Title = "Mastering C# for Beginners", Content = "A step-by-step guide to understanding the fundamentals of C# programming." },
                new() { ID=3, Title = "The Psychology of Habit Formation", Content = "How habits are formed and how to use psychology to build better ones.", Author = "Sarah Thompson" },
                new() { ID=4, Title = "Top 10 JavaScript Frameworks in 2025", Content = "A detailed comparison of the most popular JavaScript frameworks this year." },
                new() { ID=5, Title = "Mindfulness and Mental Health", Content = "Techniques to incorporate mindfulness into your daily life to improve mental well-being.", Author = "Dr. Rachel Green" },
                new() { ID=6, Title = "Understanding SQL Indexes", Content = "How indexes work in SQL and how to optimize your database queries." },
                new() { ID=7, Title = "A Beginner's Guide to Express.js", Content = "Learn how to set up and build web applications using Express.js.", Author = "Sophia Williams" },
                new() { ID=8, Title = "Why You Should Start a Side Project", Content = "The benefits of working on personal projects and how they can boost your career." },
                new() { ID=9, Title = "The Evolution of Web Development", Content = "A look at how web development has changed over the years.", Author = "Olivia Martinez" },
                new() { ID=10, Title = "Cybersecurity Trends in 2025", Content = "What to expect in the world of cybersecurity and how to stay protected." },
                new() { ID=11, Title = "The Role of UX in Modern Web Design", Content = "Why user experience is crucial in designing successful web applications.", Author = "Charlotte Brown" },
                new() { ID=12, Title = "Exploring the World of Game Development", Content = "An introduction to game development and how to get started." },
                new() { ID=13, Title = "Best Practices for Writing Clean Code", Content = "Tips and techniques to make your code more readable and maintainable.", Author = "James Wilson" },
                new() { ID=14, Title = "How to Stay Motivated as a Developer", Content = "Overcoming burnout and staying passionate about coding." },
                new() { ID=15, Title = "The Science Behind Sleep and Productivity", Content = "How sleep affects your brain and ways to improve your productivity.", Author = "Dr. William Adams" }
            };

        }
    }
}
