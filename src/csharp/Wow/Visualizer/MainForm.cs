using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualizer
{
    public partial class MainForm : Form
    {
        //-----------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------
        public void Frame(uint dt)
        {
        }

        //-----------------------------------------------------------------------------
        private bool IsFileAllowed(string filename)
        {
            // only allow .iq and .wav files.
            return filename.ToLower().EndsWith(".iq") || filename.ToLower().EndsWith(".wav");
        }

        //=============================================================================
        // Properties
        //=============================================================================

        //-----------------------------------------------------------------------------
        public bool Done;

        //=============================================================================
        // Events
        //=============================================================================

        //-----------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Done = true;
        }

        //-----------------------------------------------------------------------------
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // is the user dropping files?
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // get the list of files the user's dropping.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    // if the list contains a valid file type, then allow it.
                    if (IsFileAllowed(file))
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }

            // if there were no valid files, then ignore the dragdrop.
            e.Effect = DragDropEffects.None;
        }

        //-----------------------------------------------------------------------------
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            // get the list of files the user's dropping.
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string str = "";
            foreach (string file in files)
            {
                if (IsFileAllowed(file))
                    str += String.Format(" '{0}'", file);
            }
            Program.MainForm.Text = str;
        }
    }
}
