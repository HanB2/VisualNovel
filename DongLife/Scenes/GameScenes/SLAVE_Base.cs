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
            Sequences.RegisterSequence(2, "Player", "Camp La Fuckwhat?");
            Sequences.RegisterSequence(3, "Guard", "Camp La Fuckya!");
            Sequences.RegisterSequence(4, "Player", "Okay... where is Camp La Fuckya?");
            Sequences.RegisterSequence(5, "Guard", "Cambodia!");
            Sequences.RegisterSequence(6, "Player", "What am I doing here?");
            Sequences.RegisterSequence(7, "Guard", "You're a sex slave now!");
            Sequences.RegisterSequence(8, "Player", "What?!");
            Sequences.RegisterSequence(9, "Guard", "You've been sold to us to perform sexual acts for all of our clients!");
            Sequences.RegisterSequence(10, "Player", "But... I don't want to be a sex slave...");
            Sequences.RegisterSequence(11, "Guard", "Well, then I guess I get to kill you!");
            Sequences.RegisterSequence(12, new SequenceDecision("Player",
                "Accept new life as a slave.",
                "Attempt to fight the guard."));
            ((SequenceDecision)Sequences.Sequences[12]).Choice += (sender, e) =>
            {
                if (e == 0) //Accept life
                    Sequences.SetStage(20);
                else if (e == 1) //Attempt to fight
                    Sequences.SetStage(13);

                Sequences.ExecuteSequence(this);
            };

            //Attempt to fight the guard
            Sequences.RegisterSequence(13, "Player", "I will destroy you!");
            Sequences.RegisterSequence(14, "Guard", "You know I have a gun, right?");
            Sequences.RegisterSequence(15, "Player", "Yaaarrrr!!!");
            Sequences.RegisterSequence(16, NO_ACTOR, "*bang*");
            Sequences.RegisterSequence(17, new SequenceSceneTransition("BEND_SlaveGunDeath"));

            //Accept life
            Sequences.RegisterSequence(20, "Player", "Welp, the slave life might not be so bad after all!");
            Sequences.RegisterSequence(21, "Guard", "That's the spirit!");
        }
    }
}
