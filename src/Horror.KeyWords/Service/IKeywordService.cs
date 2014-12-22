// <copyright file="IKeywordService.cs" company="Horror">
// Copyright (c) 2014 Open source under MIT License
// </copyright>
// <author>rewso</author>
// <date>2014-12-20 05:53</date>
// <summary>Class for Horror.Keywords</summary>
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public interface IKeywordService
  {
    /// <summary>
    /// Executes the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    void Execute(string input);
    /// <summary>
    /// Loads the magic words.
    /// </summary>
    MagicWordContainer LoadMagicWords();
    /// <summary>
    /// Saves the magic words.
    /// </summary>
    void SaveMagicWords();
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    void Dispose();
  }
}
