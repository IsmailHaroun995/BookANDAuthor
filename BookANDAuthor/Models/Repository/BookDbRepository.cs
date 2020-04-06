using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookANDAuthor.Models.Repository
{
    public class BookDbRepository : IBookRepository<Book>
    {
        BookStoreDbContext db;
        public BookDbRepository( BookStoreDbContext _db)
        {
            db = _db;
        }

        public void Add(Book entity)
        {
          
            db.Books.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            db.Books.Remove(book);
            db.SaveChanges();

        }

        public Book Find(int Id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == Id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();


        }
    }
}

