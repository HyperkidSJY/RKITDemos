using Microsoft.AspNetCore.Mvc;

namespace RequestProcess.Controllers
{
    /// <summary>
    /// Controller responsible for handling API requests related to "Home".
    /// </summary>
    [Route("api/[controller]")]
    //[Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region GetAll Action
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A string indicating that all items are being retrieved.</returns>
        [Route("getall")]
        public string GetAll()
        {
            return "GET ALL";
        }
        #endregion

        #region GetById Action
        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>A string indicating the ID of the item retrieved.</returns>
        [Route("get/{id}")]
        public string GetById(int id)
        {
            return "id" + id;
        }
        #endregion
    }
}
