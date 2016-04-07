using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;

namespace DongLife.Scenes
{
    public class RenderScene : Scene
    {
        private GeoRenderer renderer;

        private Circle[] circles;
        private int index = 0;
        private float initTimer = 0f;
        
        public RenderScene() : base("RenderScene")
        {
            circles = new Circle[21 * 12];

            int i = 0;
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 21; x++)
                {
                    circles[i] = new Circle();
                    circles[i].Position = new Vector2(x * 64, y * 64);
                    circles[i].Radius = 0f;

                    i++;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            for (int i = 0; i < index; i++)
            {
                renderer.FillCircle(circles[i].Position, circles[i].Radius, 8, Color4.White);
            }
            renderer.End();

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            initTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
            if (index < circles.Length)
            {
                index += 2;
                initTimer = 0f;
            }

            for (int i = 0; i < index; i++)
            {
                circles[i].Radius += (float)gameTime.ElapsedTime.TotalSeconds * 18f;
                circles[i].Radius = MathHelper.Clamp(circles[i].Radius, 0f, 48f);
            }

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            renderer = new GeoRenderer(
                content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                Manager.Game.Window.Width, Manager.Game.Window.Height);

            base.LoadContent(content);
        }

        private struct Circle
        {
            public Vector2 Position;
            public float Radius;
        }
    }
}
