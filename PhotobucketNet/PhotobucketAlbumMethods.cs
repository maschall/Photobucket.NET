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
      #region Create New Album
      private Album CreateNewAlbum(string albumName, string albumPath)
      {
         string relativePath = GenerateRelativeAlbumUrl(albumPath);

         QueryParameterList paramaters = new QueryParameterList( );
         paramaters.Add( new QueryParameter( _name, albumName ) );
         paramaters.Add( new QueryParameter( _format, "xml" ) );

         string createNewAlbumUrl = OAuth.GenerateURL(ApiUrl, relativePath, _createAlbumMethod, Token, paramaters);

         XmlResponseMessage createNewAlbumResponseMessage = GetXmlResponseMessageFromUrl(createNewAlbumUrl, _createAlbumMethod);

         return new Album(albumName, albumPath);
      }

      public Album CreateNewAlbumInAlbum(string albumName, ref Album album)
      {
         Album newAlbum = CreateNewAlbum(albumName, album.Path);
         album.SubAlbums.Add(newAlbum);
         return newAlbum;
      }

      public Album CreateNewAlbumInAlbum(string albumName, Album album)
      {
         return CreateNewAlbumInAlbum( albumName, ref album );
      }

      public Album CreateNewAlbumAtBase(string albumName)
      {
         Album baseAlbum = BaseAlbum;
         return CreateNewAlbumInAlbum(albumName, ref baseAlbum);
      }
      #endregion

      #region Get Album
      private Album GetAlbum(string albumPath, bool recurse, string view, string media, bool paginated, int page, int perpage)
      {
         string relativePath = GenerateRelativeAlbumUrl(albumPath);

         QueryParameterList paramaters = new QueryParameterList( );
         paramaters.Add( new QueryParameter( _format, "xml" ) );
         paramaters.Add(new QueryParameter(_recurse, recurse ? "1" : "0"));
         paramaters.Add(new QueryParameter(_view, view));
         paramaters.Add(new QueryParameter(_media, media));
         paramaters.Add(new QueryParameter(_paginated, paginated ? "1" : "0"));
         paramaters.Add(new QueryParameter(_page, page.ToString()));
         paramaters.Add(new QueryParameter(_perpage, perpage.ToString()));

         string getUsersAlbumUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getAlbumMethod, Token, paramaters);

         XmlResponseMessage getUsersAlbumResponseMessage = GetXmlResponseMessageFromUrl(getUsersAlbumUrl, _getAlbumMethod);

         return new Album(getUsersAlbumResponseMessage.ResponseXml.SelectSingleNode("descendant::album"));
      }

      private Album GetAlbum(string albumPath)
      {
         return GetAlbum(albumPath, true, "nested", "all", false, 1, 20);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="username"></param>
      /// <param name="albumPath">this is the path to the album from the base album</param>
      /// <param name="recurse"></param>
      /// <param name="view"></param>
      /// <param name="media"></param>
      /// <param name="paginated"></param>
      /// <param name="page"></param>
      /// <param name="perpage"></param>
      /// <returns></returns>
      public Album GetUsersAlbum(string username, string albumPath, bool recurse, string view, string media, bool paginated, int page, int perpage)
      {
         albumPath = String.IsNullOrEmpty(albumPath) ? "" : "/" + albumPath;
         return GetAlbum(username + albumPath, recurse, view, media, paginated, page, perpage);
      }

      public Album GetUsersAlbum(string username, string albumPath)
      {
         albumPath = String.IsNullOrEmpty(albumPath) ? "" : "/" + albumPath;
         return GetAlbum(username + albumPath, true, "nested", "all", false, 1, 20);
      }

      public Album GetCurrentUsersAlbum(string albumPath, bool recurse, string view, string media, bool paginated, int page, int perpage)
      {
         return GetUsersAlbum(CurrentUser.Username, albumPath, recurse, view, media, paginated, page, perpage);
      }

      public Album GetCurrentUsersAlbum(string albumPath)
      {
         return GetUsersAlbum(CurrentUser.Username, albumPath);
      }

      public Album GetUsersBaseAlbum(string username, bool recurse, string view, string media, bool paginated, int page, int perpage)
      {
         return GetUsersAlbum(username, String.Empty, recurse, view, media, paginated, page, perpage);
      }

      public Album GetUsersBaseAlbum(string username)
      {
         return GetUsersAlbum(username, String.Empty);
      }

      public Album GetCurrentUsersBaseAlbum(bool recurse, string view, string media, bool paginated, int page, int perpage)
      {
         _currentUser.SetAlbum = GetUsersBaseAlbum(CurrentUser.Username, recurse, view, media, paginated, page, perpage);
         return _currentUser.Album;
      }

      public Album GetCurrentUsersBaseAlbum()
      {
         _currentUser.SetAlbum = GetUsersBaseAlbum(CurrentUser.Username);
         return _currentUser.Album;
      }

      public AlbumList GetAllUsersAlbums(string username)
      {
         string relativePath = GenerateRelativeAlbumUrl(username);         

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_recurse, "1"));
         paramaters.Add(new QueryParameter(_view, "flat"));
         paramaters.Add(new QueryParameter(_media, "none"));

         string getAllUsersAlbumsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getAlbumMethod, Token, paramaters);         

         XmlResponseMessage getAllUsersAlbumsResponseMessage = GetXmlResponseMessageFromUrl(getAllUsersAlbumsUrl, _getAlbumMethod);

         return AlbumList.CreateFromXmlResponseMessage(getAllUsersAlbumsResponseMessage);
      }

      public AlbumList GetAllCurrentUsersAlbums()
      {
         return GetAllUsersAlbums(CurrentUser.Username);
      }
      #endregion

      #region Rename Album
      private void RenameAlbum(string origAlbumPath, string newAlbumName)
      {
         string relativePath = GenerateRelativeAlbumUrl(origAlbumPath);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_name, newAlbumName));
         paramaters.Add(new QueryParameter(_format, "xml"));

         string renameAlbumUrl = OAuth.GenerateURL(ApiUrl, relativePath, _renameAlbumMethod, Token, paramaters);

         XmlResponseMessage renameAlbumResponseMessage = GetXmlResponseMessageFromUrl(renameAlbumUrl, _renameAlbumMethod);
      }

      public void RenameAlbum(ref Album album, string newAlbumName)
      {
         RenameAlbum(album.Name, newAlbumName);

         album.Name = newAlbumName;
      }

      public void RenameAlbum(Album album, string newAlbumName)
      {
         RenameAlbum( ref album, newAlbumName );
      }
      #endregion

      #region Delete Album
      private void DeleteAlbum(string albumPath)
      {
         string relativePath = GenerateRelativeAlbumUrl(albumPath);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string deleteAlbumUrl = OAuth.GenerateURL(ApiUrl, relativePath, _deleteAlbumMethod, Token, paramaters);

         XmlResponseMessage deleteAlbumResponseMessage = GetXmlResponseMessageFromUrl(deleteAlbumUrl, _deleteAlbumMethod);
      }

      /// <summary>
      /// deletes from server and sets album to null
      /// </summary>
      /// <param name="album"></param>
      public void DeleteAlbum(ref Album album)
      {
         DeleteAlbum(album.Path);
         album = null;
      }

      public void DeleteAlbum(Album album)
      {
         DeleteAlbum( album.Path );
      }

      #endregion

      #region Get Album Privacy Settings
      private Privacy GetAlbumsPrivacySettings(string albumPath)
      {
         string relativePath = GenerateRelativeAlbumPrivacyUrl(albumPath);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getAlbumPrivacySettingsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getAlbumPrivacyMethod, Token, paramaters);

         XmlResponseMessage getAlbumPrivacySettingsResponseMessage = GetXmlResponseMessageFromUrl(getAlbumPrivacySettingsUrl, _getAlbumPrivacyMethod);

         return GetPrivacyFromResponseMessage(getAlbumPrivacySettingsResponseMessage);
      }

      public Privacy GetAlbumsPrivacySettings(ref Album album)
      {
         album.Privacy = GetAlbumsPrivacySettings(album.Path);

         return album.Privacy;
      }

      public Privacy GetAlbumsPrivacySettings(Album album)
      {
         return GetAlbumsPrivacySettings( ref album );
      }

      public Privacy GetBaseAlbumsPrivacySettings()
      {
         Album baseAlbum = BaseAlbum;
         return GetAlbumsPrivacySettings(ref baseAlbum);
      }

      private Privacy GetPrivacyFromResponseMessage(XmlResponseMessage responseMessage)
      {
         XmlNode privacyNode = responseMessage.ResponseXml.SelectSingleNode("descendant::content");

         if (privacyNode == null)
         {
            throw new PhotobucketApiException("Did not find content in xml response from GetAlbumPrivacySettigns");
         }

         if (privacyNode.InnerText == "public")
         {
            return Privacy.PUBLIC;
         }
         else if (privacyNode.InnerText == "private")
         {
            return Privacy.PRIVATE;
         }
         else
         {
            throw new PhotobucketApiException("Did not see public or private for privacy settings");
         }
      }
      #endregion

      #region Update Album Privacy Settings
      private Privacy UpdateAlbumPrivacySettings(string albumPath, Privacy privacySetting, string password)
      {
          string relativePath = GenerateRelativeAlbumPrivacyUrl(albumPath);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         if (privacySetting == Privacy.PUBLIC)
         {
            paramaters.Add(new QueryParameter(_privacy, "public"));
         }
         else
         {
            paramaters.Add(new QueryParameter(_privacy, "private"));
            paramaters.Add(new QueryParameter(_password, password));
         }

         string updateAlbumPrivacySettingsUrl = OAuth.GenerateURL(ApiUrl, relativePath, _updateAlbumPrivacyMethod, Token, paramaters);

         XmlResponseMessage updateAlbumPrivacySettingsResponseMessage = GetXmlResponseMessageFromUrl(updateAlbumPrivacySettingsUrl, _updateAlbumPrivacyMethod);

         return GetPrivacyFromResponseMessage(updateAlbumPrivacySettingsResponseMessage);
      }

      public Privacy UpdateAlbumPrivacySettings(ref Album album, Privacy privacySetting, string password)
      {
         album.Privacy = UpdateAlbumPrivacySettings(album.Path, privacySetting, password);

         return album.Privacy;
      }

      public Privacy MakeAlbumPublic(ref Album album)
      {
         return UpdateAlbumPrivacySettings(ref album, Privacy.PUBLIC, "");
      }

      public Privacy MakeAlbumPrivate(ref Album album, string password)
      {
         return UpdateAlbumPrivacySettings(ref album, Privacy.PRIVATE, password);
      }

      public Privacy UpdateAlbumPrivacySettings(Album album, Privacy privacySetting, string password)
      {
         return UpdateAlbumPrivacySettings( ref album, privacySetting, password );
      }

      public Privacy MakeAlbumPublic(Album album)
      {
         return UpdateAlbumPrivacySettings(album, Privacy.PUBLIC, "" );
      }

      public Privacy MakeAlbumPrivate(Album album, string password)
      {
         return UpdateAlbumPrivacySettings( album, Privacy.PRIVATE, password );
      }

      public Privacy MakeBaseAlbumPublic()
      {
         Album baseAlbum = BaseAlbum;
         return MakeAlbumPublic(ref baseAlbum);
      }

      public Privacy MakeBaseAlbumPrivate(string password)
      {
         Album baseAlbum = BaseAlbum;
         return MakeAlbumPrivate(ref baseAlbum, password);
      }

      #endregion

      #region Generate Urls
      private string GenerateRelativeAlbumUrl(string albumName)
      {
         return GenerateAlbumIdentifier(albumName);
      }

      private string GenerateRelativeAlbumPrivacyUrl(string albumName)
      {
          return GenerateRelativeAlbumUrl(albumName) + "/" + _privacy;
      }

      private string GenerateAlbumIdentifier(string albumName)
      {
         return _albumUrl + "/" + OAuth.UrlEncode(OAuth.UrlEncode(albumName));
      }
      #endregion

      #region constants
      private const string _createAlbumMethod = "POST";
      private const string _getAlbumMethod = "GET";
      private const string _getAlbumPrivacyMethod = "GET";
      private const string _updateAlbumPrivacyMethod = "POST";
      private const string _renameAlbumMethod = "PUT";
      private const string _deleteAlbumMethod = "DELETE";
      private const string _albumUrl = "album";
      private const string _name = "name";
      private const string _format = "format";
      private const string _paginated = "paginated";
      private const string _view = "view";
      private const string _media = "media";
      private const string _recurse = "recurse";
      private const string _privacy = "privacy";
      private const string _password = "password";
      #endregion
   }
}