using System;
using System.Collections.Generic;
using Minalear;
using OpenTK;

namespace DongLife.Scenes.EndScenes
{
    public class GEND_TimeParadox : EndScene
    {
        private Texture2D shiaHead;
        private List<ShiaHead> shiaPositions;

        private float spawnTimer = 0f;
        private float crashTimer = 5000f;

        public GEND_TimeParadox() : base(
            "GEND_TimeParadox", true, "YOU CREATED A TIME PARADOX!")
        {
            shiaPositions = new List<ShiaHead>();
        }

        public override void Update(GameTime gameTime)
        {
            crashTimer -= (float)gameTime.ElapsedTime.TotalMilliseconds;
            if (crashTimer <= 0f)
                throw new TimeParadoxException("YOU CREATED A TIME PARADOX!");

            spawnTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
            if (spawnTimer > 450f)
            {
                spawnTimer = 0f;
                spawnNewShiaHead();
            }

            for (int i = 0; i < shiaPositions.Count; i++)
            {
                shiaPositions[i].Opacity = MathHelper.Clamp(
                    shiaPositions[i].Opacity + 0.1f * (float)gameTime.ElapsedTime.TotalMilliseconds,
                    0f, 1f);
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < shiaPositions.Count; i++)
            {
                spriteBatch.Draw(shiaHead, shiaPositions[i].Position, new OpenTK.Graphics.Color4(1f, 1f, 1f, shiaPositions[i].Opacity));
            }
        }

        public override void LoadContent(ContentManager content)
        {
            shiaHead = content.LoadTexture2D(@"Textures/Props/shia_head.png");

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            shiaHead.Dispose();

            base.UnloadContent();
        }

        private void spawnNewShiaHead()
        {
            ShiaHead head = new ShiaHead();
            head.Position = new Vector2(
                RNG.NextFloat(0, GameSettings.WindowWidth),
                RNG.NextFloat(0, GameSettings.WindowHeight));
            head.Opacity = 0f;

            shiaPositions.Add(head);
        }

        private class ShiaHead
        {
            public Vector2 Position;
            public float Opacity;
        }
    }

    public class TimeParadoxException : Exception
    {
        public TimeParadoxException() : base() { }
        public TimeParadoxException(string msg) : base(msg) { }
        public TimeParadoxException(string msg, Exception e) : base(msg, e) { }
    }
}
