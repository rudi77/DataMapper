using System;

namespace DataMapper
{
	public class FooEntity
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime CreatedOn { get; set; }

		public override string ToString ()
		{
			return string.Format ("[FooEntity: Id={0}, Name={1}, CreatedOn={2}]", Id, Name, CreatedOn);
		}
	}
}

