using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace PhotobucketNet
{
   internal class ResponseMessage
   {
      protected string _responseString;
      public string ResponseString
      {
         get { return _responseString; }
      }

      public ResponseMessage()
      {

      }

      public ResponseMessage(Stream responseStream)
      {
         StreamReader responseReader = new StreamReader(responseStream);
         _responseString = responseReader.ReadToEnd( );
      }
   }

   internal class XmlResponseMessage : ResponseMessage
   {
      private XmlDocument _responseXml;
      public XmlDocument ResponseXml { get { return _responseXml; } }

      private bool _status = true;
      public bool Status { get { return _status; } }

      public XmlResponseMessage(Stream responseStream)
      {
         _responseXml = new XmlDocument( );
         _responseXml.Load( responseStream );
         _responseString = _responseXml.OuterXml;

         XmlNode statusNode = _responseXml.SelectSingleNode( "descendant::status" );
      }
   }
}
