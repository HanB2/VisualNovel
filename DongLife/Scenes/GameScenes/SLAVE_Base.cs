using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SLAVE_Base : VNScene
    {
        private Actor player, guard, headhoncho;

        public SLAVE_Base() : base("SLAVE_Base")
        {
            background = new Background(@"Textures/Backgrounds/slave_camp.png");

            player = ActorFactory.CreateActor("Player");

            guard = new Actor("Guard", @"Textures/Actors/slave_guard.png");
            guard.Position = new Vector2(GameSettings.WindowWidth / 2 + 250f, GameSettings.WindowHeight / 2 + 150f);
            guard.CurrentScale = 1.25f;
            guard.NormalScale = 1.25f;
            guard.FocusScale = 1.5f;

            headhoncho = new Actor("HeadHoncho", @"Textures/Actors/head_honcho.png");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(guard);
            //RegisterActor(headhoncho);

            Sequences.RegisterSequence(0, "Player", "Where am I?");
            Sequences.RegisterSequence(1, "Guard", "Welcome to Camp La Fuckya!");
            Sequences.RegisterSequence(2, new SequenceStageTransition(0));
        }
    }
}
