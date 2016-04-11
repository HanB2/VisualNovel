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
            float alpha = (targetAlpha - sourceAlpha) * Percent + sourceAlpha;
            this.Control.SetAlpha(alpha);

            base.Update(gameTime);
        }
    }
}
