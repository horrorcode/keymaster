// <copyright file="MagicWord.cs" company="Horror">
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
  /// A container for the magic word, this contains augument file paths alias etc.
  /// </summary>
  public class MagicWord : ViewBinderBase
  {
    #region Fields

    private string _Alias;

    private string _Arguments;

    private string _Filename;

    private bool _RunAsAdmin;

    private System.Diagnostics.ProcessWindowStyle _Startup;

    private string _WorkingDirectory;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the alias.
    /// </summary>
    /// <value>
    /// The alias.
    /// </value>
    public string Alias
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _Alias;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_Alias == value)
          return;
        _Alias = value;
        RaisePropertyChanged(() => Alias);
      }
    }

    /// <summary>
    /// Gets or sets the arguments.
    /// </summary>
    /// <value>
    /// The arguments.
    /// </value>
    public string Arguments
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _Arguments;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_Arguments == value)
          return;
        _Arguments = value;
        RaisePropertyChanged(() => Arguments);
      }
    }

    /// <summary>
    /// Gets or sets the filename.
    /// </summary>
    /// <value>
    /// The filename.
    /// </value>
    public string FileName
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _Filename;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_Filename == value)
          return;
        _Filename = value;
        RaisePropertyChanged(() => FileName);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [run as admin].
    /// </summary>
    /// <value>
    /// <c>true</c> if [run as admin]; otherwise, <c>false</c>.
    /// </value>
    public bool RunAsAdmin
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _RunAsAdmin;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_RunAsAdmin == value)
          return;
        _RunAsAdmin = value;

        RaisePropertyChanged(() => RunAsAdmin);
      }
    }

    /// <summary>
    /// Gets or sets the start up mode.
    /// </summary>
    /// <value>
    /// The start up mode.
    /// </value>
    public System.Diagnostics.ProcessWindowStyle Startup
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _Startup;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_Startup == value)
          return;
        _Startup = value;
        RaisePropertyChanged(() => Startup);
      }
    }

    /// <summary>
    /// Gets or sets the working directory.
    /// </summary>
    /// <value>
    /// The working directory.
    /// </value>
    public string WorkingDirectory
    {
      [System.Diagnostics.DebuggerStepThrough]
      get
      {
        return _WorkingDirectory;
      }
      [System.Diagnostics.DebuggerStepThrough]
      set
      {
        if (_WorkingDirectory == value)
          return;
        _WorkingDirectory = value;
        RaisePropertyChanged(() => WorkingDirectory);
      }
    }

    #endregion

  }
}
