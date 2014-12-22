// <copyright file="App.xaml.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="App.xaml.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="App.xaml.cs" company="Vestas">
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

  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the App class.
    /// </summary>
    public App()
    {
      ServiceRegistration();
      InitializeComponent();
      Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles the DispatcherUnhandledException event of the Current control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      using (var page = new System.Windows.Forms.ThreadExceptionDialog(e.Exception))
        page.ShowDialog();
    }

    /// <summary>
    /// Services the registration.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
    private static void ServiceRegistration()
    {
      var hotkeyService = new HotkeyService();
      var keywordService = new KeywordService();

      MagicWordContainer magicWordContainer = keywordService.LoadMagicWords();

      var mainViewModel = new MainViewModel(magicWordContainer, hotkeyService, keywordService);
      ServiceLocator.Register<IMainViewModel>(() => mainViewModel);
      ServiceLocator.Register<IKeywordService>(() => keywordService);
      ServiceLocator.Register<IHotkeyService>(() => hotkeyService);
      ServiceLocator.Register<IRegistryService>(() => new RegistryService());
    }

    #endregion

  }
}
