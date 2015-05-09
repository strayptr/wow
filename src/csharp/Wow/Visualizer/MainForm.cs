using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.DSP;

namespace Visualizer
{
    public partial class MainForm : Form
    {
        //=============================================================================
        // Variables
        //=============================================================================

        private ISignal _signal;

        //=============================================================================
        // Methods
        //=============================================================================

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
        public bool LoadSignalFromFile(string filepath)
        {
            if (!IsFileAllowed(filepath))
                return false;

            if (filepath.ToLower().EndsWith(".iq"))
            {
                // for now, just fill in some values for testing purposes.  (Eventually 
                // we'll prompt the user to provide this info.)
                HackRFSignal.Settings settings;
                settings.Frequency = 27000000;
                settings.SamplesPerSec = 10000000;

                _signal = new HackRFSignal(settings, filepath);

                Complex[] samples = new Complex[2048];
                _signal.ReadSamples(samples, 0, 2048);

                return true;
            }
            
            throw new NotImplementedException("Support for that filetype isn't implemented yet.");
        }

        //-----------------------------------------------------------------------------
        bool IsFileAllowed(string filepath)
        {
            // only allow .iq and .wav files.
            return filepath.ToLower().EndsWith(".iq") || filepath.ToLower().EndsWith(".wav");
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
            foreach (string file in files)
            {
                // try to load the first valid file.
                if (IsFileAllowed(file))
                {
                    if (this.LoadSignalFromFile(file))
                        return;
                }
            }
        }
    }
}
