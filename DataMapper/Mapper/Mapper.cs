using System;

namespace AutomapperTest
{
	public class Mapper : IMapping
	{
		private readonly TypeMap _typeMap;

		public Mapper (TypeMap typeMap)
		{
			if (typeMap == null)
				throw new ArgumentNullException ("typeMap");

			_typeMap = typeMap;
		}


		#region IMapping implementation
		public TOut Map<TOut> (object source) where TOut : class, new()
		{
			if (source == null)
				throw new ArgumentNullException ("source");

			var destination = _typeMap.GetDestinationObject<TOut> ( source.GetType() );

			return PropertyMapper.MapProperties (source, destination);
		}
		#endregion
	}
}

