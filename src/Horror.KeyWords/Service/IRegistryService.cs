// <copyright file="IRegistryService.cs" company="Horror">
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
