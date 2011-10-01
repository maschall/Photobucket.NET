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
      #region Search Media
      public MediaItemList SearchMedia(string query, int num, int perpage, int page, int offset, int secondaryperpage, string type, bool recentFirst)
      {
         string relativePath = GenerateRelativeSearchUrl(query);

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));
         paramaters.Add(new QueryParameter(_num, num.ToString()));
         paramaters.Add(new QueryParameter(_perpage, perpage.ToString()));
         paramaters.Add(new QueryParameter(_page, page.ToString()));
         paramaters.Add(new QueryParameter(_offset, offset.ToString()));
         paramaters.Add(new QueryParameter(_secondaryperpage, secondaryperpage.ToString()));
         paramaters.Add(new QueryParameter(_type, type));
         paramaters.Add(new QueryParameter(_recentfirst, recentFirst ? "1" : "0" ));

         string searchMediaUrl = OAuth.GenerateURL(_apiUrl, relativePath, _searchMediaMethod, Token, paramaters);

         XmlResponseMessage searchMediaResponseMessage = GetXmlResponseMessageFromUrl(searchMediaUrl, _searchMediaMethod);

         return MediaItemList.CreateFromXmlResponseMessage(searchMediaResponseMessage);
      }

      public MediaItemList SearchMedia(string query)
      {
         return SearchMedia(query, 20, 20, 1, 0, 5, "images", false);
      }
      #endregion

      #region Get Featured Media
      public MediaItemList GetFeaturedMedia()
      {
         string relativePath = GenerateRelativeFeaturedUrl();

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add(new QueryParameter(_format, "xml"));

         string getFeaturedMediaUrl = OAuth.GenerateURL(_apiUrl, relativePath, _featuredMediaMethod, Token, paramaters);

         XmlResponseMessage getFeaturedMediaResponseMessage = GetXmlResponseMessageFromUrl(getFeaturedMediaUrl, _featuredMediaMethod);

         return MediaItemList.CreateFromXmlResponseMessage(getFeaturedMediaResponseMessage);
      }
      #endregion

      #region Generate Urls
      private string GenerateRelativeSearchUrl(string query)
      {
         return _searchUrl + "/" + OAuth.UrlEncode(query);
      }
      private string GenerateRelativeFeaturedUrl()
      {
         return _featured;
      }
      #endregion

      #region constants
      private const string _featured = "featured";
      private const string _searchMediaMethod = "GET";
      private const string _featuredMediaMethod = "GET";
      private const string _num = "num";
      private const string _offset = "offset";
      private const string _secondaryperpage = "secondaryperpage";
      private const string _recentfirst = "recentfirst";
      #endregion
   }
}