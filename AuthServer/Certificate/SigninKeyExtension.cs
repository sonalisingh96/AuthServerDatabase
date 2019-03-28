using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Certificate
{
    public static class SigninKeyExtension
    {
        private const string KeyType = "KeyFile";
        private const string KeyTypeKeyFile = "KeyFile";
        private const string KeyTypeTemporary = "Temporary";
        private const string KeyFilePath = "C:\\workspace\\IdentityServer4Auth.pfx";
        private const string KeyFilePassword = "ABC$1234";
        public static IIdentityServerBuilder AddSigninCredentialFromConfig( this
             IIdentityServerBuilder builder,IConfiguration options)
        {
            switch (KeyType)
            {
                case KeyTypeTemporary:
                    builder.AddDeveloperSigningCredential();
                    break;
                case KeyTypeKeyFile:
                    AddCertificateFromFile(builder);
                    break;
                default:
            }
            return builder;
        }
        private static void AddCertificateFromFile(IIdentityServerBuilder builder)
        {
            if (File.Exists(KeyFilePath))
            {
                builder.AddSigningCredential(new X509Certificate2(KeyFilePath, KeyFilePassword));
            }
            else
            {
                Console.WriteLine("Cannot find any key file at the specified path");
            }
        }
    }
}
