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
      public Photobucket(string apiKey, string apiSecret)
         : this( apiKey, apiSecret, false )
      {

      }

      public Photobucket(string apiKey, string apiSecret, bool getLoginToken)
      {
         _apiKey = apiKey;
         _apiSecret = apiSecret;
         OAuth = new OAuth( _apiUrl, _apiKey, _apiSecret );

         if (getLoginToken)
         {
            RequestLoginToken();
         }
      }

      #region paramaters

      private ConsumerToken _consumerToken = null;

      private UserToken _userToken = null;
      public UserToken UserToken
      {
         set
         {
            if (value.Equals(new UserToken()) == false)
            {
               _userToken = value;
               _userLoggedOn = true;
               _appAuthorized = false;
               _consumerToken = null;
               _currentUser = new User(_userToken.Username);
            }
         }
         get 
         {
            if (UserLoggedOn)
            {
               return _userToken;
            }
            else
            {
               throw new PhotobucketApiException("Token Not Authorized");
            }
         }
      }

      public void ClearUserToken()
      {
         _userToken = null;
         _userLoggedOn = false;
         _currentUser = null;
      }

      private PhotobucketToken Token
      {
         get
         {
            if (UserLoggedOn)
            {
               return _userToken;
            }
            else if (AppAuthorized)
            {
               return _consumerToken;
            }
            else
            {
               RequestLoginToken();
               return _consumerToken;
            }
         }
      }

      private User _currentUser = null;
      public User CurrentUser
      {
         get 
         {
            if (_currentUser == null)
            {
               GetCurrentUser();
            }
            return _currentUser;
         }
      }

      public Album BaseAlbum
      {
         get 
         {
            if(_currentUser.Album == null)
            {
               GetCurrentUsersBaseAlbum();
            }

            return _currentUser.Album;
         }
      }

      public string Username
      {
         get
         {
            if (UserLoggedOn)
            {
               return _userToken.Username;
            }
            else
            {
               throw new PhotobucketApiException( "User not logged on" );
            }
         }
      }

      public string ApiUrl
      {
         get
         {
            if (UserLoggedOn)
            {
               return _userToken.UserApi;
            }
            else
           { 
               return _apiUrl;
            }
         }
      }

      private string _apiKey;
      private string _apiSecret;

      private bool _userLoggedOn = false;
      public bool UserLoggedOn
      {
         get
         {
            if (_userLoggedOn && _userToken != null)
            {
               return true;
            }
            return false;
         }
      }

      private bool _appAuthorized = false;
      public bool AppAuthorized
      {
         get
         {
            if (_appAuthorized && _consumerToken != null)
            {
               return true;
            }
            return false;
         }
      }

      #endregion
   }
}
