using System;
using System.Collections.Concurrent;
using System.Linq;
using FluentValidation;
using MRI.MVVM.Interfaces.ViewModels;

namespace MRI.MVVM.Helpers
{
  /// <summary>
  /// Base view model with validation support
  /// </summary>
  public abstract class BaseFormViewModel<TValidator>
    : BasePropertyChanged, IFormViewModel
    where TValidator : IValidator
  {
    #region Fields

    private readonly TValidator m_validator;
    private readonly ConcurrentDictionary<string, bool> m_properties;
    private readonly ValidationContext<IFormViewModel> m_context;

    #endregion

    #region Properties

    /// <inheritdoc />
    public string Error
    {
      get
      {
        var results = m_validator.Validate(m_context);
        return results.Errors.Any()
          ? string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage))
          : string.Empty;
      }
    }

    /// <inheritdoc />
    public bool HasErrors
      => m_validator.Validate(m_context).Errors.Any();

    #endregion

    #region Events

    /// <inheritdoc />
    public event EventHandler<(string property, bool hasError)>? ErrorsChanged;
    private void OnErrorsChanged(string property, bool hasError)
      => ErrorsChanged?.Invoke(this, (property, hasError));

    #endregion

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="validator">Form validator</param>
    protected BaseFormViewModel(TValidator validator)
    {
      m_validator = validator;
      m_properties = new ConcurrentDictionary<string, bool>();
      m_context = new ValidationContext<IFormViewModel>(this);
    }

    /// <inheritdoc />
    public string this[string columnName]
    {
      get
      {
        var error = m_validator.Validate(m_context)
          .Errors
          .FirstOrDefault(x => x.PropertyName == columnName)
          ?.ErrorMessage;
        var hasError = error is null || error.Length > 0;
        if (!m_properties.ContainsKey(columnName))
        {
          m_properties.TryAdd(columnName, hasError);
          if (hasError)
            OnErrorsChanged(columnName, true);
        }
        else
        {
          m_properties.TryGetValue(columnName, out var e);
          if (hasError == e)
            return error!;

          m_properties.AddOrUpdate(columnName, hasError, (_, x) => !x);
          OnErrorsChanged(columnName, hasError);

          return null!;
        }

        return error!;
      }
    }

    /// <summary>
    /// Clears all discovered errors
    /// </summary>
    protected void ClearErrors()
    {
      m_properties.Clear();
    }

    /// <summary>
    /// Checks whether the given form has any errors
    /// </summary>
    /// <returns>True if errors are present</returns>
    protected bool CheckAnyErrors()
      => m_properties.IsEmpty || m_properties.All(x => !x.Value);
  }
}