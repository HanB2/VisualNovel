using System;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using DongLife.Controls;
using DongLife.Code;
using OpenTK.Input;

namespace DongLife.Scenes
{
    public class MainMenuScene : Scene
    {
        private Image background;
        private Image earthImage;
        private Image titleText;
        private Image beginText;
        
        private ControlAnimator earthAnimator;
        private ControlAnimator titleAnimator;
        private ControlAnimator beginAnimator;

        public MainMenuScene() : base("MainMenuScene")
        {
            background = new Image(@"Textures/Intro/background.png");
            background.Position = Vector2.Zero;
            background.Size = new Vector2(GameSettings.WindowWidth, GameSettings.WindowHeight);
            background.DrawOrder = 1f;

            earthImage = new Image(@"Textures/Intro/earth.png");
            earthImage.Position = new Vector2(-750f, -200f);
            earthImage.AutoSize = true;
            earthImage.DrawOrder = 0.5f;
            earthAnimator = new ControlAnimator();
            earthImage.AddChild(earthAnimator);

            earthAnimator.AnimationEnd += EarthAnimator_AnimationEnd;

            titleText = new Image(@"Textures/Intro/title_text.png");
            titleText.Position = new Vector2(75f, 75f);
            titleText.Size = new Vector2(GameSettings.WindowWidth, GameSettings.WindowHeight);
            titleText.DrawOrder = 0.6f;
            titleAnimator = new ControlAnimator();
            titleText.AddChild(titleAnimator);

            beginText = new Image(@"Textures/Intro/begin_text.png");
            beginText.DrawColor = new Color4(1f, 1f, 1f, 0f);
            beginText.Position = new Vector2(499f, 637f);
            beginText.Size = new Vector2(282f, 46f);
            beginText.DrawOrder = 0f;
            beginAnimator = new ControlAnimator();
            beginText.AddChild(beginAnimator);

            AddChild(background);
            AddChild(titleText);
            AddChild(earthImage);
            AddChild(beginText);
        }

        private void EarthAnimator_AnimationEnd(ControlAnimator.AnimationModes finishedMode)
        {
            beginAnimator.FadeIn(3500f);
        }

        public override void OnEnter()
        {
            //Reset Positions
            background.Position = Vector2.Zero;
            earthImage.Position = new Vector2(-750f, -200f);
            titleText.Position = new Vector2(75f, 75f);

            //ANIMATE
            earthAnimator.AnimateSlide(new Vector2(150, 92), 15000f);
            titleAnimator.AnimateSlide(new Vector2(0, 0), 15000f);

            base.OnEnter();
        }

        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (earthAnimator.Animating || titleAnimator.Animating)
            {
                earthAnimator.ForceEndAnimation();
                titleAnimator.ForceEndAnimation();

                beginAnimator.FadeIn(1f);
            }
            else
            {
                Manager.ChangeScene("IN_HospitalScene");
            }
        }
    }
}
