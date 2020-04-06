using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookANDAuthor.Models.Repository
{
    public class BookRepository : IBookRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>
            {
                new Book
                {
                    Id=1,
                    Title="Csharp programming",Description="No Desc",Author= new Author()

                },
                  new Book
                {
                    Id=2,
                    Title="JAVA ",Description="No Desc",Author= new Author()

                },
                    new Book
                {
                    Id=3,
                    Title="Python",Description="No Desc",Author= new Author()

                }

            };
                
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1; 
            books.Add(entity);
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            books.Remove(book);
        }

        public Book Find(int Id)
        {
            var book = books.SingleOrDefault(b => b.Id==Id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id,Book newBook)
        {
            var book = Find(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;

            
        }
    }
}
