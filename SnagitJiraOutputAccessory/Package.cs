namespace SnagitJiraOutputAccessory
{
    using System;
    using System.Runtime.InteropServices;
    using SNAGITLib;

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("55A50CAA-D9E3-4239-8B02-D6D2A396F4AF")]
    public class Package : MarshalByRefObject, IPackageSetup
    {
        public void Install()
        {
        }

        public void Uninstall()
        {
        }
    }
}
