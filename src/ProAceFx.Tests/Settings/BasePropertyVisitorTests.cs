using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using ProAceFx.Settings;
using Rhino.Mocks;

namespace ProAceFx.Tests.Settings
{
	[TestFixture]
	public class BasePropertyVisitorTests
	{
		private MockRepository _mockRepository;
		private IPropertyInfoProvider _propertyProvider;
		[SetUp]
		public void Initialize()
		{
			_mockRepository = new MockRepository();
			_propertyProvider = _mockRepository.DynamicMock<IPropertyInfoProvider>();
		}
		[Test]
		public void Get_Attribute_Finds_Attribute()
		{
			var attr = new RequiredAttribute();
			var propertyInfo = _mockRepository.DynamicMock<IPropertyInfo>();
			propertyInfo
				.Expect(p => p.GetCustomAttributes(null, false))
				.IgnoreArguments()
				.Return(new object[] { attr });
			
			_mockRepository.ReplayAll();

			var visitor = new FakePropertyVisitor(_propertyProvider);
			Assert.AreEqual(attr, visitor.GetAttribute<RequiredAttribute>(propertyInfo));
		}
		
		[Test]
		public void Visit_Returns_Instance_If_No_Properties()
		{
			_propertyProvider
				.Expect(p => p.GetProperties(null))
				.IgnoreArguments()
				.Return(new List<IPropertyInfo>());
			_mockRepository.ReplayAll();

			var visitor = new FakePropertyVisitor(_propertyProvider);
			visitor.Visit((object)null); // throws not implemented if hit
		}

		[Test]
		public void Set_Value_Uses_Type_Converter()
		{
			var locator = _mockRepository.DynamicMock<IServiceLocator>();
			locator
				.Expect(l => l.GetInstance(typeof (NestedBasePropertyVisitorTesterConverter)))
				.IgnoreArguments()
				.Return(new NestedBasePropertyVisitorTesterConverter());
			_mockRepository.ReplayAll();

			ServiceLocator.SetLocatorProvider(() => locator);

			var tester = new BasePropertyVisitorTester();
			var provider = new ReflectionPropertyInfoProvider();
			var visitor = new FakePropertyVisitor(provider);
			visitor.SetPropertyValue(provider.GetProperties(typeof(BasePropertyVisitorTester)).Single(p => p.Name == "Setting2"), 
				tester, "Hello, World!");

			Assert.AreEqual("Hello, World!", tester.Setting2.SettingA);

		}

		public class FakePropertyVisitor : BasePropertyVisitor
		{
			public FakePropertyVisitor(IPropertyInfoProvider propertyProvider) 
				: base(propertyProvider)
			{
			}

			protected override TInstance Visit<TInstance>(IEnumerable<IPropertyInfo> properties, TInstance instance)
			{
				throw new NotImplementedException();
			}
		}

		public class BasePropertyVisitorTester
		{
			[Required]
			public string Setting1 { get; set; }
			[TypeConverter("ProAceFx.Tests.Settings.BasePropertyVisitorTests+NestedBasePropertyVisitorTesterConverter, ProAceFx.Tests")]
			public NestedBasePropertyVisitorTester Setting2 { get; set; }
		}

		public class NestedBasePropertyVisitorTester
		{
			public string SettingA { get; set; }
		}

		public class NestedBasePropertyVisitorTesterConverter : ITypeConverter
		{
			public object ConvertTo(Type destinationType, object value)
			{
				if(destinationType != typeof(NestedBasePropertyVisitorTester))
				{
					throw new InvalidOperationException("Unable to cast.");
				}

				return new NestedBasePropertyVisitorTester
				       	{
				       		SettingA = value.ToString()
				       	};
			}
		}
	}
}
