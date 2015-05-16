using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Utility
{
    public static class Util
    {
        //======================================================================
        // math.
        //======================================================================

        //----------------------------------------------------------------------
        public static PointF Add(PointF A, PointF B)
        {
            return new PointF(
                A.X + B.X,
                A.Y + B.Y);
        }

        //----------------------------------------------------------------------
        public static PointF Subtract(PointF A, PointF B)
        {
            return new PointF(
                A.X - B.X,
                A.Y - B.Y);
        }

        //----------------------------------------------------------------------
        // constrains a value to be in [min .. max].
        //----------------------------------------------------------------------
        public static float Clamp(float min, float max, float val)
        {
            // if the programmer said Clamp(max, min, val) instead of the 
            // expected Clamp(min, max, val), then swap them.  There's no reason
            // not to.
            if (min > max) Swap(ref min, ref max);

            if (val < min) return min;
            if (val > max) return max;
            return val;
        }

        //----------------------------------------------------------------------
        public static float Dot(PointF A, PointF B)
        {
            return (A.X * B.X) + (A.Y * B.Y);
        }

        //----------------------------------------------------------------------
        public static float Distance(float x1, float x2)
        {
            return Abs(x2 - x1);
        }

        //----------------------------------------------------------------------
        public static float DistanceSq(PointF A, PointF B)
        {
            PointF P = Subtract(B, A);
            return Dot(P, P); // "ha ha you said peepee"
        }

        //----------------------------------------------------------------------
        public static float Distance(PointF A, PointF B)
        {
            return Sqrt(DistanceSq(A, B));
        }

        //----------------------------------------------------------------------
        public static Color Invert(Color color)
        {
            return Color.FromArgb(color.A,
                // using base 10 is like sooooo 0x7ce.
                0xff - color.R,
                0xff - color.G,
                0xff - color.B);
        }

        //----------------------------------------------------------------------
        // the one, the only, the classic!  lerp!  now in technicolor.
        //----------------------------------------------------------------------
        public static float Lerp(float a, float b, float t)
        {
            return (((b - a) * t) + a);
        }

        //----------------------------------------------------------------------
        public static Color Multiply(Color color, float s)
        {
            return Color.FromArgb(color.A,
                (int)Clamp(0.0f, 255.0f, (color.R * s)),
                (int)Clamp(0.0f, 255.0f, (color.G * s)),
                (int)Clamp(0.0f, 255.0f, (color.B * s)));
        }

        //----------------------------------------------------------------------
        public static PointF Negate(PointF P)
        {
            return new PointF(-P.X, -P.Y);
        }

        //----------------------------------------------------------------------
        // hides the ugly type conversions.  I wonder why Math.* doesn't contain
        // overloads for float types...
        //----------------------------------------------------------------------
        public static float Abs(float x) { return (float)Math.Abs((double)x); }
        public static float Abs(double x) { return (float)Math.Abs(x); }
        public static float Sqrt(float x) { return (float)Math.Sqrt((double)x); }
        public static float Sqrt(double x) { return (float)Math.Sqrt(x); }

        //======================================================================
        // drawing utils.
        //======================================================================
        
        //----------------------------------------------------------------------
        public static RectangleF Add(RectangleF rect, PointF offset)
        {
            PointF pos = Add(rect.Location, offset);
            return new RectangleF(pos, rect.Size);
        }
        public static RectangleF Translate(RectangleF rect, PointF offset)
        {
            return Add(rect, offset);
        }

        //----------------------------------------------------------------------
        public static RectangleF Add(RectangleF rect,
            float top, float bottom, float left, float right)
        {
            return RectFromSides(
                rect.Top + top,
                rect.Bottom + bottom,
                rect.Left + left,
                rect.Right + right);
        }

        //----------------------------------------------------------------------
        // Occasionally, the result of some sequence of operations is actually
        // 1px larger than you need.  Not for any bad reason, just because of
        // a similar thing as http://en.wikipedia.org/wiki/Off-by-one_error#Fencepost_error
        //
        // (When you're working with pixels, sometimes you're "counting fenceposts,"
        // and other times you're "counting sections."  This function is used
        // when you need to convert from 'fenceposts' to 'sections'.")
        //----------------------------------------------------------------------
        public static RectangleF AdjustRectForDrawing(RectangleF rect)
        {
            return new RectangleF(
                rect.Location,
                new SizeF(rect.Width - 1.0f, rect.Height - 1.0f));
        }
        //----------------------------------------------------------------------
        // represents a rect as a closed set of connected points, starting
        // from its upper-right corner and going anticlockwise.
        //
        // (useful for getting the border of a rect as a GraphicsPath, which 
        // can then be used for e.g. drawing its dropshadow.)
        //----------------------------------------------------------------------
        public static PointF[] BorderOf(RectangleF rect)
        {
            rect = Unpretzel(rect);
            return new PointF[]
            { 
                new PointF(rect.Right, rect.Top), 
                new PointF(rect.Left, rect.Top),
                new PointF(rect.Left, rect.Bottom),
                new PointF(rect.Right, rect.Bottom),
                new PointF(rect.Right, rect.Top)
            };
        }
        // an alias for getting the border of a control.
        public static PointF[] BorderOf(Control control)
        {
            // get the control's rectangle.
            RectangleF rect = Worldspace(control);

            // subtract 1px from its right and bottom sides.
            // In other words, move its right side <--- that way by 1px,
            // and move the bottom side ^ upwards by 1px.
            rect = Add(rect, 0.0f, -1.0f, 0.0f, -1.0f);

            return BorderOf(rect);
        }
        // converts the border to a GraphicsPath.
        public static GraphicsPath ToPath(PointF[] points)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(points);
            return gp;
        }

        //----------------------------------------------------------------------
        // the center of a rectangle.
        //----------------------------------------------------------------------
        public static PointF Center(RectangleF rect)
        {
            rect = Unpretzel(rect);
            float x = rect.X + (rect.Width / 2.0f);
            float y = rect.Y + (rect.Height / 2.0f);
            return new PointF(x, y);
        }

        //----------------------------------------------------------------------
        // constrains val.x to be in [container.left .. container.right]
        // constrains val.y to be in [container.top .. container.bottom]
        //----------------------------------------------------------------------
        public static PointF Clamp(RectangleF container, PointF val)
        {
            float x = Clamp(container.Left, container.Right, val.X);
            float y = Clamp(container.Top, container.Bottom, val.Y);
            return new PointF(x, y);
        }
        public static PointF Clip(PointF val, RectangleF container)
        {
            return Clamp(container, val);
        }

        //----------------------------------------------------------------------
        // CornerOf(rect, -1.0f, -1.0f) will give the rect's bottom-left corner.
        // CornerOf(rect,  1.0f,  1.0f) will give the rect's upper-right corner.
        // etc.
        //----------------------------------------------------------------------
        public static PointF CornerOf(RectangleF rect, float sx, float sy)
        {
            // remap from [-1.0 .. 1.0] to [0.0 ..1.0]
            float tx = ((sx * 0.5f) + 0.5f); 
            float ty = ((sy * 0.5f) + 0.5f);
            return new PointF(
                rect.Location.X + rect.Width * tx,
                rect.Location.Y + rect.Height * ty);
        }
        public static PointF CornerOf(Control control, float sx, float sy)
        {
            // return a "world position," i.e. the resulting point is in a space
            // relative to the control's parent.
            return CornerOf(Worldspace(control), sx, sy);
        }
        public static PointF LocalCornerOf(Control control, float sx, float sy)
        {
            // return a "local position," i.e. the resulting point is in a space
            // relative to the control itself.  (The upper-left is at (0,0).)
            return CornerOf(Localspace(control), sx, sy);
        }

        //----------------------------------------------------------------------
        // returns the distance between two rectangles.
        //----------------------------------------------------------------------
        public static float Distance(RectangleF a, RectangleF b)
        {
            return Distance(
                Clip(b.Location, a),
                Clip(a.Location, b));
        }

        //----------------------------------------------------------------------
        public static bool IsInRange(float left, float right, float at)
        {
            if (left > right)
                Swap<float>(ref left, ref right);
            return ((at >= left) && (at <= right));
        }

        //----------------------------------------------------------------------
        // some drawing functions require an integer rectangle.
        // (Those functions are such squares!)
        //----------------------------------------------------------------------
        public static Rectangle Quantize(RectangleF rect)
        {
            // TODO: should we do (int)Round(foo) instead of (int)foo?
            return new Rectangle(
                (int) rect.Location.X,
                (int) rect.Location.Y,
                (int) rect.Width,
                (int) rect.Height);
        }

        //----------------------------------------------------------------------
        // converts [0.0 .. 1.0] to [0 .. 255]
        //----------------------------------------------------------------------
        public static int QuantizeColorComponent(float colorComponent)
        {
            return (int) (255.0f * Clamp(0.0f, 1.0f, colorComponent));
        }

        //----------------------------------------------------------------------
        // you tell it where you want a rectangle's top, bottom, left, and right 
        // to be, and it'll deliver one to you via Euclidcart (YC S1NaN).
        //----------------------------------------------------------------------
        public static RectangleF RectFromSides(float top, float bottom, float left, float right)
        {
            float x = left;
            float y = top;
            float w = (right - left);
            float h = (bottom - top);
            return new RectangleF(x, y, w, h);
        }
        public static Rectangle RectFromSides(int top, int bottom, int left, int right)
        {
            return Quantize(RectFromSides((float)top, (float)bottom, (float)left, (float)right));
        }

        //----------------------------------------------------------------------
        // if 'val' is at beforeLeft, returns afterLeft.
        // if 'val' is at beforeRight, returns afterRight.
        // if 'val' is anywhere inside [beforeLeft .. beforeRight], returns
        //   an interpolation such that it ends up inside [afterLeft .. afterRight].
        //
        // in other words, it transforms 'val' such that it starts out in a space
        // with a range "[beforeLeft .. beforeRight]", and ends up in a space 
        // with a range "[afterLeft .. afterRight]".
        //
        // (it's not actually implemented like that, obviously.  I'm just
        // explaining the operation like that.)
        //----------------------------------------------------------------------
        public static float Remap(
            float beforeLeft, float beforeRight,
            float afterLeft, float afterRight,
            float val)
        {
            //----------------------------------------------------------------------
            // who says there's no room for ugly mathematics?  pssh, we'll make
            // space!
            //
            // float r;
            // r  = ((afterRight - afterLeft) * (val - beforeLeft));
            // r /= (beforeRight - beforeLeft);
            // r += afterLeft;
            // return r;
            //----------------------------------------------------------------------

            // the following code is equivalent to the code in the above comments.
            // I've expanded it and made it readable, in hopes that someone
            // might one day find this illuminating.

            // start with the value we want to remap.
            float r = val;

            // alias some quantities for convenience.
            float afterSize = (afterRight - afterLeft);
            float beforeSize = (beforeRight - beforeLeft);

            // translate the value such that the "origin" becomes the left side
            // of the `before` range.  Graphics programmers, this is like going
            // from worldspace to localspace.
            // i.e. map from [beforeLeft .. beforeRight] to [0.0 .. beforeSize].
            r -= beforeLeft;

            // undo the "scaling that was being imposed by the `before` range."
            // i.e. map from [0.0 .. beforeSize] to [0.0 .. 1.0].
            r /= beforeSize;

            // map from [0.0 .. 1.0] to [0.0 .. afterSize].
            r *= afterSize;

            // now translate this back to worldspace, except we want to be
            // relative to the `after` origin, not the `before` origin.
            // i.e. map from [0.0 .. afterSize] to [afterLeft .. afterRight].
            r += afterLeft;

            // presto!
            return r;
        }
        // the 2D version of Remap().  Takes a point that lives inside `before`
        // and returns the corresponding point that lives in `after`.  The cool
        // thing is that this works as expected regardless of whether the point
        // is inside or outside the `before` range.  It will always end up relative
        // to `after`.  (I remember how confusing this was when first starting
        // out, though, so I feel your pain.  On the other hand, maybe you feel
        // no pain, in which case you're far smarter than I was at the time.)
        public static PointF Transform(RectangleF before, RectangleF after, PointF pt)
        {
            return new PointF(
                Lerp(after.Left, after.Right, WhereBetween(before.Left, before.Right, pt.X)),
                Lerp(after.Bottom, after.Top, WhereBetween(before.Bottom, before.Top, pt.Y)));
        }
        // the 2D version of Remap(), Takes a rectangle that lives inside `before`
        // and returns the corresponding rectangle that lives in `after`.
        public static RectangleF Transform(RectangleF before, RectangleF after, RectangleF rect)
        {
            PointF tf = Transform(before, after, new PointF(rect.Left, rect.Top));
            PointF tf2 = Transform(before, after, new PointF(rect.Right, rect.Bottom));
            return RectFromSides(tf.Y, tf2.Y, tf.X, tf2.X);
        }

        //----------------------------------------------------------------------
        // scales a rect's width and height where the scaling operation is
        // relative to the rect's center.
        //----------------------------------------------------------------------
        public static RectangleF Scale(RectangleF rect, float factorX, float factorY)
        {
            return new RectangleF(
                rect.Location.X - (((rect.Width * factorX) - rect.Width) / 2.0f),
                rect.Location.Y - (((rect.Height * factorY) - rect.Height) / 2.0f),
                rect.Width * factorX,
                rect.Height * factorY);
        }

        //----------------------------------------------------------------------
        // I'll give you N = N - 1 guesses what this function does.
        //----------------------------------------------------------------------
        public static void Swap<T>(ref T a, ref T b)
        {
            T local = a;
            a = b;
            b = local;
        }

        //----------------------------------------------------------------------
        // converts a GraphicsPath from "worldspace" to "localspace", i.e. it
        // starts in a space relative to `client.Parent` and ends up in a space
        // relative to `client`.
        //----------------------------------------------------------------------
        public static GraphicsPath ToClient(GraphicsPath path, Control client)
        {
            GraphicsPath path2 = path.Clone() as GraphicsPath;
            Matrix matrix = new Matrix();
            matrix.Translate((float)-client.Left, (float)-client.Top);
            path2.Transform(matrix);
            return path2;
        }

        //----------------------------------------------------------------------
        // if a rect has negative width, then negate the width.  Same for height.
        //
        // Seem strange?  Turns out negative heights occur a lot in 2D graphics,
        // usually because people tend to confuse whether (0,0) is supposed to
        // mean "upper-left" or "lower-left."  And by "people" I mean "I."
        //
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // (The answer, by the way, is "(0,0) means lower-left."  Why?  Well,
        // OpenGL adopts that convention, for one.  But the most persuasive
        // reason is as follows:
        //
        // Imagine a graph where the domain and range are both [-1.0 .. 1.0].
        // On such a graph, (0,0) is at the center.  Then imagine a graph where
        // the domain and range are [0.0 .. 1.0].  Now (0,0) is at the lower-left.
        //
        // But I've given up fighting that battle long ago, since everyone uses
        // "(0,0) is upper-left."
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //
        // You can sort of think of this operation as "taking the absolute value
        // of a rectangle." So why'd I call this "Unpretzel" instead of "Abs"? 
        // Because "taking the absolute value of a rectangle" could mean several
        // different things. It could mean:
        //
        //   Rect(rect.Location, Abs(rect.Size))
        //
        // ... Or it could mean:
        //
        //   Rect(Abs(rect.Location), Abs(rect.Size))
        // 
        // Whereas "Unpretzel" is perfectly clear:  If the rectangle is shaped
        // like a pretzel, then turn it inside-out!
        //
        // Just kidding, just kidding.  The name makes no sense to anyone but me.
        // (We physicists now.)
        //
        // ..... fine, think of it like this:  If the width or height is shaped
        // like a sock that's been turned inside-out, then turn it rightside-in.
        // But I couldn't very well call this "Unsock(rect)" now, could I?  Gawdd!
        //
        // Okay, the real reason for this big ol' comment block is because I'm
        // trying to maintain my motivation, and it was fun to write.  Selfish!
        // Here's the boooooringgggg (but simple) explanation:
        //
        //  returns Rectangle(rect.Location, Abs(rect.Size))
        //
        // (Oh who am I kidding, no one will ever read any of this but me.)
        // (And you.)
        //----------------------------------------------------------------------
        public static RectangleF Unpretzel(RectangleF rect)
        {
            float top = rect.Top;
            float bottom = rect.Bottom;
            float left = rect.Left;
            float right = rect.Right;
            if (rect.Height < 0.0f) Swap<float>(ref top, ref bottom);
            if (rect.Width  < 0.0f) Swap<float>(ref left, ref right);
            return RectFromSides(top, bottom, left, right);
        }

        //----------------------------------------------------------------------
        // remaps `at` from [left .. right] to [0.0 .. 1.0]
        //----------------------------------------------------------------------
        public static float WhereBetween(float left, float right, float at)
        {
            return ((at - left) / (right - left));
        }

        //----------------------------------------------------------------------
        // constrains `rect.Location` to be within the bounds of `container`.
        //----------------------------------------------------------------------
        public static RectangleF Within(RectangleF container, RectangleF rect)
        {
            float num = Clamp(0.0f, container.Width, (container.Width - rect.Width));
            float num2 = Clamp(0.0f, container.Height, (container.Height - rect.Height));
            float x = Clamp(container.Location.X, container.Location.X + num, rect.Location.X);
            float y = Clamp(container.Location.Y, container.Location.Y + num2, rect.Location.Y);
            return new RectangleF(x, y, rect.Width, rect.Height);
        }

        //======================================================================
        // misc.
        //======================================================================

        //----------------------------------------------------------------------
        // to keep the distinction clear in my head.  (And, incidentally, in code.)
        //----------------------------------------------------------------------
        public static RectangleF Localspace(Control control) { return control.ClientRectangle; }
        public static RectangleF Worldspace(Control control) { return control.Bounds; }
        public static RectangleF Worldspace(ToolStripItem tsi) { return tsi.Bounds; }

        //----------------------------------------------------------------------
        // returns whether your mouse is in control of your life's decisions.
        //----------------------------------------------------------------------
        public static bool MouseInControl(Control control)
        {
            // get the cursor's position relative to the control.
            Point point = control.PointToClient(Cursor.Position);

            // if the control has a region, return whether the pos is within it.
            if (control.Region != null)
                return control.Region.IsVisible(point);

            // otherwise return whether the control's local boundary contains 
            // the pos.
            return Localspace(control).Contains(point);
        }
        public static bool MouseInControl(ToolStripItem toolStripItem)
        {
            Point pt = toolStripItem.Owner.PointToClient(Cursor.Position);
            return Worldspace(toolStripItem).Contains(pt);
        }

        //======================================================================
        // actual drawing operations.
        //======================================================================
        public static class Draw
        {
            //----------------------------------------------------------------------
            // draws the dropshadow of a path.
            //----------------------------------------------------------------------
            public static void Dropshadow(Graphics g, GraphicsPath gp,
                int alpha, float offsetInPixelsX, float offsetInPixelsY)
            {
                Matrix matrix = new Matrix();
                using (GraphicsPath path = (GraphicsPath)gp.Clone())
                {
                    matrix.Translate(offsetInPixelsX, offsetInPixelsY);
                    path.Transform(matrix);
                    using (Brush brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                    {
                        g.FillPath(brush, path);
                    }
                }
            }
        }
    }
}
