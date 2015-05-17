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
        // Private Variables
        //=============================================================================
        Control _trackedParent = null;
        Control TrackedParent
        {
            get { return _trackedParent; }
            set
            {
                if (_trackedParent != null)
                    _trackedParent.Paint -= _trackedParent_Paint;
                _trackedParent = value;
                if (_trackedParent != null)
                    _trackedParent.Paint += _trackedParent_Paint;
            }
        }

        //=============================================================================
        // Methods
        //=============================================================================

        public GradientPanel()
        {
            InitializeComponent();
            _gradientColor1 = Color.AliceBlue;
            _gradientColor2 = Color.LightSteelBlue;
            _border3DStyle = Border3DStyle.RaisedInner;
            _dropshadow = true;
        }

        //=============================================================================
        // Properties
        //=============================================================================

        //-----------------------------------------------------------------------------
        bool _dropshadow;
        [Category("Appearance")]
        public bool Dropshadow
        {
            get { return _dropshadow; }
            set
            {
                _dropshadow = value;
                base.Invalidate();
            }
        }

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

        //-----------------------------------------------------------------------------
        Border3DStyle _border3DStyle;
        [Category("Appearance")]
        public Border3DStyle Border3DStyle
        {
            get { return _border3DStyle; }
            set
            {
                _border3DStyle = value;
                base.Invalidate();
            }
        }

        //=============================================================================
        // Events
        //=============================================================================

        //-----------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (Utility.Util.PaintingIsVisible(this, e))
            {
                // draw an etched border.
                ControlPaint.DrawBorder3D(g, base.ClientRectangle, this.Border3DStyle);

                // if our parent has changed, hook its paint event.
                this.TrackedParent = this.Parent;
            }
            base.OnPaint(e);
        }

        //-----------------------------------------------------------------------------
        void _trackedParent_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (Utility.Util.PaintingIsVisible(this, e))
            {
                if (this.Dropshadow)
                {
                    // draw our dropshadow onto the parent control.
                    Utility.Util.Draw.Dropshadow(g, this);
                }
            }
        }

        //-----------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (Utility.Util.PaintingIsVisible(this, e))
            {
                // draw the gradient from the upper-left corner to the lower-
                // right corner, changing from one color to the other.
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    e.ClipRectangle,
                    this.GradientColor1, this.GradientColor2,
                    45.0f))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                    return;
                }
            }
            base.OnPaintBackground(e);
        }
    }
}
