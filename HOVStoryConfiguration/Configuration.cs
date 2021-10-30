using Microsoft.Extensions.Configuration;
using System.IO;

namespace HOVStoryConfiguration
{
    public static class Configuration
    {
        #region Private Members to get Configuration
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
        #endregion

        #region Public Configuration Fields

        #region Database Stuffs
        /// <summary>
        /// Connection string for MongoDB at HOVStory
        /// </summary>
        public static string ConnectionString => GetConfiguration().GetSection("HOVStoryDB")["ConnectionString"];

        /// <summary>
        /// Default Database name for the project
        /// </summary>
        public static string DatabaseName => GetConfiguration().GetSection("HOVStoryDB")["DatabaseName"];

        /// <summary>
        /// Confessions Table Name in the Database
        /// </summary>
        public static string ConfessionsTableName => GetConfiguration().GetSection("HOVStoryDB")["ConfessionsTable"];

        /// <summary>
        /// Users Table name in the Database
        /// </summary>
        public static string UsersTableName => GetConfiguration().GetSection("HOVStoryDB")["UsersTable"];
        
        /// <summary>
        /// Logs Table name in the Database
        /// </summary>
        public static string LogsTableName => GetConfiguration().GetSection("HOVStoryDB")["LogsTable"];
        #endregion
        #region Confession Status
        /// <summary>
        /// Approved Status for confession
        /// </summary>
        public static string ConfessionApproved => GetConfiguration().GetSection("ConfessionStatus")["Approved"];

        /// <summary>
        /// Rejected Status for confession
        /// </summary>
        public static string ConfessionRejected => GetConfiguration().GetSection("ConfessionStatus")["Rejected"];

        /// <summary>
        /// Pening Status for confession
        /// </summary>
        public static string ConfessionPending => GetConfiguration().GetSection("ConfessionStatus")["Pending"];
        #endregion

        #region JWT
        /// <summary>
        /// JWT Valid Audience
        /// </summary>
        public static string ValidAudience => GetConfiguration().GetSection("JWT")["ValidAudience"];

        /// <summary>
        /// JWT Valid Issuer
        /// </summary>
        public static string ValidIssuer => GetConfiguration().GetSection("JWT")["ValidIssuer"];

        /// <summary>
        /// JWT Secret
        /// </summary>
        public static string Secret => GetConfiguration().GetSection("JWT")["Secret"];
        #endregion

        #endregion
    }
}
