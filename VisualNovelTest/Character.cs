using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;

namespace VisualNovelTest
{
    public class Character
    {
        private Texture2D characterTexture;
        private Vector2 position;
        private float scale;

        private bool hasFocus = false;
        private RenderFlags flags = RenderFlags.None;

        private float timer = 0f;
        private float transitionTime = 100f;
        private bool isTransitioning = false;

        public Character(Texture2D texture)
        {
            characterTexture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterTexture, position, Color4.White, 0f, Vector2.Zero, 1f, flags);
        }
        public void Update(GameTime gameTime)
        {
            if (isTransitioning)
            {
                timer += (float)gameTime.ElapsedTime.TotalMilliseconds;
                if (timer >= transitionTime)
                {
                    isTransitioning = false;
                    timer = 0f;
                    scale = 1.5f;
                }
                else
                {
                    scale = 1.0f * (timer / transitionTime);
                }
            }
        }

        public void SetFocus(bool focus)
        {
            hasFocus = focus;

            if (focus)
            {
                flags &= ~RenderFlags.Desaturate;
                //isTransitioning = true;
                scale += 0.05f;
            }
            else
            {
                flags |= RenderFlags.Desaturate;
                scale -= 0.05f;
            }
        }

        #region Properties
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }
        public bool HasFocus
        {
            get { return this.hasFocus; }
            set { this.SetFocus(value); }
        }
        #endregion
    }
}
