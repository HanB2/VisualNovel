using System;
using OpenTK.Input;
using Minalear;
using UIPrototype.Controls;

namespace UIPrototype.Scenes
{
    public class Scene02 : UIScene
    {
        private Image image;
        private Button button;

        public Scene02() : base("Scene02")
        {
            image = new Image(@"Textures/cantbeatit.jpg");
            button = new Button(@"Textures/button.png");
            button.Position = new OpenTK.Vector2(10, 371);

            button.Click += Button_Click;

            AddChild(image);
            AddChild(button);
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            Manager.ChangeScene("Scene01");
        }
    }
}
