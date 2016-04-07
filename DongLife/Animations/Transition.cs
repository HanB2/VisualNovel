using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;

namespace DongLife.Animations
{
    public class Transition : IDrawable
    {
        private GeoRenderer renderer;
        private bool loadedContent = false;

        private Circle[] circles;
        private int index = 0;
        private float initTimer = 0f;
        
        private float maxRadius = 48f;

        public Transition(float radius)
        {
            int xCount = (int)(GameSettings.WindowWidth / radius) + 1;
            int yCount = (int)(GameSettings.WindowHeight / radius) + 1;

            this.circles = new Circle[xCount * yCount];

            float spacing = radius;
            this.maxRadius = radius;

            int i = 0;
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    this.circles[i] = new Circle();
                    this.circles[i].Position = new Vector2(x * spacing, y * spacing);
                    this.circles[i].Radius = 0f;

                    i++;
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            if (!loadedContent)
            {
                renderer = new GeoRenderer(
                    content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                    GameSettings.WindowWidth, GameSettings.WindowHeight);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            for (int i = 0; i < index && i < circles.Length; i++)
            {
                renderer.FillCircle(this.circles[i].Position, this.circles[i].Radius, 4, Color4.White);
            }
            renderer.End();
        }
        public void Update(GameTime gameTime)
        {
            initTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
            if (initTimer >= 1f && index < circles.Length)
            {
                index += 24;
                initTimer = 0f;
            }

            for (int i = 0; i < index && i < circles.Length; i++)
            {
                this.circles[i].Radius += 32f * (float)gameTime.ElapsedTime.TotalSeconds;
                this.circles[i].Radius = MathHelper.Clamp(this.circles[i].Radius, 0, maxRadius);
            }
        }

        public void BeginTransition()
        {
            index = 0;
            initTimer = 0f;

            for (int i = 0; i < circles.Length; i++)
                circles[i].Radius = 0f;
        }
        public bool Done()
        {
            if (this.circles.Length == 0)
                return true;
            return this.circles[this.circles.Length - 1].Radius >= maxRadius;
        }

        private struct Circle
        {
            public Vector2 Position;
            public float Radius;
        }
    }
}
