using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaHive.Application.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
	private bool _busy;

	public bool Busy
	{
		get => _busy;
		set => SetValue(ref _busy, value);
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	public event EventHandler? ComponentInitialized;
	public event EventHandler? ComponentAfterRender;
	public event EventHandler? ComponentParametersSet;

	protected void OnPropertyChanged([CallerMemberName] string propertyName = default!)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected void SetValue<TItem>(ref TItem field, TItem value, [CallerMemberName] string propertyName = default!)
	{
		if (EqualityComparer<TItem>.Default.Equals(field, value)) return;

		field = value;
		OnPropertyChanged(propertyName);
	}

	public void OnComponentInitialized()
	{
		ComponentInitialized?.Invoke(this, EventArgs.Empty);
	}

	public void OnComponentAfterRender()
	{
		ComponentAfterRender?.Invoke(this, EventArgs.Empty);
	}

	public void OnComponentParametersSet()
	{
		ComponentParametersSet?.Invoke(this, EventArgs.Empty);
	}
}