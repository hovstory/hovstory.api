using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOVStoryConfiguration
{
    internal class ConfigurationDAO
    {
        private static ConfigurationDAO instance = null;
        private static readonly object instanceLock = new object();
        private static IConfigurationRoot Configuration = null;

        private ConfigurationDAO()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

            Configuration = builder.Build();
        }

        internal static ConfigurationDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationDAO();
                    }
                    return instance;
                }
            }
        }

        internal IConfigurationRoot GetConfiguration() => Configuration;
    }
}
