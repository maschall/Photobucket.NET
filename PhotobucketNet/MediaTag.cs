using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PhotobucketNet
{
   public class MediaTag
   {
      internal static MediaTag CreateFromXmlNode(XmlNode mediaTagNode)
      {
         MediaTag mediaTag = new MediaTag();

         XmlNode nameNode = mediaTagNode.SelectSingleNode("descendant::name");
         mediaTag._name = nameNode == null ? String.Empty : nameNode.InnerText;

         XmlNode countNode = mediaTagNode.SelectSingleNode("descendant::count");
         mediaTag._count = countNode == null ? -1 : Convert.ToInt32(countNode.InnerText);

         XmlNode tagNode = mediaTagNode.SelectSingleNode("descendant::tag");
         mediaTag._name = tagNode == null ? String.Empty : tagNode.InnerText;

         XmlNode tagIdNode = mediaTagNode.SelectSingleNode("descendant::tagid");
         mediaTag._tagId = tagIdNode == null ? -1 : Convert.ToInt32(tagIdNode.InnerText);

         XmlNode topLeftXNode = mediaTagNode.SelectSingleNode("descendant::topLeftXCoord");
         mediaTag._topLeftX = topLeftXNode == null ? -1 : Convert.ToSingle(topLeftXNode.InnerText);

         XmlNode topLeftYNode = mediaTagNode.SelectSingleNode("descendant::topLeftYCoord");
         mediaTag._topLeftY = topLeftYNode == null ? -1 : Convert.ToSingle(topLeftYNode.InnerText);

         XmlNode bottomRightXNode = mediaTagNode.SelectSingleNode("descendant::bottomRightXCoord");
         mediaTag._bottomRightX = bottomRightXNode == null ? -1 : Convert.ToSingle(bottomRightXNode.InnerText);

         XmlNode bottomRightYNode = mediaTagNode.SelectSingleNode("descendant::bottomRightYCoord");
         mediaTag._bottomRightY = bottomRightYNode == null ? -1 : Convert.ToSingle(bottomRightYNode.InnerText);

         XmlNode contactNode = mediaTagNode.SelectSingleNode("descendant::contact");
         mediaTag._contact = contactNode == null ? String.Empty : contactNode.InnerText;

         XmlNode urlNode = mediaTagNode.SelectSingleNode("descendant::url");
         mediaTag._url = urlNode == null ? String.Empty : urlNode.InnerText;

         return mediaTag;
      }

      private MediaTag()
      {

      }

      public MediaTag(string name)
      {
         _name = name;
      }

      private string _name = String.Empty;
      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }

      private int _count = -1;
      public int Count
      {
         get { return _count; }
         set { _count = value; }
      }

      private int _tagId = -1;

      public int TagId
      {
         get { return _tagId; }
         set { _tagId = value; }
      }

      private float _topLeftX = -1;

      public float TopLeftX
      {
         get { return _topLeftX; }
         set { _topLeftX = value; }
      }

      private float _topLeftY = -1;

      public float TopLeftY
      {
         get { return _topLeftY; }
         set { _topLeftY = value; }
      }

      private float _bottomRightX = -1;

      public float BottomRightX
      {
         get { return _bottomRightX; }
         set { _bottomRightX = value; }
      }

      private float _bottomRightY = -1;

      public float BottomRightY
      {
         get { return _bottomRightY; }
         set { _bottomRightY = value; }
      }

      private string _contact = String.Empty;

      public string Contact
      {
         get { return _contact; }
         set { _contact = value; }
      }

      private string _url = String.Empty;

      public string Url
      {
         get { return _url; }
         set { _url = value; }
      }
   }

   public class MediaTagList : List<MediaTag>
   {
      internal static MediaTagList CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         return CreateFromXmlDocument(responseMessage.ResponseXml);
      }

      internal static MediaTagList CreateFromXmlDocument(XmlDocument document)
      {
         return CreateFromXmlNodeList(document.SelectNodes("descendant::content"));
      }

      internal static MediaTagList CreateFromXmlNodeList(XmlNodeList mediaTagNodes)
      {
         MediaTagList mediaTagList = new MediaTagList();

         foreach (XmlNode mediaTagNode in mediaTagNodes)
         {
            mediaTagList.Add(MediaTag.CreateFromXmlNode(mediaTagNode));
         }

         return mediaTagList;
      }
   }
}
