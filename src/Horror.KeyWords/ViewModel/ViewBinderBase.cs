namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Linq.Expressions;

  /// <summary>
  /// Base class for the view model
  /// </summary>
  public abstract class ViewBinderBase : INotifyPropertyChanged
  {

    #region Events

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Protected Methods

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
    protected static string GetPropertyName<T>(Expression<Func<T>> expression)
    {
      return GetPropertyNameFast(expression);
    }

    /// <summary>
    /// Gets the property name fast.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException">expression</exception>
    protected static string GetPropertyNameFast(LambdaExpression expression)
    {
      if (expression == null)
        throw new ArgumentNullException("expression", "expression is null.");

      MemberExpression body = expression.Body as MemberExpression;
      if (body == null)
        throw new ArgumentException("LambdaExpression is wrong");
      return body.Member.Name;
    }

    /// <summary>
    /// Raises the event.
    /// </summary>
    /// <param name="property">The property.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
    protected void RaisePropertyChanged(string property)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(property));
    }

    /// <summary>
    /// Raises the property changed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression">The expression.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"),
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
    protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
    {
      RaisePropertyChanged(GetPropertyName<T>(expression));
    }

    #endregion

  }
}
