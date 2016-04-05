using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Detention : VNScene
    {
        private Actor player;
        private int timesWaited = 0;

        public SCHL_Detention() : base("SCHL_Detention")
        {
            background = new Background(@"Textures/Backgrounds/detention.png");

            player = ActorFactory.CreateActor("Player");

            AddChild(background);
            RegisterActor(player);

            Sequences.RegisterSequence(0, "Player", "Waiting in here is pure agony...");
            Sequences.RegisterSequence(1, new SequenceSpecial("Waiting"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                timesWaited++;
                if (timesWaited == 50)
                    Manager.ChangeScene("BEND_DetentionDeath");
                else
                {
                    Sequences.SetStage(0);
                    Sequences.ExecuteSequence(this);
                }
            };
        }

        public override void OnEnter()
        {
            base.OnEnter();

            timesWaited = 0;
        }
    }
}
