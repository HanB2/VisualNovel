using System;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using System.Drawing;
using OpenTK.Input;

namespace DongLife.Controls.Computer
{
    public class Window : Control
    {
        private Icon closeButton;

        public Window()
        {
            closeButton = new Icon(new RectangleF(875, 104, 33, 24));
        }

        public void RegisterIcon(Icon icon)
        {
            AddChild(icon);
        }

        public Icon CloseButton
        {
            get { return this.closeButton; }
        }
    }
}
