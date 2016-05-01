using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class KIJU_HomeArrest : VNScene
    {
        private Actor player, cop;

        public KIJU_HomeArrest() : base("KIJU_HomeArrest")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            cop = ActorFactory.CreateActor("Cop");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(cop);

            Sequences.RegisterSequence(0, "Player", "Ahh... what a productive day.  Time to catch some Zzzz.");
            Sequences.RegisterSequence(1, new SequenceSpecial("CopShowsUp"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                cop.Animator.AnimateFade(1f, 800f);

                SetActorFocus("Cop");
                MessageBox.SetText("Hold it right there, bub!");

                Sequences.SetStage(2);
            };
            Sequences.RegisterSequence(2, "Cop", "You're under arrest!");
            Sequences.RegisterSequence(3, "Player", "Wait what?  Why?!");
            Sequences.RegisterSequence(4, "Cop", "We received a call from a small reptilian boy detailing how you just murdered his father.");
            Sequences.RegisterSequence(5, "Player", "I mean... can you murder something that isn't human?  You must understand, officer!");
            Sequences.RegisterSequence(6, "Cop", "Look who's talking, big penis man!");
            Sequences.RegisterSequence(7, "Player", "Hey!  That's racist!");
            Sequences.RegisterSequence(8, "Cop", "Tell it to the judge!");
            Sequences.RegisterSequence(9, new SequenceSceneTransition("CORT_Base"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            cop.SetAlpha(0f);
        }
    }
}
