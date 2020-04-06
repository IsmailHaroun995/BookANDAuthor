using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookANDAuthor.Models.Repository
{
    public class AuthorRepository : IBookRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>
            {
                new Author
                {
                    Id=1,
                   FullName="ismail"

                },
                  new Author
                {
                    Id=2,
                    FullName="Haroun"

                },
                    new Author
                {
                    Id=3,
                   FullName="Ali"

                }

            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            authors.Remove(author);
        }

        public Author Find(int Id)
        {
            var author = authors.SingleOrDefault(b => b.Id == Id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int Id, Author newAuthor)
        {
            var author = Find(Id);
            author.FullName = newAuthor.FullName;
            
        }
    }
}
