using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace PhotobucketNet
{
   public class User
   {
      public User(string username)
      {
         _username = username;
      }

      internal User(XmlDocument userXml)
      {
         XmlNode contentNode = userXml.SelectSingleNode("descendant::content");

         ParseFromXmlNode(contentNode);
      }

      internal static User CreateFromXmlDocument(XmlNode userNode)
      {
         User user = new User("username");

         user.ParseFromXmlNode(userNode);

         return user;
      }

      private void ParseFromXmlNode(XmlNode userNode)
      {
         XmlNode usernameNode = userNode.SelectSingleNode(_usernameXml);
         _username = usernameNode.InnerText;

         XmlNode albumUrlNode = userNode.SelectSingleNode(_albumUrlXml);
         _albumUrl = albumUrlNode.InnerText;

         XmlNode megabytesUsedNode = userNode.SelectSingleNode(_megabytesUsedXml);
         _megabytesUsed = Convert.ToInt32(megabytesUsedNode.InnerText);

         XmlNode megabytesAllowedNode = userNode.SelectSingleNode(_megabytesAllowedXml);
         _megabytesAllowed = Convert.ToInt32(megabytesAllowedNode.InnerText);

         XmlNode premiumNode = userNode.SelectSingleNode(_premiumXml);
         _premium = Convert.ToBoolean(Convert.ToInt32(premiumNode.InnerText));

         XmlNode publicAccessNode = userNode.SelectSingleNode(_publicAccessXml);
         _publicAccess = Convert.ToBoolean(Convert.ToInt32(publicAccessNode.InnerText));

         XmlNode mediaCountNode = userNode.SelectSingleNode(_mediaCountXml);
         _mediaCount = Convert.ToInt32(mediaCountNode.InnerText);

         XmlNode preferredPictureSizeNode = userNode.SelectSingleNode(_preferredPictureSizeXml);
         _preferredPictureSize = PhotobucketImageSize.CreateFromString(preferredPictureSizeNode.InnerText);
      }

      #region MediaList
      private MediaItemList _mediaList = new MediaItemList();
      public MediaItemList MediaList { get { return _mediaList; } }
      internal MediaItemList SetMediaList { set { _mediaList = value; } }

      private MediaItemList _imageMediaList = new MediaItemList();
      public MediaItemList ImageMediaList { get { return _imageMediaList; } }
      internal MediaItemList SetImageMediaList { set { _imageMediaList = value; } }

      private MediaItemList _videoMediaList = new MediaItemList();
      public MediaItemList VideoMediaList { get { return _videoMediaList; } }
      internal MediaItemList SetVideoMediaList { set { _videoMediaList = value; } }

      #endregion

      #region MediaTags
      private MediaTagList _mediaTags = new MediaTagList();
      public MediaTagList MediaTags { get { return _mediaTags; } }
      internal MediaTagList SetMediaTags { set { _mediaTags = value; } }
      #endregion

      #region ContactList
      private UserContactList _contacts = new UserContactList();
      public UserContactList Contacts { get { return _contacts; } }
      internal UserContactList SetContact { set { _contacts = value; } }
      #endregion
      
      #region Album
      private Album _album;
      public Album Album
      {
         get { return _album; }
      }
      internal Album SetAlbum
      {
         set { _album = value; }
      }
      #endregion

      #region UploadOptions
      private UploadOptions _uploadOptions = null;
      public UploadOptions UploadOptions { get { return _uploadOptions; } }
      internal UploadOptions SetUploadOptions { set { _uploadOptions = value; } }
      #endregion

      #region Urls
      private UserUrls _userUrls = null;
      public UserUrls UserUrls { get { return _userUrls; } }
      internal UserUrls SetUserUrls { set { _userUrls = value; } }
      #endregion

      #region Paramaters

      private string _username = String.Empty;
      public string Username { get { return _username; } }

      private string _albumUrl = String.Empty;
      public string AlbumUrl { get { return _albumUrl; } }

      private bool _premium = false;
      public bool Premium { get { return _premium; } }

      private bool _publicAccess = true;
      public bool PublicAccess { get { return _publicAccess; } }

      private int _mediaCount = -1;
      public int MediaCount { get { return _mediaCount; } }

      private ImageSize _preferredPictureSize = ImageSize.S100x75;
      public ImageSize PreferredPictureSize { get { return _preferredPictureSize; } }

      private int _megabytesUsed = -1;
      public int MegabytesUsed { get { return _megabytesUsed; } }

      private int _megabytesAllowed = -1;
      public int MegabytesAllowed { get { return _megabytesAllowed; } }

      #endregion

      #region Constants
      private const string _usernameXml = "descendant::username";
      private const string _albumUrlXml = "descendant::album_url";
      private const string _premiumXml = "descendant::premium";
      private const string _publicAccessXml = "descendant::public";
      private const string _mediaCountXml = "descendant::total_pictures";
      private const string _preferredPictureSizeXml = "descendant::preferred_picture_size";
      private const string _megabytesUsedXml = "descendant::megabytes_used";
      private const string _megabytesAllowedXml = "descendant::megabytes_allowed";
      #endregion

      #region overides
      public override bool Equals(object obj)
      {
         User user = obj as User;
         return Username.Equals( user.Username );
      }
      public override int GetHashCode()
      {
         return base.GetHashCode( );
      }
      #endregion
   }

   public enum ImageSize
   {
      S100x75 = 100,
      S160x120 = 160,
      S320x240 = 320,
      S640x480 = 640,
      S800x600 = 800,
      S1024x768 = 1024,
      S1280x960 = 1280,
      S1600x1200 = 1600,
      S1048x1536 = 2048,
      S2240x1680 = 2240,
      UNKNOWN
   }

   internal class PhotobucketImageSize
   {

      public static ImageSize CreateFromString(string imageSize)
      {
         if (imageSize == "100")
         {
           return ImageSize.S100x75;
         }
         else if (imageSize == "160")
         {
            return ImageSize.S160x120;
         }
         else if (imageSize == "320")
         {
            return ImageSize.S320x240;
         }
         else if (imageSize == "640")
         {
            return ImageSize.S640x480;
         }
         else if (imageSize == "800")
         {
            return ImageSize.S800x600;
         }
         else if (imageSize == "1024")
         {
            return ImageSize.S1024x768;
         }
         else if (imageSize == "1280")
         {
            return ImageSize.S1280x960;
         }
         else if (imageSize == "1600")
         {
            return ImageSize.S1600x1200;
         }
         else if (imageSize == "2048")
         {
            return ImageSize.S1048x1536;
         }
         else if (imageSize == "2240")
         {
            return ImageSize.S2240x1680;
         }

         return ImageSize.UNKNOWN;
      }
   }

   public class UploadOptions
   {
      private ImageSize _defaultImageSize;
      public ImageSize DefaultImageSize { get { return _defaultImageSize; } }
      internal ImageSize SetDefaultImageSize { set { _defaultImageSize = value; } }

      private bool _autoTagging;
      public bool AutoTagging { get { return _autoTagging; } }
      internal bool SetAutoTagging { set { _autoTagging = value; } }

      internal static UploadOptions CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         UploadOptions userUploadOptions = new UploadOptions();
         XmlNode uploadOptionsNode = responseMessage.ResponseXml.SelectSingleNode("descendant::content");

         XmlNode defaultImageSizeNode = uploadOptionsNode.SelectSingleNode("descendant::defaultimagesize");
         userUploadOptions.SetDefaultImageSize = PhotobucketImageSize.CreateFromString(defaultImageSizeNode.InnerText);

         XmlNode autoTaggingNode = uploadOptionsNode.SelectSingleNode("descendant::autotagging");
         userUploadOptions._autoTagging = Boolean.Parse(autoTaggingNode.InnerText);

         return userUploadOptions;
      }
   }

   public class UserUrls
   {
      private UserUrls()
      {

      }

      internal static UserUrls CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         UserUrls userUrl = new UserUrls();

         XmlNode subdomainNode = responseMessage.ResponseXml.SelectSingleNode("descendant::subdomain");

         XmlNode albumNode = subdomainNode.SelectSingleNode("descendant::album");
         userUrl._album = albumNode.InnerText;

         XmlNode imageNode = subdomainNode.SelectSingleNode("descendant::image");
         userUrl._image = imageNode.InnerText;

         XmlNode apiNode = subdomainNode.SelectSingleNode("descendant::api");
         userUrl._api = apiNode.InnerText;

         XmlNode feedNode = subdomainNode.SelectSingleNode("descendant::feed");
         userUrl._feed = feedNode.InnerText;

         XmlNode pathNode = responseMessage.ResponseXml.SelectSingleNode("descendant::path");
         userUrl._path = pathNode.InnerText;

         return userUrl;
      }

      private string _album;
      public string Album { get { return _album; } }

      private string _image;
      public string Image { get { return _image; } }

      private string _api;
      public string Api { get { return _api; } }

      private string _feed;
      public string Feed { get { return _feed; } }

      private string _path;
      public string Path { get { return _path; } }
   }
}
