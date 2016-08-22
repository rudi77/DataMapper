using System;
using System.Collections.Generic;
using System.Linq;

namespace Example
{
	public class DataContext
	{
		private readonly List<object> _objects = new List<object>();

		public DataContext ()
		{
		}

		public DataContext SetItem<T> (T item)
		{
			_objects.Add (item);

			return this;
		}

		public IQueryable<T> Set<T>()
		{
			return _objects.AsQueryable ().OfType<T> ();
		}
	}
}

