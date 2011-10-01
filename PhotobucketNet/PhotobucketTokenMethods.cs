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
      public bool ValidateUserToken(UserToken userToken)
      {
         string relativePath = GenerateRelativeUserUrl( userToken.Username );

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add( new QueryParameter( _format, "xml" ) );

         string validateUserUrl = OAuth.GenerateURL( userToken.UserApi , relativePath, _getUserMethod, userToken, paramaters );

         try
         {
            XmlResponseMessage getUserResponseMessage = GetXmlResponseMessageFromUrl( validateUserUrl, _getUserMethod );
            return true;
         }
         catch (PhotobucketApiException apiEx)
         {
            if (apiEx.ErrorCode == ErrorCode.CouldNotGet)
            {
               return false;
            }
            throw;
         }
      }

      public bool ValidateCurrentUserToken()
      {
         return ValidateUserToken(UserToken);
      }
   }
}