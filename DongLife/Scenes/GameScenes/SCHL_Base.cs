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
            janitor = ActorFactory.CreateActor("Janitor");
            janitorNormalPos = janitor.Position;
            principal = ActorFactory.CreateActor("Principal");

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
                janitor.Animator.AnimateSlide(janitorNormalPos, 800f);
                Sequences.SetStage(42);
            };
            Sequences.RegisterSequence(42, "Janitor", "Why thank you young man!  Boy, most kids would just kick me while I'm down, but you helped me up!  I appreciate it.");
            Sequences.RegisterSequence(43, "Player", "Don't worry m8, it's all good.  Just thought I would be kind today.");
            Sequences.RegisterSequence(44, "Janitor", "I don't recognize you... are you new to this school?");
            Sequences.RegisterSequence(45, "Player", "Yea, I am new.");
            Sequences.RegisterSequence(46, "Janitor", "Ohh, right!  I've heard of you.  You're the kid with the giant, fucking penis!");
            Sequences.RegisterSequence(47, "Player", "Yea... that's me.");
            Sequences.RegisterSequence(48, "Janitor", "It's okay man, having a freakishly long dong isn't the end of the world.  I bet some people may actually like it!");
            Sequences.RegisterSequence(49, "Player", "It's an absolute pain and only causes me trouble.  My parents are dead because of it.");
            Sequences.RegisterSequence(50, "Janitor", "That's a shame... a damn shame... I can help you with your problem, If you want.");
            Sequences.RegisterSequence(51, new SequenceDecision("Player",
                "Go to class.",
                "Keep talking to him."));
            ((SequenceDecision)Sequences.Sequences[51]).Choice += (sender, e) =>
            {
                if (e == 0) //Go to class
                    Sequences.SetStage(60);
                else if (e == 1) //Keep talking
                    Sequences.SetStage(80);

                Sequences.ExecuteSequence(this);
            };

            //Keep talking
            Sequences.RegisterSequence(60, "Player", "I appreciate your concern, but I really need to get to class.  Sorry.");
            Sequences.RegisterSequence(61, "Janitor", "You know what?  Fuck you too!  You don't just come into someone's life like that and leave all of the sudden.  Screw off, faggot.");
            Sequences.RegisterSequence(62, new SequenceSpecial("JanitorLeaves"));
            ((SequenceSpecial)Sequences.Sequences[62]).OnSequenceExecution += (sender, e) =>
            {
                Sequences.SetStage(63);
                SetActorFocus("Player");
                MessageBox.SetText("Wonder what his problem is...");
                janitor.Animator.FadeOut(800f);

                GameManager.PissedOffJanitor = true;
            };
            Sequences.RegisterSequence(63, new SequenceSceneTransition("SCHL_SchoolRoom"));

            //Keep talking
            Sequences.RegisterSequence(80, "Player", "Help me?  With... my weiner?");
            Sequences.RegisterSequence(81, "Janitor", "Yes... :)  I can help you out.  Rid you of that curse for the rest of your life!  You just need to follow me to my van behind the school.");
            Sequences.RegisterSequence(82, new SequenceDecision("Player",
                "Go with him.",
                "Go to class."));
            ((SequenceDecision)Sequences.Sequences[82]).Choice += (sender, e) =>
            {
                if (e == 0) //Go with him
                    Sequences.SetStage(83);
                else if (e == 1) //Go to class
                    Sequences.SetStage(60);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(83, "Player", "Sure, lead the way!");
            Sequences.RegisterSequence(84, new SequenceSceneTransition("SCHL_Alley"));
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
