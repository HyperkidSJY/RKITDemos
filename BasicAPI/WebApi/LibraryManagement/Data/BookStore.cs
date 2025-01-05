using LibraryManagement.Models;
using System.Collections.Generic;

namespace LibraryManagement.Data
{
    public static class BookStore
    {
        public static List<Book> Books { get; } = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2001 },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2002 },
            new Book { Id = 3, Title = "Book 3", Author = "Author 3", Year = 2003 }
        };
    }
}