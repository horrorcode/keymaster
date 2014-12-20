namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public interface IRegistryService
  {
    /// <summary>
    /// Removes the application from the startup sequenze
    /// </summary>
    /// <param name="appName">Name of the application.</param>
    void RemoveRunOnStart(string appName);
    /// <summary>
    /// Adds the application to the startup sequenze
    /// </summary>
    /// <param name="appName">Name of the application.</param>
    /// <param name="appPath">The application path.</param>
    void RunOnStart(string appName, string appPath);
  }
}
