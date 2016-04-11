using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class MOM_Basement : VNScene
    {
        private Actor player, father, kingMole, molePeople;

        private Image briefcase;
        private Image nuclearFallout;

        private ControlAnimator nuclearAnimator;

        private int timesScreamed = 0;
        private int timesWaited = 0;
        private int timesToWait = 3;

        public MOM_Basement() : base("MOM_Basement")
        {
            background = new Background(@"Textures/Backgrounds/basement.png");

            player = ActorFactory.CreateActor("Player");
            father = ActorFactory.CreateActor("Father");
            kingMole = new Actor("KingMole", @"Textures/Actors/mole_king.png");
            kingMole.Position = new Vector2(GameSettings.WindowWidth / 2 + 250f, GameSettings.WindowHeight / 2 + 50f);
            kingMole.NormalScale = 0.35f;
            kingMole.FocusScale = 0.4f;
            kingMole.CurrentScale = 0.25f;

            molePeople = new Actor("MolePeople", @"Textures/Actors/mole_people.png");
            molePeople.Position = new Vector2(GameSettings.WindowWidth / 2, GameSettings.WindowHeight / 2);
            molePeople.NormalScale = 1f;
            molePeople.FocusScale = 1f;
            molePeople.CurrentScale = 1f;
            molePeople.DrawOrder = 0.8f;

            father.Animator.AnimationEnd += Father_AnimationEnd;
            kingMole.Animator.AnimationEnd += KingMole_AnimationEnd;

            briefcase = new Image(@"Textures/Props/briefcase.png");
            briefcase.Position = kingMole.Position;
            briefcase.DrawOrder = 0.4f;
            briefcase.Size = new Vector2(244, 133);
            briefcase.PosX += 120f;
            briefcase.PosY += 80f;
            briefcase.AutoSize = false;
            briefcase.Visible = false;

            nuclearFallout = new Image(@"Textures/Props/nuclear_fallout.png");
            nuclearFallout.Position = Vector2.Zero;
            nuclearFallout.AutoSize = false;
            nuclearFallout.Size = new Vector2(GameSettings.WindowWidth, GameSettings.WindowHeight);
            nuclearFallout.DrawOrder = 0.1f;

            nuclearAnimator = new ControlAnimator();
            nuclearFallout.AddChild(nuclearAnimator);

            AddChild(background);
            AddChild(briefcase);
            AddChild(nuclearFallout);
            RegisterActor(player);
            RegisterActor(father);
            RegisterActor(kingMole);
            RegisterActor(molePeople);

            Sequences.RegisterSequence(0, "Father", "And you can stay down here until you die, you fucking trashbag of a human being.");
            Sequences.RegisterSequence(1, new SequenceSpecial("FatherLeaves"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                father.Animator.AnimateFade(0f, 1000f);
                //player.Animator.AnimateMove(new Vector2(GameSettings.WindowWidth / 2, player.PosY), 1250f);
            };
            Sequences.RegisterSequence(2, "Player", "Well... that sucks.  Wonder what I'll do now.");
            Sequences.RegisterSequence(3, new SequenceDecision("Player",
                "Scream for help.",
                "Wait out the inevitable."));
            ((SequenceDecision)Sequences.Sequences[3]).Choice += (sender, e) =>
            {
                #region WaitingRegion
                if (e == 0) //Scream for help
                {
                    timesScreamed++;
                    if (timesScreamed == timesToWait)
                    {
                        kingMole.Animator.AnimateFade(1f, 800f);
                    }
                    else
                    {
                        player.SetFocus(false);
                        MessageBox.SetText(
                            String.Format("Your screams for help fall on deaf ears...{0}",
                            new string('.', timesScreamed * 2)));
                        Sequences.SetStage(3);
                    }
                }
                else if (e == 1) //Waiting
                {
                    timesWaited++;
                    if (timesWaited == timesToWait)
                    {
                        Sequences.SetStage(10);
                        Sequences.ExecuteSequence(this);
                    }
                    else
                    {
                        player.SetFocus(false);
                        MessageBox.SetText(
                            String.Format("You sit in the corner quietly waiting the inevitable...{0}", 
                            new string('.', timesWaited * 2)));
                        Sequences.SetStage(3);
                    }
                }
                #endregion
            };

            //Waited long enough
            Sequences.RegisterSequence(10, new SequenceSceneTransition("BEND_DeadInBasement"));

            //Screamed enough
            Sequences.RegisterSequence(20, "KingMole", "You raaang?");
            Sequences.RegisterSequence(21, "Player", "WHAT THE FUCK ARE YOU?!?");
            Sequences.RegisterSequence(22, "KingMole", "I am King Mole, silly.  King of the Mole People!");
            Sequences.RegisterSequence(23, "KingMole", "Now, I will offer you this piece of advice.  Don't insult me again or you will suffer the consequences.");
            Sequences.RegisterSequence(24, new SequenceDecision("Player",
                "Who are you?",
                "What are you?"));
            ((SequenceDecision)Sequences.Sequences[24]).Choice += (sender, e) =>
            {
                if (e == 0) //Who are you
                    Sequences.SetStage(30);
                else if (e == 1) //What are you
                    Sequences.SetStage(25);

                Sequences.ExecuteSequence(this);
            };

            //What are you
            Sequences.RegisterSequence(25, "KingMole", "I told you...");
            Sequences.RegisterSequence(26, "KingMole", "DON'T INSULT THE KING!!!");
            Sequences.RegisterSequence(27, new SequenceSceneTransition("BEND_SandCoffin"));

            //Who are you
            Sequences.RegisterSequence(30, "KingMole", "Like I said earlier... I am the Mole King, King of the Mole People.");
            Sequences.RegisterSequence(31, "Player", "What are you doing here?");
            Sequences.RegisterSequence(32, "KingMole", "I heard an earth dweller screaming and thought I would check it out.  What are YOU doing here?");
            Sequences.RegisterSequence(33, "Player", "My foster father locked me down here for skadoozling his wife.");
            Sequences.RegisterSequence(34, "KingMole", "Very nice!  As King of the Moles, I am allowed to sodomize any of my followers.  I think I like you, human.");
            Sequences.RegisterSequence(35, "Player", "Thank you, do you think you can help me out of here?");
            Sequences.RegisterSequence(36, "KingMole", "Hrmmm... I could help you... but only if you help me out :)");
            Sequences.RegisterSequence(37, "Player", "What do you need?");
            Sequences.RegisterSequence(38, "KingMole", "Well, since I am a Mole, I do not have opposable thumbs.  I need you to open this case.");
            Sequences.RegisterSequence(39, new SequenceSpecial("BriefcaseAppears"));
            ((SequenceSpecial)Sequences.Sequences[39]).OnSequenceExecution += (sender, e) =>
            {
                briefcase.Visible = true;
                Sequences.SetStage(40);
            };
            Sequences.RegisterSequence(40, "Player", "What's in the case?");
            Sequences.RegisterSequence(41, "KingMole", "Nuclear launch codes.");
            Sequences.RegisterSequence(42, "Player", "WHAT?!");
            Sequences.RegisterSequence(43, "KingMole", "I plan on conquering the surface for the mole people.  If you help me with this small request, I will liberate you from this basement.  However, if you do not, I will have to eliminate you.");
            Sequences.RegisterSequence(44, new SequenceDecision("Player",
                "Help the Mole King.",
                "Don't help the Mole King."));
            ((SequenceDecision)Sequences.Sequences[44]).Choice += (sender, e) =>
            {
                if (e == 0) //Help
                    Sequences.SetStage(45);
                else if (e == 1) //Don't help
                    Sequences.SetStage(100);

                Sequences.ExecuteSequence(this);
            };

            Sequences.RegisterSequence(45, "Player", "Hand over the briefcase, chief.");
            Sequences.RegisterSequence(46, new SequenceSpecial("BriefcaseOpened"));
            ((SequenceSpecial)Sequences.Sequences[46]).OnSequenceExecution += (sender, e) =>
            {
                briefcase.Visible = false;
                Sequences.SetStage(47);
            };
            Sequences.RegisterSequence(47, "KingMole", "Thank you!  Now, my minions, rise out of the earth for we will conquer the earth!");
            Sequences.RegisterSequence(48, new SequenceSpecial("MolePeopleAppear"));
            ((SequenceSpecial)Sequences.Sequences[48]).OnSequenceExecution += (sender, e) =>
            {
                molePeople.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(49);
            };
            Sequences.RegisterSequence(49, "Player", "Oh boy...");
            Sequences.RegisterSequence(50, new SequenceSpecial("NuclearFallout"));
            ((SequenceSpecial)Sequences.Sequences[50]).OnSequenceExecution += (sender, e) =>
            {
                nuclearAnimator.AnimateFade(1f, 800f);
                Sequences.SetStage(51);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(51, NO_ACTOR, "Through nuclear attrition, the mole people eventually conquer the surface and claim it for themselves.");
            Sequences.RegisterSequence(52, new SequenceSpecial("NuclearFallout"));
            ((SequenceSpecial)Sequences.Sequences[52]).OnSequenceExecution += (sender, e) =>
            {
                nuclearAnimator.AnimateFade(0f, 800f);
                Sequences.SetStage(53);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(53, "KingMole", "Thank you human for your help in this endeavour, it was a long and arduous process, but we finally killed the last remaining human.");
            Sequences.RegisterSequence(54, "KingMole", "...well... almost the last one.");
            Sequences.RegisterSequence(55, "Player", "What do you mean?");
            Sequences.RegisterSequence(56, "KingMole", "I mean, you filthy humans all need to die for us Mole people to live peacefully.  I have no option but to kill you.");
            Sequences.RegisterSequence(57, new SequenceDecision("Player",
                "Try to overthrow the Mole King.",
                "Plead with the Mole King."));
            ((SequenceDecision)Sequences.Sequences[57]).Choice += (sender, e) =>
            {
                if (e == 0) //Overthrow
                    Sequences.SetStage(70);
                else if (e == 1) //Plead for life
                    Sequences.SetStage(60);

                Sequences.ExecuteSequence(this);
            };

            //Plead for life
            Sequences.RegisterSequence(60, "Player", "Oh great and might Mole King, I request you spare my pathetic life so I may live for another day!");
            Sequences.RegisterSequence(61, "KingMole", "You're right.  Your life is pathetic.  You're too ugly to serve as a sex slave, so I will just end your misery.");
            Sequences.RegisterSequence(62, new SequenceStageTransition(101));

            //Overthrow
            Sequences.RegisterSequence(70, "Player", "Listen to me, denizens of the underworld!");
            Sequences.RegisterSequence(71, "KingMole", "What are you doing?!");
            Sequences.RegisterSequence(72, "Player", "Your king is a pathetic worm that is not worthy to lead such an amazing race such as you!");
            Sequences.RegisterSequence(73, "KingMole", "Stop it!");
            Sequences.RegisterSequence(74, "Player", "I will prove my worth to you that I would be a much superior king than him!");
            Sequences.RegisterSequence(75, new SequenceDecision("Player",
                "Whip out your dick to show you're a superior being.",
                "Promise them that they will be treated as your equals under your regime."));
            ((SequenceDecision)Sequences.Sequences[75]).Choice += (sender, e) =>
            {
                if (e == 0) //Whip out the dong
                    Sequences.SetStage(76);
                else if (e == 1) //False promises
                    Sequences.SetStage(110);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(76, "Player", "Look at my magnificent dong!  I could anally please all of you!");
            Sequences.RegisterSequence(77, "KingMole", "NOOOOO!!!!");
            Sequences.RegisterSequence(78, new SequenceSceneTransition("GEND_DongMolePeople")); 

            //Don't help the Mole King
            Sequences.RegisterSequence(100, "KingMole", "Well, that's a shame.  We'll have to find another hapless sap to help us.  For you, however, I'll just have to kill you.");
            Sequences.RegisterSequence(101, new SequenceSceneTransition("BEND_SandCoffin"));

            //Fail to overthrow king
            Sequences.RegisterSequence(110, "KingMole", "LOL!  You think that will convince them?  Time to die, cretin!");
            Sequences.RegisterSequence(111, new SequenceSceneTransition("BEND_SandCoffin"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            father.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 1f);
            kingMole.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            molePeople.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            briefcase.Visible = false;
            nuclearFallout.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }

        private void Father_AnimationEnd(object sender, Animations.AnimationTypes finishedMode)
        {
            if (finishedMode == Animations.AnimationTypes.Fade && Sequences.GetCurrentSequence().SequenceStage == 1)
            {
                Sequences.SetStage(2);
                Sequences.ExecuteSequence(this);
            }
        }
        private void KingMole_AnimationEnd(object sender, Animations.AnimationTypes finishedMode)
        {
            if (finishedMode == Animations.AnimationTypes.Fade && Sequences.GetCurrentSequence().SequenceStage == 3)
            {
                Sequences.SetStage(20);
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
