using JWTAuhtentication.Helpers;
using System;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Web.Http;

namespace JWTAuhtentication.Controllers
{
    /// <summary>
    /// Demonstrates role-based access control and caching mechanisms using JWT authorization.
    /// </summary>
    [RoutePrefix("api/Demo")]
    public class DemoController : ApiController
    {
        /// <summary>
        /// Retrieves data accessible only to users with the "Admin" role.
        /// </summary>
        /// <returns>
        /// A success message if the user is authenticated and authorized as an Admin.
        /// </returns>
        [JwtAuthorizor("Admin")]
        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetAdminData()
        {
            return Ok("You are authenticated and authorized as admin!");
        }

        /// <summary>
        /// Retrieves data accessible only to users with the "User" role.
        /// </summary>
        /// <returns>
        /// A success message if the user is authenticated and authorized as a User.
        /// </returns>
        [JwtAuthorizor("User")]
        [HttpGet]
        [Route("user")]
        public IHttpActionResult GetUserData()
        {
            return Ok("You are authenticated and authorized as User!");
        }

        // Uncomment the following methods for advanced caching scenarios:

        /*
        /// <summary>
        /// Retrieves admin data with a Cache-Control header to cache the response for 60 seconds.
        /// </summary>
        [JwtAuthorizor("Admin")]
        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetAdminDataWithCache()
        {
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, "You are authenticated and authorized as admin!");

            // Set Cache-Control header
            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(60),  // Cache for 60 seconds
                Public = true  // Cacheable by clients and proxies
            };

            return ResponseMessage(response);
        }

        /// <summary>
        /// Retrieves user data with a Cache-Control header to cache the response for 5 minutes.
        /// </summary>
        [JwtAuthorizor("User")]
        [HttpGet]
        [Route("user")]
        public IHttpActionResult GetUserDataWithCache()
        {
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, "You are authenticated and authorized as User!");

            // Set Cache-Control header
            response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromMinutes(5),  // Cache for 5 minutes
                Public = true
            };

            return ResponseMessage(response);
        }

        /// <summary>
        /// Retrieves admin data with in-memory caching using <see cref="MemoryCache"/>.
        /// </summary>
        private static readonly ObjectCache Cache = MemoryCache.Default;

        [JwtAuthorizor("Admin")]
        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetAdminDataWithMemoryCache()
        {
            const string cacheKey = "AdminData";
            var cachedData = Cache[cacheKey] as string;

            if (cachedData == null)
            {
                // Simulate fetching data
                cachedData = "You are authenticated and authorized as admin!";

                // Add data to cache with a 10-minute expiration
                Cache.Add(cacheKey, cachedData, DateTimeOffset.Now.AddMinutes(10));
            }

            return Ok(cachedData);
        }
        */
    }
}
