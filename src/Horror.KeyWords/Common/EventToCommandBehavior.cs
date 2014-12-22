// <copyright file="EventToCommandBehavior.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Globalization;
  using System.Reflection;
  using System.Windows;
  using System.Windows.Input;
  using System.Windows.Interactivity;

  /// <summary>
  /// Behavior that will connect an UI event to a viewmodel Command,
  /// allowing the event arguments to be passed as the CommandParameter.
  /// </summary>
  public class EventToCommandBehavior : Behavior<FrameworkElement>
  {

    #region Readonly

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommandBehavior), new PropertyMetadata(null));

    public static readonly DependencyProperty EventProperty = DependencyProperty.Register("Event", typeof(string), typeof(EventToCommandBehavior), new PropertyMetadata(null, OnEventChanged));

    public static readonly DependencyProperty PassArgumentsProperty = DependencyProperty.Register("PassArguments", typeof(bool), typeof(EventToCommandBehavior), new PropertyMetadata(false));

    #endregion

    #region Fields

    private Delegate _handler;

    private EventInfo _oldEvent;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the command.
    /// </summary>
    /// <value>
    /// The command.
    /// </value>
    public ICommand Command
    {
      get
      {
        return (ICommand)GetValue(CommandProperty);
      }
      set
      {
        SetValue(CommandProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    /// <value>
    /// The event.
    /// </value>
    public string Event
    {
      get
      {
        return (string)GetValue(EventProperty);
      }
      set
      {
        SetValue(EventProperty, value);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [pass arguments].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [pass arguments]; otherwise, <c>false</c>.
    /// </value>
    public bool PassArguments
    {
      get
      {
        return (bool)GetValue(PassArgumentsProperty);
      }
      set
      {
        SetValue(PassArgumentsProperty, value);
      }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Called after the behavior is attached to an AssociatedObject.
    /// </summary>
    /// <remarks>
    /// Override this to hook up functionality to the AssociatedObject.
    /// </remarks>
    protected override void OnAttached()
    {
      AttachHandler(Event);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Attaches the handler to the event
    /// </summary>
    private void AttachHandler(string eventName)
    {
      if (_oldEvent != null)
        _oldEvent.RemoveEventHandler(AssociatedObject, _handler);
      if (!string.IsNullOrEmpty(eventName))
      {
        var ei = AssociatedObject.GetType().GetEvent(eventName);
        if (ei != null)
        {
          var mi = GetType().GetMethod("ExecuteCommand", BindingFlags.Instance | BindingFlags.NonPublic);
          _handler = Delegate.CreateDelegate(ei.EventHandlerType, this, mi);
          ei.AddEventHandler(AssociatedObject, _handler);
          _oldEvent = ei;
        }
        else
          throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The event '{0}' was not found on type '{1}'", eventName, AssociatedObject.GetType().Name));
      }
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void ExecuteCommand(object sender, EventArgs e)
    {
      var parameter = PassArguments ? e : null;
      if (Command != null)
      {
        if (Command.CanExecute(parameter))
          Command.Execute(parameter);
      }
    }

    /// <summary>
    /// Called when [event changed].
    /// </summary>
    /// <param name="d">The d.</param>
    /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
    private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var beh = (EventToCommandBehavior)d;

      if (beh.AssociatedObject != null)
        beh.AttachHandler((string)e.NewValue);
    }

    #endregion

  }
}
