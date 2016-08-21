using System;
using System.Collections.Generic;

namespace AutomapperTest
{
	public class TypeMap
	{
		private readonly Dictionary<Type,Type> _defaultConstructableTypes = new Dictionary<Type,Type>();
		private readonly Dictionary<Type,Func<object,object>> _constructableTypes = new Dictionary<Type, Func<object,object>>();

		public TypeMap ()
		{}

		public void AddTypePair<TIn, TOut>() where TOut : class, new()
		{
			_defaultConstructableTypes.Add(typeof(TIn), typeof(TOut) );
		}

		public void AddTypePair<TIn, TOut>( Func<TIn,TOut> creator )
		{
			_constructableTypes.Add (typeof(TIn), creator as Func<object,object> );
		}

		public TOut CreateInstance<TOut>( Type sourceType ) where TOut : class, new()
		{
			if (!_defaultConstructableTypes.ContainsKey (sourceType))
				throw new Exception ("Invalid source type");

			var destinationType = _defaultConstructableTypes [sourceType];

			if (destinationType != typeof(TOut))
				throw new Exception ("Invalid destination type. Source type is mapped to a different destination type");

			return Activator.CreateInstance<TOut> ();
		}

		public Func<TIn,TOut> GetCreatorMethod<TIn, TOut>()
		{
			var sourceType = typeof(TIn);

			if (!_constructableTypes.ContainsKey(sourceType))
				throw new Exception ("Invalid source type");

			var creatorMethod = _constructableTypes [sourceType] as Func<TIn, TOut>;

			if (creatorMethod == null)
				throw new Exception ("Invalid destination type. Source type is mapped to a different destination type");

			return creatorMethod;
		}

		public Func<object,object> GetCreatorMethod( Type sourceType )
		{
			var sourceType = typeof(TIn);

			if (!_constructableTypes.ContainsKey(sourceType))
				throw new Exception ("Invalid source type");

			var creatorMethod = _constructableTypes [sourceType] as Func<TIn, TOut>;

			if (creatorMethod == null)
				throw new Exception ("Invalid destination type. Source type is mapped to a different destination type");

			return creatorMethod;
		}
	}
}

