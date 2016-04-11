using System;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Code;

namespace DongLife.Animations
{
    public class AnimationActorZoom : Animation
    {
        private Actor actor; //Local ref to actor
        private float targetScale, originalScale;

        public AnimationActorZoom(Actor actor, float amount) 
            : base(actor, AnimationTypes.ActorZoom)
        {
            this.actor = actor;
            this.targetScale = amount;
            this.originalScale = actor.CurrentScale;
        }

        public override void Update(GameTime gameTime)
        {
            //Actors scale from their normal scale
            this.actor.CurrentScale = (targetScale - originalScale) * Percent + originalScale;

            base.Update(gameTime);
        }

        protected override void onAnimationEnd()
        {
            this.actor.CurrentScale = targetScale;

            base.onAnimationEnd();
        }
    }
}
