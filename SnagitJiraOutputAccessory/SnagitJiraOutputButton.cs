namespace SnagitJiraOutputAccessory
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Commands;
    using SNAGITLib;

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("D3B2FCA3-7A2C-4AF9-86D4-2E542118E8F8")]
    public class SnagitJiraOutputButton : MarshalByRefObject, IComponentInitialize, IOutput, IOutputMenu, IComponentWantsCategoryPreferences
    {
        private ISnagIt _snagit;
        private CommandFactory _commandFactory;
        
        public void InitializeComponent(object pExtensionHost, IComponent pComponent, componentInitializeType initType)
        {
            try
            {
                _snagit = pExtensionHost as ISnagIt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Output()
        {
            _commandFactory.CreateDefaultCommand().Execute();
        }

        public string GetOutputMenuData()
        {
            var formatter = new SnagitMenuFormatter();
            return formatter.Format();
        }

        public void SelectOutputMenuItem(string pStrID)
        {
            _commandFactory.CreateCommand(pStrID).Execute();
        }

        public void SetComponentCategoryPreferences(SnagItOutputPreferences pIComponentCategoryPreferences)
        {
            _commandFactory = new CommandFactory();
        }
    }
}
