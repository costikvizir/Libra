using Libra.Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Common
{
	public class BaseRepository : IDisposable
	{
		private readonly LibraContext _context;

        public BaseRepository()
        {
			_context = new LibraContext();
        }
        public void Dispose()
		{
			this._context?.Dispose();
		}

		protected LibraContext Context => this._context;
	}
}
