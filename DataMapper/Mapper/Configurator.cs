using System;

namespace AutomapperTest
{
	public class Configurator : IConfiguration
	{
		private readonly TypeMap _typeMap = new TypeMap();
		
		public Configurator ()
		{}

		public void Create<TIn, TOut>() where TOut : class, new()
		{
			_typeMap.AddTypePair<TIn, TOut> ();
		}

		public void Create<TIn, TOut>( Func<TIn,TOut> creator )
		{
			if (creator == null)
				throw new ArgumentNullException ("creator");

			_typeMap.AddTypePair (creator);
		}

		public IMapping NewMapper()
		{
			return new Mapper (_typeMap);
		}
	}
}
