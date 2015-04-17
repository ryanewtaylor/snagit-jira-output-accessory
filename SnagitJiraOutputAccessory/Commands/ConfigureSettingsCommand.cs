namespace SnagitJiraOutputAccessory.Commands
{
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Models;
    using SnagitJiraOutputAccessory.ViewModels;
    using SnagitJiraOutputAccessory.Views;
    using SNAGITLib;

    public class ConfigureSettingsCommand : ICommand
    {
        private readonly ISnagIt _snagit;
        private OutputPreferencesRepository _outputPreferencesRepo;

        public ConfigureSettingsCommand(ISnagIt snagit, OutputPreferencesRepository outputPreferencesRepo)
        {
            _snagit = snagit;
            _outputPreferencesRepo = outputPreferencesRepo;
        }

        public void Execute()
        {
            OutputOptionsView view = new OutputOptionsView();
            var windowHelper = new System.Windows.Interop.WindowInteropHelper(view);
            windowHelper.Owner = (System.IntPtr)(_snagit.TopLevelHWnd);
            view.DataContext = new OutputOptionsViewModel(_outputPreferencesRepo);
            view.ShowDialog();
        }
    }
}
