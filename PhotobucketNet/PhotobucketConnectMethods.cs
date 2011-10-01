using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace PhotobucketNet
{
   public partial class Photobucket
   {
      public void Ping()
      {
         string pingUrl = OAuth.GenerateURL( ApiUrl, _pingUrl, _pingMethod, new PhotobucketToken() );

         ResponseMessage pingResponseMessage = GetResponseMessageFromUrl( pingUrl, _pingMethod );
      }

      /// <summary>
      /// Requests the Auth token and secret for the API
      /// And stores into _consumerToken
      /// </summary>
      private void RequestLoginToken()
      {
         string loginTokenUrl = OAuth.GenerateURL( ApiUrl, _loginTokenUrl, _loginTokenMethod, new PhotobucketToken() );

         ResponseMessage loginTokenResponseMessage = GetResponseMessageFromUrl( loginTokenUrl, _loginTokenMethod );

         _consumerToken = OAuth.GetConsumerTokenFromResponse( loginTokenResponseMessage.ResponseString );

         _appAuthorized = true;
      }

      public string GenerateUserLoginUrl()
      {
         RequestLoginToken();

         if (!AppAuthorized || string.IsNullOrEmpty( _consumerToken.NextStepUrl ))
         {
            throw ( new PhotobucketApiException( "App Not Authorized" ) );
         }

         return _consumerToken.NextStepUrl + "?oauth_token=" + _consumerToken.Token;
      }

      public void LaunchUserLogin()
      {
         try
         {
            System.Diagnostics.Process.Start(GenerateUserLoginUrl());
         }
         catch (Exception)
         {
            throw new PhotobucketApiException("Trouble launching login url");
         }
      }

      public string GenerateUserLogoutUrl()
      {         
         return _photbucketUrl + _apilogin + _logout + "?oauth_consumer_key=" + _apiKey;
      }

      public void LaunchUserLogout()
      {
         try
         {
            System.Diagnostics.Process.Start(GenerateUserLogoutUrl());
            ClearUserToken();
         }
         catch (Exception)
         {
            throw new PhotobucketApiException("Trouble launching login url");
         }
      }

      /// <summary>
      /// Requests the token and secret for the user that logged in
      /// </summary>
      public UserToken RequestUserToken()
      {
         if (!AppAuthorized)
         {
            throw ( new PhotobucketApiException( "App Not Authorized" ) );
         }

         string userTokenUrl = OAuth.GenerateURL( _apiUrl, _userTokenUrl, _userTokenMethod, _consumerToken );

         ResponseMessage userTokenResponseMessage = GetResponseMessageFromUrl( userTokenUrl, _userTokenMethod );

         _userToken = OAuth.GetUserTokenFromResponse( userTokenResponseMessage.ResponseString );
         _userLoggedOn = true;
         _appAuthorized = false;
         _consumerToken = null;
         _currentUser = new User(_userToken.Username);
         
         return _userToken;
      }

      #region Get Response From Url
      private Stream GetResponseStreamFromUrl(string url, string method)
      {
         try
         {
            WebRequest urlRequest = WebRequest.Create( url );
            urlRequest.Method = method;
            urlRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            WebResponse urlResponse = urlRequest.GetResponse( );
            Stream urlResponseStream = urlResponse.GetResponseStream( );

            return urlResponseStream;
         }
         catch (WebException webEx)
         {
            throw PhotobucketException.CreateFromWebException(webEx);
         }
      }

      private XmlResponseMessage GetXmlResponseMessageFromUrl(string url, string method)
      {
         Stream urlResponseStream = GetResponseStreamFromUrl( url, method );

         return new XmlResponseMessage( urlResponseStream );
      }

      private ResponseMessage GetResponseMessageFromUrl(string url, string method)
      {
         Stream urlResponseStream = GetResponseStreamFromUrl( url, method );

         return new ResponseMessage( urlResponseStream );
      }

      #endregion

      private OAuth OAuth = null;

      #region constants
      private const string _apiUrl = "http://api.photobucket.com/";
      private const string _photbucketUrl = "http://photobucket.com/";
      private const string _apilogin = "apilogin/";
      private const string _logout = "logout";
      private const string _loginTokenUrl = "login/request";
      private const string _pingUrl = "ping";
      private const string _pingMethod = "POST";
      private const string _loginTokenMethod = "POST";
      private const string _userTokenUrl = "login/access";
      private const string _userTokenMethod = "POST";
      #endregion
   }
}