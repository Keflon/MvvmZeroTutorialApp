# MvvmTutorialApp
A very simple application demonstrating how to use `FunctionZero.MvvmZero` and `Xamarin.Forms` to build a cross-platform application using 
the MVVM pattern, dependency injection and an IoC container

## Quickstart
**Launch Visual Studio, grab a coffee and before it's gone cold you'll have an app!**

This tutorial will guide you through the creation of a 4-page cross platform application as shown here:
![alt text](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/Roadmap.png "Tutorial goal")

### Patterns used in this tutorial
There are many great resources online that can teach you MVVM. This is what you need to know to follow this tutorial:
- Each `Page` of the application consists of a `ContentPage` and a corresponding `ViewModel`
- `ViewModels` expose  data the `ContentPage` can display and `Commands` the `ContentPage` can _invoke_
- **There is no business logic in a `ContentPage`**
- **There is no UI in a `ViewModel`**
- MvvmZero delegates the creation of `ContentPages` and `ViewModels` to a 'simple factory' method. In this tutorial we'll be using an IoC container called `SimpleInjector` as our factory

If any of this is new to you, all will become clear!

### Create new Mobile App (Xamarin.Forms)
![Image of dialog showing creation of a Xamarin Forms project](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/CreateNewXamarinFormsProject.png "Create new project")


### Give it a name
![Dialog for Configure Project](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/ConfigureProject.png "Configure your new project")  

### Choose project template
![Dialog for Choose Project Template](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/ChooseProjectTemplate.png "Choose your project template")


### Make sure the default application works
As this is cross-platform, we need to specify the platform we want to run. Here we're testing the UWP build  

![Image showing Visual Studio header bar settings to run the UWP version of the application](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/RunDefaultApplicationUwp.png "Run the default application")

And here it is ...  

![Image of the default UWP application running before any changes have been made](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/DefaultApplicationRunningUwp.png "Default app UWP")

### Projects in the Solution
Your Visual Studio Solution will contain one platform-specific project for each target platform you have choosen to support. We will not be making any changes to those  
All of our work will be in the cross-platform project `MvvmZeroTutorialApp`  

![Image of Solution Tree showing the project in which we will be working](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/OnlyWorkingInMvvmZeroTutorialAppProject.png "MvvmZeroTutorialApp project")

### Now add two NuGet packages


Right-click->Manage NuGet packages...  
![Image showing context menu to launch the Nuget package manager](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/LaunchManageNuGetPackages.png "Manage NuGet packages")

### Install MvvmZero
![Image showing how to install Mvvm Zero using NuGet](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/InstallMvvmZero.png "Install MvvmZero")

