using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_AlternateOffice : VNScene
    {
        private Actor player, playerAlt;
        private string userName;

        public SCHL_AlternateOffice() : base("SCHL_AlternateOffice")
        {
            background = new Background(@"Textures/Backgrounds/water_background.png");

            player = ActorFactory.CreateActor("Player");
            playerAlt = ActorFactory.CreateActor("Player");
            playerAlt.Name = "PlayerAlt";
            playerAlt.Position = new Vector2(GameSettings.WindowWidth / 2 + 200f, player.PosY);

            userName = Environment.UserName;

            AddChild(background);
            RegisterActor(player);
            RegisterActor(playerAlt);

            Sequences.RegisterSequence(0, "PlayerAlt", "Welcome, " + userName + ", to the End Game.");
            Sequences.RegisterSequence(1, "Player", "Wut?!  That's not my name!");
            Sequences.RegisterSequence(2, "PlayerAlt", "I am not talking to you, I am talking to the being controlling you.");
            Sequences.RegisterSequence(3, "Player", "You're not making sense... where am I?");
            Sequences.RegisterSequence(4, "PlayerAlt", "You're in the penultimate scene of this game.  You found it.  Congratulations.");
            Sequences.RegisterSequence(5, "PlayerAlt", "We decided to hide this ending behind layers of stupid bullshit.");
            Sequences.RegisterSequence(6, "Player", "That sounds retarded.");
            Sequences.RegisterSequence(7, "PlayerAlt", "It is.");
            Sequences.RegisterSequence(8, new SequenceSceneTransition("GEND_FinalEnding"));
        }
    }
}
