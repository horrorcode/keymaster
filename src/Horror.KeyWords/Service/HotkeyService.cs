// <copyright file="HotkeyService.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Globalization;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Runtime.InteropServices;
  using System.Windows.Input;

  /// <summary>
  /// The hotkey service
  /// </summary>
  public class HotkeyService : IDisposable, IHotkeyService
  {

    #region Fields

    private bool _Disposed;

    /// <summary>
    /// Event to be invoked asynchronously (BeginInvoke) each time key is pressed.
    /// </summary>
    private readonly KeyboardCallbackAsync _HookedKeyboardCallbackAsync;

    /// <summary>
    /// Contains the hooked callback in runtime.
    /// </summary>
    private readonly NativeMethods.LowLevelKeyboardProc _HookedLowLevelKeyboardProc;

    /// <summary>
    /// Hook ID
    /// </summary>
    private readonly IntPtr _HookId = IntPtr.Zero;

    #endregion

    #region Events

    /// <summary>
    /// Fired when any of the keys is pressed down.
    /// </summary>
    public event EventHandler<GlobalKeyEventArgs> KeyDown;

    /// <summary>
    /// Fired when any of the keys is released.
    /// </summary>
    public event EventHandler<GlobalKeyEventArgs> KeyUp;

    #endregion

    #region Delegates

    /// <summary>
    /// Asynchronous callback hook.
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    private delegate void KeyboardCallbackAsync(NativeMethods.KeyEvent keyEvent, int vkCode);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the HotkeyService class.
    /// </summary>
    public HotkeyService()
    {
      // We have to store the HookCallback, so that it is not garbage collected runtime
      _HookedLowLevelKeyboardProc = (NativeMethods.LowLevelKeyboardProc)LowLevelKeyboardProc;

      // Set the hook
      _HookId = NativeMethods.SetHook(_HookedLowLevelKeyboardProc);

      // Assign the asynchronous callback event
      _HookedKeyboardCallbackAsync = KeyPressCallbackAsync;
    }

    #endregion

    #region Properties

    public Key ControlKey { get; set; }

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
        NativeMethods.UnhookWindowsHookEx(_HookId);
      }

      _Disposed = true;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Determines whether [is control key] [the specified key].
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    static private bool IsControlKey(Key key)
    {
      bool isControlKey = false;
      switch (key)
      {
        case System.Windows.Input.Key.LeftCtrl:
          isControlKey = true;
          break;
        case System.Windows.Input.Key.RightCtrl:
          isControlKey = true;
          break;
        case System.Windows.Input.Key.LeftAlt:
          isControlKey = true;
          break;
        case System.Windows.Input.Key.RightAlt:
          isControlKey = true;
          break;
        case System.Windows.Input.Key.LWin:
          isControlKey = true;
          break;
        case System.Windows.Input.Key.RWin:
          isControlKey = true;
          break;
      }

      return isControlKey;
    }

    /// <summary>
    /// HookCallbackAsync procedure that calls accordingly the KeyDown or KeyUp events.
    /// </summary>
    /// <param name="keyEvent">Keyboard event</param>
    /// <param name="vkCode">VKCode</param>
    void KeyPressCallbackAsync(NativeMethods.KeyEvent keyEvent, int vkCode)
    {
      Key key = System.Windows.Input.KeyInterop.KeyFromVirtualKey(vkCode);

      switch (keyEvent)
      {
        // KeyDown events
        case NativeMethods.KeyEvent.WM_KEYDOWN:
          if (KeyDown != null)
          {
            if (IsControlKey(key))
              ControlKey = key;
            else
              KeyDown(this, new GlobalKeyEventArgs(key, ControlKey));
          }
          break;
        case NativeMethods.KeyEvent.WM_SYSKEYDOWN:
          if (KeyDown != null)
          {
            if (IsControlKey(key))
              ControlKey = key;
            else
              KeyDown(this, new GlobalKeyEventArgs(key, ControlKey));
          }
          break;

        // KeyUp events
        case NativeMethods.KeyEvent.WM_KEYUP:
          ControlKey = Key.None;
          if (KeyUp != null)
            KeyUp(this, new GlobalKeyEventArgs(key, ControlKey));
          break;
        case NativeMethods.KeyEvent.WM_SYSKEYUP:
          ControlKey = Key.None;
          if (KeyUp != null)
            KeyUp(this, new GlobalKeyEventArgs(key, ControlKey));
          break;
        default:
          break;
      }

      Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "Key event {0} key {1} with control key", keyEvent, key, ControlKey));
    }


    /// <summary>
    /// Lows the level keyboard proc.
    /// </summary>
    /// <param name="nCode">The n code.</param>
    /// <param name="wParam">The w parameter.</param>
    /// <param name="lParam">The l parameter.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam)
    {
      if (nCode >= 0)
        if (wParam.ToUInt32() == (int)NativeMethods.KeyEvent.WM_KEYDOWN ||
        wParam.ToUInt32() == (int)NativeMethods.KeyEvent.WM_KEYUP ||
        wParam.ToUInt32() == (int)NativeMethods.KeyEvent.WM_SYSKEYDOWN ||
        wParam.ToUInt32() == (int)NativeMethods.KeyEvent.WM_SYSKEYUP)
          _HookedKeyboardCallbackAsync.BeginInvoke((NativeMethods.KeyEvent)wParam.ToUInt32(), Marshal.ReadInt32(lParam), null, null);

      return NativeMethods.CallNextHookEx(_HookId, nCode, wParam, lParam);
    }

    #endregion

  }
}
