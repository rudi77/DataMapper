using System;

namespace DataMapper
{
	public class HasConversionMethodAttribute : Attribute
	{
		public HasConversionMethodAttribute (Type mappedType)
		{
			if (mappedType == null)
				throw new ArgumentNullException ("mappedType");

			MappedType = mappedType;
		}

		public Type MappedType {get;set;}
	}
}

