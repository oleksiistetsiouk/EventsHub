using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using TheraLang.Web.ExceptionHandling;

namespace EventsHub.WebAPI.ActionFilters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IExceptionHandler _errorHandler;

        public ExceptionFilter(IExceptionHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await _errorHandler.Handle(context);
        }
    }
}
