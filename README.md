![PopupIcon](https://user-images.githubusercontent.com/76768870/189304544-4df7f15d-876d-4646-b16c-4cb174cdd00f.png)

[![NuGet](https://img.shields.io/nuget/v/HMPopup.svg)](https://www.nuget.org/packages/HMPopup/) 
[![NuGet](https://img.shields.io/nuget/dt/HMPopup.svg)](https://www.nuget.org/packages/HMPopup/)

# HMPopup
#### Use this package to show popup window in your xamarin apps just like DisplayAlert
#### Android and iOS supported

# Class structure
```csharp
public class Popup : IPopup
```

# Setup
- `HMPopup` Available on NuGet: https://www.nuget.org/packages/HMPopup
- #### Visual studio setup
	- Tools -> NuGet Pckage Manager -> Package Manager Console -> Install-Package ShamsiDatePicker -Version 3.0.20

# How to use
- #### After setup the package use this line on Csharp file header: 
```csharp
using HMPopup;
```
- #### Then build your popup object from Popup class:
```csharp
readonly Popup popup = new Popup()
{
    OkTitle = "Ok",
    CancelTitle = "Cancel",
    NoTitle = "No",
    YesTitle = "Yes",
    SelectTitle = "Select",
    FlowDirection = FlowDirection.LeftToRight
};
```
