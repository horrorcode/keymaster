namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
      InitializeComponent();
      DataContext = ServiceLocator.Resolve<IMainViewModel>();
    }

    #endregion

    /// <summary>
    /// Handles the Loaded event of the masterKeyword control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
    private void masterKeyword_Loaded(object sender, RoutedEventArgs e)
    {
      var cmb = sender as ComboBox;
      TextBox txt = (TextBox)cmb.Template.FindName("PART_EditableTextBox", cmb);
      txt.Focus();
      txt.ContextMenu = cmb.ContextMenu;
    }
  }
}
