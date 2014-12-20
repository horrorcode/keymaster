namespace Horror.Keywords.Tests
{
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using System;
  using System.Collections.Generic;
  using System.Linq;

  [TestClass()]
  public class RegistryServiceTests
  {
    private const string STR_SOFTWAREMicrosoftWindowsCurrentVersionRun = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

    [TestMethod()]
    public void RemoveRunOnStartTest()
    {


      using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(STR_SOFTWAREMicrosoftWindowsCurrentVersionRun, true))
      {
        Key.SetValue("test", "testpath");
        var s = new RegistryService();
        s.RemoveRunOnStart("test");
        var test = Key.GetValue("test");
        Assert.IsNull(test);
      }
    }

    [TestMethod()]
    public void RunOnStartTest()
    {
      var s = new RegistryService();
      s.RunOnStart("test", "testpath");

      using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(STR_SOFTWAREMicrosoftWindowsCurrentVersionRun, true))
      {
        var test = Key.GetValue("test");
        Assert.IsNotNull(test);
        Key.DeleteValue("test");
      }
    }
  }
}
