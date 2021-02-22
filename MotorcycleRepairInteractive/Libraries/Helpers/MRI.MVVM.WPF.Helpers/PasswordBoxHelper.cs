using System.Windows;
using System.Windows.Controls;

namespace MRI.MVVM.WPF.Helpers
{
  /// <summary>
  /// Helper class for binding to the WPF password box content
  /// </summary>
  public static class PasswordBoxHelper
  {
    /// <summary>
    /// Determines whether the password is to be bound
    /// </summary>
    public static readonly DependencyProperty BoundPassword =
      DependencyProperty.RegisterAttached(nameof(BoundPassword),
        typeof(string),
        typeof(PasswordBoxHelper),
        new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

    /// <summary>
    /// Password binding property
    /// </summary>
    public static readonly DependencyProperty BindPassword =
      DependencyProperty.RegisterAttached(nameof(BindPassword),
        typeof(bool),
        typeof(PasswordBoxHelper),
        new PropertyMetadata(false, OnBindPasswordChanged));

    /// <summary>
    /// Determines whether the password value is being modified
    /// </summary>
    // ReSharper disable once InconsistentNaming
    private static readonly DependencyProperty UpdatingPassword =
      DependencyProperty.RegisterAttached(nameof(UpdatingPassword),
        typeof(bool),
        typeof(PasswordBoxHelper),
        new PropertyMetadata(false));

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var box = d as PasswordBox;

      // only handle this event when the property is attached to a PasswordBox
      // and when the BindPassword attached property has been set to true
      if (box is null || !GetBindPassword(d))
        return;

      // avoid recursive updating by ignoring the box's changed event
      box.PasswordChanged -= HandlePasswordChanged;

      var newPassword = (string) e.NewValue;

      if (!GetUpdatingPassword(box))
        box.Password = newPassword;

      box.PasswordChanged += HandlePasswordChanged;
    }

    private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
    {
      // when the BindPassword attached property is set on a PasswordBox,
      // start listening to its PasswordChanged event

      if (dp is not PasswordBox box)
        return;

      var wasBound = (bool) e.OldValue;
      var needToBind = (bool) e.NewValue;

      if (wasBound)
        box.PasswordChanged -= HandlePasswordChanged;

      if (needToBind)
        box.PasswordChanged += HandlePasswordChanged;
    }

    private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
    {
      var box = sender as PasswordBox;

      // set a flag to indicate that we're updating the password
      SetUpdatingPassword(box!, true);
      // push the new password into the BoundPassword property
      SetBoundPassword(box, box.Password);
      SetUpdatingPassword(box, false);
    }

    /// <summary>
    /// Set the <see cref="BindPassword"/> property
    /// </summary>
    /// <param name="dp">Object of which the property value is to be set</param>
    /// <param name="value">Value type</param>
    public static void SetBindPassword(DependencyObject dp, bool value)
      => dp.SetValue(BindPassword, value);

    /// <summary>
    /// Gets the value of the <see cref="BindPassword"/> property
    /// </summary>
    /// <param name="dp">Object containing the bound property</param>
    /// <returns>Property value</returns>
    public static bool GetBindPassword(DependencyObject dp)
      => (bool) dp.GetValue(BindPassword);

    /// <summary>
    /// Gets the value of the <see cref="BoundPassword"/> property
    /// </summary>
    /// <param name="dp">Object containing the bound property</param>
    /// <returns>Property value</returns>
    public static string GetBoundPassword(DependencyObject dp)
      => (string) dp.GetValue(BoundPassword);

    /// <summary>
    /// Set the <see cref="BoundPassword"/> property
    /// </summary>
    /// <param name="dp">Object of which the property value is to be set</param>
    /// <param name="value">Value type</param>
    public static void SetBoundPassword(DependencyObject dp, string value)
      => dp.SetValue(BoundPassword, value);

    /// <summary>
    /// Gets the value of the <see cref="UpdatingPassword"/> property
    /// </summary>
    /// <param name="dp">Object containing the bound property</param>
    /// <returns>Property value</returns>
    private static bool GetUpdatingPassword(DependencyObject dp)
      => (bool) dp.GetValue(UpdatingPassword);

    /// <summary>
    /// Set the <see cref="UpdatingPassword"/> property
    /// </summary>
    /// <param name="dp">Object of which the property value is to be set</param>
    /// <param name="value">Value type</param>
    private static void SetUpdatingPassword(DependencyObject dp, bool value)
      => dp.SetValue(UpdatingPassword, value);
  }
}