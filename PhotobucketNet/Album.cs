using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace PhotobucketNet
{
   public enum Privacy
   {
      PUBLIC,
      PRIVATE
   }

   #region Album
   public class Album
   {
      private Privacy _privacy;
      public Privacy Privacy
      {
         get { return _privacy; }
         set { _privacy = value; }
      }

      private string _name;
      public string Name
      {
         get { return _name; }
         set 
         {
            _path.Replace( _name, value );
            _name = value; 
         }
      }

      private string _path;
      public string Path
      {
         get { return _path; }
         set
         {
            _path = value;
            int index = value.LastIndexOf('/');
            if (index == -1)
            {
               _name = value;
            }
            else
            {
               _name = value.Substring( index + 1 );
            }
         }
      }

      private int _photoCount;
      public int PhotoCount { get { return _photoCount; } }

      private int _videoCount;
      public int VideoCount { get { return _videoCount; } }

      private int _subAlbumCount;
      public int SubAlbumCount { get { return _subAlbumCount; } }

      private string _username;
      public string Username { get { return _username; } }

      private MediaItemList _mediaList = new MediaItemList();
      public MediaItemList MediaList
      {
         get { return _mediaList; }
      }

      private AlbumList _subAlbums = new AlbumList();
      public AlbumList SubAlbums
      {
         get { return _subAlbums; }
      }

      internal Album()
      {

      }

      public Album(string name, string parentPath)
      {
         Path = parentPath + "/" + name;
      }

      public Album(string albumPath)
      {
         Path = albumPath;
      }
      
      internal Album(XmlNode albumNode)
      {
         ParseFromXmlNode(albumNode);

         XmlNodeList subAlbumNodes = albumNode.SelectNodes("descendant::album");

         foreach (XmlNode subAlbumNode in subAlbumNodes)
         {
            _subAlbums.Add(Album.CreateFromXmlNode(subAlbumNode));
         }

         XmlNodeList mediaNodeList = albumNode.SelectNodes("descendant::media");

         foreach (XmlNode mediaNode in mediaNodeList)
         {
            _mediaList.Add(MediaItem.CreateFromXmlNode(mediaNode));
         }
      }

      internal static Album CreateFromXmlNode(XmlNode albumNode)
      {
         return new Album(albumNode);
      }

      private void ParseFromXmlNode(XmlNode albumNode)
      {
         XmlAttributeCollection attributes = albumNode.Attributes;

         foreach (XmlAttribute attribute in attributes)
         {
            if (attribute.Name == "name")
            {
               Path = attribute.Value;
            }
            else if (attribute.Name == "photo_count")
            {
               _photoCount = Convert.ToInt32(attribute.Value);
            }
            else if (attribute.Name == "subalbum_count")
            {
               _subAlbumCount = Convert.ToInt32(attribute.Value);
            }
            else if (attribute.Name == "video_count")
            {
               _videoCount = Convert.ToInt32(attribute.Value);
            }
            else if (attribute.Name == "username")
            {
               _username = attribute.Value;
            }
         }
      }

      #region overrides
      public override bool Equals(object obj)
      {
         Album album = obj as Album;
         return Username.Equals(album.Username) && Path.Equals(album.Path);
      }
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
      #endregion
   }
   #endregion

   #region AlbumList
   public class AlbumList : List<Album>
   {
      internal static AlbumList CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         return CreateFromXmlDocument(responseMessage.ResponseXml);
      }

      internal static AlbumList CreateFromXmlDocument(XmlDocument document)
      {
         return CreateFromXmlNodeList(document.SelectNodes("descendant::media"));
      }

      internal static AlbumList CreateFromXmlNodeList(XmlNodeList albumNodeList)
      {
         AlbumList albumList = new AlbumList();

         foreach (XmlNode albumNode in albumNodeList)
         {
            albumList.Add(Album.CreateFromXmlNode(albumNode));
         }

         return albumList;
      }
   }
   #endregion
}
