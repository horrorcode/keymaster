namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Windows;
  using System.Xml.Serialization;

  /// <summary>
  /// Keyword service provide you with magic words, save and load the settings file
  /// </summary>
  public class KeywordService : IDisposable, IKeywordService
  {

    #region Fields

    private bool _Disposed;

    private string _KeyMasterSettingsPath;

    private MagicWordContainer _MagicWordContainer;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the KeywordService class.
    /// </summary>
    public KeywordService()
    {
      _Disposed = false;
      _KeyMasterSettingsPath = String.Empty;
    }

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
    /// Executes the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    public void Execute(string input)
    {
      var keyword = _MagicWordContainer.MagicWords.Where(m => m.Alias == input).FirstOrDefault();

      if (keyword != null)
      {
        using (var process = new Process())
        {
          process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
          process.StartInfo.FileName = keyword.FileName;
          process.StartInfo.Arguments = keyword.Arguments;
          process.StartInfo.UseShellExecute = false;

          //Vista or higher check
          if (System.Environment.OSVersion.Version.Major >= 6 && keyword.RunAsAdmin)
            process.StartInfo.Verb = "runas";

          if (!process.Start())
          {
            MessageBox.Show(
              string.Format(CultureInfo.InvariantCulture, "Error starting alias {0}", input),
              Properties.Horror_Keyword_Resources.AliasNotFound,
              MessageBoxButton.OK,
              MessageBoxImage.Hand);
          }
        }
      }
      else
      {
        MessageBox.Show(
          string.Format(CultureInfo.InvariantCulture, Properties.Horror_Keyword_Resources.TheAliasX0WasNotFound, input),
          Properties.Horror_Keyword_Resources.AliasNotFound,
          MessageBoxButton.OK,
          MessageBoxImage.Hand);
      }
    }

    /// <summary>
    /// Loads the magic words.
    /// </summary>
    public MagicWordContainer LoadMagicWords()
    {
      XmlSerializer serializer = new XmlSerializer(typeof(List<MagicWord>));
      var keywordFile = GetKeyworkPath();

      if (File.Exists(keywordFile))
      {
        using (var reader = File.OpenText(keywordFile))
          _MagicWordContainer = serializer.Deserialize(reader) as MagicWordContainer;
      }

      if (_MagicWordContainer == null)
      {
        var workingDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
        var shutdown = Path.Combine(workingDirectory, @"\System32\shutdown.exe");

        _MagicWordContainer = new MagicWordContainer();
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "notepad", FileName = Path.Combine(workingDirectory, "notepad.exe"), WorkingDirectory = workingDirectory });
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "sleep", FileName = shutdown, Arguments = "-s", WorkingDirectory = workingDirectory });
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "reboot", FileName = shutdown, Arguments = "-r", WorkingDirectory = workingDirectory });
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "logoff", FileName = shutdown, Arguments = "-l", WorkingDirectory = workingDirectory });
        //TODO: Figure out the parameter for shutdown
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "shutdown", FileName = shutdown, Arguments = "-l", WorkingDirectory = workingDirectory });
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "standby", FileName = shutdown, Arguments = "powrprof.dll,SetSuspendState Standby", WorkingDirectory = workingDirectory });
        _MagicWordContainer.MagicWords.Add(new MagicWord() { Alias = "hibernate", FileName = shutdown, Arguments = "powrprof.dll,SetSuspendState Hibernate", WorkingDirectory = workingDirectory });
      }

      return _MagicWordContainer;
    }

    /// <summary>
    /// Saves the magic words.
    /// </summary>
    public void SaveMagicWords()
    {
      XmlSerializer ser = new XmlSerializer(typeof(List<MagicWord>));
      using (var streamWriter = new StreamWriter(GetKeyworkPath()))
        ser.Serialize(streamWriter, _MagicWordContainer);
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
        if (_MagicWordContainer != null)
        {
          _MagicWordContainer.Dispose();
          _MagicWordContainer = null;
        }
      }

      _Disposed = true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets the keywork path.
    /// </summary>
    /// <returns></returns>
    private string GetKeyworkPath()
    {
      if (!string.IsNullOrEmpty(_KeyMasterSettingsPath) || !File.Exists(_KeyMasterSettingsPath))
      {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Keymaster");
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        _KeyMasterSettingsPath = Path.Combine(path, "KeymasterSettings.xml");
      }

      return _KeyMasterSettingsPath;
    }

    #endregion

  }
}
