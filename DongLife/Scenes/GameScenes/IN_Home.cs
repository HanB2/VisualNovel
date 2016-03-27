using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes
{
    public class IN_Home : VNScene
    {
        private Actor player, mother, father;

        public IN_Home() : base("IN_Home")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            mother = ActorFactory.CreateActor("Mother");
            father = ActorFactory.CreateActor("Father");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(mother);
            RegisterActor(father);
        }

        public override void OnEnter()
        {
            mother.Position = new Vector2(725, 500);
            father.Position = new Vector2(980, 425);

            base.OnEnter();
        }
    }
}
