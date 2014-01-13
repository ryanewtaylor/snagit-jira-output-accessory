namespace SnagitJiraOutputAccessory
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using SNAGITLib;

    [ClassInterface(ClassInterfaceType.None)]
    [Guid("D3B2FCA3-7A2C-4AF9-86D4-2E542118E8F8")]
    public class SnagitJiraOutputButton : MarshalByRefObject, IComponentInitialize, IOutput, IOutputMenu, IComponentWantsCategoryPreferences
    {
        private ISnagIt _snagit;
        
        public void InitializeComponent(object pExtensionHost, IComponent pComponent, componentInitializeType initType)
        {
            try
            {
                _snagit = pExtensionHost as ISnagIt;
                MessageBox.Show("initialized!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Output()
        {
            MessageBox.Show("click!");
        }

        public string GetOutputMenuData()
        {
            StringBuilder menu = new StringBuilder();
            menu.Append("<menu>");
            menu.AppendFormat("<menuitem id=\"{0}\" label=\"{1}\" />", "UploadToNewIssue", "Upload to New Issue");
            menu.AppendFormat("<menuitem id=\"{0}\" label=\"{1}\" />", "UploadToExistingIssue", "Upload to Existing Issue");
            menu.Append("</menu>");
            return menu.ToString();
        }

        public void SelectOutputMenuItem(string pStrID)
        {
            // TODO: Implementation
        }

        public void SetComponentCategoryPreferences(SnagItOutputPreferences pIComponentCategoryPreferences)
        {
            // TODO: Implementation
        }
    }
}
