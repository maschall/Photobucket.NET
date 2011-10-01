using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace PhotobucketNet
{
   public class UploadMediaProgressEventArgs : EventArgs
   {
      private int _dataToSend;
      private int _dataSent;

      /// <summary>
      /// Number of bytes transfered so far.
      /// </summary>
      public int DataSent
      {
         get { return _dataSent; }
      }

      /// <summary>
      /// True if all bytes have been uploaded.
      /// </summary>
      public int DataToSend
      {
         get { return _dataToSend; }
      }

      public UploadMediaProgressEventArgs(int dataSent, int dataToSend)
      {
         _dataToSend = dataToSend;
         _dataSent = dataSent;
      }
   }

   public partial class Photobucket
   {
      public delegate void UploadMediaProgressHandler(object sender, UploadMediaProgressEventArgs eventArgs);

      public event UploadMediaProgressHandler OnUploadMediaProgress;

      private MediaItem UploadMediaItemToAlbum(MediaItem mediaItem, string albumPath)
      {
         string relativeUploadUrl = GenerateRelativeUploadUrl( albumPath );

         QueryParameterList paramaters = new QueryParameterList();
         paramaters.Add( new QueryParameter( _format, "xml" ) );
         paramaters.Add( new QueryParameter( _type, mediaItem.Type ) );
         if (string.IsNullOrEmpty( mediaItem.Description ) == false)
         {
            paramaters.Add( new QueryParameter( _description, OAuth.UrlEncode(mediaItem.Description) ) );
         }
         if (string.IsNullOrEmpty( mediaItem.Title ) == false)
         {
            paramaters.Add( new QueryParameter( _title, OAuth.UrlEncode(mediaItem.Title) ) );
         }

         QueryParameterList mediaParamaters = OAuth.GenerateOAuthParamaters( relativeUploadUrl, _uploadMediaMethod, Token, paramaters );

         string uploadMediaItemUrl = ApiUrl + relativeUploadUrl + "?format=xml";

         XmlResponseMessage uploadMediaItemResponse = GetResponseForUploadMediaItem( uploadMediaItemUrl, mediaItem, mediaParamaters );

         MediaItem newMediaItem = new MediaItem( uploadMediaItemResponse.ResponseXml );
         _currentUser.MediaList.Add(newMediaItem);
         return newMediaItem;
      }

      public MediaItem UploadMediaItemToAlbum(MediaItem mediaItem, ref Album album)
      {
         MediaItem newMediaItem = UploadMediaItemToAlbum( mediaItem, album.Path );
         album.MediaList.Add( newMediaItem );
         return newMediaItem;
      }

      public MediaItem UploadMediaItemToAlbum(MediaItem mediaItem, Album album)
      {
         return UploadMediaItemToAlbum( mediaItem, album.Path );
      }

      public MediaItem UploadMediaItemToBaseAlbum(MediaItem mediaItem)
      {
         Album baseAlbum = BaseAlbum;
         return UploadMediaItemToAlbum( mediaItem, ref baseAlbum );
      }

      private string GenerateRelativeUploadUrl(string albumPath)
      {
         string albumIdentifier = GenerateAlbumIdentifier( albumPath );

         return albumIdentifier + _uploadUrl;
      }

      private XmlResponseMessage GetResponseForUploadMediaItem(string uploadMediaItemUrl, MediaItem mediaItem, QueryParameterList paramaters)
      {
         string boundary = "PHOTBUCKET_MIME_" + DateTime.Now.ToString( "yyyyMMddhhmmss" );

         HttpWebRequest request = ( HttpWebRequest )HttpWebRequest.Create( uploadMediaItemUrl );
         request.UserAgent = "Mozilla/4.0 PhotobucketNet API (compatible; MSIE 6.0; Windows NT 5.1)";
         request.Method = "POST";
         request.KeepAlive = true;
         request.ContentType = "multipart/form-data; boundary=" + boundary + "";
         request.Expect = "";

         StringBuilder sb = new StringBuilder();

         foreach (QueryParameter paramater in paramaters)
         {
            if (paramater.Name == "format")
            {
               continue;
            }
            else if (paramater.Name == "oauth_signature")
            {
               paramater.Value = OAuth.UrlDecode( paramater.Value );
            }
            else if (paramater.Name == _description || paramater.Name == _title)
            {
               paramater.Value = OAuth.UrlDecode( paramater.Value );
            }
            sb.Append( "--" + boundary + "\r\n" );
            sb.Append( "Content-Disposition: form-data; name=\"" + paramater.Name + "\"\r\n" );
            sb.Append( "\r\n" );
            sb.Append( paramater.Value + "\r\n" );
         }

         sb.Append( "--" + boundary + "\r\n" );
         sb.Append( "Content-Disposition: form-data; name=\"uploadfile\"; filename=\"" + mediaItem.Name + "\"\r\n" );
         sb.Append( "Content-Type: mimetype\r\nContent-Transfer-Ecoding: binary" );
         sb.Append( "\r\n\r\n" );

         UTF8Encoding encoding = new UTF8Encoding();

         byte[] postContents = encoding.GetBytes( sb.ToString() );

         Stream stream = mediaItem.MediaStream;

         byte[] photoContents = new byte[stream.Length];
         stream.Read( photoContents, 0, photoContents.Length );
         stream.Close();

         byte[] postFooter = encoding.GetBytes( "\r\n--" + boundary + "--\r\n" );

         byte[] dataBuffer = new byte[postContents.Length + photoContents.Length + postFooter.Length];
         Buffer.BlockCopy( postContents, 0, dataBuffer, 0, postContents.Length );
         Buffer.BlockCopy( photoContents, 0, dataBuffer, postContents.Length, photoContents.Length );
         Buffer.BlockCopy( postFooter, 0, dataBuffer, postContents.Length + photoContents.Length, postFooter.Length );

         request.ContentLength = dataBuffer.Length;

         Stream resStream = request.GetRequestStream();

         int count = 1;
         int uploadBit = Math.Max( dataBuffer.Length / 100, 50 * 1024 );
         int uploadSoFar = 0;

         if ( OnUploadMediaProgress != null )
         {
            OnUploadMediaProgress( this, new UploadMediaProgressEventArgs( 0, dataBuffer.Length ) );
         }
         for (int i = 0; i < dataBuffer.Length; i = i + uploadBit)
         {
            int toUpload = Math.Min( uploadBit, dataBuffer.Length - i );
            uploadSoFar += toUpload;

            resStream.Write( dataBuffer, i, toUpload );

            if (( OnUploadMediaProgress != null ) && ( ( count++ ) % 5 == 0 || uploadSoFar == dataBuffer.Length ))
            {
               OnUploadMediaProgress( this, new UploadMediaProgressEventArgs( uploadSoFar, dataBuffer.Length ) );
            }
         }
         resStream.Close();
         try
         {
            request.Method = _uploadMediaMethod;
            HttpWebResponse response = ( HttpWebResponse )request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            return new XmlResponseMessage( responseStream );
         }
         catch (WebException webEx)
         {
            throw PhotobucketException.CreateFromWebException( webEx );
         }
      }

      #region constants
      private const string _uploadMediaMethod = "POST";
      private const string _uploadUrl = "/upload";
      private const string _uploadfile = "uploadfile";
      private const string _type = "type";
      private const string _title = "title";
      private const string _description = "description";
      private const string _filename = "filename";
      private const string _scramble = "scramble";
      private const string _degrees = "degrees";
      private const string _size = "size";
      #endregion
   }
}