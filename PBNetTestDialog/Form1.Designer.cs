namespace PBNetTestDialog
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && ( components != null ))
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           this.LaunchUserLogin = new System.Windows.Forms.Button();
           this.RequestUserToken = new System.Windows.Forms.Button();
           this.Input = new System.Windows.Forms.TextBox();
           this.GetCurrentUsersAlbum = new System.Windows.Forms.Button();
           this.CreateAlbum = new System.Windows.Forms.Button();
           this.UploadImageToBase = new System.Windows.Forms.Button();
           this.GetUserInformation = new System.Windows.Forms.Button();
           this.GetAllUsersMedia = new System.Windows.Forms.Button();
           this.Ping = new System.Windows.Forms.Button();
           this.GetUsersMediaTags = new System.Windows.Forms.Button();
           this.DeleteAlbum = new System.Windows.Forms.Button();
           this.GetAlbumPrivacy = new System.Windows.Forms.Button();
           this.RenameAlbum = new System.Windows.Forms.Button();
           this.GetUsersAlbum = new System.Windows.Forms.Button();
           this.LaunchLogout = new System.Windows.Forms.Button();
           this.MakeAlbumPublic = new System.Windows.Forms.Button();
           this.MakeAlbumPrivate = new System.Windows.Forms.Button();
           this.GetRecentCurrentUserMedia = new System.Windows.Forms.Button();
           this.GetRecentUserMedia = new System.Windows.Forms.Button();
           this.button1 = new System.Windows.Forms.Button();
           this.GetAllCurrentUsersMedia = new System.Windows.Forms.Button();
           this.Output = new System.Windows.Forms.TextBox();
           this.Username = new System.Windows.Forms.TextBox();
           this.Album = new System.Windows.Forms.TextBox();
           this.label1 = new System.Windows.Forms.Label();
           this.label2 = new System.Windows.Forms.Label();
           this.label3 = new System.Windows.Forms.Label();
           this.label4 = new System.Windows.Forms.Label();
           this.UploadVideoToBase = new System.Windows.Forms.Button();
           this.GetMediaInformation = new System.Windows.Forms.Button();
           this.DeleteMediaItem = new System.Windows.Forms.Button();
           this.SuspendLayout();
           // 
           // LaunchUserLogin
           // 
           this.LaunchUserLogin.Location = new System.Drawing.Point( 12, 41 );
           this.LaunchUserLogin.Name = "LaunchUserLogin";
           this.LaunchUserLogin.Size = new System.Drawing.Size( 125, 23 );
           this.LaunchUserLogin.TabIndex = 1;
           this.LaunchUserLogin.Text = "LaunchUserLogin";
           this.LaunchUserLogin.UseVisualStyleBackColor = true;
           this.LaunchUserLogin.Click += new System.EventHandler( this.LaunchUserLogin_Click );
           // 
           // RequestUserToken
           // 
           this.RequestUserToken.Location = new System.Drawing.Point( 12, 70 );
           this.RequestUserToken.Name = "RequestUserToken";
           this.RequestUserToken.Size = new System.Drawing.Size( 125, 23 );
           this.RequestUserToken.TabIndex = 2;
           this.RequestUserToken.Text = "RequestUserToken";
           this.RequestUserToken.UseVisualStyleBackColor = true;
           this.RequestUserToken.Click += new System.EventHandler( this.RequestUserToken_Click );
           // 
           // Input
           // 
           this.Input.Location = new System.Drawing.Point( 12, 436 );
           this.Input.Name = "Input";
           this.Input.Size = new System.Drawing.Size( 260, 20 );
           this.Input.TabIndex = 5;
           // 
           // GetCurrentUsersAlbum
           // 
           this.GetCurrentUsersAlbum.Location = new System.Drawing.Point( 212, 70 );
           this.GetCurrentUsersAlbum.Name = "GetCurrentUsersAlbum";
           this.GetCurrentUsersAlbum.Size = new System.Drawing.Size( 125, 23 );
           this.GetCurrentUsersAlbum.TabIndex = 6;
           this.GetCurrentUsersAlbum.Text = "GetCurrentUsersAlbum";
           this.GetCurrentUsersAlbum.UseVisualStyleBackColor = true;
           this.GetCurrentUsersAlbum.Click += new System.EventHandler( this.GetAlbum_Click );
           // 
           // CreateAlbum
           // 
           this.CreateAlbum.Location = new System.Drawing.Point( 212, 128 );
           this.CreateAlbum.Name = "CreateAlbum";
           this.CreateAlbum.Size = new System.Drawing.Size( 125, 23 );
           this.CreateAlbum.TabIndex = 7;
           this.CreateAlbum.Text = "CreateAlbum";
           this.CreateAlbum.UseVisualStyleBackColor = true;
           this.CreateAlbum.Click += new System.EventHandler( this.CreateAlbum_Click );
           // 
           // UploadImageToBase
           // 
           this.UploadImageToBase.Location = new System.Drawing.Point( 212, 12 );
           this.UploadImageToBase.Name = "UploadImageToBase";
           this.UploadImageToBase.Size = new System.Drawing.Size( 125, 23 );
           this.UploadImageToBase.TabIndex = 8;
           this.UploadImageToBase.Text = "UploadImageToBase";
           this.UploadImageToBase.UseVisualStyleBackColor = true;
           this.UploadImageToBase.Click += new System.EventHandler( this.UploadMediaItem_Click );
           // 
           // GetUserInformation
           // 
           this.GetUserInformation.Location = new System.Drawing.Point( 700, 157 );
           this.GetUserInformation.Name = "GetUserInformation";
           this.GetUserInformation.Size = new System.Drawing.Size( 125, 23 );
           this.GetUserInformation.TabIndex = 9;
           this.GetUserInformation.Text = "GetUserInformation";
           this.GetUserInformation.UseVisualStyleBackColor = true;
           this.GetUserInformation.Click += new System.EventHandler( this.GetUser_Click );
           // 
           // GetAllUsersMedia
           // 
           this.GetAllUsersMedia.Location = new System.Drawing.Point( 700, 99 );
           this.GetAllUsersMedia.Name = "GetAllUsersMedia";
           this.GetAllUsersMedia.Size = new System.Drawing.Size( 125, 23 );
           this.GetAllUsersMedia.TabIndex = 10;
           this.GetAllUsersMedia.Text = "GetAllUsersMedia";
           this.GetAllUsersMedia.UseVisualStyleBackColor = true;
           this.GetAllUsersMedia.Click += new System.EventHandler( this.GetAllUsersMedia_Click );
           // 
           // Ping
           // 
           this.Ping.Location = new System.Drawing.Point( 12, 12 );
           this.Ping.Name = "Ping";
           this.Ping.Size = new System.Drawing.Size( 125, 23 );
           this.Ping.TabIndex = 11;
           this.Ping.Text = "Ping";
           this.Ping.UseVisualStyleBackColor = true;
           this.Ping.Click += new System.EventHandler( this.Ping_Click );
           // 
           // GetUsersMediaTags
           // 
           this.GetUsersMediaTags.Location = new System.Drawing.Point( 700, 186 );
           this.GetUsersMediaTags.Name = "GetUsersMediaTags";
           this.GetUsersMediaTags.Size = new System.Drawing.Size( 125, 23 );
           this.GetUsersMediaTags.TabIndex = 13;
           this.GetUsersMediaTags.Text = "GetUsersMediaTags";
           this.GetUsersMediaTags.UseVisualStyleBackColor = true;
           this.GetUsersMediaTags.Click += new System.EventHandler( this.GetUsersMediaTags_Click );
           // 
           // DeleteAlbum
           // 
           this.DeleteAlbum.Location = new System.Drawing.Point( 212, 186 );
           this.DeleteAlbum.Name = "DeleteAlbum";
           this.DeleteAlbum.Size = new System.Drawing.Size( 125, 23 );
           this.DeleteAlbum.TabIndex = 14;
           this.DeleteAlbum.Text = "DeleteAlbum";
           this.DeleteAlbum.UseVisualStyleBackColor = true;
           this.DeleteAlbum.Visible = false;
           // 
           // GetAlbumPrivacy
           // 
           this.GetAlbumPrivacy.Location = new System.Drawing.Point( 212, 215 );
           this.GetAlbumPrivacy.Name = "GetAlbumPrivacy";
           this.GetAlbumPrivacy.Size = new System.Drawing.Size( 125, 23 );
           this.GetAlbumPrivacy.TabIndex = 15;
           this.GetAlbumPrivacy.Text = "GetAlbumPrivacy";
           this.GetAlbumPrivacy.UseVisualStyleBackColor = true;
           this.GetAlbumPrivacy.Visible = false;
           // 
           // RenameAlbum
           // 
           this.RenameAlbum.Location = new System.Drawing.Point( 212, 157 );
           this.RenameAlbum.Name = "RenameAlbum";
           this.RenameAlbum.Size = new System.Drawing.Size( 125, 23 );
           this.RenameAlbum.TabIndex = 16;
           this.RenameAlbum.Text = "RenameAlbum";
           this.RenameAlbum.UseVisualStyleBackColor = true;
           this.RenameAlbum.Visible = false;
           this.RenameAlbum.Click += new System.EventHandler( this.RenameAlbum_Click );
           // 
           // GetUsersAlbum
           // 
           this.GetUsersAlbum.Location = new System.Drawing.Point( 212, 99 );
           this.GetUsersAlbum.Name = "GetUsersAlbum";
           this.GetUsersAlbum.Size = new System.Drawing.Size( 125, 23 );
           this.GetUsersAlbum.TabIndex = 18;
           this.GetUsersAlbum.Text = "GetUsersAlbum";
           this.GetUsersAlbum.UseVisualStyleBackColor = true;
           this.GetUsersAlbum.Click += new System.EventHandler( this.GetUsersAlbum_Click );
           // 
           // LaunchLogout
           // 
           this.LaunchLogout.Location = new System.Drawing.Point( 12, 99 );
           this.LaunchLogout.Name = "LaunchLogout";
           this.LaunchLogout.Size = new System.Drawing.Size( 125, 23 );
           this.LaunchLogout.TabIndex = 19;
           this.LaunchLogout.Text = "LaunchLogout";
           this.LaunchLogout.UseVisualStyleBackColor = true;
           this.LaunchLogout.Click += new System.EventHandler( this.LaunchLogout_Click );
           // 
           // MakeAlbumPublic
           // 
           this.MakeAlbumPublic.Location = new System.Drawing.Point( 212, 244 );
           this.MakeAlbumPublic.Name = "MakeAlbumPublic";
           this.MakeAlbumPublic.Size = new System.Drawing.Size( 125, 23 );
           this.MakeAlbumPublic.TabIndex = 20;
           this.MakeAlbumPublic.Text = "MakeAlbumPublic";
           this.MakeAlbumPublic.UseVisualStyleBackColor = true;
           this.MakeAlbumPublic.Visible = false;
           // 
           // MakeAlbumPrivate
           // 
           this.MakeAlbumPrivate.Location = new System.Drawing.Point( 212, 273 );
           this.MakeAlbumPrivate.Name = "MakeAlbumPrivate";
           this.MakeAlbumPrivate.Size = new System.Drawing.Size( 125, 23 );
           this.MakeAlbumPrivate.TabIndex = 21;
           this.MakeAlbumPrivate.Text = "MakeAlbumPrivate";
           this.MakeAlbumPrivate.UseVisualStyleBackColor = true;
           this.MakeAlbumPrivate.Visible = false;
           // 
           // GetRecentCurrentUserMedia
           // 
           this.GetRecentCurrentUserMedia.Location = new System.Drawing.Point( 700, 12 );
           this.GetRecentCurrentUserMedia.Name = "GetRecentCurrentUserMedia";
           this.GetRecentCurrentUserMedia.Size = new System.Drawing.Size( 125, 23 );
           this.GetRecentCurrentUserMedia.TabIndex = 22;
           this.GetRecentCurrentUserMedia.Text = "GetRecentCurrentUserMedia";
           this.GetRecentCurrentUserMedia.UseVisualStyleBackColor = true;
           this.GetRecentCurrentUserMedia.Click += new System.EventHandler( this.GetRecentCurrentUserMedia_Click );
           // 
           // GetRecentUserMedia
           // 
           this.GetRecentUserMedia.Location = new System.Drawing.Point( 700, 41 );
           this.GetRecentUserMedia.Name = "GetRecentUserMedia";
           this.GetRecentUserMedia.Size = new System.Drawing.Size( 125, 23 );
           this.GetRecentUserMedia.TabIndex = 23;
           this.GetRecentUserMedia.Text = "GetRecentUserMedia";
           this.GetRecentUserMedia.UseVisualStyleBackColor = true;
           this.GetRecentUserMedia.Click += new System.EventHandler( this.GetRecentUserMedia_Click );
           // 
           // button1
           // 
           this.button1.Location = new System.Drawing.Point( 700, 128 );
           this.button1.Name = "button1";
           this.button1.Size = new System.Drawing.Size( 125, 23 );
           this.button1.TabIndex = 24;
           this.button1.Text = "GetUsersContacts";
           this.button1.UseVisualStyleBackColor = true;
           this.button1.Click += new System.EventHandler( this.button1_Click );
           // 
           // GetAllCurrentUsersMedia
           // 
           this.GetAllCurrentUsersMedia.Location = new System.Drawing.Point( 700, 70 );
           this.GetAllCurrentUsersMedia.Name = "GetAllCurrentUsersMedia";
           this.GetAllCurrentUsersMedia.Size = new System.Drawing.Size( 125, 23 );
           this.GetAllCurrentUsersMedia.TabIndex = 25;
           this.GetAllCurrentUsersMedia.Text = "GetAllCurrentUsersMedia";
           this.GetAllCurrentUsersMedia.UseVisualStyleBackColor = true;
           this.GetAllCurrentUsersMedia.Click += new System.EventHandler( this.GetAllCurrentUsersMedia_Click );
           // 
           // Output
           // 
           this.Output.Location = new System.Drawing.Point( 565, 436 );
           this.Output.Name = "Output";
           this.Output.Size = new System.Drawing.Size( 260, 20 );
           this.Output.TabIndex = 26;
           // 
           // Username
           // 
           this.Username.Location = new System.Drawing.Point( 12, 382 );
           this.Username.Name = "Username";
           this.Username.Size = new System.Drawing.Size( 260, 20 );
           this.Username.TabIndex = 27;
           // 
           // Album
           // 
           this.Album.Location = new System.Drawing.Point( 12, 329 );
           this.Album.Name = "Album";
           this.Album.Size = new System.Drawing.Size( 260, 20 );
           this.Album.TabIndex = 28;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.Location = new System.Drawing.Point( 13, 313 );
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size( 36, 13 );
           this.label1.TabIndex = 29;
           this.label1.Text = "Album";
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.Location = new System.Drawing.Point( 13, 420 );
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size( 186, 13 );
           this.label2.TabIndex = 30;
           this.label2.Text = "Input (rename AlbumName, password)";
           // 
           // label3
           // 
           this.label3.AutoSize = true;
           this.label3.Location = new System.Drawing.Point( 13, 366 );
           this.label3.Name = "label3";
           this.label3.Size = new System.Drawing.Size( 55, 13 );
           this.label3.TabIndex = 31;
           this.label3.Text = "Username";
           // 
           // label4
           // 
           this.label4.AutoSize = true;
           this.label4.Location = new System.Drawing.Point( 562, 420 );
           this.label4.Name = "label4";
           this.label4.Size = new System.Drawing.Size( 39, 13 );
           this.label4.TabIndex = 32;
           this.label4.Text = "Output";
           // 
           // UploadVideoToBase
           // 
           this.UploadVideoToBase.Location = new System.Drawing.Point( 212, 41 );
           this.UploadVideoToBase.Name = "UploadVideoToBase";
           this.UploadVideoToBase.Size = new System.Drawing.Size( 125, 23 );
           this.UploadVideoToBase.TabIndex = 33;
           this.UploadVideoToBase.Text = "UploadVideoToBase";
           this.UploadVideoToBase.UseVisualStyleBackColor = true;
           this.UploadVideoToBase.Click += new System.EventHandler( this.UploadVideoToBase_Click );
           // 
           // GetMediaInformation
           // 
           this.GetMediaInformation.Location = new System.Drawing.Point( 412, 12 );
           this.GetMediaInformation.Name = "GetMediaInformation";
           this.GetMediaInformation.Size = new System.Drawing.Size( 125, 23 );
           this.GetMediaInformation.TabIndex = 34;
           this.GetMediaInformation.Text = "GetMediaInformation";
           this.GetMediaInformation.UseVisualStyleBackColor = true;
           this.GetMediaInformation.Click += new System.EventHandler( this.GetMediaInformation_Click );
           // 
           // DeleteMediaItem
           // 
           this.DeleteMediaItem.Location = new System.Drawing.Point( 412, 41 );
           this.DeleteMediaItem.Name = "DeleteMediaItem";
           this.DeleteMediaItem.Size = new System.Drawing.Size( 125, 23 );
           this.DeleteMediaItem.TabIndex = 35;
           this.DeleteMediaItem.Text = "DeleteMediaItem";
           this.DeleteMediaItem.UseVisualStyleBackColor = true;
           this.DeleteMediaItem.Click += new System.EventHandler( this.DeleteMediaItem_Click );
           // 
           // Form1
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size( 837, 468 );
           this.Controls.Add( this.DeleteMediaItem );
           this.Controls.Add( this.GetMediaInformation );
           this.Controls.Add( this.UploadVideoToBase );
           this.Controls.Add( this.label4 );
           this.Controls.Add( this.label3 );
           this.Controls.Add( this.label2 );
           this.Controls.Add( this.label1 );
           this.Controls.Add( this.Album );
           this.Controls.Add( this.Username );
           this.Controls.Add( this.Output );
           this.Controls.Add( this.GetAllCurrentUsersMedia );
           this.Controls.Add( this.button1 );
           this.Controls.Add( this.GetRecentUserMedia );
           this.Controls.Add( this.GetRecentCurrentUserMedia );
           this.Controls.Add( this.MakeAlbumPrivate );
           this.Controls.Add( this.MakeAlbumPublic );
           this.Controls.Add( this.LaunchLogout );
           this.Controls.Add( this.GetUsersAlbum );
           this.Controls.Add( this.RenameAlbum );
           this.Controls.Add( this.GetAlbumPrivacy );
           this.Controls.Add( this.DeleteAlbum );
           this.Controls.Add( this.GetUsersMediaTags );
           this.Controls.Add( this.Ping );
           this.Controls.Add( this.GetAllUsersMedia );
           this.Controls.Add( this.GetUserInformation );
           this.Controls.Add( this.UploadImageToBase );
           this.Controls.Add( this.CreateAlbum );
           this.Controls.Add( this.GetCurrentUsersAlbum );
           this.Controls.Add( this.Input );
           this.Controls.Add( this.RequestUserToken );
           this.Controls.Add( this.LaunchUserLogin );
           this.Name = "Form1";
           this.Text = "GetAllCurrentUsersMedia";
           this.ResumeLayout( false );
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LaunchUserLogin;
        private System.Windows.Forms.Button RequestUserToken;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.Button GetCurrentUsersAlbum;
        private System.Windows.Forms.Button CreateAlbum;
        private System.Windows.Forms.Button UploadImageToBase;
        private System.Windows.Forms.Button GetUserInformation;
        private System.Windows.Forms.Button GetAllUsersMedia;
        private System.Windows.Forms.Button Ping;
        private System.Windows.Forms.Button GetUsersMediaTags;
        private System.Windows.Forms.Button DeleteAlbum;
        private System.Windows.Forms.Button GetAlbumPrivacy;
        private System.Windows.Forms.Button RenameAlbum;
        private System.Windows.Forms.Button GetUsersAlbum;
        private System.Windows.Forms.Button LaunchLogout;
        private System.Windows.Forms.Button MakeAlbumPublic;
        private System.Windows.Forms.Button MakeAlbumPrivate;
        private System.Windows.Forms.Button GetRecentCurrentUserMedia;
        private System.Windows.Forms.Button GetRecentUserMedia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button GetAllCurrentUsersMedia;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Album;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button UploadVideoToBase;
        private System.Windows.Forms.Button GetMediaInformation;
        private System.Windows.Forms.Button DeleteMediaItem;
    }
}