Note that installing MvvmZero also installs FunctionZero.CommandZero. You can read about it [here](https://github.com/Keflon/FunctionZero.CommandZero)

### While you're there, also add SimpleInjector
![Image showing how to install Simple Injector using NuGet](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/InstallSimpleInjector.png "Install SimpleInjector")

### Create folder structure
Everything we do in this tutorial is in the `MvvmZeroTutorialApp` project. The platform-specific projects are not touched  
Add these folders to the project (Right-click->Add->New Folder ...)   
![Image of Solution Tree with Mvvm folders added](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/CreateFolderStructure.png "Create Mvvm folders")

### Right-click to add our first `ContentPage` to the Mvvm/Pages folder
![Context menu for Add New Item](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/AddNewItemToPages.png "Add New Item")


### Add a `ContentPage` and call it HomePage.xaml
![Dialog for adding a Content Page](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/AddContentPage.png "Add ContentPage")

### Add the rest of our `ContentPages`
- CabbagesPage.xaml
- OnionsPage.xaml
- ResultsPage.xaml

The Solution tree should now look like this:  
![Image showing the Solution Tree after our Content Pages have been added](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/SolutionTreePages.png "Solution with Pages added")

### Now add our `ViewModels`
One `ViewModel` for each `ContentPage`  

![Image of context menu for adding a class](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/AddClass.png "'Add class' context menu")


### Add HomePageVm.cs
![Image of dialog for adding a POCO class](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/AddClassHomePage.png "Logo Title Text 1")

### Now add the remaining ViewModel classes
#### In the Mvvm/PageViewModels folder
- Mvvm/PageViewModels/CabbagesPageVm.cs
- Mvvm/PageViewModels/OnionsPageVm.cs
- Mvvm/PageViewModels/ResultsPageVm.cs
#### In the Mvvm/ViewModels folder
- Mvvm/ViewModels/TutorialBaseVm.cs
#### In the Mvvm/PageBoilerplate folder
- Mvvm/PageBoilerplate/Locator.cs

### The Solution tree should now to look like this:  
![Image of the full Solution Tree with our additions highlighted](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/FullSolutionTree.png "Full Solution Tree")


### Locator.cs
MvvmZero uses a PageService object to navigate from one page to the next. `Locator.cs` is where we create a `PageService` 
and provide it with a suitable factory method for creating `Pages` and `ViewModels`. Since [SimpleInjector](https://simpleinjector.org/) is my new favourite toy
that's what we'll be using as our factory, though it's straightforward enough to write your own factory if that's what you prefer  
Tip: If your `ContentPage` constructor requires the ViewModel to be injected as a dependency, 
you can let an IoC container resolve your ViewModels directly and use the single-generic overload `PushPageAsync<TPage>(Action<object> setStateAction, ...)`
```csharp
using FunctionZero.MvvmZero;
using MvvmZeroTutorialApp.Mvvm.Pages;
using MvvmZeroTutorialApp.Mvvm.PageViewModels;
using SimpleInjector;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmZeroTutorialApp.Mvvm.Boilerplate
{
	public class Locator
	{
		private Container _IoCC;

		internal Locator(Application currentApplication)
		{
			// Create the IoC container that will contain all our configurable classes ...
			_IoCC = new Container();

			// Tell the IoC container what to do if asked for an IPageService
			_IoCC.Register<IPageServiceZero>(
				() =>
				{
					// This is how we create an instance of PageServiceZero.
					// The PageService needs to know how to get the current NavigationPage it is to interact with.
					// (If you have a FlyoutPage at the root, the navigationGetter should return the current Detail item)
					// It also needs to know how to get Page and ViewModel instances so we provide it with a factory
					// that uses the IoC container. We could easily provide any sort of factory, we don't need to use an IoC container.
					var pageService = new PageServiceZero(() => App.Current.MainPage.Navigation, (theType) => _IoCC.GetInstance(theType));
					return pageService;
				},
				// One only ever will be created.
				Lifestyle.Singleton
			);

			// Tell the IoC container about our Pages.
			_IoCC.Register<HomePage>(Lifestyle.Singleton);
			_IoCC.Register<CabbagesPage>(Lifestyle.Singleton);
			_IoCC.Register<OnionsPage>(Lifestyle.Singleton);
			_IoCC.Register<ResultsPage>(Lifestyle.Singleton);

			// Tell the IoC container about our ViewModels.
			_IoCC.Register<HomePageVm>(Lifestyle.Singleton);
			_IoCC.Register<CabbagesPageVm>(Lifestyle.Singleton);
			_IoCC.Register<OnionsPageVm>(Lifestyle.Singleton);
			_IoCC.Register<ResultsPageVm>(Lifestyle.Singleton);

			// Optionally add more to the IoC conatainer, e.g. loggers, Http comms objects etc. E.g.
			// IoCC.Register<ILogger, MyLovelyLogger>(Lifestyle.Singleton);
		}

		/// <summary>
		/// This is called once during application startup
		/// </summary>
		internal async Task SetFirstPage()
		{
			// Create and assign a top-level NavigationPage.
			// If you use a FlyoutPage instead then its Detail item will need to be a NavigationPage
			// and you will need to modify the 'navigationGetter' provided to the PageServiceZero instance to 
			// something like this:
            // () => ((FlyoutPage)App.Current.MainPage).Detail.Navigation
			App.Current.MainPage = new NavigationPage();
			// Ask the PageService to assemble and present our HomePage ...
			await _IoCC.GetInstance<IPageServiceZero>().PushPageAsync<HomePage, HomePageVm>((vm) => {/* Optionally interact with the vm, e.g. to inject seed-data */ });
		}

		/// <summary>
		/// For debug purposes to let us know when a Page is assembled by the PageService
		/// </summary>
		/// <param name="thePage">A reference to the page that has been presented</param>
		private void PageCreated(Page thePage)
		{
			Debug.WriteLine(thePage);
		}
	}
}
```


### Getting things started
Go to App.xaml.cs, add the `Locator` backing variable, add the `using` statement and replace the constructor as shown below:

```csharp
using MvvmZeroTutorialApp.Mvvm.Boilerplate;
using Xamarin.Forms;

namespace MvvmZeroTutorialApp
{
    public partial class App : Application
    {
        // Backing property for the Locator instance
        public Locator Locator { get; private set; }

        public App()
        {
            InitializeComponent();

            // Create our Locator instance and tell it about the Application instance ...
            Locator = new Locator(this);

            // Ask the Locator to get us going ...
            _ = Locator.SetFirstPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

```
### Write the base class for our `ViewModels`
Paste this into TutorialBaseVm.cs
```csharp
using FunctionZero.MvvmZero;

namespace MvvmZeroTutorialApp.Mvvm.ViewModels
{
    /// <summary>
    /// This is a base class for your viewmodels
    /// Note: It is not required, your viewmodels can be POCO or derive directly from MvvmZeroBaseVm
    /// </summary>
    public abstract class TutorialBaseVm : MvvmZeroBaseVm
    {
        // TODO: Put any base class specialization for your app in here
    }
}

```

### Now we're going to finish the HomePage

The `HomePage` will have two `Buttons` so we'll need two `Commands` for the `Buttons` to bind to  
The `Commands` navigate to new `Pages`, so we'll need the instance of `PageServiceZero` described in `Locator.cs`  
- As we're using an IoC container, it is simply a case of adding `IPageServiceZero` as a constructor parameter and the container will do the rest

Installing `MvvmZero` automatically installed `FunctionZero.CommandZero` and that contains the `ICommand` implementation 
we're going to use, documented [here](https://github.com/Keflon/FunctionZero.CommandZero)  
If you have a preferred implementation feel free to use that instead

Now paste the following into HomePageVm.cs:
```csharp
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using MvvmZeroTutorialApp.Mvvm.Pages;
using MvvmZeroTutorialApp.Mvvm.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmZeroTutorialApp.Mvvm.PageViewModels
{
    public class HomePageVm : TutorialBaseVm
    {
        private readonly IPageServiceZero _pageService;

        /// <summary>
        /// The UI can bind to this command and use it to get to the CabbagesPage
        /// </summary>
        public ICommand CabbagesPageCommand { get; }
        /// <summary>
        /// The UI can bind to this command and use it to get to the OnionsPage
        /// </summary>
        public ICommand OnionsPageCommand { get; }

        /// <summary>
        /// Here we inject an IPageService, so we can use it to get to another page when we're ready
        /// </summary>
        /// <param name="pageService"></param>
        public HomePageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;

            // Set up our commands for the UI to bind to ...
            CabbagesPageCommand = new CommandBuilder().SetExecuteAsync(CabbagesPageCommandExecuteAsyncAsync).SetName("Cabbages").Build();
            OnionsPageCommand = new CommandBuilder().SetExecuteAsync(OnionsPageCommandExecute).SetName("Onions").Build();
        }

        private async Task CabbagesPageCommandExecuteAsync(/* Optional : object arg */)
        {
            // Take us to the CabbagesPage page ...
            await _pageService.PushPageAsync<CabbagesPage, CabbagesPageVm>((vm) => { /* Initialize the vm in here if necessary */ });
        }

        private async Task OnionsPageCommandExecuteAsync(/* Optional : object arg */)
        {
            // Take us to the OnionsPage page ...
            await _pageService.PushPageAsync<OnionsPage, OnionsPageVm>((vm) => { /* Initialize the vm in here if necessary */ });
        }
    }
}
```

### Write the HomePage UI ...
Replace the Page content with the following. MvvmZero will automatically set the `BindingContext` of the 
`ContentPage` to an instance of `HomePageVm` so we simply create two `Buttons` and 'bind' them to the `CabbagesPageCommand` and the `OnionsCommand` in our `ViewModel`  
```xml
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand" HorizontalOptions="CenterAndExpand">
            <Label Text="Make your choice"/>
            <Button Command="{Binding CabbagesPageCommand}" Text="{Binding CabbagesPageCommand.Text}"/>
            <Button Command="{Binding OnionsPageCommand}" Text="{Binding OnionsPageCommand.Text}"/>
        </StackLayout>
    </ContentPage.Content>
```
### If you run it you should see this ...
![Image of the completed HomePage running on UWP](https://github.com/Keflon/MvvmZeroTutorialApp/raw/master/Images/HomePageUwp.png "Home Page running on UWP")
HomePageUwp

The buttons will take us to a new page and the back-button will bring us back to the home page.

### Write the CabbagesPageVm ...
This `ViewModel` exposes a `string` and a `ICommand` for the UI to bind to  
If the `string` passes validation, `NextCommand` is enabled and the user can proceed  
Note that the `PageService` injects data into the `ResultsPage` via an `Init` method 
on `ResultsPageVm`. We'll add the `Init` method next
```csharp
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using MvvmZeroTutorialApp.Mvvm.Pages;
using MvvmZeroTutorialApp.Mvvm.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmZeroTutorialApp.Mvvm.PageViewModels
{
    public class CabbagesPageVm : TutorialBaseVm
    {
        private string _name;
        private IPageServiceZero _pageService;

        /// <summary>
        /// The UI can bind to this and display or modify it
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The UI can bind to this command and use it to get to the ResultsPage
        /// </summary>
        public ICommand NextCommand { get; }

        public CabbagesPageVm(IPageServiceZero pageService)
        {
            _pageService = pageService;

            // Initialise Name so we don't have to null-check it later
            Name = string.Empty;

            // Set up our Command for the UI to bind to ...
            NextCommand = new CommandBuilder()
                .SetExecuteAsync(NextCommandExecuteAsync)
                .SetCanExecute(NextCommandCanExecute)
                .SetName(GetCurrentName)
                // This command can enable or disable itself or change its Text if the 'Name' property changes
                .AddObservedProperty(this, nameof(Name))
                .Build();
        }

        // When the NextCommand is invoked (by the UI) this method is called to take us to the results page
        private async Task NextCommandExecuteAsync(object arg)
        {
            string payload = $"The Cabbages Page has been visited by {Name}";

            await _pageService.PushPageAsync<ResultsPage, ResultsPageVm>((vm) => vm.Init(payload));
        }

        // Returns true if the Name property is considered valid
        private bool NextCommandCanExecute()
        {
            return Name.Length >= 4;
        }

        // This is reevaluated (by the UI) every time the 'Name' property changes
        private string GetCurrentName()
        {
            if (NextCommandCanExecute() == false)
                return "Keep typing ...";

            return "Next";
        }
    }
}


```
Now add this to ResultsPageVm so we can build what we have so-far
```csharp
public void Init(string payload)
{
    throw new NotImplementedException();
}
```
### Now write the UI for CabbagesPage
Replace the Page content in `CabbagesPage.xaml` with the following
```xml
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand" HorizontalOptions="CenterAndExpand">
            <Label Text="Cabbages Page!"/>
            <Label Text="Please tell me your name ..."/>
            <Editor Text="{Binding Name}" Placeholder="Minimum 4 characters please"/>
            <Button Command="{Binding NextCommand}" Text="{Binding NextCommand.Text}"/>
        </StackLayout>
    </ContentPage.Content>
```

### Now for the ResultsPage. First the ViewModel ...
Replace the contents of `ResultsPageVm.cs` with the following
```csharp
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using System.Windows.Input;

namespace MvvmZeroTutorialApp.Mvvm.PageViewModels
{
    public class ResultsPageVm
    {
        /// <summary>
        ///  The UI can bind to this to display its content
        /// </summary>
        public string DisplayText { get; private set; }

        /// <summary>
        /// The UI can bind to this command and use it to start again
        /// </summary>
        public ICommand StartAgainCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageService"></param>
        public ResultsPageVm(IPageServiceZero pageService)
        {
            StartAgainCommand = new CommandBuilder().SetExecuteAsync(async () => await pageService.PopToRootAsync()).SetName("Restart").Build();
        }

        public void Init(string payload)
        {
            DisplayText = payload;
        }
    }
}

```

### ... then in ResultsPage.xaml, replace the page content with this
```xml
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand" HorizontalOptions="CenterAndExpand">
            <Label Text="Results Page!"/>
            <Label Text="{Binding DisplayText}"/>
            <Button Command="{Binding StartAgainCommand}" Text="{Binding StartAgainCommand.Text}"/>
        </StackLayout>
    </ContentPage.Content>
```
You should now be able to run the application and visit the CabbagePage and the ResultsPage. That leaves the OnionsPage

## Finishing off ...

Here's the code for OnionsPage.cs ...

```csharp
using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using MvvmZeroTutorialApp.Mvvm.Pages;
using MvvmZeroTutorialApp.Mvvm.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MvvmZeroTutorialApp.Mvvm.PageViewModels
{
    public class OnionsPageVm : TutorialBaseVm
    {
        /// <summary>
        /// The UI can bind to this command and use it to get to the ResultsPage
        /// </summary>
        public ICommand NextCommand { get; }

        /// <summary>
        /// A very basic ViewModel
        /// </summary>
        public OnionsPageVm(IPageServiceZero pageService)
        {
            NextCommand = new CommandBuilder()
                .SetExecuteAsync(() => pageService.PushPageAsync<ResultsPage, ResultsPageVm>((vm) => vm.Init("Hello from the Onions Page!")))
                .SetName("Next")
                .Build();
        }
    }
}
```
... and the UI for OnionsPage.xaml
```xml
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand" HorizontalOptions="CenterAndExpand" >
            <Label Text="Welcome to the Onions Page" />
            <Button Command="{Binding NextCommand}" Text="{Binding NextCommand.Text}"/>
        </StackLayout>
    </ContentPage.Content>
```
And there you have it. A cross-platform app for iOS, Android and UWP using the MVVM pattern, dependency injection and an IoC container!  
To run this on an Android device
- Enable developer mode on the device (often by tapping on the build number in settings 5 times)
- Plug it into your PC
- Select 'Android' as your build target and build for 'Any CPU'
- Build and run!

It's a little more involved deploying to iOS. Good luck! :)