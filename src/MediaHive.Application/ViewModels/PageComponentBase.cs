using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace MediaHive.Application.ViewModels;

public class PageComponentBase<TViewModel> : ComponentBase, IDisposable where TViewModel : ViewModelBase
{
	[Inject] [Parameter] public required TViewModel Model { get; set; }

	public void Dispose()
	{
		Model.PropertyChanged -= OnModelPropertyChanged;
	}

	protected override bool ShouldRender()
	{
		return Model.Busy is false;
	}

	protected override Task OnInitializedAsync()
	{
		Model.PropertyChanged += OnModelPropertyChanged;
		
		Model.OnComponentInitialized();
		
		return base.OnInitializedAsync();
	}

	protected override Task OnParametersSetAsync()
	{
		Model.OnComponentParametersSet();
		
		return base.OnParametersSetAsync();
	}

	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender) Model.OnComponentAfterRender();

		return base.OnAfterRenderAsync(firstRender);
	}

	private async void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs args)
	{
		await InvokeAsync(StateHasChanged);
	}
}