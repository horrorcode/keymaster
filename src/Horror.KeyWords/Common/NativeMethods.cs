// <copyright file="NativeMethods.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Diagnostics;
  using System.Runtime.InteropServices;

  /// <summary>
  /// Summary description for Win32.
  /// </summary>
  internal class NativeMethods
  {
    #region Enums

    /// <summary>
    /// 
    /// </summary>
    public enum KeyEvent : int
    {
      WM_KEYDOWN = 0x100,
      WM_KEYUP = 0x101,
      WM_SYSKEYUP = 0x105,
      WM_SYSKEYDOWN = 0x104
    }

    #endregion

    #region Fields

    /// <summary>
    /// The w h_ keyboar d_ ll
    /// </summary>
    public static int WH_KEYBOARD_LL = 13;

    #endregion

    #region Delegates

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nCode">The n code.</param>
    /// <param name="wParam">The w parameter.</param>
    /// <param name="lParam">The l parameter.</param>
    /// <returns></returns>
    public delegate IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam);

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the NativeMethods class.
    /// </summary>
    private NativeMethods()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Calls the next hook ex.
    /// </summary>
    /// <param name="hhk">The HHK.</param>
    /// <param name="nCode">The n code.</param>
    /// <param name="wParam">The w parameter.</param>
    /// <param name="lParam">The l parameter.</param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Gets the module handle.
    /// </summary>
    /// <param name="lpModuleName">Name of the lp module.</param>
    /// <returns></returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    /// <summary>
    /// Sets the hook.
    /// </summary>
    /// <param name="proc">The proc.</param>
    /// <returns></returns>
    public static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
      using (Process curProcess = Process.GetCurrentProcess())
      using (ProcessModule curModule = curProcess.MainModule)
      {
        return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
        GetModuleHandle(curModule.ModuleName), 0);
      }
    }

    /// <summary>
    /// Sets the windows hook ex.
    /// </summary>
    /// <param name="idHook">The identifier hook.</param>
    /// <param name="lpfn">The LPFN.</param>
    /// <param name="hMod">The h mod.</param>
    /// <param name="dwThreadId">The dw thread identifier.</param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    /// <summary>
    /// Unhooks the windows hook ex.
    /// </summary>
    /// <param name="hhk">The HHK.</param>
    /// <returns></returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    #endregion

  }
}