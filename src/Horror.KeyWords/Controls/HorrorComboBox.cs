
namespace Horror.Keywords
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;

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
