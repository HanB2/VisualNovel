using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using DongLife.Animations;

namespace DongLife.Scenes
{
    public class RenderScene : Scene
    {
        private Transition transition;
        private bool transitioning = true;
        
        public RenderScene() : base("RenderScene")
        {
            transition = new Transition(16f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (transitioning)
                transition.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            if (transitioning)
                transition.Update(gameTime);

            transitioning = !transition.Done();

            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            transition.LoadContent(content);

            base.LoadContent(content);
        }
    }
}
