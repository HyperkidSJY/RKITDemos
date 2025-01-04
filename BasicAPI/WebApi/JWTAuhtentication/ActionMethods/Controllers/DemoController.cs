using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Demo of all action methods and different status code
    /// </summary>
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        private static readonly Dictionary<int, string> Items = new Dictionary<int, string>()
        {
            { 1, "Item 1" },
            { 2, "Item 2" },
            { 3, "Item 3" }
        };

        /// <summary>
        /// Get an item by ID.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = "GetItemById")]
        public IHttpActionResult GetItem(int id)
        {
            if (Items.ContainsKey(id))
            {
                return Ok(new
                {
                    Id = id,
                    Name = Items[id]
                }); // HTTP 200
            }

            return NotFound(); // HTTP 404
        }

        /// <summary>
        /// Create a new item.
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateItem([FromBody] string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                return BadRequest("Item name cannot be null or empty."); // HTTP 400
            }

            int newId = Items.Count + 1;
            Items.Add(newId, itemName);

            return CreatedAtRoute(
                routeName: "GetItemById",
                routeValues: new { id = newId },
                content: new
                {
                    Id = newId,
                    Name = itemName
                }
            ); // HTTP 201
        }

        /// <summary>
        /// Update an item by ID.
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateItem(int id, [FromBody] string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest("Item name cannot be null or empty."); // HTTP 400
            }

            if (!Items.ContainsKey(id))
            {
                return NotFound(); // HTTP 404
            }

            Items[id] = newName;

            return Ok(new
            {
                Message = "Item updated successfully.",
                UpdatedItem = new
                {
                    Id = id,
                    Name = newName
                }
            }); // HTTP 200
        }

        /// <summary>
        /// Delete an item by ID.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteItem(int id)
        {
            if (!Items.ContainsKey(id))
            {
                return NotFound(); // HTTP 404
            }

            Items.Remove(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent); // HTTP 204
        }
    }
}
