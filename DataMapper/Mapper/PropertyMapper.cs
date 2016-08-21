using System;
using System.Reflection;
using System.Diagnostics;

namespace AutomapperTest
{
	public static class PropertyMapper
	{
		public static TOut MapProperties<TOut> (object source, TOut destination) where TOut : class
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (destination == null)
				throw new ArgumentNullException ("destination");

			var sourceType = source.GetType ();
			var destType = destination.GetType ();

			var properties = sourceType.GetProperties (BindingFlags.Instance | BindingFlags.Public);

			foreach (var propertyInfo in properties)
			{
				var name = propertyInfo.Name;

				if (!HasProperty (destType, name))
					continue;
				
				var destPropertyInfo = destType.GetProperty (name);

				var value = propertyInfo.GetValue (source);

				// Check if property types are equal
				if (PropertyTypesAreEqual (propertyInfo, destPropertyInfo))
				{
					destPropertyInfo.SetValue (destination, value);
				}
				// Property types differ, so check if there has been registered a conversion method.
				else
				{

				}
			}

			return destination;
		}

		private static bool HasProperty (Type type, string propName)
		{
			Debug.Assert (type != null);
			Debug.Assert (!string.IsNullOrWhiteSpace (propName));

			return type.GetProperty (propName) != null;
		}

		private static bool PropertyTypesAreEqual (PropertyInfo sourceProp, PropertyInfo destProp)
		{
			Debug.Assert (sourceProp != null);
			Debug.Assert (destProp != null);

			return sourceProp.PropertyType == destProp.PropertyType;
		}
	}
}

