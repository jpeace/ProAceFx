using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using ProAceFx.Settings;

namespace ProAceFx.Tests.Settings
{
	[TestFixture]
	public class PropertyInfoAdapterTests
	{
		private PropertyInfo _testProperty;
		private PropertyInfoTester _tester;
		private PropertyInfo _testPropertyWithAttribute;
		[SetUp]
		public void Initialize()
		{
			_tester = new PropertyInfoTester
			          	{
			          		TestProperty = "Test",
			          		TestPropertyWithAttribute = "TestWithAttribute"
			          	};

			var properties = _tester.GetType().GetProperties();
			_testProperty = properties.Single(p => p.Name == "TestProperty");
			_testPropertyWithAttribute = properties.Single(p => p.Name == "TestPropertyWithAttribute");
		}
		[Test]
		public void Name_Returns_Underlying_Name()
		{
			var adapter = new PropertyInfoAdapter(_testProperty);
			Assert.AreEqual("TestProperty", adapter.Name);
		}
		[Test]
		public void Property_Type_Returns_Underlying_Property_Type()
		{
			var adapter = new PropertyInfoAdapter(_testProperty);
			Assert.AreEqual(typeof(string), adapter.PropertyType);
		}
		[Test]
		public void Get_Value_Returns_Underlying_Value()
		{
			var adapter = new PropertyInfoAdapter(_testProperty);
			Assert.AreEqual("Test", adapter.GetValue(_tester, null));
		}
		[Test]
		public void Set_Value_Sets_Underlying_Value()
		{
			var adapter = new PropertyInfoAdapter(_testProperty);
			var guid = Guid.NewGuid().ToString("N");
			adapter.SetValue(_tester, guid, null);
			Assert.AreEqual(guid, adapter.GetValue(_tester, null));
		}
		[Test]
		public void Get_Custom_Attributes_Returns_Underlying_Attributes()
		{
			var adapter = new PropertyInfoAdapter(_testPropertyWithAttribute);
			var attributes = adapter.GetCustomAttributes(typeof (System.ComponentModel.DescriptionAttribute), false);
			Assert.AreEqual(1, attributes.Length);
		}
		public class PropertyInfoTester
		{
			public string TestProperty { get; set; }
			[System.ComponentModel.Description("Test description")]
			public string TestPropertyWithAttribute { get; set; }
		}
	}
}