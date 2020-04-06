using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookANDAuthor.Models.Repository
{
    public class AuthorDbRepository : IBookRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
                
        }
        public void Add(Author entity)
        {
            //entity.Id = db.Authors.Max(b => b.Id) + 1;
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int Id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == Id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int Id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();

        }
    }
}
