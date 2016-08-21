using System;

namespace DataMapper
{
	public interface IConfiguration
	{
		void Create<TIn, TOut>() where TOut : class, new();

		void Create<TIn, TOut>( Func<object,object> converter );
	}
}

