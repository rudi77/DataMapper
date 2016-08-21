using System;

namespace DataMapper
{
	public interface IMapping
	{
		/// <summary>
		/// Map the specified source to a new Type
		/// </summary>
		/// <param name="source">Source.</param>
		/// <typeparam name="TOut">The type parameter.</typeparam>
		TOut Map<TOut> (object source) where TOut : class, new();
	}
}

