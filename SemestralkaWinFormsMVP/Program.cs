using ZakovskaApp.Controller;
using ZakovskaApp.Service;
using ZakovskaApp.View;
using System;
using System.Windows.Forms;


namespace SemestralkaWinFormsMVP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            SkolaService service = new SkolaService();
           

            MainForm view = new MainForm();

            ZakovskaController controller = new ZakovskaController(view, service);

            controller.Show();


        }
    }
}