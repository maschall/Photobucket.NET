using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace PhotobucketNet
{

   internal class OAuth
   {
      public OAuth(string baseUrl, string apiKey, string apiSecret)
      {
         _baseUrl = baseUrl;
         _apiKey = apiKey;
         _apiSecret = apiSecret;
      }

      public string GenerateURL(string authedBaseUrl, string relativeUrl, string httpMethod, PhotobucketToken token)
      {
         return GenerateURL( authedBaseUrl, relativeUrl, httpMethod, token, null );
      }

      public string GenerateURL(string authedBaseUrl, string relativeUrl, string httpMethod, PhotobucketToken token, QueryParameterList paramaters)
      {
         if (string.IsNullOrEmpty( authedBaseUrl ))
         {
            authedBaseUrl = _baseUrl;
         }

         if (string.IsNullOrEmpty( httpMethod ))
         {
            throw new ArgumentNullException( "httpMethod empty" );
         }

         QueryParameterList realParamaters = GenerateOAuthParamaters(relativeUrl, httpMethod, token, paramaters);

         string normalizedRequestParameters = realParamaters.NormalizeRequestParameters();

         string finalUrl = authedBaseUrl + relativeUrl + "?" + normalizedRequestParameters;

         return finalUrl;
      }

      public QueryParameterList GenerateOAuthParamaters(string relativeUrl, string httpMethod, PhotobucketToken token, QueryParameterList paramaters)
      {
         string nonce = GenerateNonce( );
         string timestamp = GenerateTimeStamp( );

         if (paramaters == null)
         {
            paramaters = new QueryParameterList( );
         }

         paramaters.Add( new QueryParameter( OAuthVersionKey, OAuthVersion ) );
         paramaters.Add( new QueryParameter( OAuthNonceKey, nonce ) );
         paramaters.Add( new QueryParameter( OAuthTimestampKey, timestamp ) );
         paramaters.Add( new QueryParameter( OAuthSignatureMethodKey, HMACSHA1SignatureType ) );
         paramaters.Add( new QueryParameter( OAuthConsumerKeyKey, _apiKey ) );

         if (!string.IsNullOrEmpty( token.Token ))
         {
            paramaters.Add( new QueryParameter( OAuthTokenKey, token.Token ) );
         }

         string signature = GenerateHashedSignature( relativeUrl, httpMethod, token.TokenSecret, timestamp, nonce, paramaters );

         paramaters.Add( new QueryParameter( OAuthSignatureKey, UrlEncode(signature) ) );

         paramaters.Sort( );

         return paramaters;
      }

      public ConsumerToken GetConsumerTokenFromResponse(string response)
      {
         QueryParameterList paramaterList = QueryUtility.GetQueryParameters( response, String.Empty );

         ConsumerToken consumerToken = new ConsumerToken( );

         foreach (QueryParameter paramater in paramaterList)
         {
            if (paramater.Name == OAuthTokenKey)
            {
               consumerToken.Token = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == OAuthTokenSecretKey)
            {
               consumerToken.TokenSecret = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == NextStep)
            {
               consumerToken.NextStepUrl = UrlDecode( paramater.Value );
            }
         }

         return consumerToken;
      }

      public UserToken GetUserTokenFromResponse(string response)
      {
         QueryParameterList paramaterList = QueryUtility.GetQueryParameters( response, String.Empty );

         UserToken userToken = new UserToken( );

         foreach (QueryParameter paramater in paramaterList)
         {
            if (paramater.Name == OAuthTokenKey)
            {
               userToken.Token = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == OAuthTokenSecretKey)
            {
               userToken.TokenSecret = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == Username)
            {
               userToken.Username = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == Subdomain)
            {
               userToken.Subdomain = UrlDecode( paramater.Value );
            }
            else if (paramater.Name == HomeUrl)
            {
               userToken.UserHomeUrl = UrlDecode( paramater.Value );
            }
         }

         return userToken;
      }

      public string GenerateHashedSignature(string relativeUrl, string httpMethod, string apiSecret, string tokenSecret, string timestamp, string nonce, QueryParameterList paramaters)
      {
         string signatureBase = GenerateSignatureBase( _baseUrl + relativeUrl, httpMethod, timestamp, nonce, paramaters );

         HMACSHA1 hmacsha1 = new HMACSHA1( );
         hmacsha1.Key = Encoding.ASCII.GetBytes( string.Format( "{0}&{1}", UrlEncode( apiSecret ), UrlEncode( tokenSecret ) ) );

         byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes( signatureBase );
         byte[] hashBytes = hmacsha1.ComputeHash( dataBuffer );

         return Convert.ToBase64String( hashBytes );
      }

      public string GenerateHashedSignature(string relativeUrl, string httpMethod, string tokenSecret, string timestamp, string nonce, QueryParameterList paramaters)
      {
         return GenerateHashedSignature( relativeUrl, httpMethod, _apiSecret, tokenSecret, timestamp, nonce, paramaters );
      }

      private string GenerateSignatureBase(string url, string httpMethod, string timestamp, string nonce, QueryParameterList paramaters)
      {
         if (string.IsNullOrEmpty( httpMethod ))
         {
            throw new ArgumentNullException( "httpMethod empty" );
         }

         paramaters.Sort();

         string normalizedRequestParameters = paramaters.NormalizeRequestParameters();

         StringBuilder signatureBase = new StringBuilder( );
         signatureBase.AppendFormat( "{0}&", httpMethod.ToUpper( ) );
         signatureBase.AppendFormat( "{0}&", UrlEncode( url ) );
         signatureBase.AppendFormat( "{0}", UrlEncode( normalizedRequestParameters ) );

         return signatureBase.ToString( );
      }

      public string UrlEncode(string Url)
      {
         if (string.IsNullOrEmpty(Url))
         {
            return "";
         }

         StringBuilder result = new StringBuilder( );

         foreach (char symbol in Url)
         {
            if (unreservedChars.IndexOf( symbol ) != -1)
            {
               result.Append( symbol );
            }
            else
            {
               result.Append( '%' + String.Format( "{0:X2}", ( int )symbol ) );
            }
         }

         return result.ToString( );
      }

      public string UrlDecode(string Url)
      {
         StringBuilder result = new StringBuilder( );

         string hexString = "";
         bool hexFound = false;

         foreach (char symbol in Url)
         {
            if (hexFound)
            {
               if (hexString.Length == 0)
               {
                  hexString += symbol;
               }
               else if (hexString.Length == 1)
               {
                  hexString += symbol;
                  hexFound = false;
                  int hexInt = Convert.ToInt32( hexString, 16 );
                  string hexChar = Char.ConvertFromUtf32( hexInt );
                  result.Append( hexChar );
                  hexString = "";
               }
            }
            else
            {
               if (symbol == '%')
               {
                  hexFound = true;
               }
               else
               {
                  result.Append( symbol );
               }
            }
         }

         return result.ToString( );
      }

      private string GenerateTimeStamp()
      {
         TimeSpan ts = DateTime.UtcNow - new DateTime( 1970, 1, 1, 0, 0, 0, 0 );
         return Convert.ToInt32(ts.TotalSeconds).ToString( );
      }

      private Random _random = new Random( );
      private string GenerateNonce()
      {
         return _random.Next( 12345678, 999999999 ).ToString( );
      }

      #region paramaters
      private string _baseUrl;
      private string _apiKey;
      private string _apiSecret;
      #endregion

      #region string constants
      private const string OAuthParameterPrefix = "oauth_";
      private const string OAuthVersion = "1.0";
      private const string OAuthConsumerKeyKey = "oauth_consumer_key";
      private const string OAuthCallbackKey = "oauth_callback";
      private const string OAuthVersionKey = "oauth_version";
      private const string OAuthSignatureMethodKey = "oauth_signature_method";
      private const string OAuthSignatureKey = "oauth_signature";
      private const string OAuthTimestampKey = "oauth_timestamp";
      private const string OAuthNonceKey = "oauth_nonce";
      private const string OAuthTokenKey = "oauth_token";
      private const string OAuthTokenSecretKey = "oauth_token_secret";
      private const string HMACSHA1SignatureType = "HMAC-SHA1";
      private const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
      private const string NextStep = "next_step";
      private const string Subdomain = "subdomain";
      private const string HomeUrl = "homeurl";
      private const string Username = "username";
      private const string _uploadfile = "uploadfile";
      #endregion
   }

   #region QueryParamaters

   internal class QueryUtility
   {
      public static QueryParameterList GetQueryParameters(string parameters, string ignoredPrefix)
      {
         if (parameters.StartsWith( "?" ))
         {
            parameters = parameters.Remove( 0, 1 );
         }

         QueryParameterList result = new QueryParameterList( );

         if (!string.IsNullOrEmpty( parameters ))
         {
            string[] paramatersSplit = parameters.Split( '&' );
            foreach (string paramater in paramatersSplit)
            {
               if (!string.IsNullOrEmpty( paramater ) && ( ignoredPrefix == String.Empty || !paramater.StartsWith( ignoredPrefix ) ))
               {
                  if (paramater.IndexOf( '=' ) > -1)
                  {
                     string[] temp = paramater.Split( '=' );
                     result.Add( new QueryParameter( temp[0], temp[1] ) );
                  }
                  else
                  {
                     result.Add( new QueryParameter( paramater, string.Empty ) );
                  }
               }
            }
         }

         return result;
      }
   }

   internal class QueryParameterList : List<QueryParameter>
   {
      public string NormalizeRequestParameters()
      {
         StringBuilder sb = new StringBuilder();
         QueryParameter p = null;
         for (int i = 0; i < Count; i++)
         {
            p = this[i];
            if (string.IsNullOrEmpty(p.Value))
            {
               sb.AppendFormat( "{0}", p.Name );
            }
            else
            {
               sb.AppendFormat( "{0}={1}", p.Name, p.Value );
            }

            if (i < Count - 1)
            {
               sb.Append("&");
            }
         }

         return sb.ToString();
      }

      public new void Sort()
      {
         base.Sort(new QueryParameterComparer());
      }
   }

   /// <summary>
   /// Provides an internal structure to sort the query parameter
   /// </summary>
   internal class QueryParameter
   {
      private string _name = null;
      private string _value = null;

      public QueryParameter(string name, string value)
      {
         _name = name;
         _value = value;
      }

      public string Name { get { return _name; } set { _name = value; } }
      public string Value { get { return _value; } set { _value = value; } }
   }

   /// <summary>
   /// Comparer class used to perform the sorting of the query parameters
   /// </summary>
   internal class QueryParameterComparer : IComparer<QueryParameter>
   {
      public int Compare(QueryParameter x, QueryParameter y)
      {
         if (x.Name == y.Name)
         {
            return string.Compare( x.Value, y.Value );
         }
         else
         {
            return string.Compare( x.Name, y.Name );
         }
      }
   }

   #endregion

}
