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
- ## Showing message:
```csharp
await popup.ShowMessageAsync("Test Message", "This is a test message");
```
```csharp
popup.ShowMessage("Test Message", "This is a test message");
```
- ## Light Theme

![Message-Light](https://user-images.githubusercontent.com/76768870/192133645-3cf3067a-b497-4e08-a589-765178bf80cd.jpg)

- ## Dark Theme

![Message-Dark](https://user-images.githubusercontent.com/76768870/192133649-95179515-03b6-4e37-9a24-c17095c0a6b6.jpg)

- ## Showing question:
```csharp
var answer = await popup.ShowQuestionAsync("Test Question", "Are you sure?");
//answer is true if Yes selected and is false if No selected
```

```
