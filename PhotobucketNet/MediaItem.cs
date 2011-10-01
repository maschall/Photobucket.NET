using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace PhotobucketNet
{
   #region MediaItem
   public class MediaItem
   {
      #region paramaters
      protected string _descriptionId = string.Empty;
      public string DescriptionId
      {
         get { return _descriptionId; }
         set { _descriptionId = value; }
      }

      protected string _name = string.Empty;
      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }

      protected string _username = string.Empty;
      public string Username
      {
         get { return _username; }
      }

      protected MediaType _mediaType = MediaType.None;
      public MediaType MediaType { get { return _mediaType; } }

      public string Type
      {
         get 
         {
            if (_mediaType == MediaType.Image)
            {
               return _image;
            }
            else if (_mediaType == MediaType.Video)
            {
               return _video;
            }
            else
            {
               throw new PhotobucketApiException( "unknow mediatype" );
            }
         }
         set
         {
            if (value == _image)
            {
               _mediaType = MediaType.Image;
            }
            else if (value == _video)
            {
               _mediaType = MediaType.Video;
            }
            else
            {
               throw new PhotobucketApiException("unknow mediatype");
            }
         }
      }

      protected bool _isPublic = true;
      public bool IsPublic
      {
         get { return _isPublic; }
      }

      protected string _browserUrl = string.Empty;
      public string BrowserUrl
      {
         get { return _browserUrl; }
      }

      protected string _url = string.Empty;
      public string Url
      {
         get { return _url; }
         set { _url = value; }
      }

      protected string _thumbUrl = string.Empty;
      public string ThumbUrl
      {
         get { return _thumbUrl; }
      }

      protected DateTime _uploadDate;
      public DateTime UploadDate
      {
         get { return _uploadDate; }
      }

      protected string _title = string.Empty;
      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }

      protected string _description = string.Empty;
      public string Description
      {
         get { return _description; }
         set { _description = value; }
      }

      private Stream _mediaStream = null;
      public Stream MediaStream
      {
         get { return _mediaStream; }
         set { _mediaStream = value; }
      }

      private MediaTagList _mediaTags = new MediaTagList();
      public MediaTagList MediaTags
      {
         get { return _mediaTags; }
      }
      internal MediaTagList SetMediaTags
      {
         set { _mediaTags = value; }
      }

      private MetaData _metaData = null;
      public MetaData MetaData
      {
         get { return _metaData; }
      }
      internal MetaData SetMetaData
      {
         set { _metaData = value; }
      }
      #endregion

      private MediaItem(string username, MediaType type)
      {
         _username = username;
         _mediaType = type;
      }

      private MediaItem(MediaType type)
      {
         _mediaType = type;
      }

      public MediaItem(string username, string name, Stream mediaStream, MediaType type)
      {
         _username = username;
         _name = name;
         _mediaStream = mediaStream;
         _mediaType = type;
      }

      internal MediaItem(XmlDocument mediaXml)
      {
         XmlNode contentNode = mediaXml.SelectSingleNode( "descendant::content" );

         ParseFromXmlNode( contentNode );
      }

      public static MediaItem CreateFromUrl(string url, MediaType type)
      {
         string name = url.Substring(url.LastIndexOf('/') + 1);

         int findAlbums = url.IndexOf("/albums/");
         if (findAlbums == -1)
         {
            throw new PhotobucketApiException("media item url does not have have albums in it");
         }

         string rightOfAlbums = url.Substring(findAlbums + 8);

         int beginingOfUsername = rightOfAlbums.IndexOf('/') + 1;
         int endOfUsername = rightOfAlbums.IndexOf('/', beginingOfUsername);

         string username = url.Substring(beginingOfUsername, endOfUsername - beginingOfUsername);

         MediaItem mediaItem = new MediaItem(username, name, null, type);

         mediaItem._url = url;
         mediaItem._browserUrl = url;

         return mediaItem;
      }

      public static MediaItem CreateFromFile(string username, string filepath, MediaType type)
      {
         FileStream stream = new FileStream(filepath, FileMode.Open);

         string name;
         if (filepath.LastIndexOf( '/' ) == -1)
         {
            name = filepath.Substring( filepath.LastIndexOf( '\\' ) + 1 );
         }
         else
         {
            name = filepath.Substring( filepath.LastIndexOf( '/' ) + 1 );
         }

         return new MediaItem(username, name, stream, type);
      }

      internal static MediaItem CreateFromXmlNode(MediaType type, XmlNode mediaNode)
      {
         MediaItem mediaItem = new MediaItem(type);
         
         mediaItem.ParseFromXmlNode( mediaNode );

         return mediaItem;
      }

      internal static MediaItem CreateFromXmlNode(XmlNode mediaNode)
      {
         return CreateFromXmlNode(MediaType.None, mediaNode);
      }

      protected void ParseFromXmlNode(XmlNode mediaNode)
      {
         XmlAttributeCollection attributes = mediaNode.Attributes;

         foreach (XmlAttribute attribute in attributes)
         {
            if (attribute.Name == _nameXml)
            {
               _name = attribute.Value;
            }
            else if (attribute.Name == _description_id)
            {
               _descriptionId = attribute.Value;
            }
            else if (attribute.Name == _public)
            {
               if (attribute.Value == "0")
               {
                  _isPublic = false;
               }
            }
            else if (attribute.Name == _typeXml)
            {
               if (attribute.Value == _video)
               {
                  _mediaType = MediaType.Video;
               }
               else if (attribute.Value == _image)
               {
                  _mediaType = MediaType.Image;
               }
            }
            else if (attribute.Name == _uploaddate)
            {
               if (attribute.Value == string.Empty) break;
               double totalSeconds = Convert.ToDouble( attribute.Value );
               int hours = Convert.ToInt32(totalSeconds/3600);
               int mins = Convert.ToInt32((totalSeconds % 3600)/60);
               int secs = Convert.ToInt32((totalSeconds % 3600) % 60);
               TimeSpan ts = new TimeSpan(hours,mins,secs);

               _uploadDate = new DateTime( 1970, 1, 1, 0, 0, 0, 0 ).Add(ts);
            }
            else if (attribute.Name == _usernameXml)
            {
               _username = attribute.Value;
            }
         }

         XmlNode browserUrlNode = mediaNode.SelectSingleNode( "descendant::browseurl" );
         _browserUrl = browserUrlNode.InnerText;

         XmlNode urlNode = mediaNode.SelectSingleNode( "descendant::url" );
         _url = urlNode.InnerText;

         XmlNode thumbNode = mediaNode.SelectSingleNode( "descendant::thumb" );
         _thumbUrl = thumbNode.InnerText;

         XmlNode titleNode = mediaNode.SelectSingleNode( "descendant::title" );
         _title = titleNode.InnerText;

         XmlNode descriptionNode = mediaNode.SelectSingleNode( "descendant::description" );
         _description = descriptionNode.InnerText;
      }

      #region constants
      private const string _usernameXml = "username";
      private const string _uploaddate = "uploaddate";
      private const string _image = "image";
      private const string _video = "video";
      private const string _typeXml = "type";
      private const string _public = "public";
      private const string _description_id = "description_id";
      private const string _nameXml = "name";
      #endregion

      #region overrides
      public override bool Equals(object obj)
      {
         MediaItem mediaItem = obj as MediaItem;
         return Name.Equals( mediaItem.Name ) && Type.Equals( mediaItem.Type ) && Username.Equals( mediaItem.Username );
      }
      public override int GetHashCode()
      {
         return base.GetHashCode( );
      }
      #endregion
   }
   #endregion

   public enum MediaType
   {
      Image,
      Video,
      None,
      All
   }

   #region ImageMediaItem
   public class ImageMediaItem : MediaItem
   {
      public ImageMediaItem(string username, string name, Stream mediaStream)
         : base(username, name, mediaStream, MediaType.Image)
      {

      }

      public static MediaItem CreateFromFile(string username, string filepath)
      {
         return MediaItem.CreateFromFile(username, filepath, MediaType.Image);
      }

      public static MediaItem CreateFromUrl(string url)
      {
         return MediaItem.CreateFromUrl(url, MediaType.Image);
      }
   }
   #endregion

   #region VideoMediaItem
   public class VideoMediaItem : MediaItem
   {
      public VideoMediaItem(string username, string name, Stream mediaStream)
         : base(username, name, mediaStream, MediaType.Video)
      {

      }

      public static MediaItem CreateFromFile(string username, string filepath)
      {
         return MediaItem.CreateFromFile(username, filepath, MediaType.Video);
      }

      public static MediaItem CreateFromUrl(string url)
      {
         return MediaItem.CreateFromUrl(url, MediaType.Video);
      }
   }
   #endregion

   #region MediaItemList
   public class MediaItemList: List<MediaItem>
   {
      internal static MediaItemList CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         return CreateFromXmlDocument(responseMessage.ResponseXml);
      }

      internal static MediaItemList CreateFromXmlDocument(XmlDocument document)
      {
         return CreateFromXmlNodeList(document.SelectNodes("descendant::media"));
      }

      internal static MediaItemList CreateFromXmlNodeList(XmlNodeList mediaNodeList)
      {
         MediaItemList mediaList = new MediaItemList();

         foreach (XmlNode mediaNode in mediaNodeList)
         {
            mediaList.Add(MediaItem.CreateFromXmlNode(mediaNode));
         }

         return mediaList;
      }

      public MediaItem Find(MediaItem mediaItem)
      {
         foreach (MediaItem currentMediaItem in this)
         {
            if (currentMediaItem.Name == mediaItem.Name)
            {
               return currentMediaItem;
            }
         }
         throw new PhotobucketApiException(ErrorCode.CouldNotGet, "Could not find in mediaList");
      }
   }
   #endregion
}
