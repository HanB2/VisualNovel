using System;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Animations
{
    public class AnimationMove : Animation
    {
        private Vector2 sourcePos, destPos;

        public AnimationMove(Control control, Vector2 destination) 
            : base(control, AnimationTypes.Move)
        {
            this.sourcePos = control.Position;
            this.destPos = destination;
        }

        public override void Update(GameTime gameTime)
        {
            this.Control.Position = Vector2.Lerp(sourcePos, destPos, this.Percent);

            base.Update(gameTime);
        }

        protected override void onAnimationEnd()
        {
            this.Control.Position = this.destPos;

            base.onAnimationEnd();
        }
    }
}
