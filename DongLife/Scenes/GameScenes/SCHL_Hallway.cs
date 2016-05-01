using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Hallway : VNScene
    {
        private Actor player, prime;

        public SCHL_Hallway() : base("SCHL_Hallway")
        {
            background = new Background(@"Textures/Backgrounds/school_hall.png");

            player = ActorFactory.CreateActor("Player");
            prime = ActorFactory.CreateActor("JaegerPrime");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(prime);

            Sequences.RegisterSequence(0, "Player", "Hey, you're the kid that back talked the professor!");
            Sequences.RegisterSequence(1, "JaegerPrime", "Yea, what's it to you, loser?");
            Sequences.RegisterSequence(2, new SequenceDecision("Player",
                "I thought that was pretty rad.",
                "You should be more respectful to authority."));
            ((SequenceDecision)Sequences.Sequences[2]).Choice += (sender, e) =>
            {
                if (e == 0) //Rad
                    Sequences.SetStage(20);
                else if (e == 1) //Not Rad
                    Sequences.SetStage(10);

                Sequences.ExecuteSequence(this);
            };

            //Not Rad
            Sequences.RegisterSequence(10, "JaegerPrime", "What are you?  Some kind of Kaiju lover?");
            Sequences.RegisterSequence(11, "JaegerPrime", "Ahhh, I getchu'.  You freaks gotta stick together, I get it.  Whatever, Fat Cock, get out of my face!");
            Sequences.RegisterSequence(12, "Player", "I'm going home, meany!");
            Sequences.RegisterSequence(13, new SequenceSceneTransition("BASE_Home"));

            //Rad
            Sequences.RegisterSequence(20, "JaegerPrime", "You think so, Fat Cock?  How about you follow me to the back alley?  You can meet the crew.");
            Sequences.RegisterSequence(21, new SequenceDecision("Player",
                "Sure!",
                "I better not."));
            ((SequenceDecision)Sequences.Sequences[21]).Choice += (sender, e) =>
            {
                if (e == 0) //Yes
                    Sequences.SetStage(22);
                else if (e == 1) //No
                    Sequences.SetStage(30);

                Sequences.ExecuteSequence(this);
            };

            //Yes
            Sequences.RegisterSequence(22, "JaegerPrime", "Follow me then, Fat Cock.");
            Sequences.RegisterSequence(23, new SequenceSceneTransition("JAGR_Alley"));

            //No
            Sequences.RegisterSequence(30, "JaegerPrime", "Whatever weirdo, go home and cry to your mommy!");
            Sequences.RegisterSequence(31, new SequenceSceneTransition("BASE_Home"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            prime.PosX += 275;
        }
    }
}
