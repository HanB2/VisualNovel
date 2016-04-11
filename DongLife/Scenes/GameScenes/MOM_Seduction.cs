using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class MOM_Seduction : VNScene
    {
        private Actor player, sexyMother, father;
        private bool up = false;
        private bool fatherCameIn = false;

        private Vector2 startPos;
        private Vector2 playerStartPos;

        public MOM_Seduction() : base("MOM_Seduction")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            playerStartPos = player.Position;
            sexyMother = ActorFactory.CreateActor("SexyMother");
            sexyMother.PosY -= 60f;
            startPos = sexyMother.Position;

            player.DrawOrder = 0.5f;
            sexyMother.DrawOrder = 0.6f;

            father = ActorFactory.CreateActor("Father");

            sexyMother.Animator.AnimationEnd += Mother_AnimationEnd;
            father.Animator.AnimationEnd += Father_AnimationEnd;

            AddChild(background);
            RegisterActor(player);
            RegisterActor(sexyMother);
            RegisterActor(father);

            Sequences.RegisterSequence(0, new SequenceDecision("Player",
                "Why on earth are you naked?",
                "Hmmmm... that feels real good."));
            ((SequenceDecision)Sequences.Sequences[0]).Choice += (sender, e) =>
            {
                if (e == 0) //Why are you naked?
                    Sequences.SetStage(10);
                else if (e == 1) //HMMMMMMMMMMMMMMMM
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            Sequences.RegisterSequence(10, "Mother", "It's easier for me to move around and... get into the tighter spots :)");
            Sequences.RegisterSequence(11, new SequenceStageTransition(21));

            Sequences.RegisterSequence(20, "Mother", "Hrmmm... I'm glad you like it.");
            Sequences.RegisterSequence(21, "Mother", "My back massages are okay, but I give other... massages, if you want to try them out.");
            Sequences.RegisterSequence(22, new SequenceDecision("Player",
                "I'm... not sure this is appropriate, New Mom.",
                "Only if you apply some more lotion."));
            ((SequenceDecision)Sequences.Sequences[22]).Choice += (sender, e) =>
            {
                if (e == 0) //Not appropriate
                    Sequences.SetStage(30);
                else if (e == 1) //More lotion
                    Sequences.SetStage(39);

                Sequences.ExecuteSequence(this);
            };

            Sequences.RegisterSequence(30, "Mother", "Don't worry, baby, it's perfectly okay.  Your father is asleep, he will never find out, and no one will get hurt!  Let's just have some fun :)");
            Sequences.RegisterSequence(31, new SequenceStageTransition(40));

            Sequences.RegisterSequence(39, "Mother", "Ohhhh... you naughty little boy ;)");
            Sequences.RegisterSequence(40, "Mother", "{PLAYERNAME}!  I can't hold it anymore!  I need you...");
            Sequences.RegisterSequence(41, "Mother", "right here...");
            Sequences.RegisterSequence(42, "Mother", "right now...");
            Sequences.RegisterSequence(43, new SequenceDecision("Player",
                "I shall take you to pound town right now!",
                "I... I don't know New Mom... I have to think about this.  This is all too sudden.",
                "No."));
            ((SequenceDecision)Sequences.Sequences[43]).Choice += (sender, e) =>
            {
                if (e == 0) //Pound Town
                    Sequences.SetStage(60);
                else if (e == 1) //Wait a minute
                    Sequences.SetStage(70);
                else if (e == 2) //No
                    Sequences.SetStage(80);

                Sequences.ExecuteSequence(this);
            };

            //Pound Town
            Sequences.RegisterSequence(60, "Mother", "Oh baby, I knew you would want this hot bod.  Now... let me see your secret :)");
            Sequences.RegisterSequence(61, NO_ACTOR, "HOT AND HEAVY SEX SCENE.");
            Sequences.RegisterSequence(62, "Mother", "Ooohhhhhh, that monster of a dong was exactly what I needed.  You are a wizard with that thing!");
            Sequences.RegisterSequence(63, "Player", "Thanks.");
            Sequences.RegisterSequence(64, "Mother", "Baby... I think I'm in love with you... Run away with me!  Get me out of this hell hole of a household.  Run away with me to Cambodia!");
            Sequences.RegisterSequence(65, new SequenceDecision("Player",
                "Run away to Cambodia with your new lover.",
                "Tell her to fuck off."));
            ((SequenceDecision)Sequences.Sequences[65]).Choice += (sender, e) =>
            {
                if (e == 0) //Cambodia
                    Sequences.SetStage(66);
                else if (e == 1) //Fuck off
                    Sequences.SetStage(71);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(66, "Player", "Let's get out of here before New Dad wakes up!");
            Sequences.RegisterSequence(67, new SequenceSceneTransition("MOM_Cambodia"));

            //Wait a minute
            Sequences.RegisterSequence(70, "Mother", "Baby, trust me, it's...");
            Sequences.RegisterSequence(71, NO_ACTOR, "WHAT THE FUCK IS GOING ON HERE?!?");
            Sequences.RegisterSequence(72, new SequenceSpecial("FatherComesIn"));
            ((SequenceSpecial)Sequences.Sequences[72]).OnSequenceExecution += (sender, e) =>
            {
                father.Animator.AnimateFade(1f, 1500f);
                sexyMother.Animator.AnimateMove(startPos, 1500f);
                player.Animator.AnimateMove(playerStartPos, 1500f);

                fatherCameIn = true;
            };
            Sequences.RegisterSequence(73, "Mother", "BABY!  It's not what it looks like!");
            Sequences.RegisterSequence(74, "Father", "It looks like our new fucking rodent is flippity flopping all over my lady!");
            Sequences.RegisterSequence(75, new SequenceDecision("Player",
                "Kill him.",
                "Run away like a little bitch."));
            ((SequenceDecision)Sequences.Sequences[75]).Choice += (sender, e) =>
            {
                if (e == 0) //Kill him
                    Sequences.SetStage(90);
                else if (e == 1) //Run away
                    Sequences.SetStage(100);

                Sequences.ExecuteSequence(this);
            };

            //No hanky spanky
            Sequences.RegisterSequence(80, "Mother", "Well, I guess I can understand... if you change your mind, you know where to find me ;)");
            Sequences.RegisterSequence(81, new SequenceSceneTransition("BASE_Home"));

            //Kill your New Dad
            Sequences.RegisterSequence(90, "Player", "TIME TO DIE MO'FUCKER!");
            Sequences.RegisterSequence(91, "Father", "Wut?!");
            Sequences.RegisterSequence(92, "Father", "*BLURRRRGHHHHHH*  *Dead*");
            Sequences.RegisterSequence(93, new SequenceSpecial("FatherDead"));
            ((SequenceSpecial)Sequences.Sequences[93]).OnSequenceExecution += (sender, e) =>
            {
                father.Animator.AnimateFade(0f, 1000f);
            };
            Sequences.RegisterSequence(94, "Mother", "OH MY GOD.... He's... dead...");
            Sequences.RegisterSequence(95, "Player", "Quick baby... we must flee before the cops arrive.");
            Sequences.RegisterSequence(96, "Mother", "Where will we go?");
            Sequences.RegisterSequence(97, "Player", "To... Cambodia!");
            Sequences.RegisterSequence(98, new SequenceSceneTransition("MOM_Cambodia"));

            //Father kills you
            Sequences.RegisterSequence(100, "Father", "Oh no you don't, you little cunt!");
            Sequences.RegisterSequence(101, NO_ACTOR, "!WHAM!");
            Sequences.RegisterSequence(102, new SequenceSceneTransition("MOM_Basement"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            //Simulate animation finish to start animation
            Mother_AnimationEnd(sexyMother, Animations.AnimationTypes.Move);
            player.Position = new Vector2(GameSettings.WindowWidth / 2, player.Position.Y);
            sexyMother.Position = startPos;

            father.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }

        private void Mother_AnimationEnd(object sender, Animations.AnimationTypes finishedMode)
        {
            if (!fatherCameIn)
            {
                if (finishedMode == Animations.AnimationTypes.ActorZoom)
                {
                    sexyMother.Animator.AnimateMove(startPos, 1500f);
                }
                else if (finishedMode == Animations.AnimationTypes.Move)
                {
                    Vector2 currentPos = sexyMother.Position;

                    up = !up;
                    if (up)
                    {
                        sexyMother.Animator.AnimateMove(new Vector2(currentPos.X + 15f, currentPos.Y - 30f), 1500f);
                    }
                    else
                    {
                        sexyMother.Animator.AnimateMove(new Vector2(currentPos.X - 10f, currentPos.Y + 30f), 1500f);
                    }
                }
            }
            else
            {
                if (finishedMode == Animations.AnimationTypes.Move && Sequences.GetCurrentSequence().SequenceStage == 72)
                {
                    Sequences.SetStage(73);
                    Sequences.ExecuteSequence(this);
                }
            }
        }
        private void Father_AnimationEnd(object sender, Animations.AnimationTypes finishedMode)
        {
            if (finishedMode == Animations.AnimationTypes.Fade && Sequences.GetCurrentSequence().SequenceStage == 93)
            {
                Sequences.SetStage(94);
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
