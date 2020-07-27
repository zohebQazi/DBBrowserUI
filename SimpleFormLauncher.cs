using System;
using System.Windows.Forms;
using Itecx.Gui;

namespace SimpleGuiClient
{
    static class SimpleFormLauncher
    {
       // static readonly Log Logger = Log.GetLogger("GuiClient");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OrchestradeCommon.CommonAssemblyLoader.ResolveInCommonDirectory;
            //ConfigUtil.IsGui = true;
            //Logger.Info("..... Starting GUI Client ........................");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
