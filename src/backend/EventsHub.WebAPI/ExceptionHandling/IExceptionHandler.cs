using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace TheraLang.Web.ExceptionHandling
{
    public interface IExceptionHandler
    {
        Task Handle(ExceptionContext exceptionContext);
    }
}