using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes
{
    public class BASE_Home : VNScene
    {
        private Actor player;

        public BASE_Home() : base("BASE_Home")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            player.Position = new Vector2(GameSettings.WindowWidth / 2, player.Position.Y);

            AddChild(background);
            RegisterActor(player);

            Sequences.RegisterSequence(0, new SequenceDecision("Player",
                "Go to school.",
                "Access computer.",
                "Talk to foster mother.",
                "Kill yourself."));
            ((SequenceDecision)Sequences.Sequences[0]).Choice += (sender, e) =>
            {
                if (e == 0) //Go to school
                {
                    Sequences.SetStage(10);
                }
                else if (e == 1) //Access computer
                {
                    Sequences.SetStage(20);
                }
                else if (e == 2) //Access computer
                {
                    Sequences.SetStage(30);
                }
                else if (e == 3) //Kill yourself
                {
                    Sequences.SetStage(40);
                }

                Sequences.ExecuteSequence(this);
            };

            //Go to school
            Sequences.RegisterSequence(10, "Player", "Guess I will get my fat cock to school.");
            Sequences.RegisterSequence(11, new SequenceSceneTransition("SCHL_Base"));

            //Access computer
            Sequences.RegisterSequence(20, "Player", "Guess I could rub one out before class.");
            Sequences.RegisterSequence(21, new SequenceSceneTransition("CMP_Base"));

            //Talk to foster mother
            Sequences.RegisterSequence(30, "Player", "I wonder what my new mom is doing...");
            Sequences.RegisterSequence(31, new SequenceSceneTransition("MOM_Base"));

            //Kill yourself
            Sequences.RegisterSequence(40, "Player", "Well I got nothing better to do other than ending this fucking misery...");
            Sequences.RegisterSequence(41, new SequenceSpecial("KillYourself"));
            ((SequenceSpecial)Sequences.Sequences[41]).OnSequenceExecution += (sender, e) =>
            {
                int chance = Minalear.RNG.Next(1, 101);
                if (chance <= 50)
                {
                    player.Animator.FadeOut(2000f);
                    Sequences.SetStage(50); //Succeed
                }
                else
                    Sequences.SetStage(60); //Fail
                Sequences.ExecuteSequence(this);
            };

            //Succeed in suicide
            Sequences.RegisterSequence(50, "Player", "Whelp, goodbye cruel world.  *blurgh*");
            Sequences.RegisterSequence(51, new SequenceSceneTransition("MainMenuScene"));

            //Fail in suicide
            Sequences.RegisterSequence(60, "Player", "Whelp, goodbye cruel world... wait.");
            Sequences.RegisterSequence(61, new SequenceSceneTransition("PSYCHIATRIC_WARD"));
        }
    }
}
