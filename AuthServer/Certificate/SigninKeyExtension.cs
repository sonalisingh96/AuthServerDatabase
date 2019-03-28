using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthServer.Certificate
{
    public class SigninKeyExtension
    {

        private const string KeyFilePath = "C:\\workspace\\IdentityServer4Auth.pfx";
        private const string KeyFilePassword = "ABC$1234";

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
            IConfigurationSection options, ILogger logger)
        {
            var keyFilePath = options.GetValue<string>(KeyFilePath);
            var keyFilePassword = options.GetValue<string>(KeyFilePassword);

            if (File.Exists(keyFilePath))
            {
                logger.LogDebug($"SigninCredentialExtension adding key from file {keyFilePath}");
                builder.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword));
            }
            else
            {
                logger.LogError($"SigninCredentialExtension cannot find key file {keyFilePath}");
            }
        }
    }
}
