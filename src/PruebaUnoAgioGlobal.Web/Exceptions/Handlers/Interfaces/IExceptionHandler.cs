using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Web.Exceptions.Handlers.Interfaces
{
    public interface IExceptionHandler
    {
        Task Handle(HttpContext context);
    }
}
