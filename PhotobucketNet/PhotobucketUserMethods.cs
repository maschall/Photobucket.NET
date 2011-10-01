using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;

namespace PhotobucketNet
{
   public partial class Photobucket
   {
      #region GetUser
      /// <summary>
      /// Get the User object that represents the user currently logged on
      /// </summary>
      /// <returns></returns>
      private User GetUser(string username)
      {
         string relativePath = GenerateRelativeUserUrl( username );

         QueryParameterList paramaters = new QueryParameterList( );
         paramaters.Add( new QueryParameter( _format, "xml" ) );

         string getUserUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUserMethod, Token, paramaters);

         XmlResponseMessage getUserResponseMessage = GetXmlResponseMessageFromUrl( getUserUrl, _getUserMethod );

         return new User(getUserResponseMessage.ResponseXml);
      }

      public User GetCurrentUser()
      {
         _currentUser = GetUser(Username);
         return _currentUser;
      }
      #endregion

      #region GetUsersMedia

      #region GetUsersRecentMedia
      public MediaItemList GetUsersRecentMedia(string username, MediaType type, int page, int perPage)
      {
         string relativePath = GenerateRelativeUserSearchUrl(username);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_type, type.ToString().ToLower()));
         paramaters.Add(new QueryParameter(_page, page.ToString()));
         paramaters.Add(new QueryParameter(_perpage, perPage.ToString()));

         string getUsersRecentMediaUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUserMediaMethod, Token, paramaters);

         XmlResponseMessage getUsersRecentMediaResponseMessage = GetXmlResponseMessageFromUrl(getUsersRecentMediaUrl, _getUserMediaMethod);

         return MediaItemList.CreateFromXmlResponseMessage(getUsersRecentMediaResponseMessage);
      }

      public MediaItemList GetCurrentUsersRecentMedia(MediaType type, int page, int perPage)
      {
         return GetUsersRecentMedia(CurrentUser.Username, type, page, perPage);
      }
      #endregion

      #region GetAllUsersMedia
      /// <summary>
      /// Get all the users media of a specific type and number perPage
      /// </summary>
      /// <param name="user"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public MediaItemList GetAllUsersMedia(string username, MediaType type, int perPage)
      {
         MediaItemList mediaList = new MediaItemList();

         int curPageNum = 0;
         bool stillMoreToGet = true;

         while (stillMoreToGet)
         {
            curPageNum++;

            MediaItemList curPageMediaList = GetUsersRecentMedia(username, type, curPageNum, perPage);

            mediaList.AddRange(curPageMediaList);

            if (curPageMediaList.Count < perPage)
            {
               stillMoreToGet = false;
            }
         }

         return mediaList;
      }

      /// <summary>
      /// default perpage 200
      /// </summary>
      /// <param name="username"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public MediaItemList GetAllUsersMedia(string username, MediaType type)
      {
         return GetAllUsersMedia(username, type, 200);
      }

      /// <summary>
      /// default type to "all" and perpage to 200
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public MediaItemList GetAllUsersMedia(string username)
      {
         return GetAllUsersMedia(username, MediaType.All);
      }

      /// <summary>
      /// takes a User and defaults other paramaters
      /// </summary>
      /// <param name="user"></param>
      /// <returns></returns>
      public MediaItemList GetAllUsersMedia(ref User user)
      {
         user.SetMediaList = GetAllUsersMedia(user.Username);
         return user.MediaList;
      }

      /// <summary>
      /// Assumes user is current logged on user
      /// </summary>
      /// <returns></returns>
      public MediaItemList GetAllCurrentUsersMedia()
      {
         _currentUser = CurrentUser;
         return GetAllUsersMedia(ref _currentUser);
      }
      #endregion

      #region GetAllUsersImageMedia
      public MediaItemList GetAllUsersImageMedia(string username)
      {
         return GetAllUsersMedia(username, MediaType.Image);
      }

      public MediaItemList GetAllUsersImageMedia(ref User user)
      {
         user.SetImageMediaList = GetAllUsersImageMedia(user.Username);
         return user.ImageMediaList;
      }

      public MediaItemList GetAllCurrentUsersImageMedia()
      {
         _currentUser = CurrentUser;
         return GetAllUsersImageMedia(ref _currentUser);
      }
      #endregion

      #region GetAllUsersVideoMedia

      public MediaItemList GetAllUsersVideoMedia(string username)
      {
         return GetAllUsersMedia(username, MediaType.Video);
      }

      public MediaItemList GetAllUsersVideoMedia(ref User user)
      {
         user.SetVideoMediaList = GetAllUsersVideoMedia(user.Username);
         return user.VideoMediaList;
      }

      public MediaItemList GetAllCurrentUsersVideoMedia()
      {
         _currentUser = CurrentUser;
         return GetAllUsersVideoMedia(ref _currentUser);
      }
      #endregion
      #endregion

      #region Get User Contacts
      public UserContactList GetCurrentUsersContacts()
      {
         string relativePath = GenerateRelativeUsersContactsUrl(_currentUser.Username);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getUsersContactsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUserContactsMethod, Token, paramaters);

         XmlResponseMessage getUsersContactsResponseMessage = GetXmlResponseMessageFromUrl(getUsersContactsUrl, _getUserContactsMethod);

         _currentUser.SetContact = UserContactList.CreateFromXmlResponseMessage(getUsersContactsResponseMessage);
         return _currentUser.Contacts;
      }
      #endregion

      #region Get Media MediaTags
      public MediaTagList GetUsersMediaTags(string username)
      {
         string relativePath = GenerateRelativeUserMediaTagsUrl(username);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getUsersMediaTagsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUsersMediaTagsMethod, Token, paramaters);

         XmlResponseMessage getUsersMediaTagsResponseMessage = GetXmlResponseMessageFromUrl(getUsersMediaTagsUrl, _getUsersMediaTagsMethod);

         return MediaTagList.CreateFromXmlResponseMessage(getUsersMediaTagsResponseMessage);
      }

      public MediaTagList GetCurrentUsersMediaTags()
      {
         _currentUser.SetMediaTags = GetUsersMediaTags(CurrentUser.Username);
         return _currentUser.MediaTags;
      }

      public MediaItemList GetUsersMediaWithTag(string username, string tagname, int page, int perPage)
      {
         if (string.IsNullOrEmpty(tagname))
         {
            throw new PhotobucketApiException("tagname is null");
         }

         string relativePath = GenerateRelativeUsersMediaWithTagUrl(username, tagname);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_page, page.ToString()));
         paramaters.Add(new QueryParameter(_perpage, perPage.ToString()));

         string getUsersMediaWithTagTagsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUsersMediaTagsMethod, Token, paramaters);

         XmlResponseMessage getUsersMediaWithTagResponseMessage = GetXmlResponseMessageFromUrl(getUsersMediaWithTagTagsUrl, _getUsersMediaTagsMethod);

         return MediaItemList.CreateFromXmlResponseMessage(getUsersMediaWithTagResponseMessage);
      }

      public MediaItemList GetUsersMediaWithTag(string username, string tagname)
      {
         return GetUsersMediaWithTag(username, tagname, 1, 200);
      }

      public MediaItemList GetAllUsersMediaWithTag(string username, MediaTag mediaTag)
      {
         MediaItemList mediaList = new MediaItemList();

         int curPageNum = 0;
         bool stillMoreToGet = true;

         while (stillMoreToGet)
         {
            curPageNum++;

            MediaItemList curPageMediaList = GetUsersMediaWithTag(username, mediaTag.Name, curPageNum, 200);

            mediaList.AddRange(curPageMediaList);

            if (curPageMediaList.Count < 200)
            {
               stillMoreToGet = false;
            }
         }

         return mediaList;
      }

      public MediaItemList GetAllCurrentUsersMediaWithTag(MediaTag mediaTag)
      {
         return GetAllUsersMediaWithTag(CurrentUser.Username, mediaTag);
      }
      #endregion

      #region Get User Upload Options
      public UploadOptions GetCurrentUsersUploadOptions()
      {
         string relativePath = GenerateRelativeUsersUploadOptionsUrl();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getCurrentUsersUploadOptionsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUsersUploadOptionsMethod, Token, paramaters);

         XmlResponseMessage getCurrentUsersUploadOptionsResponseMessage = GetXmlResponseMessageFromUrl(getCurrentUsersUploadOptionsUrl, _getUsersUploadOptionsMethod);

         CurrentUser.SetUploadOptions = UploadOptions.CreateFromXmlResponseMessage(getCurrentUsersUploadOptionsResponseMessage);
         return CurrentUser.UploadOptions;
      }
      #endregion

      #region Update User Upload Options
      public UploadOptions UpdateCurrentUsersUploadOptions(ImageSize imageSize, bool autoTagging)
      {
         string relativePath = GenerateRelativeUsersUploadOptionsUrl();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_defaultimagesize, Convert.ToString(imageSize)));
         paramaters.Add(new QueryParameter(_autotagging, autoTagging ? "1" : "0" ));

         string updateCurrentUsersUploadOptionsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _updateUsersUploadOptionsMethod, Token, paramaters);

         XmlResponseMessage updateCurrentUsersUploadOptionsResponseMessage = GetXmlResponseMessageFromUrl(updateCurrentUsersUploadOptionsUrl, _updateUsersUploadOptionsMethod);

         UploadOptions userUploadOptions = new UploadOptions();
         userUploadOptions.SetDefaultImageSize = imageSize;
         userUploadOptions.SetAutoTagging = autoTagging;

         CurrentUser.SetUploadOptions = userUploadOptions;
         return _currentUser.UploadOptions;
      }
      #endregion

      #region Get User URLs
      public UserUrls GetUsersUrls(string username)
      {
         string relativePath = GenerateRelativeUserUrl(username);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getUsersUrlsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getUsersUrlMethod, Token, paramaters);

         XmlResponseMessage getUsersUrlsResponseMessage = GetXmlResponseMessageFromUrl(getUsersUrlsUrl, _getUsersUrlMethod);

         return UserUrls.CreateFromXmlResponseMessage(getUsersUrlsResponseMessage);
      }

      public UserUrls GetCurrentUsersUrls()
      {
         CurrentUser.SetUserUrls = GetUsersUrls(CurrentUser.Username);
         return _currentUser.UserUrls;
      }
      #endregion

      #region GenerateUrls
      private string GenerateRelativeUserUrl(string username)
      {
         if (string.IsNullOrEmpty( username ))
         {
            return _userUrl;
         }
         else
         {
            return GenerateUserUser( username );
         }
      }

      private string GenerateRelativeUserSearchUrl(string username)
      {
         return GenerateUserUser(username) + "/" + _searchUrl;
      }

      private string GenerateRelativeUserSearchByNameUrl(string username, string mediaName)
      {
         return GenerateRelativeUserSearchUrl(username) + "/" + mediaName;
      }

      private string GenerateRelativeUsersContactsUrl(string username)
      {
         return GenerateUserUser(username) + "/" + _contact;
      }

      private string GenerateRelativeUserMediaTagsUrl(string username)
      {
         return GenerateUserUser(username) + "/" + _tag;
      }

      private string GenerateRelativeUsersMediaWithTagUrl(string username, string tagname)
      {
         return GenerateUserUser(username) + "/" + _tag + "/" + tagname;
      }

      private string GenerateRelativeUsersUploadOptionsUrl()
      {
         return GenerateUserUser(CurrentUser.Username) + "/" + _uploadOption;
      }

      private string GenerateRelativeUsersUrlUrl(string username)
      {
         return GenerateUserUser(username) + "/" + _url;
      }

      private string GenerateUserUser(string username)
      {
         return _userUrl + username;
      }
      #endregion

      #region constants
      private const string _userUrl = "user/";
      private const string _getUserMethod = "GET";
      private const string _getUserMediaMethod = "GET";
      private const string _getUserContactsMethod = "GET";
      private const string _getUsersMediaTagsMethod = "GET";
      private const string _getUsersUploadOptionsMethod = "GET";
      private const string _getUsersUrlMethod = "GET";
      private const string _updateUsersUploadOptionsMethod = "PUT";
      private const string _searchUrl = "search";
      private const string _page = "page";
      private const string _tag = "tag";
      private const string _perpage = "perpage";
      private const string _contact = "contact";
      private const string _uploadOption = "uploadoption";
      private const string _defaultimagesize = "defaultimagesize";
      private const string _autotagging = "autotagging";
      private const string _url = "url";
      #endregion

   }
}