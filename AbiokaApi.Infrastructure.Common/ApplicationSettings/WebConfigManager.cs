using System;
using System.Configuration;

namespace AbiokaApi.Infrastructure.Common.ApplicationSettings
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AbiokaApi.Infrastructure.Common.ApplicationSettings.IConfigurationManager" />
    public class WebConfigManager : IConfigurationManager
    {
        /// <summary>
        /// Reads the application setting.
        /// </summary>
        /// <param name="appSettingName">Name of the application setting.</param>
        /// <returns></returns>
        public string ReadAppSetting(string appSettingName) => ConfigurationManager.AppSettings[appSettingName];

        /// <summary>
        /// Reads the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string ReadConnectionString(string connectionStringName) {
            var conn = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (conn == null)
                throw new ArgumentNullException($"Connection string with name {connectionStringName} couldn't be found in the configuration file.");

            return conn.ConnectionString;
        }
    }
}
