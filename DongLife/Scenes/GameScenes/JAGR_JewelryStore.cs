using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class JAGR_JewelryStore : VNScene
    {
        private Actor player, cop;

        public JAGR_JewelryStore() : base("JAGR_JewelryStore")
        {
            background = new Background(@"Textures/Backgrounds/jewelry_store.png");

            player = ActorFactory.CreateActor("Player");
            cop = ActorFactory.CreateActor("Cop");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(cop);

            Sequences.RegisterSequence(0, "Player", "Alright, time to start stealing shit... though should I go through with this?");
            Sequences.RegisterSequence(1, new SequenceDecision("Player",
                "Back out of the robbery.",
                "Rob them blind."));
            ((SequenceDecision)Sequences.Sequences[1]).Choice += (sender, e) =>
            {
                if (e == 0) //Backout
                    Sequences.SetStage(2);
                else if (e == 1) //Rob them blind
                    Sequences.SetStage(10);

                Sequences.ExecuteSequence(this);
            };

            //Backout
            Sequences.RegisterSequence(2, "Player", "Screw this!  I'm going home.");
            Sequences.RegisterSequence(3, new SequenceSceneTransition("JAGR_HomeAmbush"));

            //Rob them blind
            Sequences.RegisterSequence(10, "Player", "Screw these Kaiju freaks, I'm a Jaeger now!");
            Sequences.RegisterSequence(11, "Player", "What on Earth should I do now...");
            Sequences.RegisterSequence(12, new SequenceDecision("Player",
                "Start stealing shit.",
                "Disable the alarm.",
                "Defecate on the counter and leave."));
            ((SequenceDecision)Sequences.Sequences[12]).Choice += (sender, e) =>
            {
                if (e == 0) //Steal stuff
                {
                    GameManager.RobbedJewelryStore = true;
                    Sequences.SetStage(20);
                }
                else if (e == 1) //Disable the alarm
                    Sequences.SetStage(30);
                else if (e == 2) //Defecation
                {
                    GameManager.PoopedOnCounter = true;
                    Sequences.SetStage(40);
                }

                Sequences.ExecuteSequence(this);
            };

            //Steal stuff
            Sequences.RegisterSequence(20, "Player", "I'll just start stealing shit.");
            Sequences.RegisterSequence(21, NO_ACTOR, "*As you begin to steal shit, the alarm goes off.*");
            Sequences.RegisterSequence(22, new SequenceSpecial("CopShowsUp"));
            ((SequenceSpecial)Sequences.Sequences[22]).OnSequenceExecution += (sender, e) =>
            {
                cop.Animator.AnimateFade(1f, 600f);

                SetActorFocus("Cop");
                MessageBox.SetText("Freeze!");

                Sequences.SetStage(23);
            };
            Sequences.RegisterSequence(23, "Player", "Oh no, the fuzz!");
            Sequences.RegisterSequence(24, "Cop", "Ah, I'm just joshing you.  I came here to steal shit too.");
            Sequences.RegisterSequence(25, "Player", "Really!?");
            Sequences.RegisterSequence(26, "Cop", "Fuck yea, I'll disable the alarm and you start filling this sack.");
            Sequences.RegisterSequence(27, "Player", "Yipee!");
            Sequences.RegisterSequence(28, NO_ACTOR, "*You two rob the old Kaiju lady blind and split up the loot, going your separate ways*");
            Sequences.RegisterSequence(29, new SequenceSceneTransition("JAGR_Alley"));

            //Disable the alarm
            Sequences.RegisterSequence(30, "Player", "I better disable that pesky alarm before I get caught.");
            Sequences.RegisterSequence(31, NO_ACTOR, "*You go to work on the alarm box when you accidently place your hand in the incorrect place*");
            Sequences.RegisterSequence(32, new SequenceSceneTransition("BEND_Electrocution"));

            //Defecation
            Sequences.RegisterSequence(40, "Player", "This will show them, hehehe!");
            Sequences.RegisterSequence(41, NO_ACTOR, "*squirt*");
            Sequences.RegisterSequence(42, "Player", "Time to get back to the gang!");
            Sequences.RegisterSequence(43, new SequenceSceneTransition("JAGR_Alley"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            cop.SetAlpha(0f);
        }
    }
}
