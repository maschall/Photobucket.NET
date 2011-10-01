using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PhotobucketNet;

namespace PBNetTestDialog
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent( );
      }

      private void LaunchUserLogin_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.LaunchUserLogin();
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void RequestUserToken_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.RequestUserToken( );
            MessageBox.Show( "RequestUser Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetAlbum_Click(object sender, EventArgs e)
      {
         try
         {
            Album album = Program.Instance.Photobucket.GetCurrentUsersAlbum(Album.Text);
            MessageBox.Show( "GetAlbum Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void CreateAlbum_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.CreateNewAlbumAtBase(Album.Text);
            MessageBox.Show( "CreateAlbum Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      MediaItem _mediaItem;

      private void UploadMediaItem_Click(object sender, EventArgs e)
      {
         OpenFileDialog fileSelector = new OpenFileDialog( );

         if (fileSelector.ShowDialog( ) == DialogResult.OK)
         {
            PhotobucketNet.MediaItem newItem = Program.Instance.Photobucket.CreateCurrentUsersImageMediaItemFromFile(fileSelector.FileName);
            try
            {
               newItem.Title = "TEST265/asd f /asdf 43";
               newItem.Description = "PLEASE /asdf/ as dONE MORE TIME";               
               _mediaItem = Program.Instance.Photobucket.UploadMediaItemToBaseAlbum( newItem);
               MessageBox.Show( "UploadMediaItem Succeeded" );
               newItem.MediaStream.Close();
            }
            catch (PhotobucketWebException)
            {
               MessageBox.Show("Network problems");
               newItem.MediaStream.Close();
            }
            catch (PhotobucketApiException apiEx)
            {
               MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
               newItem.MediaStream.Close();
            }            
         }
      }

      private void GetUser_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.GetCurrentUser();
            MessageBox.Show( "GetUser Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetAllUsersMedia_Click(object sender, EventArgs e)
      {
         try
         {
            MediaItemList mediaList = Program.Instance.Photobucket.GetAllUsersMedia(Username.Text);
            MessageBox.Show( "GetAllUsersMedia Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show( "Network problems" );
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void Ping_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.Ping();
            MessageBox.Show( "Ping Succeeded" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }

      }

      private void GetUsersMediaTags_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.GetCurrentUsersMediaTags();
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void LaunchLogout_Click(object sender, EventArgs e)
      {
         Program.Instance.Photobucket.LaunchUserLogout();
      }

      private void SilentLogoutLogon_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.LaunchUserLogin();
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetUsersAlbum_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.GetUsersAlbum(Username.Text, Album.Text);
            MessageBox.Show("GetAlbum Succeeded");
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void UploadVideoToBase_Click(object sender, EventArgs e)
      {
         OpenFileDialog fileSelector = new OpenFileDialog();

         if (fileSelector.ShowDialog() == DialogResult.OK)
         {
            PhotobucketNet.MediaItem newItem = Program.Instance.Photobucket.CreateCurrentUsersVideoMediaItemFromFile(fileSelector.FileName);
            try
            {
               newItem.Title = "TEST265/asd f /asdf 43";
               newItem.Description = "PLEASE /asdf/ as dONE MORE TIME";
               Program.Instance.Photobucket.UploadMediaItemToBaseAlbum(newItem);
               MessageBox.Show("UploadMediaItem Succeeded");
               newItem.MediaStream.Close();
            }
            catch (PhotobucketWebException)
            {
               MessageBox.Show("Network problems");
               newItem.MediaStream.Close();
            }
            catch (PhotobucketApiException apiEx)
            {
               MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
               newItem.MediaStream.Close();
            }
         }
      }

      private void RenameAlbum_Click(object sender, EventArgs e)
      {
         try
         {
           
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetRecentCurrentUserMedia_Click(object sender, EventArgs e)
      {
         try
         {
            MediaItemList mediaList = Program.Instance.Photobucket.GetCurrentUsersRecentMedia(MediaType.All, 1, 200);
            Output.Text = mediaList.Count.ToString();
            MessageBox.Show("Success");
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetRecentUserMedia_Click(object sender, EventArgs e)
      {
         try
         {
            MediaItemList mediaList = Program.Instance.Photobucket.GetUsersRecentMedia(Username.Text, MediaType.All, 1, 200);
            Output.Text = mediaList.Count.ToString();
            MessageBox.Show("Success");
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetAllCurrentUsersMedia_Click(object sender, EventArgs e)
      {
         try
         {
            MediaItemList mediaList = Program.Instance.Photobucket.GetAllCurrentUsersMedia();
            Output.Text = mediaList.Count.ToString();
            MessageBox.Show("Success");
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void button1_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.GetCurrentUsersContacts();
            MessageBox.Show("Success");
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show("Network problems");
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show("Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage);
         }
      }

      private void GetMediaInformation_Click(object sender, EventArgs e)
      {
         try
         {            
            _mediaItem = Program.Instance.Photobucket.GetMediaInformation(_mediaItem);
            MessageBox.Show( "Success" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show( "Network problems" );
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show( "Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage );
         }
      }

      private void DeleteMediaItem_Click(object sender, EventArgs e)
      {
         try
         {
            Program.Instance.Photobucket.DeleteMedia( _mediaItem );
            MessageBox.Show( "Success" );
         }
         catch (PhotobucketWebException)
         {
            MessageBox.Show( "Network problems" );
         }
         catch (PhotobucketApiException apiEx)
         {
            MessageBox.Show( "Error Code: " + apiEx.ErrorCode.ToString() + ": " + apiEx.ResponseMessage );
         }
      }
   }
}
