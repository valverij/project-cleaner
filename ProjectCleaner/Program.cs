using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectCleaner
{
	static class Program
	{
        private static IContainer _container;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            _container = ConfigureAutofac();

            Application.ApplicationExit += (sender, e) => _container.Dispose();

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_container.Resolve<ProjectCleaner>());
        }

        private static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProjectCleaner>();
            builder.RegisterType<Cleaners.AsyncCleaner>().As<Cleaners.IAsyncCleaner>().InstancePerLifetimeScope();
            builder.RegisterType<Cleaners.CleanerStatusTracker>().As<Cleaners.IStatusTracker>().InstancePerLifetimeScope();

            return builder.Build();
        }
	}
}
