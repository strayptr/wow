﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.DSP;
using Utility;
using System.Drawing.Drawing2D;

namespace Visualizer
{
    public partial class MainForm : Form
    {
        //=============================================================================
        // Variables
        //=============================================================================

        private ISignal _signal;
        private readonly Font _font = new Font(FontFamily.GenericSansSerif, 10.0f);

        //=============================================================================
        // Methods
        //=============================================================================

        //-----------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
            splitMain.Panel1.Paint += Panel1_Paint;
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

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // pretty stuff.  (like you!)
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //-----------------------------------------------------------------------------
        void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // draw a fake panel to the right of a real panel, but with rounded corners!
            // (We apple now.)
            {
                GraphicsPath gp = Util.RoundedRect(Util.Worldspace(pnTestDropshadow), 5.0f);
                PointF pos = new PointF(2.0f * pnTestDropshadow.Width + 2, 0.0f);
                gp.Transform(Util.Mat.Translate(pos.X, pos.Y));
                pos = Util.Add(pos, pnTestDropshadow.Location);
                Util.Draw.Dropshadow(g, gp);
                Util.Draw.Dropshadow(g, gp, 128 + 64, pnTestDropshadow.BackColor, 0.0f, 0.0f);
                using (Pen p = new Pen(Color.FromArgb(64, Color.MidnightBlue)))
                {
                    g.DrawPath(p, gp);
                }
                pos = Util.Add(pos, new PointF(20.0f, 20.0f));
                pos = Util.Add(pos, new PointF(pnTestDropshadow.Width, 0.0f));
                Util.Draw.TextBubble(g, pos, "wow", Color.AliceBlue, Color.SteelBlue, _font, 5.0f);

                Util.Draw.Tracker(g, new RectangleF(Util.Add(pos, new PointF(50.0f, 0.0f)), new SizeF(20.0f, 20.0f)));
            }
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // drag'n'drop.
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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
                    {
                        // update the header bar's text to indicate a file is open.
                        string basename = System.IO.Path.GetFileName(file);
                        this.Text = String.Format("{0} ({1})", Program.Title, basename);
                        return;
                    }
                }
            }
        }
    }
}
