namespace Horror.Keywords.Tests
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  [TestClass()]
  public class ServiceLocatorTests
  {
    private const string MyConst = "ConstString";
    #region Public Methods

    [TestMethod()]
    public void RegisterTest()
    {

      ServiceLocator.Register<int>(() => 1);
      Assert.AreEqual(1, ServiceLocator.Resolve<int>());
    }

    [TestMethod()]
    public void ResolveTest()
    {

      ServiceLocator.Register<string>(() => MyConst);
      Assert.AreEqual(MyConst, ServiceLocator.Resolve<string>());
    }

    [TestMethod()]
    [ExpectedException(typeof(ApplicationException))]
    public void DuplicateTest()
    {

      ServiceLocator.Register<string>(() => MyConst);
      ServiceLocator.Register<string>(() => MyConst);

    }

    #endregion

  }
}
