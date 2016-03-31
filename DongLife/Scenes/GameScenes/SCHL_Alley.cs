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
            Sequences.RegisterSequence(1, new SequenceStageTransition(0));
        }
    }
}
