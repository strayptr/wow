using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualizer
{
    static class Program
    {
        //----------------------------------------------------------------------
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (!MainForm.Done)
            {
                // compute elapsed time since last frame.
                uint dt = 0;

                // process application events.
                Application.DoEvents();

                // process the current frame.
                MainForm.Frame(dt);
            }
        }

        //----------------------------------------------------------------------
        static MainForm _mainForm;
        public static MainForm MainForm
        {
            get
            {
                if (_mainForm == null)
                {
                    _mainForm = new MainForm();
                    _title = _mainForm.Text;
                    _mainForm.Show();
                }
                return _mainForm;
            }
        }

        //----------------------------------------------------------------------
        static string _title;
        public static string Title
        {
            get { return _title; }
        }
    }
}
