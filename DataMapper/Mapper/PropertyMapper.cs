using System;
using System.Reflection;
using System.Diagnostics;

namespace AutomapperTest
{
	public static class PropertyMapper
	{
		public static TOut MapProperties<TOut> (object source, TOut destination, TypeMap typeMap) where TOut : class
		{
			if (source == null)
				throw new ArgumentNullException ("source");
			if (destination == null)
				throw new ArgumentNullException ("destination");
			if (typeMap == null)
				throw new ArgumentNullException ("typeMap");


			var sourceType = source.GetType ();
			var properties = sourceType.GetProperties (BindingFlags.Instance | BindingFlags.Public);

			var destType = destination.GetType ();

			foreach (var propertyInfo in properties)
			{
				var name = propertyInfo.Name;

				// Continue if the destination type does not have a property with "name"
				if (!HasProperty (destType, name))
					continue;

				// Get the property of the destiniation type
				var destPropertyInfo = destType.GetProperty (name);
			
				// Check if property types are equal
				if (PropertyTypesAreEqual (propertyInfo, destPropertyInfo))
				{
					// Get the value of the source type
					var value = propertyInfo.GetValue (source);
					destPropertyInfo.SetValue (destination, value);
				}
				// Property types differ, so check if there has been registered a type conversion method: new_type = f(old_type)
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

