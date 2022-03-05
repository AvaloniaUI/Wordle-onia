namespace Wordle.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System;
using System.ComponentModel;

public abstract class ObservableViewModel<T> : ObservableObject, IDisposable
    where T : ObservableModel
{
    private readonly bool _forwardModelPropertyChanges;
    private bool _disposed;

    protected ObservableViewModel(T model, bool forwardPropertyChanged = true)
    {
        ArgumentNullException.ThrowIfNull(model);

        Model = model;
        _forwardModelPropertyChanges = forwardPropertyChanged;

        model.PropertyChanged += OnModelPropertyChanged;
    }

    public T Model { get; }

    public void Dispose()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException("Object already disposed.");
        }

        _disposed = true;
        Model.PropertyChanged -= OnModelPropertyChanged;
    }

    protected virtual void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_forwardModelPropertyChanges)
        {
            OnPropertyChanged(e);
        }
    }
}