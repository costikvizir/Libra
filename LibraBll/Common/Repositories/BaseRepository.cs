using Libra.Dal.Context;
using Serilog;
using System;

namespace LibraBll.Common
{
    public class BaseRepository : IDisposable
    {
        private readonly LibraContext _context;
        protected readonly ILogger _logger;

        public BaseRepository(LibraContext context)
        {
            //_context = context ?? throw new ArgumentNullException(nameof(context));
            _context = new LibraContext();
            // _logger = Log.ForContext<BaseRepository>();
            //_logger = new LoggerConfiguration().WriteTo.Console();
            _context = context;
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }

        protected LibraContext Context => this._context;
    }
}