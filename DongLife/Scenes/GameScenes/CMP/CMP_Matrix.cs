using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class CMP_Matrix : VNScene
    {
        private Actor player, morph;

        public CMP_Matrix() : base("CMP_Matrix")
        {
            background = new Background(@"Textures/Backgrounds/matrix.png");

            player = ActorFactory.CreateActor("Player");
            morph = new Actor("Morph", @"Textures/Actors/morpheus.png");
            morph.CurrentScale = 0.7f;
            morph.FocusScale = 0.85f;
            morph.NormalScale = 0.7f;
            morph.Position = new Vector2(950, 500);

            AddChild(background);
            RegisterActor(player);
            RegisterActor(morph);

            Sequences.RegisterSequence(0, "Player", "Where am I?");
            Sequences.RegisterSequence(1, "Morph", "You are in the Matrix, {PLAYERNAME}.");
            Sequences.RegisterSequence(2, "Player", "THE Matrix?  Holy shit!");
            Sequences.RegisterSequence(3, "Morph", "Yes, that Matrix.");
            Sequences.RegisterSequence(4, "Morph", "You take the blue pill, the story ends. You wake up in your bed and believe whatever you want to believe. You take the red pill, you stay in Wonderland, and I show you how deep the rabbit hole goes.");
            Sequences.RegisterSequence(5, "Player", "That line is really corny coming from a shitty video game...");
            Sequences.RegisterSequence(6, "Morph", "Yes.");
            Sequences.RegisterSequence(7, new SequenceDecision("Player",
                "Take the Red Pill.",
                "Take the Blue Pill.",
                "Take Both."));
            ((SequenceDecision)Sequences.Sequences[7]).Choice += (sender, e) =>
            {
                if (e == 0) //Red pill
                    Sequences.SetStage(10);
                else if (e == 1) //Blue pill
                    Sequences.SetStage(20);
                else if (e == 2) //Both
                    Sequences.SetStage(30);

                Sequences.ExecuteSequence(this);
            };

            //Red Pill
            Sequences.RegisterSequence(10, "Player", "I'll take one red pill please!");
            Sequences.RegisterSequence(11, "Morph", "Here you go!");
            Sequences.RegisterSequence(12, NO_ACTOR, "*gulp*");
            Sequences.RegisterSequence(13, "Player", "...I feel funny.");
            Sequences.RegisterSequence(14, "Player", "...");
            Sequences.RegisterSequence(15, "Morph", "hehehe");
            Sequences.RegisterSequence(16, new SequenceSceneTransition("BEND_Roofie"));

            //Blue Pill
            Sequences.RegisterSequence(20, "Player", "I'll take one blue pill please!");
            Sequences.RegisterSequence(21, "Morph", "With this, you will wake up.  Here you go!");
            Sequences.RegisterSequence(22, "Player", "Good-bye shitty computer world!");
            Sequences.RegisterSequence(23, NO_ACTOR, "*gulp*");
            Sequences.RegisterSequence(24, new SequenceSceneTransition("BASE_Home"));

            //Both
            Sequences.RegisterSequence(30, "Player", "Give me those!");
            Sequences.RegisterSequence(31, "Morph", "Woh!  What are you doing?!");
            Sequences.RegisterSequence(32, NO_ACTOR, "*gulp*");
            Sequences.RegisterSequence(33, "Morph", "You're not supposed to take both!");
            Sequences.RegisterSequence(34, "Player", "What happens now?");
            Sequences.RegisterSequence(35, "Morph", "You get stuck in a loop and are forced to restart the game...");
            Sequences.RegisterSequence(36, "Player", "Really?");
            Sequences.RegisterSequence(37, "Morph", "Yes.");
            Sequences.RegisterSequence(38, new SequenceStageTransition(36));
        }
    }
}
