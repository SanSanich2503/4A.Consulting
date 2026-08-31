using Core;
using Scrutor.AspNetCore;

namespace Services.Services
{
    /// <summary>
    /// Базовый сервис, имеющий Transient-цикл жизнзи
    /// </summary>
    public abstract class BaseService : ISelfTransientLifetime
    {
        protected readonly DataContext _context;

        public BaseService(DataContext context)
        {
            _context = context;
        }
    }
}