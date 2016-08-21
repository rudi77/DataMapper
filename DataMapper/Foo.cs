using System;

namespace AutomapperTest
{
	public class Foo
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTimeOffset CreatedOn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Foo: Id={0}, Name={1}, CreatedOn={2}]", Id, Name, CreatedOn);
		}
	}
}

