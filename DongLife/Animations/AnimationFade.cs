using System;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Animations
{
    public class AnimationFade : Animation
    {
        private float sourceAlpha = 0f;
        private float targetAlpha = 1f;

        public AnimationFade(Control control, float targetAlpha)
            : base(control, AnimationTypes.Fade)
        {
            this.targetAlpha = targetAlpha;
            this.sourceAlpha = this.Control.DrawColor.A;
        }

        public override void Update(GameTime gameTime)
        {
            //Actor fading in seems to be broken at the moment for no immediate reason
            float alpha = (targetAlpha - sourceAlpha) * Percent + sourceAlpha;
            this.Control.SetAlpha(alpha);

            base.Update(gameTime);
        }

        protected override void onAnimationEnd()
        {
            this.Control.SetAlpha(targetAlpha);

            base.onAnimationEnd();
        }
    }
}
