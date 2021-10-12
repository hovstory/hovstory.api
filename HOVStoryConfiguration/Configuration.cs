using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOVStoryConfiguration
{
    public static class Configuration
    {
        /// <summary>
        /// Connection string for MongoDB at HOVStory
        /// </summary>
        public static string ConnectionString
        {
            get => ConfigurationDAO.Instance.GetConfiguration().GetSection("HOVStoryDB")["ConnectionString"];
        }

        /// <summary>
        /// Default Database name for the project
        /// </summary>
        public static string DatabaseName
        {
            get => ConfigurationDAO.Instance.GetConfiguration().GetSection("HOVStoryDB")["DatabaseName"];
        }

        /// <summary>
        /// Confessions Table Name in the Database
        /// </summary>
        public static string ConfessionsTableName
        {
            get => ConfigurationDAO.Instance.GetConfiguration().GetSection("HOVStoryDB")["ConfessionsTable"];
        }

        /// <summary>
        /// Users Table name in the Database
        /// </summary>
        public static string UsersTableName
        {
            get => ConfigurationDAO.Instance.GetConfiguration().GetSection("HOVStoryDB")["UsersTable"];
        }
    }
}
