﻿using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;

namespace GeoLocatorSample.Droid
{
	[Application]
	public class MainApplication : MauiApplication<App>
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
		}
	}
}
