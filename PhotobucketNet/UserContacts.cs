using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PhotobucketNet
{
   public class UserContact
   {
      private UserContact(string first, string last, string email)
      {
         _firstName = first;
         _lastName = last;
         _email = email;
      }

      internal static UserContact CreateFromXmlNode(XmlNode contactNode)
      {
         XmlNode firstNode = contactNode.SelectSingleNode("descendant::first");
         string first = firstNode == null ? String.Empty : firstNode.InnerText;

         XmlNode lastNode = contactNode.SelectSingleNode("descendant::last");
         string last = lastNode == null ? String.Empty : lastNode.InnerText;

         XmlNode emailNode = contactNode.SelectSingleNode("descendant::email");
         string email = emailNode == null ? String.Empty : emailNode.InnerText;

         return new UserContact(first, last, email);
      }

      private string _firstName = String.Empty;
      public string FirstName
      {
         get { return _firstName; }
      }

      private string _lastName = String.Empty;
      public string LastName
      {
         get { return _lastName; }
      }

      private string _email = String.Empty;
      public string Email
      {
         get { return _email; }
      }
   }

   public class UserContactList : List<UserContact>
   {
      internal static UserContactList CreateFromXmlResponseMessage(XmlResponseMessage responseMessage)
      {
         return CreateFromXmlDocument(responseMessage.ResponseXml);
      }

      internal static UserContactList CreateFromXmlDocument(XmlDocument document)
      {
         return CreateFromXmlNodeList(document.SelectNodes("descendant::contact"));
      }

      internal static UserContactList CreateFromXmlNodeList(XmlNodeList contactNodes)
      {
         UserContactList contactList = new UserContactList();

         foreach (XmlNode contatcNode in contactNodes)
         {
            contactList.Add(UserContact.CreateFromXmlNode(contatcNode));
         }

         return contactList;
      }
   }
}
