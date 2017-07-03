using AbiokaApi.ApplicationService.Messaging;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IFileReader : IService
    {
        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <param name="filePathRequest">The file path request.</param>
        /// <returns></returns>
        string[] ReadAllLines(FilePathRequest filePathRequest);

        /// <summary>
        /// Reads the lines.
        /// </summary>
        /// <param name="filePathRequest">The file path request.</param>
        /// <returns></returns>
        IEnumerable<string> ReadLines(FilePathRequest filePathRequest);

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="filePathRequest">The file path request.</param>
        /// <returns></returns>
        byte[] ReadAllBytes(FilePathRequest filePathRequest);

        /// <summary>
        /// Reads all text.
        /// </summary>
        /// <param name="filePathRequest">The file path request.</param>
        /// <returns></returns>
        string ReadAllText(FilePathRequest filePathRequest);
    }
}
