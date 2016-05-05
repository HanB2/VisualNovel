using System;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using System.Drawing;
using OpenTK.Input;

namespace DongLife.Controls.Computer
{
    public class Icon : Control
    {
        private double doubleClickTimer = 0.0;
        private const double DOUBLE_CLICK_DELAY = 400.0;
        private bool singleClick = false;

        public Icon(RectangleF bounds)
        {
            this.Bounds = bounds;
        }
        public Icon(int x, int y, int w, int h)
        {
            this.Bounds = new RectangleF(x, y, w, h);
        }

        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (singleClick)
                DoubleClick?.Invoke(this, e);
            else
            {
                singleClick = true;
                SingleClick?.Invoke(this, e);
            }

            base.OnMouseUp(e);
        }
        public override void OnMouseEnter()
        {
            base.OnMouseEnter();
        }

        public override void Update(GameTime gameTime)
        {
            if (singleClick)
            {
                doubleClickTimer += gameTime.ElapsedTime.TotalMilliseconds;
                if (doubleClickTimer > DOUBLE_CLICK_DELAY)
                {
                    doubleClickTimer = 0.0;
                    singleClick = false;
                }
            }

            base.Update(gameTime);
        }

        public event ButtonPressedDelegate SingleClick;
        public event ButtonPressedDelegate DoubleClick;
    }
}
