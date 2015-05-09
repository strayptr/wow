using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class ChooseOrEnterValue: UserControl
    {
        //-----------------------------------------------------------------------------
        public ChooseOrEnterValue()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------
        public delegate void ValueChangedHandler(object sender);
        public event ValueChangedHandler OnValueChanged;

        //=============================================================================
        // Properties
        //=============================================================================

        //-----------------------------------------------------------------------------
        public string Value
        {
            get { return tbValue.Text; }
            set
            {
                if (value != tbValue.Text)
                {
                    tbValue.Text = value;
                    OnValueChanged(this);
                }
            }
        }
    }
}
