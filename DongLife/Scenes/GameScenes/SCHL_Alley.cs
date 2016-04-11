using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Alley : VNScene
    {
        private Actor player, janitor;

        public SCHL_Alley() : base("SCHL_Alley")
        {
            background = new Background(@"Textures/Backgrounds/alley.png");

            player = ActorFactory.CreateActor("Player");
            janitor = ActorFactory.CreateActor("Janitor");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(janitor);

            Sequences.RegisterSequence(0, "Player", "This alley looks... suspicious.");
            Sequences.RegisterSequence(1, "Janitor", "Don't worry man!  It's all okay.");
            Sequences.RegisterSequence(2, "Player", "This doesn't even look like the school...");
            Sequences.RegisterSequence(3, "Janitor", "Calm down, it's okay!  You're okay, I'm okay!  Now, do you want your dick fixed or not?");
            Sequences.RegisterSequence(4, "Player", "Yea, that's what I came here for.");
            Sequences.RegisterSequence(5, "Janitor", "okay okay okay, listen to me... I know how to fix your dick, because I had the same issue.");
            Sequences.RegisterSequence(6, "Player", "Really?");
            Sequences.RegisterSequence(7, "Janitor", "Yea, I am you.");
            Sequences.RegisterSequence(8, "Player", "...");
            Sequences.RegisterSequence(9, "Player", "What?");
            Sequences.RegisterSequence(10, "Janitor", "I'm you.  From the future.");
            Sequences.RegisterSequence(11, "Player", "You're kidding me, right?");
            Sequences.RegisterSequence(12, "Janitor", "Dude... look at you and then look at me and then decide.  We are the same!");
            Sequences.RegisterSequence(13, new SequenceDecision("Player",
                "Believe him.",
                "Don't believe him."));
            ((SequenceDecision)Sequences.Sequences[13]).Choice += (sender, e) =>
            {
                if (e == 0) //Believe
                    Sequences.SetStage(40);
                else if (e == 1) //Don't believe
                {
                    GameManager.PissedOffJanitor = true;
                    Sequences.SetStage(20);
                }

                Sequences.ExecuteSequence(this);
            };

            //Don't believe him
            Sequences.RegisterSequence(20, "Player", "This shit is a waste of time.  See'ya weirdo.");
            Sequences.RegisterSequence(21, "Janitor", "You mother fucking piece of shit!  I poor my heart out to you, and what?!  You just up and leave me?  Fuck you!");
            Sequences.RegisterSequence(22, "Player", "Screw you!  You made me miss my class.  I'm going home.");
            Sequences.RegisterSequence(23, "Janitor", "Yea whatever, go fuck yourself!");
            Sequences.RegisterSequence(24, new SequenceSceneTransition("BASE_Home"));

            //Believe him
            Sequences.RegisterSequence(40, "Player", "Holy shit... you may be right.");
            Sequences.RegisterSequence(41, "Janitor", "Right I am!  Now, all you have to do is travel into the future.");
            Sequences.RegisterSequence(42, "Janitor", "My van here... see, it's a time machine.  All you have to do is climb in!");
            Sequences.RegisterSequence(43, "Player", "It looks like a regular ass van.");
            Sequences.RegisterSequence(44, "Janitor", "Yes!  Exactly!  Urban camouflage!");
            Sequences.RegisterSequence(45, "Player", "I don't know...");
            Sequences.RegisterSequence(46, "Janitor", "Stop being a pussy and just get in the damn van!");
            Sequences.RegisterSequence(47, "Player", "Well... if you insist.");
            Sequences.RegisterSequence(48, new SequenceSpecial("PlayerClimbsIn"));
            ((SequenceSpecial)Sequences.Sequences[48]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus(NO_ACTOR);
                player.Animator.AnimateFade(0f, 800f);
                janitor.Animator.AnimateFade(0f, 800f);

                MessageBox.SetText("*As you entered the van from behind, you feel a heavy, blunt object smack into your head.  You are out cold*");
                Sequences.SetStage(49);
            };
            Sequences.RegisterSequence(49, new SequenceSceneTransition("SLAVE_Base"));
        }
    }
}
