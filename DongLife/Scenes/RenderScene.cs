using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using DongLife.Controls;
using DongLife.Code;
using System;

namespace DongLife.Scenes
{
    public class RenderScene : VNScene
    {
        private Background background;
        private Actor player, professor;

        public RenderScene() : base("RenderScene")
        {
            background = new Background(@"Textures/Backgrounds/city_water.png");

            player = new Actor("Player", @"Textures/Actors/player_male.png");
            player.Position = new Vector2(300f, 650f);
            player.NormalScale = 1f;
            player.FocusScale = 1.25f;
            player.CurrentScale = 1.25f;

            professor = new Actor("Professor", @"Textures/Actors/professor_kaiju.png");
            professor.Position = new Vector2(1080f, 550f);
            professor.NormalScale = 1f;
            professor.FocusScale = 1.25f;
            professor.CurrentScale = 1f;

            AddChild(background);
            RegisterActor(player);
            RegisterActor(professor);

            //Sequences
            Sequences.RegisterSequence(0, new SequenceMessage("Player",
                "Professor... where are we?"));
            Sequences.RegisterSequence(1, new SequenceMessage("Professor",
                "We're in the infinite sea of your mind, my young student."));
            Sequences.RegisterSequence(2, new SequenceMessage("Player",
                "What on earth does that mean?"));
            Sequences.RegisterSequence(3, new SequenceMessage("Professor",
                "We smoked up some fat doobies and are now on one heck of a trip!"));
            Sequences.RegisterSequence(4, new SequenceMessage("Player",
                "Noiiiiiiiiiiiiiiiiiice!!"));
            Sequences.RegisterSequence(5, new SequenceSceneTransition("MainMenuScene"));
        }
    }
}
