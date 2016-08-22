using System;
using System.Reflection;
using ReflectionExample;
using DataMapper;
using System.Linq;
using System.Collections.Generic;

namespace ReflectionExamples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var config = new Configurator ();
			config.Create<Foo, FooEntity> ();
			config.Create<FooEntity,Foo> ();
			config.Create<DateTimeOffset, DateTime> (dto => ((DateTimeOffset)dto).DateTime);
			config.Create<DateTime, DateTimeOffset>( dt => new DateTimeOffset( (DateTime)dt ));

			var mapper = config.NewMapper ();

			var context = new DataContext ();
			context
				.Set (CreateFooEntity ("Bar1"))
				.Set (CreateFooEntity ("Bar2"))
				.Set (CreateFooEntity ("Bar3"))
				.Set (CreateFooEntity ("Bar4"));

			var result = CallGeneric<DataContext, Foo> (context, typeof(FooEntity), "Set", mapper);

			result.ToList ().ForEach (Console.WriteLine);

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

		private static FooEntity CreateFooEntity( string name )
		{
			return new FooEntity {
				Id = Guid.NewGuid (),
				Name = name,
				CreatedOn = DateTime.Now
			};
		}

		private static IEnumerable<TResult> CallGeneric<T, TResult>( T instance, Type targetType, string methodName, IMapping mapper ) 
			where TResult : class, new()
		{
			// Search for the right method
			var type = typeof(T);
			var mi = type.GetMethod (methodName, new Type[] {});

			//DisplayGenericMethodInfo (mi);

			// Create a generic method whose type is targetType
			var miConstructed = mi.MakeGenericMethod( targetType );

			//DisplayGenericMethodInfo (miConstructed);

			// Invoke the generic method
			var result = miConstructed.Invoke (instance, null) as IQueryable<object>;

			return result.ToList ().Select (r => mapper.Map<TResult> (r));
		}

		private static void DisplayGenericMethodInfo(MethodInfo mi)
		{
			Console.WriteLine("\r\n{0}", mi);

			Console.WriteLine("\tIs this a generic method definition? {0}", 
				mi.IsGenericMethodDefinition);

			Console.WriteLine("\tIs it a generic method? {0}", 
				mi.IsGenericMethod);

			Console.WriteLine("\tDoes it have unassigned generic parameters? {0}", 
				mi.ContainsGenericParameters);

			// If this is a generic method, display its type arguments.
			//
			if (mi.IsGenericMethod)
			{
				Type[] typeArguments = mi.GetGenericArguments();

				Console.WriteLine("\tList type arguments ({0}):", 
					typeArguments.Length);

				foreach (Type tParam in typeArguments)
				{
					// IsGenericParameter is true only for generic type
					// parameters.
					//
					if (tParam.IsGenericParameter)
					{
						Console.WriteLine("\t\t{0}  parameter position {1}" +
							"\n\t\t   declaring method: {2}",
							tParam,
							tParam.GenericParameterPosition,
							tParam.DeclaringMethod);
					}
					else
					{
						Console.WriteLine("\t\t{0}", tParam);
					}
				}
			}
		}
	}
}
