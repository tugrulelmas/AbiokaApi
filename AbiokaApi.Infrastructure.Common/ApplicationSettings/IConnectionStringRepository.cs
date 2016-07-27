namespace AbiokaApi.Infrastructure.Common.ApplicationSettings
{
    public interface IConnectionStringRepository
    {
        /// <summary>
        /// Reads the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        string ReadConnectionString(string connectionStringName);

        /// <summary>
        /// Reads the application setting.
        /// </summary>
        /// <param name="appSettingName">Name of the application setting.</param>
        /// <returns></returns>
        string ReadAppSetting(string appSettingName);
    }
}
