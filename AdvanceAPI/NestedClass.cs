using System;
using System.Collections.Generic;

namespace AdvanceAPI
{
    /// <summary>
    /// The entry point class that demonstrates how to use the Library class with the nested Book class.
    /// </summary>
    public class NestedClass
    {
        /// <summary>
        /// The Main method is the entry point of the application. It demonstrates adding books to the library 
        /// and displaying the list of books.
        /// </summary>
        static void Main()
        {
            // Create an instance of the Library class
            Library library = new Library();

            // Add some books to the library
            library.AddBook("To Kill a Mockingbird", "Harper Lee", "978-0-06-112008-4");
            library.AddBook("1984", "George Orwell", "978-0-452-28423-4");
            library.AddBook("The Great Gatsby", "F. Scott Fitzgerald", "978-0-7432-7356-5");

            // Display all books in the library
            library.DisplayBooks();
        }
    }

    /// <summary>
    /// Represents a library that holds a collection of books.
    /// </summary>
    public class Library
    {
        // A list of books in the library
        private List<Book> books = new List<Book>();

        /// <summary>
        /// Adds a new book to the library's collection.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="isbn">The ISBN number of the book.</param>
        public void AddBook(string title, string author, string isbn)
        {
            Book book = new Book(title, author, isbn);  // Creating a Book object using the nested class
            books.Add(book);
        }

        /// <summary>
        /// Displays all books in the library.
        /// </summary>
        public void DisplayBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}");
                }
            }
        }

        /// <summary>
        /// Represents a book with a title, author, and ISBN number.
        /// </summary>
        public class Book
        {
            /// <summary>
            /// Gets or sets the title of the book.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// Gets or sets the author of the book.
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// Gets or sets the ISBN number of the book.
            /// </summary>
            public string ISBN { get; set; }

            /// <summary>
            /// Initializes a new instance of the Book class with the given title, author, and ISBN.
            /// </summary>
            /// <param name="title">The title of the book.</param>
            /// <param name="author">The author of the book.</param>
            /// <param name="isbn">The ISBN number of the book.</param>
            public Book(string title, string author, string isbn)
            {
                Title = title;
                Author = author;
                ISBN = isbn;
            }
        }
    }
}
