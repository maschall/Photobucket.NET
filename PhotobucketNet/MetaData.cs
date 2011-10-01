using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PhotobucketNet
{
   public class MetaData
   {
      private MetaData()
      {

      }

      private string _resolution;
      public string Resolution { get { return _resolution; } }

      private string _comment;
      public string Comment { get { return _comment; } }

      private int _width;
      public int Width { get { return _width; } }

      private int _height;
      public int Height { get { return _height; } }

      internal static MetaData CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         MetaData metaData = new MetaData();

         XmlNode exifNode = responseMessage.ResponseXml.SelectSingleNode("descendant::exif");
         
         XmlNode resolutionNode = exifNode.SelectSingleNode("descendant::Resolution");
         metaData._resolution = resolutionNode.InnerText;

         XmlNode commentNode = exifNode.SelectSingleNode("descendant::Comment");
         metaData._comment = commentNode.InnerText;

         XmlNode widthNode = responseMessage.ResponseXml.SelectSingleNode("descendant::width");
         metaData._width = Convert.ToInt32(widthNode.InnerText);

         XmlNode heightNode = responseMessage.ResponseXml.SelectSingleNode("descendant::height");
         metaData._height = Convert.ToInt32(heightNode.InnerText);

         return metaData;
      }
   }
}
