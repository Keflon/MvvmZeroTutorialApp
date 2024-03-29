﻿using FunctionZero.CommandZero;
using FunctionZero.MvvmZero;
using MvvmZeroTutorialApp.Mvvm.ViewModels;
using System.Windows.Input;

namespace MvvmZeroTutorialApp.Mvvm.PageViewModels
{
    public class ResultsPageVm : TutorialBaseVm
    {
        private string _displayText;

        /// <summary>
        ///  The UI can bind to this to display its content
        ///  We're using the SetProperty helper in the base class to raise
        ///  INotifyPropertyChanged (INPC) for us, so the UI will know if DisplayText changes.
        ///  Compare this with the 'Name' property in CabbagesPageVm where INPC is raised manually.
        /// </summary>
        public string DisplayText 
        { 
            get => _displayText; 
            set => SetProperty(ref _displayText, value);
        }

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
