using AbiokaApi.ApplicationService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using AbiokaApi.ApplicationService.Messaging;
using System.Collections.Concurrent;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class TemplateReader : ITemplateReader
    {
        private static readonly IDictionary<string, Template> maps = new ConcurrentDictionary<string, Template>();

        private readonly IFileReader fileReader;
        private readonly IConnectionStringRepository connectionStringRepository;

        public TemplateReader(IFileReader fileReader, IConnectionStringRepository connectionStringRepository) {
            this.fileReader = fileReader;
            this.connectionStringRepository = connectionStringRepository;
        }

        public Template ReadTemplate(ReadTemplateRequest request) {
            var cacheResult = GetTemplateFromCache(request);
            if (cacheResult.Item1 != null) {
                return cacheResult.Item1;
            }

            var body = new StringBuilder();
            var subject = string.Empty;
            var lines = fileReader.ReadAllLines(new FilePathRequest { Path = cacheResult.Item2 });
            for (var i = 0; i < lines.Length; i++) {
                if (i == 0) {
                    subject = lines[i];
                    continue;
                }

                body.AppendLine(lines[i]);
            }

            var result = new Template {
                Body = body.ToString(),
                Subject = subject
            };

            maps.Add(cacheResult.Item2, result);

            return result;
        }

        public string ReadText(ReadTemplateRequest request) {
            var cacheResult = GetTemplateFromCache(request);
            if (cacheResult.Item1 != null) {
                return cacheResult.Item1.Body;
            }

            var body = fileReader.ReadAllText(new FilePathRequest { Path = cacheResult.Item2 });

            var result = new Template {
                Body = body
            };

            maps.Add(cacheResult.Item2, result);

            return body;
        }

        private Tuple<Template, string> GetTemplateFromCache(ReadTemplateRequest request) {
            var filePath = connectionStringRepository.ReadAppSetting(request.Key);
            var emailTemplatePath = filePath.Replace("{{lang}}", request.Language.ToLower());

            Template result;
            if (maps.ContainsKey(emailTemplatePath) && maps.TryGetValue(emailTemplatePath, out result)) {
                return new Tuple<Template, string>(maps[emailTemplatePath], emailTemplatePath);
            }

            return new Tuple<Template, string>(null, emailTemplatePath);
        }
    }
}
