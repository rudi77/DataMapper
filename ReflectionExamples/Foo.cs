using System;
using DataMapper;

namespace ReflectionExample
{
	public class Foo
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		[HasConversionMethod(typeof(DateTime))]
		public DateTimeOffset CreatedOn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Foo: Id={0}, Name={1}, CreatedOn={2}]", Id, Name, CreatedOn);
		}
	}
}

