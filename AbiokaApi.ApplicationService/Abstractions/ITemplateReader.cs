using AbiokaApi.ApplicationService.Messaging;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface ITemplateReader : IService
    {
        /// <summary>
        /// Reads the email template.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Template ReadTemplate(ReadTemplateRequest request);

        /// <summary>
        /// Reads the text.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        string ReadText(ReadTemplateRequest request);
    }
}
