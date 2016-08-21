using System;

namespace DataMapper
{
	public class Configurator : IConfiguration
	{
		private readonly TypeMap _typeMap = new TypeMap();
		
		public Configurator ()
		{}

		public void Create<TIn,TOut>() where TOut : class, new()
		{
			_typeMap.AddTypePair<TIn, TOut> ();
		}

		public void Create<TIn,TOut>( Func<object,object> converter)
		{
			if (converter == null)
				throw new ArgumentNullException ("converter");

			_typeMap.AddConversionMethodForType<TIn,TOut> (converter);
		}

		public IMapping NewMapper()
		{
			return new Mapper (_typeMap);
		}
	}
}
