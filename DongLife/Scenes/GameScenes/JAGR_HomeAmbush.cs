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
        }
    }
}
