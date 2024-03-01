using System;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Collections;

namespace UninstallService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the name of the service you want to uninstall:");
            string serviceName = Console.ReadLine();

            if (ServiceExists(serviceName))
            {
                try
                {
                    Uninstall(serviceName);
                    Console.WriteLine($"Service '{serviceName}' has been successfully uninstalled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while uninstalling the service: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Service '{serviceName}' does not exist.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static bool ServiceExists(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == serviceName)
                    return true;
            }
            return false;
        }

        static void Uninstall(string serviceName)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller(typeof(Program).Assembly, null))
            {
                IDictionary state = new Hashtable();
                installer.UseNewContext = true;
                try
                {
                    installer.Uninstall(state);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
