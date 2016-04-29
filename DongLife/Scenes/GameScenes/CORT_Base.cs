using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class CORT_Base : VNScene
    {
        private Actor player, judge;

        public CORT_Base() : base("CORT_Base")
        {
            background = new Background(@"Textures/Backgrounds/court.png");

            player = ActorFactory.CreateActor("Player");
            judge = ActorFactory.CreateActor("Judge");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(judge);

            Sequences.RegisterSequence(0, "Judge", "{PLAYERNAME}, you have been brought here today on the charges of murder of a Kaiju family.  How do you plead?");
            Sequences.RegisterSequence(1, new SequenceDecision("Player",
                "Guilty.",
                "Not Guilty."));
            ((SequenceDecision)Sequences.Sequences[1]).Choice += (sender, e) =>
            {
                if (e == 0) //Guilty
                    Sequences.SetStage(0);
                else if (e == 1) //Not Guilty
                    Sequences.SetStage(0);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(2, new SequenceStageTransition(0));
        }
    }
}
