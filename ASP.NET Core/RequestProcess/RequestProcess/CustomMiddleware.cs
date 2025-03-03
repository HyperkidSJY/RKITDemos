namespace RequestProcess
{
    /// <summary>
    /// Custom middleware that handles HTTP requests and sends a response.
    /// </summary>
    public class CustomMiddleware : IMiddleware
    {
        /// <summary>
        /// The method that will be invoked during the HTTP request pipeline.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="next">A delegate representing the next middleware in the pipeline.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            #region Custom Middleware Logic
            // Write a custom response when the middleware is invoked
            await context.Response.WriteAsync("hello from custom middleware");
            #endregion
        }
    }
}
