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
        private int timesToWait = 1;

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
                father.Animator.FadeOut(1000f);
                //player.Animator.AnimateSlide(new Vector2(GameSettings.WindowWidth / 2, player.PosY), 1250f);
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
                        kingMole.Animator.FadeIn(800f);
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
            Sequences.RegisterSequence(44, "Player", "Hand over the briefcase, chief.");
            Sequences.RegisterSequence(45, new SequenceSpecial("BriefcaseOpened"));
            ((SequenceSpecial)Sequences.Sequences[45]).OnSequenceExecution += (sender, e) =>
            {
                briefcase.Visible = false;
                Sequences.SetStage(46);
            };
            Sequences.RegisterSequence(46, "KingMole", "Thank you!  Now, my minions, rise out of the earth for we will conquer the earth!");
            Sequences.RegisterSequence(47, new SequenceSpecial("MolePeopleAppear"));
            ((SequenceSpecial)Sequences.Sequences[47]).OnSequenceExecution += (sender, e) =>
            {
                molePeople.Animator.FadeIn(800f);
                Sequences.SetStage(48);
            };
            Sequences.RegisterSequence(48, "Player", "Oh boy...");
            Sequences.RegisterSequence(49, new SequenceSpecial("NuclearFallout"));
            ((SequenceSpecial)Sequences.Sequences[49]).OnSequenceExecution += (sender, e) =>
            {
                nuclearAnimator.FadeIn(800f);
                Sequences.SetStage(50);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(50, NO_ACTOR, "Through nuclear attrition, the mole people eventually conquer the surface and claim it for themselves.");
            Sequences.RegisterSequence(51, new SequenceSpecial("NuclearFallout"));
            ((SequenceSpecial)Sequences.Sequences[51]).OnSequenceExecution += (sender, e) =>
            {
                nuclearAnimator.FadeOut(800f);
                Sequences.SetStage(52);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(52, "KingMole", "Thank you human for your help in this endeavour, it was a long and arduous process, but we finally killed the last remaining human.");
            Sequences.RegisterSequence(53, "KingMole", "...well... almost the last one.");
            Sequences.RegisterSequence(54, "Player", "What do you mean?");
            Sequences.RegisterSequence(55, "KingMole", "I mean, you filthy humans all need to die for us Mole people to live peacefully.  I have no option but to kill you.");
            Sequences.RegisterSequence(56, new SequenceSceneTransition("BEND_SandCoffin"));
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

        private void Father_AnimationEnd(ControlAnimator.AnimationModes finishedMode)
        {
            if (finishedMode == ControlAnimator.AnimationModes.FadeOut && Sequences.GetCurrentSequence().SequenceStage == 1)
            {
                Sequences.SetStage(2);
                Sequences.ExecuteSequence(this);
            }
        }
        private void KingMole_AnimationEnd(ControlAnimator.AnimationModes finishedMode)
        {
            if (finishedMode == ControlAnimator.AnimationModes.FadeIn && Sequences.GetCurrentSequence().SequenceStage == 3)
            {
                Sequences.SetStage(20);
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
