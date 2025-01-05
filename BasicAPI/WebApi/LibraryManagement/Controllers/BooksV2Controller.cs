using LibraryManagement.Data;
using LibraryManagement.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    /// <summary>
    /// Controller responsible for handling book-related operations, including retrieving books with optional sorting and filtering.
    /// This is version 2 of the book-related API.
    /// </summary>
    [RoutePrefix("api/v2/books")]
    public class BooksV2Controller : ApiController
    {
        /// <summary>
        /// Endpoint to retrieve books with optional sorting and filtering.
        /// </summary>
        /// <param name="sortBy">The field by which to sort the books (e.g., "title", "author", or "year").</param>
        /// <param name="filterBy">The string to filter books by (e.g., part of the title or author).</param>
        /// <returns>A list of books that match the filter and sorting criteria.</returns>
        [JwtAuthorizor("Admin", "User")] // Requires the user to be authenticated with "Admin" or "User" role
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBooks([FromUri] string sortBy = null, [FromUri] string filterBy = null)
        {
            var filteredBooks = BookStore.Books.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrWhiteSpace(filterBy))
            {
                filteredBooks = filteredBooks.Where(b =>
                    b.Title.Contains(filterBy) ||  // Filter by title if it contains the filter string
                    b.Author.Contains(filterBy)    // Filter by author if it contains the filter string
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower()) // Sort by the specified field
                {
                    case "title":
                        filteredBooks = filteredBooks.OrderBy(b => b.Title); // Sort by title
                        break;
                    case "author":
                        filteredBooks = filteredBooks.OrderBy(b => b.Author); // Sort by author
                        break;
                    case "year":
                        filteredBooks = filteredBooks.OrderBy(b => b.Year); // Sort by year
                        break;
                    default:
                        break;
                }
            }

            return Ok(filteredBooks.ToList()); // Return the filtered and sorted list of books
        }
    }
}
