using AbiokaApi.ApplicationService.Messaging;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IInstallationService : IService
    {
        /// <summary>
        /// Creates the application data.
        /// </summary>
        /// <param name="createApplicationDataRequest">The create application data request.</param>
        void CreateApplicationData(CreateApplicationDataRequest createApplicationDataRequest);

        /// <summary>
        /// Determines whether [is installation required].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is installation required]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInstallationRequired();
    }
}
