using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Base : VNScene
    {
        private Actor player, janitor, principal;
        private Vector2 janitorNormalPos;

        public SCHL_Base() : base("SCHL_Base")
        {
            background = new Background(@"Textures/Backgrounds/school_hall.png");

            player = ActorFactory.CreateActor("Player");

            janitor = new Actor("Janitor", @"Textures/Actors/janitor_normal.png");
            janitor.Position = new Vector2(GameSettings.WindowWidth / 2 + 350f, GameSettings.WindowHeight / 2 + 75f);
            janitorNormalPos = janitor.Position;
            janitor.CurrentScale = 0.65f;
            janitor.NormalScale = 0.65f;
            janitor.FocusScale = 0.75f;

            principal = new Actor("Principal", @"Textures/Actors/principal_normal.png");
            principal.Position = new Vector2(GameSettings.WindowWidth / 2 + 100f, GameSettings.WindowHeight / 2 + 75f);
            principal.CurrentScale = 0.75f;
            principal.NormalScale = 0.75f;
            principal.FocusScale = 0.9f;

            AddChild(background);
            RegisterActor(player);
            RegisterActor(janitor);
            RegisterActor(principal);

            Sequences.RegisterSequence(0, NO_ACTOR, "*You're going to be late for class*");
            Sequences.RegisterSequence(1, "Player", "I'm going to be late for class!");
            Sequences.RegisterSequence(2, NO_ACTOR, "*In your haste to get to class, you accidently bump into the school janitor, knocking him down*");

            Sequences.RegisterSequence(3, new SequenceSpecial("BumpIntoJanitor"));
            ((SequenceSpecial)Sequences.Sequences[3]).OnSequenceExecution += (sender, e) =>
            {
                janitor.Animator.FadeIn(800f);
                Sequences.SetStage(4);
            };
            
            Sequences.RegisterSequence(4, "Player", "Oh shit!");
            Sequences.RegisterSequence(5, new SequenceDecision("Player",
                "Help him up.",
                "Kick him while he's down.",
                "Ignore him and head to class."));
            ((SequenceDecision)Sequences.Sequences[5]).Choice += (sender, e) =>
            {
                if (e == 0) //Help him
                    Sequences.SetStage(40);
                else if (e == 1) //Kick him
                    Sequences.SetStage(20);
                else if (e == 2) //Ignore him
                    Sequences.SetStage(6);

                Sequences.ExecuteSequence(this);
            };

            //Ignore him and go to class
            Sequences.RegisterSequence(6, "Player", "I don't have time for this shit, I have to get to class!");
            Sequences.RegisterSequence(7, new SequenceSceneTransition("SCHL_SchoolRoom"));

            //Kick the janitor
            Sequences.RegisterSequence(20, "Player", "Take this, you useless waste of a human being!");
            Sequences.RegisterSequence(21, NO_ACTOR, "*You land several mighty kicks into the poor janitor's ribcage.  Being used to such treatment, he just sits there and takes it.*");
            Sequences.RegisterSequence(22, "Principal", "Stop that this instant, you rapscallion!");
            Sequences.RegisterSequence(23, new SequenceSpecial("PrincipalShowsUp"));
            ((SequenceSpecial)Sequences.Sequences[23]).OnSequenceExecution += (sender, e) =>
            {
                principal.Animator.FadeIn(800f);
                Sequences.SetStage(24);
            };
            Sequences.RegisterSequence(24, "Player", "Who the hell are you?");
            Sequences.RegisterSequence(25, "Principal", "I'm the principal, you little rapscallion!  Meet me in my office!");
            Sequences.RegisterSequence(26, new SequenceSpecial("PrincipalLeaves"));
            ((SequenceSpecial) Sequences.Sequences[26]).OnSequenceExecution += (sender, e) =>
            {
                principal.Animator.FadeOut(800f);
                janitor.Animator.FadeOut(800f);
                Sequences.SetStage(27);
            };
            Sequences.RegisterSequence(27, new SequenceDecision("Player",
                "Follow the principal to his office.",
                "Ignore him and go to class."));
            ((SequenceDecision)Sequences.Sequences[27]).Choice += (sender, e) =>
            {
                if (e == 0) //Go to office
                    Manager.ChangeScene("SCHL_PrincipalOffice");
                else if (e == 1) //Go to class
                    Manager.ChangeScene("SCHL_SchoolRoom");
            };

            //Help the janitor
            Sequences.RegisterSequence(40, "Player", "Oh!  I am so sorry about that.  Here, let me help you up.");
            Sequences.RegisterSequence(41, new SequenceSpecial("HelpJanitorUp"));
            ((SequenceSpecial)Sequences.Sequences[41]).OnSequenceExecution += (sender, e) =>
            {
                janitor.Animator.AnimateSlide(new Vector2(), 800f);
                Sequences.SetStage(42);
            };
            Sequences.RegisterSequence(42, "Janitor", "Why thank you young man!  Boy, most kids would just kick me while I'm down, but you helped me up!  I appreciate it.");
            Sequences.RegisterSequence(43, "Player", "Don't worry m8, it's all good.  Just thought I would be kind today.");
            Sequences.RegisterSequence(44, "Janitor", "I don't recognize... are you new to this school?");
            Sequences.RegisterSequence(45, "Player", "Yea, I am new.");
            Sequences.RegisterSequence(46, "Janitor", "Ohh, right!  I've heard of you.  You're the kid with the giant, fucking penis!");
            Sequences.RegisterSequence(47, "Player", "Yea... that's me.");

            Sequences.RegisterSequence(47, new SequenceDecision("Player",
                "Go to class.",
                "Keep talking to him."));

        }

        public override void OnEnter()
        {
            base.OnEnter();

            janitor.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            janitor.Position = new Vector2(janitor.PosX, GameSettings.WindowHeight - 25f);
            principal.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }
    }
}
