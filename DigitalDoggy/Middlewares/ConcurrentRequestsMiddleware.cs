using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalDoggy.Middlewares
{
    public class ConcurrentRequestsMiddleware : IMiddleware
    {
        private readonly uint _maxRequestsCountPerSecond;
        private readonly ConcurrentQueue<DateTime> _dates;

        public ConcurrentRequestsMiddleware(uint maxRequestsCountPerSecond)
        {
            _maxRequestsCountPerSecond = maxRequestsCountPerSecond;
            _dates = new ConcurrentQueue<DateTime>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var to = DateTime.Now;
            var from = to.AddSeconds(-1d);
            var count = _dates.Count(x => x >= from && x <= to);
            
            if (count < _maxRequestsCountPerSecond)
            {
                _dates.Enqueue(to);
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 429;
            }

            while (_dates.TryPeek(out var lastDate) && lastDate < from)
            {
                _dates.TryDequeue(out var dequed);
            }
        }
    }
}
