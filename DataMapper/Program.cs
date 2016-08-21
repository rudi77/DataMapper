﻿using System;

namespace AutomapperTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			var config = new Configurator ();
			config.Create<Foo, FooEntity> ();
			config.Create<DateTimeOffset, DateTime> ( dto => dto.DateTime );

			var mapper = config.NewMapper ();

			var foo = CreateFoo ();
			var fooEntity = mapper.Map<FooEntity> (foo);

			Console.WriteLine (foo);
			Console.WriteLine ( fooEntity );

			Console.WriteLine ( "Press any key to exit..." );
			Console.ReadKey ();
		}

		private static Foo CreateFoo()
		{
			return new Foo {
				Id = Guid.NewGuid (),
				Name = "FooBar",
				CreatedOn = new DateTimeOffset (DateTime.Now)
			};
		}
	}
}
