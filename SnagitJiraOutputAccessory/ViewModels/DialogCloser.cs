// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogCloser.cs">
// Author/Question: Orion Edwards http://stackoverflow.com/questions/501886
// Author/Answer: Joe White http://stackoverflow.com/a/3329467/19977
// </copyright>
// <summary>
//   Defines the DialogCloser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace SnagitJiraOutputAccessory.ViewModels
{
    public static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
            {
                window.DialogResult = e.NewValue as bool?;
            }
        }

        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }
}
