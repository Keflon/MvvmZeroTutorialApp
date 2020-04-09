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
					// The PageService needs to know how to make Page and ViewModel instances so we provide it with a factory
					// that uses the IoC container. We could easily provide any sort of factory, we don't need to use an IoC container.
					var pageService = new PageServiceZero(currentApplication, (theType) => _IoCC.GetInstance(theType), PageCreated);
					return pageService;
				},
				// One only ever will be created.
				Lifestyle.Singleton
			);

			// Tell the IoC container about our Pages.
			_IoCC.Register<HomePage>(Lifestyle.Transient);
			_IoCC.Register<CabbagesPage>(Lifestyle.Transient);
			_IoCC.Register<OnionsPage>(Lifestyle.Transient);
			_IoCC.Register<ResultsPage>(Lifestyle.Transient);

			// Tell the IoC container about our ViewModels.
			_IoCC.Register<HomePageVm>(Lifestyle.Transient);
			_IoCC.Register<CabbagesPageVm>(Lifestyle.Transient);
			_IoCC.Register<OnionsPageVm>(Lifestyle.Transient);
			_IoCC.Register<ResultsPageVm>(Lifestyle.Transient);

			// Optionally add more to the IoC conatainer, e.g. loggers, Http comms objects etc. E.g.
			// IoCC.Register<ILogger, MyLovelyLogger>(Lifestyle.Singleton);
		}

		/// <summary>
		/// This is called once during application startup
		/// </summary>
		internal async Task SetFirstPage()
		{
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
