using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using CouplingAlturos.Abstractions;

namespace CouplingAlturos
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = RegistrationService.CreateContainer();

			Application.Run(container.Resolve<MainForm>());
        }
    }
}
