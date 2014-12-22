// <copyright file="MagicWordContainer.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="MagicWordContainer.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>

// <copyright file="MagicWordContainer.cs" company="Vestas">
// Copyright (c) 2014  License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Drawing;
  using System.Linq;

  /// <summary>
  /// A container that contains all options for the application and all magic words
  /// </summary>
  public class MagicWordContainer : ViewBinderBase, IDisposable
  {

    #region Fields

    private string _Background;

    private int _controlKey;

    private bool _Disposed;

    private Font _Font;

    private string _FontFamily;

    private float _FontSize;

    private string _FontStyle;

    private string _Foreground;

    private int _Height = 50;

    private int _key;

    private Collection<MagicWord> _MagicWords;

    private int _Width = 400;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the MagicWordContainer class.
    /// </summary>
    public MagicWordContainer()
    {
      _Background = "#ffe37222";
      _FontFamily = "Arial";
      _FontSize = 33f;
      _FontStyle = "Regular";
      _Foreground = "#ff37424a";
      _Font = FontFactory();
      _controlKey = (int)System.Windows.Input.Key.LeftAlt;
      _key = (int)System.Windows.Input.Key.Q;
      _MagicWords = new Collection<MagicWord>();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the background.
    /// </summary>
    /// <value>
    /// The background.
    /// </value>
    public string Background
    {
      get
      {
        return _Background;
      }
      set
      {
        if (_Background == value)
          return;
        _Background = value;
        RaisePropertyChanged(() => Background);
      }
    }

    /// <summary>
    /// Gets or sets the control key.
    /// </summary>
    /// <value>
    /// The control key.
    /// </value>
    public int ControlKey
    {
      get
      {
        return _controlKey;
      }
      set
      {
        if (_controlKey == value)
          return;
        _controlKey = value;
        RaisePropertyChanged(() => ControlKey);
      }
    }

    /// <summary>
    /// Gets the font.
    /// </summary>
    /// <value>
    /// The font.
    /// </value>
    public Font Font
    {
      get
      {
        return FontFactory();
      }
    }

    /// <summary>
    /// Gets or sets the font family.
    /// </summary>
    /// <value>
    /// The font family.
    /// </value>
    public string FontFamily
    {
      get
      {
        return _FontFamily;
      }
      set
      {
        if (_FontFamily == value)
          return;
        _FontFamily = value;
        RaisFontChange();
      }
    }

    /// <summary>
    /// Gets or sets the size of the font.
    /// </summary>
    /// <value>
    /// The size of the font.
    /// </value>
    public float FontSize
    {
      get
      {
        return _Font.Size;
      }
      set
      {
        if (_FontSize == value)
          return;
        _FontSize = value;
        RaisFontChange();
      }
    }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    /// <value>
    /// The font style.
    /// </value>
    public string FontStyle
    {
      get
      {
        return _Font.Style.ToString();
      }
      set
      {
        if (_FontStyle == value)
          return;
        _FontStyle = value;
        RaisFontChange();
      }
    }

    /// <summary>
    /// Gets or sets the foreground.
    /// </summary>
    /// <value>
    /// The foreground.
    /// </value>
    public string Foreground
    {
      get
      {
        return _Foreground;
      }
      set
      {
        if (_Foreground == value)
          return;
        _Foreground = value;
        RaisePropertyChanged(() => Foreground);
      }
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public int Height
    {
      get
      {
        return _Height;
      }
      set
      {
        if (_Height == value)
          return;
        _Height = value;
        RaisePropertyChanged(() => Height);
      }
    }

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <value>
    /// The key.
    /// </value>
    public int Key
    {
      get
      {
        return _key;
      }
      set
      {
        if (_key == value)
          return;
        _key = value;
        RaisePropertyChanged(() => Key);
      }
    }

    /// <summary>
    /// Gets or sets the magic words.
    /// </summary>
    /// <value>
    /// The magic words.
    /// </value>
    public Collection<MagicWord> MagicWords
    {
      get
      {
        return _MagicWords;
      }
    }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    /// <value>
    /// The width.
    /// </value>
    public int Width
    {
      get
      {
        return _Width;
      }
      set
      {
        if (_Width == value)
          return;
        _Width = value;
        RaisePropertyChanged(() => Width);
      }
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

    #endregion

    #region Internal Methods

    /// <summary>
    /// Fonts the factory.
    /// </summary>
    /// <returns></returns>
    internal Font FontFactory()
    {
      System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
      switch (_FontStyle)
      {
        case "Regular":
          style = System.Drawing.FontStyle.Regular;
          break;
        case "Bold":
          style = System.Drawing.FontStyle.Bold;
          break;
        case "Italic":
          style = System.Drawing.FontStyle.Italic;
          break;
        case "Underline":
          style = System.Drawing.FontStyle.Underline;
          break;
        case "Strikeout":
          style = System.Drawing.FontStyle.Strikeout;
          break;
        default:
          style = System.Drawing.FontStyle.Regular;
          break;
      }
      return new Font(string.IsNullOrEmpty(_FontFamily) ? "Arial" : _FontFamily, _FontSize == 0 ? 12 : _FontSize, style, GraphicsUnit.Point);
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
        if (_Font != null)
        {
          _Font.Dispose();
          _Font = null;
        }
      }

      _Disposed = true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Raises the font change.
    /// </summary>
    private void RaisFontChange()
    {
      RaisePropertyChanged(() => FontFamily);
      RaisePropertyChanged(() => FontSize);
      RaisePropertyChanged(() => FontStyle);
      RaisePropertyChanged(() => Height);
    }

    #endregion

  }
}
