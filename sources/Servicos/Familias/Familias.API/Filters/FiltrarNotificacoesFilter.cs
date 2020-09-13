using System.Net;
using System.Threading.Tasks;
using Base.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Familias.API.Filters
{
    public class FiltrarNotificacoesFilter : IAsyncResultFilter
    {
        private readonly INotificadorBase _notificador;

        public FiltrarNotificacoesFilter(INotificadorBase notificador)
        {
            _notificador = notificador;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificador.PossuiNotificacoes)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var notificacoes = JsonConvert.SerializeObject(_notificador.Notificacoes);
                await context.HttpContext.Response.WriteAsync(notificacoes);

                return;
            }

            await next();
        }
    }
}