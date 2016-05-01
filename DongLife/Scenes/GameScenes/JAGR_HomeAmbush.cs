using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class JAGR_HomeAmbush : VNScene
    {
        private Actor player, prime, metroid, iron, kaiju;

        public JAGR_HomeAmbush() : base("JAGR_HomeAmbush")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            prime = ActorFactory.CreateActor("JaegerPrime");
            metroid = ActorFactory.CreateActor("MetroidJaeger");
            iron = ActorFactory.CreateActor("IronJaeger");
            kaiju = ActorFactory.CreateActor("KaijuJaeger");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(prime);
            RegisterActor(metroid);
            RegisterActor(iron);
            RegisterActor(kaiju);

            Sequences.RegisterSequence(0, "Player", "Hey!");
            Sequences.RegisterSequence(1, "JaegerPrime", "Hey!");
            Sequences.RegisterSequence(2, "MetroidJaeger", "Hey!");
            Sequences.RegisterSequence(3, "IronJaeger", "Hey!");
            Sequences.RegisterSequence(4, "KaijuJaeger", "Hey!");
            Sequences.RegisterSequence(5, new SequenceStageTransition(0));
        }
    }
}
