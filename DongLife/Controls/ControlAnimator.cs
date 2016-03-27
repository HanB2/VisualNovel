using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Code;

namespace DongLife.Controls
{
    public class ControlAnimator : Control
    {
        private float timer = 0f;
        private float animationLength = 0f;
        private bool animating = false;
        private AnimationModes mode = AnimationModes.None;

        //Animation Variables
        private Vector2 destination;
        private Vector2 origin;

        //Shake Variables
        private int current = 0;
        private int count = 6;
        private List<Vector2> shakePoints;
        private float shakeTimer = 0f;
        private float shakeLength;

        //Zoom Variables
        private Vector2 originalSize, targetSize;
        private Actor refActor;
        private float refScale = 1f;
        private float targetScale = 1f;

        public ControlAnimator() { }

        //TODO: Allow shader values adjustment in the animator

        public override void Update(GameTime gameTime)
        {
            if (animating)
            {
                timer += (float)gameTime.ElapsedTime.TotalMilliseconds;

                //float percent = MathHelper.Clamp(timer / animationLength, 0f, 1f);
                float percent = timer / animationLength;
                if (mode == AnimationModes.Slide)
                {
                    animateSlide(percent);
                }
                else if (mode == AnimationModes.Shake)
                {
                    #region ShakeAnimation
                    shakeTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;

                    percent = MathHelper.Clamp(shakeTimer / shakeLength, 0f, 1f);
                    animateSlide(percent);

                    if (shakeTimer >= shakeLength)
                    {
                        shakeTimer = 0f;
                        current++;

                        if (current < count)
                            destination = shakePoints[current];
                    }
                    #endregion
                }
                else if (mode == AnimationModes.Zoom)
                {
                    animateZoom(percent);
                }
                else if (mode == AnimationModes.FadeIn)
                {
                    fadeIn(percent);
                }
                else if (mode == AnimationModes.FadeOut)
                {
                    fadeOut(percent);
                }
                else if (mode == AnimationModes.ActorZoom)
                {
                    actorZoom(percent);
                }

                //Timer Check
                if (timer >= animationLength)
                {
                    timer = 0f;
                    animating = false;

                    AnimationModes finishedMode = mode;
                    mode = AnimationModes.None;

                    if (AnimationEnd != null)
                        AnimationEnd(finishedMode);
                }
            }
        }

        public void AnimateShake(float intensity, float time, int count = 5)
        {
            if (animating && mode == AnimationModes.Shake)
                Parent.Position = origin;

            timer = 0f;
            current = 0;
            this.count = count + 1;
            origin = Parent.Position;

            animationLength = time + 100f;
            shakeLength = time / count;
            shakePoints = getShakePoints(Parent.Position, intensity, count);
            destination = shakePoints[current];

            mode = AnimationModes.Shake;
            animating = true;
        }
        public void AnimateSlide(Vector2 dest, float time)
        {
            timer = 0f;
            animationLength = time;
            mode = AnimationModes.Slide;
            origin = Parent.Position;
            destination = dest;

            animating = true;
        }
        public void AnimateZoom(float zoomAmount, float time)
        {
            if (animating && mode == AnimationModes.Zoom)
                Parent.Size = originalSize;

            originalSize = Parent.Size;
            targetSize = originalSize * zoomAmount;

            timer = 0f;
            animationLength = time;
            mode = AnimationModes.Zoom;

            animating = true;
        }
        public void AnimateZoom(Vector2 baseSize, float scale, float time)
        {
            originalSize = baseSize;
            targetSize = baseSize * scale;

            timer = 0f;
            animationLength = time;
            mode = AnimationModes.Zoom;

            animating = true;
        }
        public void FadeIn(float time)
        {
            timer = 0f;
            animationLength = time;
            mode = AnimationModes.FadeIn;

            animating = true;
        }
        public void FadeOut(float time)
        {
            timer = 0f;
            animationLength = time;
            mode = AnimationModes.FadeOut;

            animating = true;
        }
        public void ActorZoom(Actor actor, float zoomAmount, float time)
        {
            timer = 0f;
            animationLength = time;
            mode = AnimationModes.ActorZoom;
            refActor = actor;
            refScale = refActor.CurrentScale;
            targetScale = zoomAmount;

            animating = true;
        }

        public void ForceEndAnimation()
        {
            timer = animationLength;
        }

        private void animateSlide(float percent)
        {
            Parent.Position = Vector2.Lerp(origin, destination, percent);
        }
        private List<Vector2> getShakePoints(Vector2 origin, float intensity, int count)
        {
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < count; i++)
            {
                points.Add(origin + getRandomShakePoint() * intensity);
            }
            
            points.Add(origin);

            return points;
        }
        private Vector2 getRandomShakePoint()
        {
            return Vector2.NormalizeFast(new Vector2(RNG.NextFloat(-1f, 1f), RNG.NextFloat(-1f, 1f)));
        }
        private void animateZoom(float percent)
        {
            Parent.Size = Vector2.Lerp(originalSize, targetSize, percent);
        }
        private void actorZoom(float percent)
        {
            refActor.CurrentScale = (targetScale - refScale) * percent + refScale;
        }
        private void fadeIn(float percent)
        {
            Color4 color = Parent.DrawColor;
            Parent.DrawColor = new Color4(color.R, color.G, color.B, percent);
        }
        private void fadeOut(float percent)
        {
            Color4 color = Parent.DrawColor;
            Parent.DrawColor = new Color4(color.R, color.G, color.B, 1.0f - percent);
        }

        public delegate void AnimationEndDelegate(AnimationModes finishedMode);
        public event AnimationEndDelegate AnimationEnd;

        public enum AnimationModes { None, Slide, Shake, Zoom, FadeIn, FadeOut, ActorZoom }

        public AnimationModes Mode { get { return mode; } }
        public bool Animating { get { return animating; } }
    }
}
