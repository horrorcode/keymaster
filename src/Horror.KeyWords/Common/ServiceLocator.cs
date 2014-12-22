// <copyright file="ServiceLocator.cs" company="Horror">
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
  static public class ServiceLocator
  {
    #region Fields

    private readonly static Dictionary<Type, Func<object>> services = new Dictionary<Type, Func<object>>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Registers the specified resolver.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resolver">The resolver.</param>
    public static void Register<T>(Func<T> resolver)
    {
      ServiceLocator.services[typeof(T)] = () => resolver();
    }

    /// <summary>
    /// Resets this instance.
    /// </summary>
    public static void Reset()
    {
      ServiceLocator.services.Clear();
    }

    /// <summary>
    /// Resolves this instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Resolve<T>()
    {
      return (T)ServiceLocator.services[typeof(T)]();
    }

    #endregion

  }
}
