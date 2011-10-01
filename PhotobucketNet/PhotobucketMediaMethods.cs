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
      #region Get Media Information
      public MediaItem GetMediaInformation(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaInformationUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaInformationUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaInformationMethod, Token, paramaters);

         XmlResponseMessage getMediaInformationResponseMessage = GetXmlResponseMessageFromUrl(getMediaInformationUrl, _getMediaInformationMethod);

         mediaItem = MediaItem.CreateFromXmlNode(mediaItem.MediaType, getMediaInformationResponseMessage.ResponseXml.SelectSingleNode("descendant::media"));
         return mediaItem;
      }

      public MediaItem GetMediaInformation(MediaItem mediaItem)
      {
         return GetMediaInformation( ref mediaItem );
      }
      #endregion

      #region Get Media Description
      public string GetMediaDescription(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaDescriptionUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaDescriptionUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaDescriptionMethod, Token, paramaters);

         XmlResponseMessage getMediaDescriptionResponseMessage = GetXmlResponseMessageFromUrl(getMediaDescriptionUrl, _getMediaDescriptionMethod);

         XmlNode descriptionNode = getMediaDescriptionResponseMessage.ResponseXml.SelectSingleNode("descendant::description");
         mediaItem.Description = descriptionNode.InnerText;
         
         return mediaItem.Description;
      }

      public string GetMediaDescription(MediaItem mediaItem)
      {
         return GetMediaDescription( ref mediaItem );
      }
      #endregion

      #region Set Media Description
      public void SetMediaDescription(ref MediaItem mediaItem, string description)
      {
         string relativePath = GenerateRelativeMediaDescriptionUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_description, description));

         string setMediaDescriptionUrl = OAuth.GenerateURL(ApiUrl, relativePath, _setMediaDescriptionMethod, Token, paramaters);

         XmlResponseMessage setMediaDescriptionResponseMessage = GetXmlResponseMessageFromUrl(setMediaDescriptionUrl, _setMediaDescriptionMethod);

         mediaItem.Description = description;
      }

      public void SetMediaDescription(MediaItem mediaItem, string description)
      {
         SetMediaDescription( ref mediaItem, description );
      }
      #endregion

      #region Delete Media Description
      public void DeleteMediaDescription(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaDescriptionUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string deleteMediaDescriptionUrl = OAuth.GenerateURL(ApiUrl, relativePath, _deleteMediaDescriptionMethod, Token, paramaters);

         XmlResponseMessage deleteMediaDescriptionResponseMessage = GetXmlResponseMessageFromUrl(deleteMediaDescriptionUrl, _deleteMediaDescriptionMethod);
         
         mediaItem.Description = String.Empty;
      }

      public void DeleteMediaDescription(MediaItem mediaItem)
      {
         DeleteMediaDescription( ref mediaItem );
      }
      #endregion

      #region Get Media Title
      public string GetMediaTitle(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaTitleUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaTitleUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaTitleMethod, Token, paramaters);

         XmlResponseMessage getMediaTitleResponseMessage = GetXmlResponseMessageFromUrl(getMediaTitleUrl, _getMediaTitleMethod);

         XmlNode titleNode = getMediaTitleResponseMessage.ResponseXml.SelectSingleNode("descendant::title");
         mediaItem.Title = titleNode.InnerText;

         return mediaItem.Title;
      }

      public string GetMediaTitle(MediaItem mediaItem)
      {
         return GetMediaTitle( ref mediaItem );
      }
      #endregion

      #region Set Media Title
      public void SetMediaTitle(ref MediaItem mediaItem, string title)
      {
         string relativePath = GenerateRelativeMediaTitleUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_title, title));

         string setMediaTitleUrl = OAuth.GenerateURL(ApiUrl, relativePath, _setMediaTitleMethod, Token, paramaters);

         XmlResponseMessage setMediaTitleResponseMessage = GetXmlResponseMessageFromUrl(setMediaTitleUrl, _setMediaTitleMethod);

         mediaItem.Title = title;
      }

      public void SetMediaTitle(MediaItem mediaItem, string title)
      {
         SetMediaTitle( ref mediaItem, title );
      }
      #endregion

      #region Delete Media Title
      public void DeleteMediaTitle(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaTitleUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string deleteMediaTitleUrl = OAuth.GenerateURL(ApiUrl, relativePath, _deleteMediaTitleMethod, Token, paramaters);

         XmlResponseMessage deleteMediaTitleResponseMessage = GetXmlResponseMessageFromUrl(deleteMediaTitleUrl, _deleteMediaTitleMethod);

         mediaItem.Title = String.Empty;
      }

      public void DeleteMediaTitle(MediaItem mediaItem)
      {
         DeleteMediaTitle( ref mediaItem );
      }
      #endregion

      #region Get Media Tag
      public MediaTag GetMediaTag(MediaItem mediaItem, int tagId)
      {
         string relativePath = GenerateRelativeMediaTagUrl(mediaItem.Url);
         relativePath = relativePath + "/" + tagId.ToString();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaTagUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaTagMethod, Token, paramaters);

         XmlResponseMessage getMediaTagResponseMessage = GetXmlResponseMessageFromUrl(getMediaTagUrl, _getMediaTagMethod);

         return MediaTag.CreateFromXmlNode(getMediaTagResponseMessage.ResponseXml.SelectSingleNode("descendant::content")); 
      }

      public MediaTagList GetMediaTags(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaTagUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaTagUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaTagMethod, Token, paramaters);

         XmlResponseMessage getMediaTagResponseMessage = GetXmlResponseMessageFromUrl(getMediaTagUrl, _getMediaTagMethod);

         mediaItem.SetMediaTags = MediaTagList.CreateFromXmlResponseMessage(getMediaTagResponseMessage);
         return mediaItem.MediaTags;
      }

      public MediaTagList GetMediaTags(MediaItem mediaItem)
      {
         return GetMediaTags( ref mediaItem );
      }
      #endregion

      #region Add Media Tag
      public void AddMediaTag(MediaItem mediaItem, MediaTag tag)
      {
         string relativePath = GenerateRelativeMediaTagUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter("topleftx", tag.TopLeftX.ToString()));
         paramaters.Add(new QueryParameter("toplefty", tag.TopLeftY.ToString()));
         paramaters.Add(new QueryParameter("bottomrightx", tag.BottomRightX.ToString()));
         paramaters.Add(new QueryParameter("bottomrighty", tag.BottomRightY.ToString()));
         paramaters.Add(new QueryParameter("tag", tag.Name));

         if (string.IsNullOrEmpty(tag.Contact) == false)
         {
            paramaters.Add(new QueryParameter("contact", tag.Contact));
         }
         if (string.IsNullOrEmpty(tag.Url) == false)
         {
            paramaters.Add(new QueryParameter("tagurl", tag.Url));
         }

         string addMediaTagUrl = OAuth.GenerateURL(ApiUrl, relativePath, _addMediaTagMethod, Token, paramaters);

         XmlResponseMessage addMediaTagResponseMessage = GetXmlResponseMessageFromUrl(addMediaTagUrl, _addMediaTagMethod);
      }
      #endregion

      #region Update Media Tag
      public void UpdateMediaTag(MediaItem mediaItem, MediaTag tag)
      {
         string relativePath = GenerateRelativeMediaTagUrl(mediaItem.Url);
         relativePath = relativePath + "/" + tag.TagId.ToString();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         if (tag.TopLeftX == -1)
         {
            paramaters.Add(new QueryParameter("topleftx", tag.TopLeftX.ToString()));
         }
         if (tag.TopLeftY == -1)
         {
            paramaters.Add(new QueryParameter("toplefty", tag.TopLeftY.ToString()));
         }
         if (tag.BottomRightX == -1)
         {
            paramaters.Add(new QueryParameter("bottomrightx", tag.BottomRightX.ToString()));
         }
         if (tag.BottomRightY == -1)
         {
            paramaters.Add(new QueryParameter("bottomrighty", tag.BottomRightY.ToString()));
         }
         if (string.IsNullOrEmpty(tag.Name) == false)
         {
            paramaters.Add(new QueryParameter("tag", tag.Name));
         }
         if (string.IsNullOrEmpty(tag.Contact) == false)
         {
            paramaters.Add(new QueryParameter("contact", tag.Contact));
         }
         if (string.IsNullOrEmpty(tag.Url) == false)
         {
            paramaters.Add(new QueryParameter("tagurl", tag.Url));
         }

         string updateMediaTagUrl = OAuth.GenerateURL(ApiUrl, relativePath, _updateMediaTagMethod, Token, paramaters);

         XmlResponseMessage updateMediaTagResponseMessage = GetXmlResponseMessageFromUrl(updateMediaTagUrl, _updateMediaTagMethod);
      }
      #endregion

      #region Delete Media Tag
      public void DeleteMediaTag(MediaItem mediaItem, MediaTag tag)
      {
         string relativePath = GenerateRelativeMediaTagUrl(mediaItem.Url);
         relativePath = relativePath + "/" + tag.TagId.ToString();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string deleteMediaTagUrl = OAuth.GenerateURL(ApiUrl, relativePath, _deleteMediaTagMethod, Token, paramaters);

         XmlResponseMessage deleteMediaTagResponseMessage = GetXmlResponseMessageFromUrl(deleteMediaTagUrl, _deleteMediaTagMethod);
      }
      #endregion

      #region Resize Image
      public void ResizeImage(MediaItem mediaItem, ImageSize newSize)
      {
         string relativePath = GenerateRelativeResizeImageUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string resizeImageUrl = OAuth.GenerateURL(ApiUrl, relativePath, _resizeImageMethod, Token, paramaters);

         XmlResponseMessage resizeImageResponseMessage = GetXmlResponseMessageFromUrl(resizeImageUrl, _resizeImageMethod);
      }
      #endregion

      #region Rotate Image
      public void RotateImage(MediaItem mediaItem, int degrees)
      {
         string relativePath = GenerateRelativeRotateImageUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string rotateImageUrl = OAuth.GenerateURL(ApiUrl, relativePath, _rotateImageMethod, Token, paramaters);

         XmlResponseMessage rotateImageResponseMessage = GetXmlResponseMessageFromUrl(rotateImageUrl, _rotateImageMethod);
      }
      #endregion

      #region Get Media Meta Data
      public MetaData GetMediaMetaData(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaMetaDataUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getMediaMetaDataUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getMediaMetaDataMethod, Token, paramaters);

         XmlResponseMessage getMediaMetaDataResponseMessage = GetXmlResponseMessageFromUrl(getMediaMetaDataUrl, _getMediaMetaDataMethod);

         mediaItem.SetMetaData = MetaData.CreateFromXmlResponseMessage(getMediaMetaDataResponseMessage);
         return mediaItem.MetaData;
      }

      public MetaData GetMediaMetaData(MediaItem mediaItem)
      {
         return GetMediaMetaData( ref mediaItem );
      }
      #endregion

      #region Get Media Links
      private XmlDocument GetMediaLinks(MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaLinksUrl( mediaItem.Url );

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add( new QueryParameter( _format, "xml" ) );

         string getMediaLinksUrl = OAuth.GenerateURL( ApiUrl, relativePath, _getMediaLinksMethod, Token, paramaters );

         XmlResponseMessage getMediaLinksResponseMessage = GetXmlResponseMessageFromUrl( getMediaLinksUrl, _getMediaLinksMethod );

         return getMediaLinksResponseMessage.ResponseXml;
      }

      public string GetMediaShareTagLink(MediaItem mediaItem)
      {
         XmlDocument document = GetMediaLinks( mediaItem );

         XmlNode shareNode = document.SelectSingleNode("descendant::sharetag");

         string shareTag = shareNode.InnerText;

         shareTag = shareTag.Replace( "&lt;", "<" );
         shareTag = shareTag.Replace( "&gt;", ">" );
         shareTag = shareTag.Replace( "&quot;", "\"" );
         shareTag = shareTag.Replace( "&amp;", "&" );

         return shareTag;
      }
      #endregion

      #region Get Related Media
      public MediaItemList GetRelatedMedia(MediaItem mediaItem, int num, MediaType mediaType)
      {
         string relativePath = GenerateRelativeRelatedMediaUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_num, num.ToString()));

         string type = mediaType == MediaType.Image ? "images" : "videos";
         paramaters.Add(new QueryParameter(_type, type));

         string getRelatedMediaUrl = OAuth.GenerateURL(ApiUrl, relativePath, _getRelatedMediaMethod, Token, paramaters);

         XmlResponseMessage getRelatedMediaResponseMessage = GetXmlResponseMessageFromUrl(getRelatedMediaUrl, _getRelatedMediaMethod);

         return MediaItemList.CreateFromXmlNodeList(getRelatedMediaResponseMessage.ResponseXml.SelectNodes("descendant::media"));
      }
      #endregion

      #region Delete Media
      public void DeleteMedia(ref MediaItem mediaItem)
      {
         string relativePath = GenerateRelativeMediaInformationUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string deleteMediaUrl = OAuth.GenerateURL( ApiUrl, relativePath, _deleteMediaMethod, Token, paramaters );

         XmlResponseMessage deleteMediaResponseMessage = GetXmlResponseMessageFromUrl(deleteMediaUrl, _deleteMediaMethod);

         mediaItem = null;
      }

      public void DeleteMedia(MediaItem mediaItem)
      {
         DeleteMedia( ref mediaItem );
      }
      #endregion

      #region Share Media via Email
      public void ShareMediaViaEmail(MediaItem mediaItem, string email, string body)
      {
         string relativePath = GenerateRelativeShareUrl(mediaItem.Url);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter("body", body));
         paramaters.Add(new QueryParameter("email", email));

         string shareMediaViaEmailUrl = OAuth.GenerateURL(ApiUrl, relativePath, _shareMediaViaEmailMethod, Token, paramaters);

         XmlResponseMessage shareMediaViaEmailResponseMessage = GetXmlResponseMessageFromUrl(shareMediaViaEmailUrl, _shareMediaViaEmailMethod);
      }

      #endregion

      #region Generate Url
      private string GenerateRelativeMediaTitleUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + _title;
      }

      private string GenerateRelativeMediaDescriptionUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + _description;
      }

      private string GenerateRelativeMediaTagUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + _tag;
      }

      private string GenerateRelativeMediaInformationUrl(string url)
      {
         return GenerateRelativeMediaUrl(url);
      }

      private string GenerateRelativeResizeImageUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + _size;
      }

      private string GenerateRelativeRotateImageUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + _degrees;
      }

      private string GenerateRelativeMediaMetaDataUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + "meta";
      }

      private string GenerateRelativeMediaLinksUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + "link";
      }

      private string GenerateRelativeRelatedMediaUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + "related";
      }

      private string GenerateRelativeShareUrl(string url)
      {
         return GenerateRelativeMediaUrl(url) + "/" + "share";
      }

      private string GenerateRelativeMediaUrl(string url)
      {
         return _media + "/" + OAuth.UrlEncode(OAuth.UrlEncode(url));
      }
      #endregion

      #region Genereate Media Item
      /// <summary>
      /// Note must be logged in to generate media item
      /// Should not use this method if not logged in
      /// </summary>
      /// <param name="filename"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public MediaItem CreateCurrentUsersMediaItemFromFile(string filename, MediaType type)
      {
         return MediaItem.CreateFromFile(Username, filename, type);
      }

      /// <summary>
      /// Note must be logged in to generate media item
      /// Should not use this method if not logged in
      /// </summary>
      /// <param name="filename"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public MediaItem CreateCurrentUsersImageMediaItemFromFile(string filename)
      {
         return ImageMediaItem.CreateFromFile(Username, filename);
      }

      /// <summary>
      /// Note must be logged in to generate media item
      /// Should not use this method if not logged in
      /// </summary>
      /// <param name="filename"></param>
      /// <param name="type"></param>
      /// <returns></returns>
      public MediaItem CreateCurrentUsersVideoMediaItemFromFile(string filename)
      {
         return VideoMediaItem.CreateFromFile(Username, filename);
      }
      #endregion

      #region constants
      private const string _getMediaInformationMethod = "GET";
      private const string _getMediaDescriptionMethod = "GET";
      private const string _setMediaDescriptionMethod = "POST";
      private const string _deleteMediaDescriptionMethod = "DELETE";
      private const string _getMediaTitleMethod = "GET";
      private const string _setMediaTitleMethod = "POST";
      private const string _deleteMediaTitleMethod = "DELETE";
      private const string _getMediaTagMethod = "GET";
      private const string _addMediaTagMethod = "POST";
      private const string _updateMediaTagMethod = "PUT";
      private const string _deleteMediaTagMethod = "DELETE";
      private const string _resizeImageMethod = "PUT";
      private const string _rotateImageMethod = "PUT";
      private const string _getMediaMetaDataMethod = "GET";
      private const string _getMediaLinksMethod = "GET";
      private const string _getRelatedMediaMethod = "GET";
      private const string _deleteMediaMethod = "DELETE";
      private const string _shareMediaViaEmailMethod = "POST";
      #endregion
   }
}