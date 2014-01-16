namespace SnagitJiraOutputAccessory.Commands
{
    using System.Diagnostics;

    public class OpenWebPageCommand : ICommand
    {
        private readonly string _url;

        public OpenWebPageCommand(string url)
        {
            _url = url;
        }

        public void Execute()
        {
            Process.Start(_url);
        }
    }
}
