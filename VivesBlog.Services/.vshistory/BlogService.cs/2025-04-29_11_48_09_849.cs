using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesBlog.Model;
using VivesBlog.Repository;

namespace VivesBlog.Services
{
    public class BlogService
    {

        private readonly BlogPostDbContext _dbContext;

        public BlogService(BlogPostDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IList<BlogPost> GetAll()
        {
            return _dbContext.BlogPosts.ToList();
        }

        public BlogPost GetById(int id)
        {
            return _dbContext.BlogPosts.FirstOrDefault(b => b.Id == id);
        }
    }
}
