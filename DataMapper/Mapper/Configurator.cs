using System;

namespace AutomapperTest
{
	public class Configurator : IConfiguration
	{
		private readonly TypeMap _typeMap = new TypeMap();
		
		public Configurator ()
		{
		}

		public void Create<TIn, TOut>() where TOut : class, new()
		{
			_typeMap.AddTypePair<TIn, TOut> ();
		}

		public IMapping NewMapper()
		{
			return new Mapper (_typeMap);
		}
	}
}

