using System;
using System.Collections.Generic;
using System.Text;

namespace PhotobucketNet
{
   public class PhotobucketToken
   {
      public PhotobucketToken()
      {

      }

      public PhotobucketToken(string token, string secret)
      {
         _token = token;
         _tokenSecret = secret;
      }

      #region overrides

      public override bool Equals(object obj)
      {
         PhotobucketToken token = obj as PhotobucketToken;
         return Token.Equals( token.Token ) && TokenSecret.Equals( token.TokenSecret );
      }
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

      #endregion

      private string _token = String.Empty;
      public string Token
      {
         get { return _token; }
         set { _token = value; }
      }

      private string _tokenSecret = String.Empty;
      public string TokenSecret
      {
         get { return _tokenSecret; }
         set { _tokenSecret = value; }
      }


   }

   internal class ConsumerToken : PhotobucketToken
   {
      public ConsumerToken()
      {

      }

      public ConsumerToken(string token, string secret, string nextStepUrl)
         : base( token, secret )
      {
         NextStepUrl = nextStepUrl;
      }

      #region overrides

      public override bool Equals(object obj)
      {
         ConsumerToken token = obj as ConsumerToken;
         return base.Equals( obj ) && NextStepUrl.Equals( token.NextStepUrl );
      }
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

      #endregion

      private string _nextStepUrl = String.Empty;
      public string NextStepUrl
      {
         get { return _nextStepUrl; }
         set { _nextStepUrl = value; }
      }
   }

   public class UserToken : PhotobucketToken
   {
      public UserToken()
      {

      }

      public UserToken(string token, string secret, string username, string subdomain, string userHomeUrl)
         : base( token, secret )
      {
         Username = username;
         Subdomain = subdomain;
         UserHomeUrl = userHomeUrl;
      }

      #region overrides

      public override bool Equals(object obj)
      {
         UserToken token = obj as UserToken;
         return base.Equals( obj ) && Username.Equals( token.Username ) && Subdomain.Equals( token.Subdomain ) && UserHomeUrl.Equals( token.UserHomeUrl );
      }
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }

      #endregion

      private string _username = String.Empty;
      public string Username
      {
         get { return _username; }
         set { _username = value; }
      }

      private string _subdomain = String.Empty;
      public string Subdomain
      {
         get { return _subdomain; }
         set
         {
            _subdomain = value;
            _userApi = "http://" + Subdomain + "/";
         }
      }

      private string _userHomeUrl = String.Empty;
      public string UserHomeUrl
      {
         get { return _userHomeUrl; }
         set { _userHomeUrl = value; }
      }

      private string _userApi = String.Empty;
      public string UserApi
      {
         get { return _userApi; }
      }
   }
}