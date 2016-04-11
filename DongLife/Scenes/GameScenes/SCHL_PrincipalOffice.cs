using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_PrincipalOffice : VNScene
    {
        private Actor player, prinicpal;
        private Image gun;
        private ControlAnimator gunAnimator;

        private int timesEnteredOffice = 0;

        public SCHL_PrincipalOffice() : base("SCHL_PrincipalOffice")
        {
            background = new Background(@"Textures/Backgrounds/principal_office.png");

            player = ActorFactory.CreateActor("Player");
            prinicpal = ActorFactory.CreateActor("Principal");

            gun = new Image(@"Textures/Props/sick_gun.png");
            gun.AutoSize = true;
            gun.Position = new Vector2(GameSettings.WindowWidth / 2 - 200f, GameSettings.WindowHeight / 2);
            gunAnimator = new ControlAnimator();
            gun.AddChild(gunAnimator);

            AddChild(background);
            AddChild(gun);
            RegisterActor(player);
            RegisterActor(prinicpal);

            Sequences.RegisterSequence(0, "Principal", "Thank you for joining me {PLAYERNAME}.  I don't need to say how much I am disappointed in you.");
            Sequences.RegisterSequence(1, new SequenceDecision("Player",
                "Be a punk.",
                "Kill him.",
                "Admit to everything.",
                "Hit on him."));
            ((SequenceDecision)Sequences.Sequences[1]).Choice += (sender, e) =>
            {
                if (e == 0) //Be a punk
                    Sequences.SetStage(20);
                else if (e == 1) //Kill him
                    Sequences.SetStage(10);
                else if (e == 2) //Admit to everything
                    Sequences.SetStage(30);
                else if (e == 3) //Hit on him
                    Sequences.SetStage(40);

                Sequences.ExecuteSequence(this);
            };

            //Kill him
            Sequences.RegisterSequence(10, new SequenceSpecial("PlayerPullsGun"));
            ((SequenceSpecial)Sequences.Sequences[10]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Player");
                MessageBox.SetText("Alright, you son of a bitch, time to meet your maker!");
                gunAnimator.AnimateFade(1f, 200f);

                Sequences.SetStage(11);
            };
            Sequences.RegisterSequence(11, "Principal", "What are you doing?!");
            Sequences.RegisterSequence(12, NO_ACTOR, "!BANG!");
            Sequences.RegisterSequence(13, new SequenceSpecial("PlayerKillsPrincipal"));
            ((SequenceSpecial)Sequences.Sequences[13]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Principal");
                MessageBox.SetText("BLURGH!");
                prinicpal.Animator.AnimateFade(0f, 800f);

                Sequences.SetStage(14);
            };
            Sequences.RegisterSequence(14, "Player", "That was pretty easy... Time to go home I guess.");
            Sequences.RegisterSequence(15, new SequenceSceneTransition("BASE_Home"));

            //Be a punk
            Sequences.RegisterSequence(20, "Player", "Yea, well... at least I aint no douche canoe!");
            Sequences.RegisterSequence(21, "Principal", "{PLAYERNAME}!  That is extremely insensitive and inconsiderate to my feelings!  Go to the principal's office!");
            Sequences.RegisterSequence(22, "Player", "W... what?");
            Sequences.RegisterSequence(23, "Principal", "You heard me, you little rapscallion!  Go to the principal's office!");
            Sequences.RegisterSequence(24, new SequenceDecision("Player",
                "Go to the Principal's office?",
                "Go home."));
            ((SequenceDecision)Sequences.Sequences[24]).Choice += (sender, e) =>
            {
                if (e == 0) //Go to the Principal's Office
                {
                    timesEnteredOffice++;
                    if (timesEnteredOffice == 1)
                    {
                        Manager.ChangeScene("SCHL_AlternateOffice");
                        return;
                    }
                    else
                        Sequences.SetStage(25);
                }
                else if (e == 1) //Go home
                    Sequences.SetStage(28);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(25, "Principal", "Thank you for joining me {PLAYERNAME}.  I don't need to say how much I am disappointed in you.");
            Sequences.RegisterSequence(26, "Player", "I... I don't understand...");
            Sequences.RegisterSequence(27, new SequenceStageTransition(1));
            Sequences.RegisterSequence(28, "Player", "Screw this, I'm going home!");
            Sequences.RegisterSequence(29, new SequenceSceneTransition("BASE_Home"));

            //Admit to Everything
            Sequences.RegisterSequence(30, "Player", "Alright!  I admit it!");
            Sequences.RegisterSequence(31, "Principal", "Admit to what, exactly?");
            Sequences.RegisterSequence(32, "Player", "I did it!  I'm the one that beat the shit out of that useless janitor!");
            Sequences.RegisterSequence(33, "Principal", "I mean... I saw you do it, so...");
            Sequences.RegisterSequence(34, "Player", "I am also the one that killed Mr. Takeshi at the Post Office!");
            Sequences.RegisterSequence(35, "Principal", "It's not like he didn't have it coming, but you shouldn't go around killing people...");
            Sequences.RegisterSequence(36, "Player", "I'm also the one that killed all of the Jews!  Not Hitler, me!!");
            Sequences.RegisterSequence(37, "Principal", "What?!  Holocausting is expressly forbidden in the school handbook!  Go to detention this instant, go little rapscallion!");
            Sequences.RegisterSequence(38, new SequenceSceneTransition("SCHL_Detention"));

            //Hit on him
            Sequences.RegisterSequence(40, "Player", "You're looking very dashing today, Mr. Principal.");
            Sequences.RegisterSequence(41, "Principal", "Why... thank you {PLAYERNAME}.  But we're not here for me.  We are here for you.");
            Sequences.RegisterSequence(42, "Player", "And your hair... the way it is... shaped.  It's so sensual.");
            Sequences.RegisterSequence(43, "Principal", "{PLAYERNAME}.  This is getting very... inappropriate.");
            Sequences.RegisterSequence(44, new SequenceDecision("Player",
                "Ask him out on a date.",
                "Kill him."));
            ((SequenceDecision)Sequences.Sequences[44]).Choice += (sender, e) =>
            {
                if (e == 0) //Ask him out on a date
                    Sequences.SetStage(50);
                else if (e == 1) //Kill him
                    Sequences.SetStage(10);

                Sequences.ExecuteSequence(this);
            };

            //Ask him out on a date
            Sequences.RegisterSequence(50, "Player", "Mr. Principal, I don't care if it is inappropriate.  I really like you.");
            Sequences.RegisterSequence(51, "Principal", "{PLAYERNAME}!  This is not okay!  I'm 87.  You're 32!");
            Sequences.RegisterSequence(52, "Principal", "I couldn't in the right mind reciprocate the feelings you have for me!");
            Sequences.RegisterSequence(53, "Player", "Just go on one measly date with me and you will see how amazing I am.  We are meant to be together.");
            Sequences.RegisterSequence(54, "Principal", "Oh... okay.  Where would we go for this date?");
            Sequences.RegisterSequence(55, new SequenceDecision("Player",
                "Take him to the movies.",
                "Take him to a baseball game.",
                "Take him bowling.",
                "Take him to laser tag."));
            ((SequenceDecision)Sequences.Sequences[55]).Choice += (sender, e) =>
            {
                if (e == 0) //Movie date
                    GameManager.ChosenDate = "MOVIES";
                else if (e == 1) //Baseball date
                    GameManager.ChosenDate = "BASEBALL";
                else if (e == 2) //Bowling date
                    GameManager.ChosenDate = "BOWLING";
                else if (e == 3) //Laser tag
                    GameManager.ChosenDate = "LASER";

                Sequences.SetStage(56);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(56, "Principal", "That sounds like a great idea!  Let's go, you little rapscallion!");
            Sequences.RegisterSequence(57, new SequenceSceneTransition("SCHL_Date"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            gun.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }
    }
}
