// <copyright file="GlobalKeyEventArgs.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Windows.Input;

  /// <summary>
  /// Raw KeyEvent arguments.
  /// </summary>
  public class GlobalKeyEventArgs : EventArgs
  {

    #region Constructors

    /// <summary>
    /// Create raw keyevent arguments.
    /// </summary>
    /// <param name="VKCode"></param>
    /// <param name="isSysKey"></param>
    public GlobalKeyEventArgs(Key key, Key controlKeyDown)
    {
      Key = key;
      ControlKey = controlKeyDown;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalKeyEventArgs"/> class.
    /// </summary>
    public GlobalKeyEventArgs()
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// WPF Key of the key.
    /// </summary>
    public Key ControlKey { get; set; }

    /// <summary>
    /// VKCode of the key.
    /// </summary>
    public Key Key { get; set; }

    #endregion

  }
}
