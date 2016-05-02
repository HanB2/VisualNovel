using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
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
            Sequences.RegisterSequence(21, new SequenceSpecial("ChangeMessageBoxTheme"));
            ((SequenceSpecial)Sequences.Sequences[21]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Computer;
                Sequences.SetStage(22);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(22, new SequenceSceneTransition("CMP_Base"));

            //Talk to foster mother
            Sequences.RegisterSequence(30, "Player", "I wonder what my new mom is doing...");
            Sequences.RegisterSequence(31, new SequenceSceneTransition("MOM_Base"));

            //Kill yourself
            Sequences.RegisterSequence(40, "Player", "Well I got nothing better to do other than ending this fucking misery...");
            Sequences.RegisterSequence(41, new SequenceSpecial("KillYourself"));
            ((SequenceSpecial)Sequences.Sequences[41]).OnSequenceExecution += (sender, e) =>
            {
                player.Animator.AnimateFade(0f, 1250f);
                Sequences.SetStage(50);

                Sequences.ExecuteSequence(this);
            };
            
            Sequences.RegisterSequence(50, "Player", "Whelp, goodbye cruel world.  *blurgh*");
            Sequences.RegisterSequence(51, new SequenceSceneTransition("BEND_Suicide"));
        }

        public override void OnEnter()
        {
            if (GameManager.PissedOffJanitor)
            {
                Manager.ChangeScene("SLAVE_HomeAmbush");
            }
            else if (GameManager.BlamedJaegers)
            {
                Manager.ChangeScene("JAGR_HomeAmbush");
            }
            else
                base.OnEnter();
        }
    }
}
