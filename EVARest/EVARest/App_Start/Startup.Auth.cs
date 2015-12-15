using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using EVARest.Providers;
using EVARest.Models.DAL;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;

namespace EVARest
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Allow all CORS requests
            app.UseCors(CorsOptions.AllowAll);

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(RestContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            var microsoftAuthentication = new MicrosoftAccountAuthenticationOptions();
            microsoftAuthentication.ClientId = "000000004C16836C";
            microsoftAuthentication.ClientSecret = "R2GPxr8k-dIftBnAEhLRXLjuTsiIxNpl";
            app.UseMicrosoftAccountAuthentication(microsoftAuthentication);

            var twitterAuthentication = new TwitterAuthenticationOptions();
            twitterAuthentication.ConsumerKey = "wOS8t5pPraOLwTH79OlgyttW6";
            twitterAuthentication.ConsumerSecret = "gj9iMcmsixA8UvcsTRqxxoCkzkrpDKc4vuzKGpFjTRiNv3aldl";
            app.UseTwitterAuthentication(twitterAuthentication);

            var facebookAuthenticationOptions = new FacebookAuthenticationOptions();
            facebookAuthenticationOptions.AppId = "1645027742443677";
            facebookAuthenticationOptions.AppSecret = "baf2eea96164855d5e0d436c6e4c9365";
            app.UseFacebookAuthentication(facebookAuthenticationOptions);

            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions();
            googleAuthenticationOptions.ClientId = "867920998848-h8vsdddu2o8dkv72243d8phu599dejgt.apps.googleusercontent.com";
            googleAuthenticationOptions.ClientSecret = "k25jN1BX4bBtWxlNbaBMeIIk";
            googleAuthenticationOptions.CallbackPath = new PathString("/signin-google");
            googleAuthenticationOptions.Provider = new GoogleOAuth2AuthenticationProvider();
            googleAuthenticationOptions.Scope.Add("email");
            app.UseGoogleAuthentication(googleAuthenticationOptions);
        }
    }
}
