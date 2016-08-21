using System;
using System.Collections.Generic;

namespace AutomapperTest
{
	public class TypeMap
	{
		private readonly Dictionary<Type,Func<object>> _constructableTypes = new Dictionary<Type, Func<object>>();

		public TypeMap ()
		{
		}

		public void AddTypePair<TIn, TOut>() where TOut : class, new()
		{
			_constructableTypes.Add(typeof(TIn), () => new TOut() );
		}

		public void AddTypePair<TIn, TOut>( Func<TOut> creator )
		{
			_constructableTypes.Add (typeof(TIn), creator as Func<object> );
		}

		public TOut GetDestinationObject<TOut>( Type sourceType ) where TOut : class, new()
		{
			if (!_constructableTypes.ContainsKey (sourceType))
				throw new Exception ("Invalid source type");

			var destinationItem = _constructableTypes [sourceType] () as TOut;

			if (destinationItem == null)
				throw new InvalidCastException ("Invalid expected type");

			return destinationItem;
		}
	}
}

