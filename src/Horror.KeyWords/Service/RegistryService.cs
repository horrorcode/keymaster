// <copyright file="RegistryService.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// 
  /// </summary>
  public class RegistryService : IRegistryService
  {

    #region Constants

    private const string STR_SOFTWAREMicrosoftWindowsCurrentVersionRun = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the RegistryService class.
    /// </summary>
    public RegistryService()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Removes the application from the startup sequenze
    /// </summary>
    /// <param name="appName">Name of the application.</param>
    public void RemoveRunOnStart(string appName)
    {
      using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(STR_SOFTWAREMicrosoftWindowsCurrentVersionRun, true))
      {
        if (Key != null)
        {
          var test = Key.GetValue(appName);

          if (test != null)
            Key.DeleteValue(appName);
        }
      }
    }

    /// <summary>
    /// Adds the application to the startup sequenze
    /// </summary>
    /// <param name="appName">Name of the application.</param>
    /// <param name="appPath">The application path.</param>
    public void RunOnStart(string appName, string appPath)
    {
      using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(STR_SOFTWAREMicrosoftWindowsCurrentVersionRun, true))
        if (Key != null)
          Key.SetValue(appName, appPath);
    }

    #endregion

  }
}
