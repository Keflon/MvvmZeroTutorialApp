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
                .SetExecute(() => pageService.PushPageAsync<ResultsPage, ResultsPageVm>((vm) => vm.Init("Hello from the Onions Page!")))
                .SetName("Next")
                .Build();
        }
    }
}
