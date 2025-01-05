using LibraryManagement.Data;
using LibraryManagement.Helpers;
using LibraryManagement.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    /// <summary>
    /// Controller responsible for handling book-related operations such as retrieving, adding, updating, and deleting books.
    /// The controller supports versioning (v1) for the book APIs.
    /// </summary>
    [RoutePrefix("api/v1/books")]
    public class BooksV1Controller : ApiController
    {
        /// <summary>
        /// Endpoint to retrieve all books.
        /// </summary>
        /// <returns>A list of all books in the system.</returns>
        [JwtAuthorizor("Admin", "User")] // Requires the user to be authenticated with "Admin" or "User" role
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllBooks()
        {
            return Ok(BookStore.Books); // Return the list of books
        }

        /// <summary>
        /// Endpoint to retrieve a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>The book if found, or a 404 Not Found if not.</returns>
        [JwtAuthorizor("Admin", "User")] // Requires the user to be authenticated with "Admin" or "User" role
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetBooksById(int id)
        {
            var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if book is not found

            return Ok(book); // Return the book details if found
        }

        /// <summary>
        /// Endpoint to add a new book.
        /// </summary>
        /// <param name="newBook">The book object to be added to the system.</param>
        /// <returns>A success message if the book is added, or a bad request if the book data is missing.</returns>
        [JwtAuthorizor("Admin")] // Requires the user to be authenticated with "Admin" role
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBooks([FromBody] Book newBook)
        {
            if (newBook == null)
                return BadRequest("Book data is missing"); // Return 400 if book data is missing

            newBook.Id = BookStore.Books.Count > 0 ? BookStore.Books.Max(b => b.Id) + 1 : 1;
            BookStore.Books.Add(newBook); // Add the new book to the list

            return Ok($"Book '{newBook.Title}' added successfully!"); // Return success message
        }

        /// <summary>
        /// Endpoint to update an existing book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="updatedBook">The updated book object.</param>
        /// <returns>A success message if the book is updated, or a 404 Not Found if the book does not exist.</returns>
        [JwtAuthorizor("Admin")] // Requires the user to be authenticated with "Admin" role
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateBooks(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest("Book data is missing"); // Return 400 if updated book data is missing

            var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if book does not exist

            // Update the book details
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            return Ok($"Book '{book.Title}' updated successfully!"); // Return success message
        }

        /// <summary>
        /// Endpoint to delete a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>A success message if the book is deleted, or a 404 Not Found if the book does not exist.</returns>
        [JwtAuthorizor("Admin")] // Requires the user to be authenticated with "Admin" role
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteBooks(int id)
        {
            var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if book does not exist

            BookStore.Books.Remove(book); // Remove the book from the list
            return Ok($"Book '{book.Title}' deleted successfully!"); // Return success message
        }
    }
}
