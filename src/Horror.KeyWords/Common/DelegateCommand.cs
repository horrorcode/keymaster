// <copyright file="DelegateCommand.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Windows.Input;

  /// <summary>
  ///
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class DelegateCommand<T> : ICommand
    where T : class
  {
    #region Fields

    private readonly Predicate<T> _canExecute;

    private readonly Action<T> _execute;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
    /// </summary>
    /// <param name="execute">The execute.</param>
    public DelegateCommand(Action<T> execute)
      : this(execute, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateCommand{T}"/> class.
    /// </summary>
    /// <param name="execute">The execute.</param>
    /// <param name="canExecute">The can execute.</param>
    public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    /// <returns>
    /// true if this command can be executed; otherwise, false.
    /// </returns>
    public bool CanExecute(object parameter)
    {
      if (_canExecute == null)
      {
        return true;
      }
      return _canExecute((T)parameter);
    }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object parameter)
    {
      if (_execute != null)
      {
        T castParameter = (T)Convert.ChangeType(parameter, typeof(T), CultureInfo.InvariantCulture);
        _execute(castParameter);
      }
    }

    /// <summary>
    /// Raises the can execute changed.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
    public void RaiseCanExecuteChanged()
    {
      if (CanExecuteChanged != null)
      {
        CanExecuteChanged(this, EventArgs.Empty);
      }
    }

    #endregion

  }
}
