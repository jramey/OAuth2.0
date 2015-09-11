using System;
using System.Configuration;

namespace OAuth.Models
{
    public class ClientData
    {
        private String clientId;
        private String clientSecret;
        public String RedirectUri { get; private set; }

        private ClientData(String clientId, String clientSecret, String redirectUri)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.RedirectUri = redirectUri;
        }

        public static ClientData ReadFromConfig()
        {
            var clientId = ConfigurationManager.AppSettings["APPDATA_client_id"];
            var clientSecret = ConfigurationManager.AppSettings["APPDATA_client_secret"];
            var redirectUri = ConfigurationManager.AppSettings["APPDATA_redirect_uri"];

            return new ClientData(clientId, clientSecret, redirectUri);
        }

        public Boolean ValidateId(String id, String uri)
        {
            if (!this.clientId.Equals(id))
                return false;

            if (!String.IsNullOrEmpty(uri) && !uri.StartsWith(RedirectUri))
                return false;

            return true;
        }

        public Boolean ValidateSecret(String secret, String id)
        {
            if (!this.clientId.Equals(id))
                return false;

            if (!this.clientSecret.Equals(secret))
                return false;

            return true;
        }
    }
}