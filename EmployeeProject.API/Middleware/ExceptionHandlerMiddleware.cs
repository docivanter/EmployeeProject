using EmployeeProject.Buisiness.Exceptions;
using Serilog;
namespace EmployeeProject.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EmployeeException employeeException)
            {
                Log.Information("Info: "+employeeException.Message); // Log issues related to Employee interaction
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message); // Log everything else
            }          
        }
    }
}
