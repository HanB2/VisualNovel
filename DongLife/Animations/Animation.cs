using System;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Animations
{
    public abstract class Animation
    {
        private AnimationTypes type;
        private Control control;
        private float currentTimer;
        private float animationLength;
        private bool active, dispose;

        public Animation(Control control, AnimationTypes type)
        {
            this.control = control;
            this.type = type;

            this.currentTimer = 0f;
            this.animationLength = 1f;
            this.active = false;
            this.dispose = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.active)
            {
                this.currentTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
                if (this.currentTimer >= this.animationLength)
                {
                    this.onAnimationEnd();
                }
            }
        }
        public virtual void ExecuteAnimation(float time)
        {
            //Prevent 0 time
            this.animationLength = MathHelper.Clamp(time, 1f, time);
            this.active = true;
        }
        public virtual void ForceEndAnimation()
        {
            //Set the current timer to the animation length, this will allow the animation to end normally on the next update
            this.currentTimer = this.animationLength;
        }

        protected virtual void onAnimationEnd()
        {
            this.active = false;
            this.currentTimer = this.animationLength;
            this.dispose = true;

            AnimationEnd?.Invoke(this, this.type);
        }
        
        public event AnimationEndDelegate AnimationEnd;

        #region Properties
        public Control Control
        {
            get { return this.control; }
            set { this.control = value; }
        }
        public AnimationTypes Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public float CurrentTimer
        {
            get { return this.currentTimer; }
            set { this.currentTimer = value; }
        }
        public float AnimationLength
        {
            get { return this.animationLength; }
            set { this.animationLength = value; }
        }
        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }
        public float Percent
        {
            get
            {
                return CurrentTimer / AnimationLength;   
            }
        }
        public bool Dispose
        {
            get { return this.dispose; }
            set { this.dispose = value; }
        }
        #endregion
    }

    public enum AnimationTypes { ActorZoom, Move, Fade }
    public delegate void AnimationEndDelegate(object sender, AnimationTypes finishedMode);
}
