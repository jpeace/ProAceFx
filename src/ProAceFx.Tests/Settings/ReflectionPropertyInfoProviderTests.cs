using System.Linq;
using NUnit.Framework;
using ProAceFx.Settings;

namespace ProAceFx.Tests.Settings
{
	[TestFixture]
	public class ReflectionPropertyInfoProviderTests
	{
		[Test]
		public void Get_Properties_Reflects_Properties()
		{
			var reflectionProvider = new ReflectionPropertyInfoProvider();
			var properties = reflectionProvider.GetProperties(typeof(ReflectionPropertyInfoTester));
			Assert.IsNotNull(properties.SingleOrDefault(p => p.Name == "SettingA"));
			Assert.IsNotNull(properties.SingleOrDefault(p => p.Name == "SettingB"));
			Assert.IsNotNull(properties.SingleOrDefault(p => p.Name == "SettingC"));
		}

		public class ReflectionPropertyInfoTester
		{
			public string SettingA { get; set; }
			public string SettingB { get; set; }
			public string SettingC { get; set; }
		}
	}
}
