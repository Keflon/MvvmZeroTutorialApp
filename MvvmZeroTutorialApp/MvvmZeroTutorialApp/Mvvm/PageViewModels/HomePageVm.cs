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
            CabbagesPageCommand = new CommandBuilder().SetExecute(CabbagesPageCommandExecute).SetName("Cabbages").Build();
            OnionsPageCommand = new CommandBuilder().SetExecute(OnionsPageCommandExecute).SetName("Onions").Build();
        }

        private async Task CabbagesPageCommandExecute(/* Optional : object arg */)
        {
            // Take us to the CabbagesPage page ...
            await _pageService.PushPageAsync<CabbagesPage, CabbagesPageVm>((vm) => { /* Initialize the vm in here if necessary */ });
        }

        private async Task OnionsPageCommandExecute(/* Optional : object arg */)
        {
            // Take us to the OnionsPage page ...
            await _pageService.PushPageAsync<OnionsPage, OnionsPageVm>((vm) => { /* Initialize the vm in here if necessary */ });
        }
    }
}
