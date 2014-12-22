// <copyright file="MainWindow.xaml.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="MainWindow.xaml.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="MainWindow.xaml.cs" company="Vestas">
// Copyright (c) 2014  License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
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
