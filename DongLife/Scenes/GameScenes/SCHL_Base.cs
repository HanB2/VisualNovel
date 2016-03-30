using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Base : VNScene
    {
        private Actor player, janitor, principal;

        public SCHL_Base() : base("SCHL_Base")
        {
            background = new Background(@"Textures/Backgrounds/school_hall.png");

            player = ActorFactory.CreateActor("Player");

            janitor = new Actor("Janitor", @"Textures/Actors/janitor_normal.png");


            AddChild(background);
            RegisterActor(player);
            RegisterActor(janitor);
            RegisterActor(principal);
        }

        public override void OnEnter()
        {
            base.OnEnter();

            janitor.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            principal.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }
    }
}
