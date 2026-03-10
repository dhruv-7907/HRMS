using System.Data;                 // For DataException (Database related errors)
using System.Diagnostics;
using System.Text;
using System.Text.Json;            // For JSON serialization

namespace WebApi.Middleware
{
    // Custom Global Exception Middleware
    // This middleware catches all unhandled exceptions in the request pipeline
    public class GlobalExceptionMiddleware
    {
        // Delegate to call the next middleware in the pipeline
        private readonly RequestDelegate _next;

        // Logger for logging exception details
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        // Used to detect current environment (Development / Production)
        private readonly IHostEnvironment _env;

        // Constructor with Dependency Injection
        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;      // Assign next middleware
            _logger = logger;  // Assign logger
            _env = env;        // Assign environment
        }

        // This method is automatically executed for every HTTP request
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                var path = context.Request.Path;
                var method = context.Request.Method;
                var traceId = context.TraceIdentifier;
                var user = context.User?.Identity?.Name ?? "Anonymous";
                context.Request.EnableBuffering();

                string body = string.Empty;

                if (context.Request.ContentLength > 0)
                {
                    context.Request.Body.Position = 0;
                    using var reader = new StreamReader(
                        context.Request.Body,
                        Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,   // ✅ optional but recommended
                        leaveOpen: true);
                  
                    body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                var curl = GenerateCurlCommand(context, body);
                // Log the exception details
                _logger.LogError(ex,
            "Unhandled Exception occurred. Path: {Path}, Method: {Method}, TraceId: {TraceId}, User: {User} , CURL:{curl}",
            path,
            method,
            traceId,
            user,
            curl);

                // Handle the exception and return custom response
                await HandleExceptionAsync(context, ex, traceId);
            }
        }

        // Method responsible for creating standardized error response
        private async Task HandleExceptionAsync(HttpContext context, Exception ex, string traceId)
        {
            // Set response content type to JSON
            context.Response.ContentType = "application/json";

            // Default values (500 - Internal Server Error)
            int statusCode = StatusCodes.Status500InternalServerError;
            string errorCode = "SERVER_ERROR";
            string message = "An unexpected error occurred";

            // Map specific exception types to HTTP status codes
            switch (ex)
            {
                // 400 - Bad Request (Invalid input)
                case ArgumentException:
                case FormatException:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorCode = "BAD_REQUEST";
                    message = ex.Message;
                    break;

                // 401 - Unauthorized (Authentication failed)
                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorCode = "UNAUTHORIZED";
                    message = ex.Message;
                    break;

                // 403 - Forbidden (User authenticated but no permission)
                case System.Security.SecurityException:
                    statusCode = StatusCodes.Status403Forbidden;
                    errorCode = "FORBIDDEN";
                    message = ex.Message;
                    break;

                // 404 - Not Found (Resource not available)
                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    errorCode = "NOT_FOUND";
                    message = ex.Message;
                    break;

                // 409 - Conflict (Duplicate or invalid state)
                case InvalidOperationException:
                    statusCode = StatusCodes.Status409Conflict;
                    errorCode = "CONFLICT";
                    message = ex.Message;
                    break;

                // 408 - Request Timeout
                case TimeoutException:
                    statusCode = StatusCodes.Status408RequestTimeout;
                    errorCode = "TIMEOUT";
                    message = ex.Message;
                    break;

                // 501 - Feature not implemented
                case NotImplementedException:
                    statusCode = StatusCodes.Status501NotImplemented;
                    errorCode = "NOT_IMPLEMENTED";
                    message = ex.Message;
                    break;

                // Database related exception
                case DataException:
                    statusCode = StatusCodes.Status500InternalServerError;
                    errorCode = "DATABASE_ERROR";
                    message = ex.Message;
                    break;
            }

            // If application is running in Development environment,
            // show actual exception message for debugging
            if (_env.IsDevelopment())
                message = ex.Message;

            // Set HTTP status code
            context.Response.StatusCode = statusCode;

            // Standardized error response object
            var response = new
            {
                status = statusCode,                   // HTTP status code
                errorCode,                             // Custom error code
                message,                               // Error message
                TraceId = traceId      // Unique request identifier (for debugging)
            };

            // Serialize response object to JSON and return to client
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }


        private static string GenerateCurlCommand(HttpContext context, string body)
        {
            var request = context.Request;
            var curl = new StringBuilder();

            curl.Append($"curl -X {request.Method} ");

            var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            curl.Append($"\"{url}\" ");

            if (request.Headers.ContainsKey("Content-Type"))
                curl.Append($"-H \"Content-Type: {request.Headers["Content-Type"]}\" ");

            curl.Append("-H \"Accept: application/json\" ");

            if (!string.IsNullOrWhiteSpace(body) && request.Method != "GET")
            {
                var escapedBody = body.Replace("\"", "\\\"");
                curl.Append($"-d \"{escapedBody}\" ");
            }

            return curl.ToString();
        }
    }
}