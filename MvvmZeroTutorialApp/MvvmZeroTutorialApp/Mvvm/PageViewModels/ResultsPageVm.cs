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
            StartAgainCommand = new CommandBuilder().SetExecute(async () => await pageService.PopToRootAsync()).SetName("Restart").Build();
        }

        public void Init(string payload)
        {
            DisplayText = payload;
        }
    }
}
