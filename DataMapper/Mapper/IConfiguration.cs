using System;

namespace AutomapperTest
{
	public interface IConfiguration
	{
		void Create<TIn, TOut>() where TOut : class, new();
	}
}

