namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows.Input;

  public interface IMainViewModel
  {
    /// <summary>
    /// Gets or sets the run command.
    /// </summary>
    /// <value>
    /// The run command.
    /// </value>
    ICommand RunCommand { get; set; }
    /// <summary>
    /// Gets or sets the input.
    /// </summary>
    /// <value>
    /// The input.
    /// </value>
    string Input { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; set; }
    /// <summary>
    /// Gets or sets the master keywords.
    /// </summary>
    /// <value>
    /// The master keywords.
    /// </value>
    MagicWordContainer MasterKeywords { get; set; }
    /// <summary>
    /// Runs the specified e.
    /// </summary>
    /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    void Run(KeyEventArgs e);
  }
}
