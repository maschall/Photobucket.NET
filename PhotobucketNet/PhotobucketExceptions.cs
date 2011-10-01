using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace PhotobucketNet
{
   public enum ErrorCode
   {
      InvalidPermissions = 1000,
      CouldNotGet = 1001,
      CouldNotCreate = 1002,
      CouldNotUploadOrUpdate = 1003,
      CouldNotAdd = 1004,
      CouldNotEmail = 1005,
      CouldNotSearch = 1006,
      None = -1,
      UnknownError = -9999
   }

   public class PhotobucketException : Exception
   {
      public PhotobucketException(string message, Exception innerException)
         : base( message, innerException )
      {

      }

      public PhotobucketException(string message)
         : base( message )
      {

      }

      public PhotobucketException(ErrorCode code, string message)
         : base( message )
      {
         _errorCode = code;
      }

      internal static PhotobucketException CreateFromWebException(WebException webEx)
      {
         if (( webEx.Message.Contains( "401" ) || webEx.Message.Contains( "500" ) ) && webEx.Response.ContentType == "text/xml")
         {
            PhotobucketApiException apiEx = new PhotobucketApiException( webEx );

            apiEx._response = new XmlResponseMessage( webEx.Response.GetResponseStream() );

            XmlNode messageNode = apiEx._response.ResponseXml.SelectSingleNode( "descendant::message" );
            apiEx._responseMessage = messageNode.InnerText;

            XmlNode errorCodeNode = apiEx._response.ResponseXml.SelectSingleNode( "descendant::code" );
            int apiError = Convert.ToInt32( errorCodeNode.InnerText );

            switch (apiError)
            {
               case 115:
               case 116:
               case 117:
               {
                  apiEx._errorCode = ErrorCode.CouldNotCreate;
                  break;
               }

               case 105:
               case 106:
               case 107:
               case 110:
               case 112:
               case 113:
               case 121:
               case 124:
               case 125:
               case 126:
               case 130:
               case 137:
               case 140:
               case 141:
               case 142:
               case 143:
               case 144:
               case 145:
               case 146:
               case 147:
               case 148:
               case 149:
               case 152:
               case 153:
               case 154:
               case 159:
               case 203:             
               {
                  apiEx._errorCode = ErrorCode.CouldNotUploadOrUpdate;
                  break;
               }

               case 123:
               case 139:
               case 102:
               {
                  apiEx._errorCode = ErrorCode.CouldNotGet;
                  break;
               }

               case 108:
               case 109:
               case 009:
               case 007:
               case 111:
               case 138:
               {
                  apiEx._errorCode = ErrorCode.InvalidPermissions;
                  break;
               }

               case 131:
               case 132:
               case 133:
               case 134:
               case 135:
               case 136:
               {
                  apiEx._errorCode = ErrorCode.CouldNotAdd;
                  break;
               }

               case 118:
               case 204:
               {
                  apiEx._errorCode = ErrorCode.CouldNotEmail;
                  break;
               }

               case 103:
               case 104:
               {
                  apiEx._errorCode = ErrorCode.CouldNotSearch;
                  break;
               }

               default:
               {
                  apiEx._errorCode = ErrorCode.UnknownError;
                  break;
               }
            }

            return apiEx;
         }
         else if(webEx.Message.Contains( "404" ))
         {
            PhotobucketApiException apiEx = new PhotobucketApiException( webEx );
            apiEx._errorCode = ErrorCode.CouldNotGet;
            return apiEx;
         }
         else
         {
            return new PhotobucketWebException( webEx );
         }
      }

      protected string _responseMessage = String.Empty;
      public string ResponseMessage { get { return _responseMessage; } }

      protected ErrorCode _errorCode = ErrorCode.None;
      public ErrorCode ErrorCode { get { return _errorCode; } }

      internal XmlResponseMessage _response = null;
      internal XmlResponseMessage Response { get { return _response; } }
   }

   public class PhotobucketWebException : PhotobucketException
   {
      public PhotobucketWebException(WebException webEx)
         : base( webEx.Message, webEx )
      {

      }

      public PhotobucketWebException(string message)
         : base( message )
      {

      }

      public PhotobucketWebException(ErrorCode code, string message)
         : base( code, message )
      {

      }
   }

   public class PhotobucketApiException : PhotobucketException
   {
      public PhotobucketApiException(Exception apiEx)
         : base( apiEx.Message, apiEx )
      {

      }

      public PhotobucketApiException(string message)
         : base( message )
      {

      }

      public PhotobucketApiException(ErrorCode code, string message)
         : base( code, message )
      {

      }
   }
}
