using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls.Basic
{
    public partial class GradientPanel : DoubleBufferedPanel
    {
        //=============================================================================
        // Variables
        //=============================================================================

        //=============================================================================
        // Methods
        //=============================================================================

        public GradientPanel()
        {
            InitializeComponent();
            _gradientColor1 = Color.AliceBlue;
            _gradientColor2 = Color.LightSteelBlue;
        }

        //=============================================================================
        // Properties
        //=============================================================================

        //-----------------------------------------------------------------------------
        Color _gradientColor1;
        [Category("Appearance")]
        public Color GradientColor1
        {
            get { return _gradientColor1; }
            set
            {
                _gradientColor1 = value;
                base.Invalidate();
            }
        }

        //-----------------------------------------------------------------------------
        Color _gradientColor2;
        [Category("Appearance")]
        public Color GradientColor2
        {
            get { return _gradientColor2; }
            set
            {
                _gradientColor2 = value;
                base.Invalidate();
            }
        }

        //=============================================================================
        // Events
        //=============================================================================

        //-----------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            if (Utility.Util.PaintingIsVisible(this, e))
            {
                ControlPaint.DrawBorder3D(g, base.ClientRectangle, Border3DStyle.Flat);
            }
            base.OnPaint(e);
        }

        //-----------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            if (Utility.Util.PaintingIsVisible(this, e))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.ClipRectangle, this.GradientColor1, this.GradientColor2, 45.0f))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    return;
                }
            }
            base.OnPaintBackground(e);
        }
    }
}
