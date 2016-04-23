using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Code;
using DongLife.Animations;

namespace DongLife.Controls
{
    public class ControlAnimator : Control
    {
        private List<Animation> activeAnimations;

        public ControlAnimator()
        {
            this.activeAnimations = new List<Animation>();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < activeAnimations.Count; i++)
            {
                this.activeAnimations[i].Update(gameTime);

                //Remove finished animation
                if (this.activeAnimations[i].Dispose)
                {
                    this.activeAnimations[i].AnimationEnd -= animationEnded;
                    this.activeAnimations.RemoveAt(i);
                    i--;
                }
            }
        }

        public void AnimateActorZoom(float amount, float time)
        {
            AnimationActorZoom animation = new AnimationActorZoom((Actor)this.Parent, amount);
            this.registerNewAnimation(animation);

            animation.ExecuteAnimation(time);
        }
        public void AnimateMove(Vector2 position, float time)
        {
            AnimationMove animation = new AnimationMove(this.Parent, position);
            this.registerNewAnimation(animation);

            animation.ExecuteAnimation(time);
        }
        public void AnimateFade(float alpha, float time)
        {
            AnimationFade animation = new AnimationFade(this.Parent, alpha);
            this.registerNewAnimation(animation);

            animation.ExecuteAnimation(time);
        }

        public void ForceEndAllAnimations()
        {
            for (int i = 0; i < this.activeAnimations.Count; i++)
            {
                this.activeAnimations[i].ForceEndAnimation();
            }
        }
        public void ForceEndAnimation(AnimationTypes type)
        {
            for (int i = 0; i < this.activeAnimations.Count; i++)
            {
                if (this.activeAnimations[i].Type == type)
                    this.activeAnimations[i].ForceEndAnimation();
            }
        }
        public void Reset()
        {
            this.activeAnimations.Clear();
        }

        private void registerNewAnimation(Animation animation)
        {
            this.activeAnimations.Add(animation);
            animation.AnimationEnd += animationEnded;
        }
        private void animationEnded(object sender, AnimationTypes type)
        {
            if (this.AnimationEnd != null)
                this.AnimationEnd(sender, type);
        }

        public event AnimationEndDelegate AnimationEnd;

        //If animation list has anything, we're still animating
        public bool Animating
        {
            get { return (this.activeAnimations.Count > 0); }
        }
    }
}
