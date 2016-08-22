using System;
using DataMapper;

namespace ReflectionExample
{
	public class FooEntity
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		[HasConversionMethod(typeof(DateTimeOffset))]
		public DateTime CreatedOn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[FooEntity: Id={0}, Name={1}, CreatedOn={2}]", Id, Name, CreatedOn);
		}
	}
}

