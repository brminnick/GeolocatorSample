﻿using Xamarin.Forms;

namespace GeoLocatorSample
{
	public abstract class BaseContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel, new()
	{
		#region Constructors
		protected BaseContentPage()
		{
			BindingContext = ViewModel;
			BackgroundColor = ColorConstants.PageBackgroundColor;
		}
		#endregion

		#region Properties
		protected TViewModel ViewModel { get; } = new TViewModel();
		#endregion
	}
}
