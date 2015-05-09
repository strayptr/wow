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
    }
}
