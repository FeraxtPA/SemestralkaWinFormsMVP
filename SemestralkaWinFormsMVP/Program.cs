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

            //Model
            SchoolService service = new SchoolService();
         
            //View 
            MainForm view = new MainForm();

            //Presenter
            ZakovskaController controller = new ZakovskaController(view, service);

            //Run app
            controller.Show();


        }
    }
}