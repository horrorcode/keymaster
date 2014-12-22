namespace Horror.Keywords.Tests
{
  using NUnit.Framework;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  [TestFixture]
  public class ServiceLocatorTests
  {
    private const string MyConst = "ConstString";
    #region Public Methods

    [Test()]
    public void RegisterTest()
    {

      ServiceLocator.Register<int>(() => 1);
      Assert.AreEqual(1, ServiceLocator.Resolve<int>());
    }

    [Test()]
    public void ResolveTest()
    {

      ServiceLocator.Register<string>(() => MyConst);
      Assert.AreEqual(MyConst, ServiceLocator.Resolve<string>());
    }

    [Test()]
    [ExpectedException(typeof(ApplicationException))]
    public void DuplicateTest()
    {

      ServiceLocator.Register<string>(() => MyConst);
      ServiceLocator.Register<string>(() => MyConst);

    }

    #endregion

  }
}
