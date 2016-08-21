using System;

namespace AutomapperTest
{
	public interface IConfiguration
	{
		void Create<TIn, TOut>() where TOut : class, new();

		void Create<TIn, TOut>( Func<TIn,TOut> creator );
	}
}

