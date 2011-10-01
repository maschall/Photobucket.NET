using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PhotobucketNet;

namespace PBNetTestDialog
{
    class Program
    {
        private Photobucket _photobucket = new Photobucket( "149826302", "d0d0af3088184270934778a59e35128c" );

        public Photobucket Photobucket
        {
            get { return _photobucket; }
            set { _photobucket = value; }
        }

        private Program()
        {

        }

        static Program _theInstance = new Program( );
        public static Program Instance
        {
            get { return _theInstance; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles( );
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1( ) );
        }
    }
}
