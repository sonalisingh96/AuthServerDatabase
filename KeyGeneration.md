# **Generating and Installing Signing Certificates for Identity server4**
 We are going to use makecert and pvk2pfx utilities to create a pair of certificates. These can be found in "Windows Software Development Kit for Windows 8.1”.
1. Open the command prompt and navigate to `C:\Program Files (x86)\Windows Kits\8.1\bin\x86`

Now we will need a Certificate Authority and a Personal Information Exchange (that contains a Private Key and a Public Key).
* The Private Key is used to sign the JWT Token.
* The Public Key is exposed to the clients, so that they can validate the JWTs.
# **Generating x509 Certificates Ready for Identity Server**
1. Create a Certificate Authority and a Private Key:

`makecert -n "CN=IdentityServerCN" -a sha256 -sv IdentityServer4Auth.pvk -r 	IdentityServer4Auth.cer`

2. Store the Private Key and the Public Key in a Personal Information Exchange.

`pvk2pfx -pvk IdentityServer4Auth.pvk -spc IdentityServer4Auth.cer -po -pfx IdentityServer4Auth.pfx`

Now you should have the following three files.

* IdentityServer4Auth.pvk

* IdentityServer4Auth.cer

* IdentityServer4Auth.pfx

# **Installing and Using Certificates**
1. Take the .pfx file and put it in your visual studio solution.

2. In the startup file of the Identity Server4 inside the ConfigureService add the following code snippet.

`services.AddIdentityServer()`

`.AddSigningCredential(new X509Certificate2("Filename.pfx", "Passwordofpfxfile"));`
