namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public interface IHotkeyService
  {
    System.Windows.Input.Key ControlKey { get; set; }
    /// <summary>
    /// Fired when any of the keys is pressed down.
    /// </summary>
    event EventHandler<GlobalKeyEventArgs> KeyDown;
    /// <summary>
    /// Fired when any of the keys is released.
    /// </summary>
    event EventHandler<GlobalKeyEventArgs> KeyUp;
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    void Dispose();
  }
}
