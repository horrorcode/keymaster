namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Windows;
  using System.Windows.Input;

  /// <summary>
  /// Model view that control the input and execution
  /// </summary>
  public class MainViewModel : ViewBinderBase, IDisposable, IMainViewModel
  {

    #region Fields

    private bool _Disposed;

    private IHotkeyService _HotKeyService;

    private string _Input;

    private bool _IsVisible = false;

    private IKeywordService _KeywordService;

    private MagicWordContainer _MasterKeywords;

    System.Windows.Forms.NotifyIcon _notifyIcon;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    /// <param name="magicWordContainer">The magic word container.</param>
    /// <param name="hotkeyService">The hot key service.</param>
    /// <param name="keywordService">The keyword service.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
    public MainViewModel(MagicWordContainer magicWordContainer, IHotkeyService hotkeyService, IKeywordService keywordService)
    {
      _HotKeyService = hotkeyService;
      _KeywordService = keywordService;
      _MasterKeywords = magicWordContainer;

      LoadedCommand = new DelegateCommand<object>(Loaded);
      RunCommand = new DelegateCommand<KeyEventArgs>(Run);

      _notifyIcon = new System.Windows.Forms.NotifyIcon() { Icon = Properties.Resources.Keymaster, Visible = true };
      _notifyIcon.DoubleClick += (sender, args) => Show();

      _HotKeyService.KeyDown += _HotKeyService_KeyDown;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the input.
    /// </summary>
    /// <value>
    /// The input.
    /// </value>
    public string Input
    {
      get
      {
        return _Input;
      }
      set
      {
        if (_Input == value)
          return;
        _Input = value;

        RaisePropertyChanged(() => Input);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is visible.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is visible; otherwise, <c>false</c>.
    /// </value>
    public bool IsVisible
    {
      get
      {
        return _IsVisible;
      }
      set
      {
        if (_IsVisible == value)
          return;
        _IsVisible = value;

        RaisePropertyChanged(() => IsVisible);
      }
    }

    /// <summary>
    /// Gets the loaded command.
    /// </summary>
    /// <value>
    /// The loaded command.
    /// </value>
    public ICommand LoadedCommand { get; set; }

    /// <summary>
    /// Gets or sets the master keywords.
    /// </summary>
    /// <value>
    /// The master keywords.
    /// </value>
    public MagicWordContainer MasterKeywords
    {
      get
      {
        return _MasterKeywords;
      }
      set
      {
        if (_MasterKeywords == value)
          return;
        _MasterKeywords = value;
        RaisePropertyChanged(() => MasterKeywords);
      }
    }

    /// <summary>
    /// Gets or sets the run command.
    /// </summary>
    /// <value>
    /// The run command.
    /// </value>
    public ICommand RunCommand { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Loadeds this instance.
    /// </summary>
    /// <returns></returns>
    public void Loaded(object o)
    {
      MessageBox.Show("Hej");
    }

    /// <summary>
    /// Runs the
    /// </summary>
    public void Run(KeyEventArgs e)
    {
      if (e == null)
        return;

      if (e.Key == Key.Enter)
      {
        _KeywordService.Execute(Input);
        Input = "";
      }
      else
        if (e.Key == Key.Escape)
        {
          IsVisible = false;
          Input = "";
        }
    }

    #endregion

    #region Internal Methods

    /// <summary>
    /// Closes this instance.
    /// </summary>
    internal void Close()
    {
      IsVisible = false;
    }

    /// <summary>
    /// Shows this instance.
    /// </summary>
    internal void Show()
    {
      IsVisible = true;
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (_Disposed)
        return;

      if (disposing)
      {
        if (_MasterKeywords != null)
        {
          _MasterKeywords.Dispose();
          _MasterKeywords = null;
        }

        if (_KeywordService != null)
        {
          _KeywordService.Dispose();
          _KeywordService = null;
        }

        if (_HotKeyService != null)
        {
          _HotKeyService.Dispose();
          _HotKeyService = null;
        }
      }

      _Disposed = true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Handles the KeyDown event of the _HotKeyService control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="GlobalKeyEventArgs"/> instance containing the event data.</param>
    void _HotKeyService_KeyDown(object sender, GlobalKeyEventArgs e)
    {
      if (_MasterKeywords.ControlKey == (int)e.ControlKey && _MasterKeywords.Key == (int)e.Key)
        Show();
    }

    /// <summary>
    /// Determines whether this instance [can execute input].
    /// </summary>
    /// <returns></returns>
    private bool CanExecuteInput()
    {
      return !string.IsNullOrEmpty(Input);
    }

    #endregion

  }
}