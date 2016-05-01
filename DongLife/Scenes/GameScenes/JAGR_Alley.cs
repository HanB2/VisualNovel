using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class JAGR_Alley : VNScene
    {
        private Actor player, prime, metroid, iron, kaiju;
        private int numberGenerated = 0;

        public JAGR_Alley() : base("JAGR_Alley")
        {
            background = new Background(@"Textures/Backgrounds/alley.png");

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

            Sequences.RegisterSequence(0, "JaegerPrime", "Okay, Fat Cock, here's the Jaeger Gang.  Introduce yourselves, lads.");
            Sequences.RegisterSequence(1, "MetroidJaeger", "I am Copyright Infringement Jaeger.  I have sick ass armor and a hot bod.");
            Sequences.RegisterSequence(2, "IronJaeger", "I am Iron Jaeger.  The oldest and greatest of the Jaegers.");
            Sequences.RegisterSequence(3, "KaijuJaeger", "I am Totally-a-Jaeger.  I am totally a Jaeger and not a Kaiju in disguise.");
            Sequences.RegisterSequence(4, "JaegerPrime", "And I am Jaeger Prime, the leader of this Jaeger Gang.");
            Sequences.RegisterSequence(5, "JaegerPrime", "Gang, this Fat Cock wishes to join us and destroy the Kaijus forever.");
            Sequences.RegisterSequence(6, "MetroidJaeger", "Prime!  We don't allow disgusting Fat Cocks in our group!");
            Sequences.RegisterSequence(7, "KaijuJaeger", "Yea!");
            Sequences.RegisterSequence(8, "JaegerPrime", "Shut the fuck up, mang.  I'm the leader, you're not, and I say he's joining the gang.");
            Sequences.RegisterSequence(9, "KaijuJaeger", "Yea!");
            Sequences.RegisterSequence(10, "Player", "I'm not sure if I want to join...");
            Sequences.RegisterSequence(11, "JaegerPrime", "What?!");
            Sequences.RegisterSequence(12, new SequenceDecision("Player",
                "Ha ha!  Just kidding, of course I want to join.",
                "Fuck this stupid ass kiddy gang."));
            ((SequenceDecision)Sequences.Sequences[12]).Choice += (sender, e) =>
            {
                if (e == 0) //Join
                {
                    GameManager.GenerateJaegerName();
                    Sequences.SetStage(20);
                }
                else if (e == 1) //Don't Join
                    Sequences.SetStage(15);

                Sequences.ExecuteSequence(this);
            };

            //Don't Join
            Sequences.RegisterSequence(15, "IronJaeger", "You done fucked up, kid.");
            Sequences.RegisterSequence(16, "JaegerPrime", "Gang!  Kill this Fat Cock!");
            Sequences.RegisterSequence(17, new SequenceSceneTransition("BEND_JaegerDeath"));

            //Join
            Sequences.RegisterSequence(20, "IronJaeger", "Good choice, Fat Cock.");
            Sequences.RegisterSequence(21, "JaegerPrime", "To join the Jaeger Gang, you need a Jaeger name.  Your dumb Fat Cock name won't do.");
            Sequences.RegisterSequence(22, "Player", "Can I name myself?");
            Sequences.RegisterSequence(23, "JaegerPrime", "Fuck no, I name all Jaegers in this gang... your name will be...");
            Sequences.RegisterSequence(24, "JaegerPrime", "....");
            Sequences.RegisterSequence(25, "JaegerPrime", "{JAEGERNAME}.  Yes, your Jaeger name is {JAEGERNAME}.");
            Sequences.RegisterSequence(26, new SequenceDecision("Player",
                "That's a lame ass name...",
                "That's fucking rad!"));
            ((SequenceDecision)Sequences.Sequences[26]).Choice += (sender, e) =>
            {
                if (e == 0) //Regenerate Name
                {
                    numberGenerated++;
                    if (numberGenerated > 10)
                        Sequences.SetStage(30); //Too many names
                    else
                    {
                        GameManager.GenerateJaegerName();
                        Sequences.SetStage(27);
                    }
                }
                else if (e == 1) //Good name
                    Sequences.SetStage(40);

                Sequences.ExecuteSequence(this);
            };

            //Regenerate Name
            Sequences.RegisterSequence(27, "JaegerPrime", "Fine... I'll give you another one.");
            Sequences.RegisterSequence(28, new SequenceStageTransition(24));

            //Too many names
            Sequences.RegisterSequence(30, "JaegerPrime", "That's it you Fat Cock loser, if you don't like my creative energies, then you can just die!");
            Sequences.RegisterSequence(31, new SequenceSceneTransition("BEND_JaegerDeath"));

            //Good Name
            Sequences.RegisterSequence(40, "JaegerPrime", "Yea, you better like it.");
            Sequences.RegisterSequence(41, "Player", "Yay, I'm a Jaeger now!");
            Sequences.RegisterSequence(42, "JaegerPrime", "Hooold it, {JAEGERNAME}!  You're not Jaeger just yet.  Anyone can call themselves {JAEGERNAME}.  You have to prove you're Jaeger material first.");
            Sequences.RegisterSequence(43, "Player", "How do I do that?");
            Sequences.RegisterSequence(44, "JaegerPrime", "You have to prove you hate Kaijus as much as us.");
            Sequences.RegisterSequence(45, "KaijuJaeger", "Yea!  We hate them Kaijus, yep we do!");
            Sequences.RegisterSequence(46, "JaegerPrime", "You have to rob a local jewelry store to prove your worth.  It's ran by this old Kaiju bitch who deserves a good robbing.");
            Sequences.RegisterSequence(47, "IronJaeger", "We all had our initiations, now it's your turn, {JAEGERNAME}.");
            Sequences.RegisterSequence(48, "JaegerPrime", "Get to it!");
            Sequences.RegisterSequence(49, new SequenceSceneTransition("JAGR_JewelryStore"));

            //Pooped
            Sequences.RegisterSequence(80, "Player", "Hey!  I did it!");
            Sequences.RegisterSequence(81, "JaegerPrime", "Where's the loot, {JAEGERNAME}?");
            Sequences.RegisterSequence(82, "Player", "I... uh... I decided to poop on the counter instead.");
            Sequences.RegisterSequence(83, "JaegerPrime", "(⊙︿⊙)");
            Sequences.RegisterSequence(84, "MetroidJaeger", "(⊙︿⊙)");
            Sequences.RegisterSequence(85, "IronJaeger", "(⊙︿⊙)");
            Sequences.RegisterSequence(86, "KaijuJaeger", "Yea!  Take that Grandma!");
            Sequences.RegisterSequence(87, "KaijuJaeger", "er...");
            Sequences.RegisterSequence(88, "KaijuJaeger", "(⊙︿⊙)");
            Sequences.RegisterSequence(89, "Player", @"¯\_(ツ)_/¯");
            Sequences.RegisterSequence(90, "JaegerPrime", "Whatever, you defaced their property.  It's good enough.  You're an official Jaeger now!");
            Sequences.RegisterSequence(91, new SequenceStageTransition(103));

            //After Jewelry Heist
            Sequences.RegisterSequence(100, "Player", "Hey!  I did it!");
            Sequences.RegisterSequence(101, "JaegerPrime", "Good job, {JAEGERNAME}.  You're an official Jaeger now!");
            Sequences.RegisterSequence(102, "KaijuJaeger", "Yay!");
            Sequences.RegisterSequence(103, "MetroidJaeger", "I still don't approve.");
            Sequences.RegisterSequence(104, "JaegerPrime", "I didn't ask for your opinion, Copyright Infringement Jaeger!  Now shut it!");
            Sequences.RegisterSequence(105, "JaegerPrime", "Now that you're an official Jaeger, you can help with our primary goal...");
            Sequences.RegisterSequence(106, "IronJaeger", "The ethnic cleansing of all Kaijus!");
            Sequences.RegisterSequence(107, "KaijuJaeger", "Ye... yea!");
            Sequences.RegisterSequence(108, "JaegerPrime", "So what do you say, {JAEGERNAME}?  Will you join us in this noble pursuit?");
            Sequences.RegisterSequence(109, new SequenceDecision("Player",
                "Death to all Kaijus!",
                "I'm not sure if we should... you know... kill them all?"));
            ((SequenceDecision)Sequences.Sequences[109]).Choice += (sender, e) =>
            {
                if (e == 0) //Kill them all
                    Sequences.SetStage(200);
                else if (e == 1) //Back out
                    Sequences.SetStage(110);

                Sequences.ExecuteSequence(this);
            };


        }

        public override void OnEnter()
        {
            base.OnEnter();

            numberGenerated = 0;

            if (GameManager.RobbedJewelryStore)
            {
                Sequences.SetStage(100);
                Sequences.ExecuteSequence(this);
            }
            else if (GameManager.PoopedOnCounter)
            {
                Sequences.SetStage(80);
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
