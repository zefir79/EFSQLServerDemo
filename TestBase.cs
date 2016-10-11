using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using NUnit.Framework;

namespace Fortigent.TDDUtilities
{
    [Category("Db_Free")]
	public class TestBase
	{
        public void CreateHttpContext()
        {
            HttpContext.Current = new HttpContext(new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );
        }

        public void UserIsLoggedIn()
        {
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity("username"),
                new string[0]
                );
        }

        public void UserIsLoggedOut()
        {
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(String.Empty),
                new string[0]
                );
        }

        public void Describes(string description)
		{
			Console.WriteLine("----------------------------------");
			Console.WriteLine(description);
			Console.WriteLine("----------------------------------");
		}

		public void Scenario(string description)
		{
			Console.WriteLine("Scenario: " + description);
		}

		public void IsPending()
		{
			Console.WriteLine(" {0} -- PENDING", GetCaller());
			Assert.Inconclusive();
		}

		public string GetCaller()
		{
			var stack = new StackTrace();
			return stack.GetFrame(2).GetMethod().Name.Replace("_", " ");

		}

        public T[] RandomInstancesOf<T>(Action<T> andThenDoThisTo = null)
        {
            var factory = (Func<Action<T>,T>) RandomInstanceOf;
            return factory.MakeSome(andThenDoThisTo);
        }

        public T RandomInstanceOf<T>(Action<T> andThenDoThisTo = null)
        {
            var type = typeof (T);
            var randomInstance = Activator.CreateInstance<T>();
            foreach (var propertyInfo in type.GetProperties())
            {
                try
                {
                    var value = RandomDataHelper.Get(propertyInfo.PropertyType);
                    propertyInfo.SetValue(randomInstance, value, null);
                }
                catch (Exception)
                {
                    Console.Out.WriteLine("Warning: {0}.{1} cannot be randomized.", type.Name, propertyInfo.Name);
                }
            }
            if (andThenDoThisTo != null)
            {
                andThenDoThisTo(randomInstance);
            }
            return randomInstance;
        }
	}
}