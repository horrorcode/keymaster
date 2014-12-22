// <copyright file="HorrorComboBox.cs" company="Horror">
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
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;

  /// <summary>
  /// 
  /// </summary>
  public class HorrorComboBox : ComboBox
  {
    #region Public Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      TextBox textBox = (TextBox)Template.FindName("PART_EditableTextBox", this);

      // Create a template-binding in code  
      Binding binding = new Binding("ContextMenu");
      binding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
      BindingOperations.SetBinding(textBox,
          FrameworkElement.ContextMenuProperty, binding);
    }

    #endregion

  }
}
