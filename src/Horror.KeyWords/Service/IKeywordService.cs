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
