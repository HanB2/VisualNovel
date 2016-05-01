using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class CORT_Base : VNScene
    {
        private Actor player, judge;

        public CORT_Base() : base("CORT_Base")
        {
            background = new Background(@"Textures/Backgrounds/court.png");

            player = ActorFactory.CreateActor("Player");
            judge = ActorFactory.CreateActor("Judge");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(judge);

            Sequences.RegisterSequence(0, "Judge", "{PLAYERNAME}, you have been brought here today on the charges of murder of a local Kaiju family.  How do you plead?");
            Sequences.RegisterSequence(1, new SequenceDecision("Player",
                "Guilty.",
                "Not Guilty."));
            ((SequenceDecision)Sequences.Sequences[1]).Choice += (sender, e) =>
            {
                if (e == 0) //Guilty
                    Sequences.SetStage(10);
                else if (e == 1) //Not Guilty
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //Guilty
            Sequences.RegisterSequence(10, "Judge", "Well... that was easier than I would have imagined.");
            Sequences.RegisterSequence(11, "Judge", "I guess I'll sentence you to life in prison.");
            Sequences.RegisterSequence(12, "Player", "What?!");
            Sequences.RegisterSequence(13, "Judge", "May God have mercy on your soul.");
            Sequences.RegisterSequence(14, new SequenceSceneTransition("BEND_Prison"));

            //Not Guilty
            Sequences.RegisterSequence(20, "Judge", "Well, we all know you are guilty, but I'll humor you... how are you not guilty?");
            Sequences.RegisterSequence(21, new SequenceDecision("Player", 
                "It was the Jaeger gang.",
                "They deserved it.",
                "I don't know them Kaijus."));
            ((SequenceDecision)Sequences.Sequences[21]).Choice += (sender, e) =>
            {
                if (e == 0) //Jaeger Gang
                {
                    GameManager.BlamedJaegers = true;
                    Sequences.SetStage(30);
                }
                else if (e == 1) //They deserved it
                    Sequences.SetStage(40);
                else if (e == 2) //I don't know them
                    Sequences.SetStage(50);

                Sequences.ExecuteSequence(this);
            };

            //Jaeger Gang
            Sequences.RegisterSequence(30, "Player", "The Jaeger gang!  They're the ones that did it.");
            Sequences.RegisterSequence(31, "Judge", "That is a serious allegation, what evidence do you have?");
            Sequences.RegisterSequence(32, "Player", "They hate Kaijus more than the average person!  I am a Giant Dongitus person, so I identify with them.  Why on Earth would I harm the Kaiju family?");
            Sequences.RegisterSequence(33, "Player", "Think, Judge!  It makes perfect sense!");
            Sequences.RegisterSequence(34, "Judge", "Hrmm... I don't know...");
            Sequences.RegisterSequence(35, "Player", "Judge... just trust me.  :)");
            Sequences.RegisterSequence(36, "Judge", "I don't trust no long dongs, but your story makes sense.  No need to look at other evidence, I'll summon them to court.");
            Sequences.RegisterSequence(37, "Judge", "You are free to go.");
            Sequences.RegisterSequence(38, "Player", "Yes!");
            Sequences.RegisterSequence(39, new SequenceSceneTransition("JAGR_HomeAmbush"));

            //They deserved it
            Sequences.RegisterSequence(40, "Player", "Them dirty Kaijus deserved what they got, your honor.");
            Sequences.RegisterSequence(41, "Judge", "We're not denying that they got what they deserved, but you can't go around being a vigilante and executing law abiding Kaijus.");
            Sequences.RegisterSequence(42, "Player", "Come on, Judge.  You're telling me you wouldn't kill them if you were in my shoes?");
            Sequences.RegisterSequence(43, "Judge", "...Maybe you're right.  I don't like you giant dong folk, but I'm going to let this slide this time.");
            Sequences.RegisterSequence(44, "Judge", "However, I am going to send you to a psychiatric ward due to your obvious mental health issues.");
            Sequences.RegisterSequence(45, "Player", "What?!");
            Sequences.RegisterSequence(46, "Judge", "It's my duty to get you dirty giant dong freaks off the streets.  May God have mercy on your pathetic soul.");
            Sequences.RegisterSequence(47, new SequenceSceneTransition("BEND_PsychiatricWard"));

            //I don't know them
            Sequences.RegisterSequence(50, "Player", "I don't know them.  I wasn't even there.");
            Sequences.RegisterSequence(51, "Judge", "What?  He's your professor and your DNA was found at the crime scene!  We even have an eye witness who saw you there!");
            Sequences.RegisterSequence(52, "Player", "That doesn't prove anything.  Anyone could have my DNA, I don't pay attention in class, and that Baby Kaiju is a dirty liar.");
            Sequences.RegisterSequence(53, "Judge", "I mean, you can't really argue against that...");
            Sequences.RegisterSequence(54, "Judge", "You're released of all charges.");
            Sequences.RegisterSequence(55, "Player", "Yes!");
            Sequences.RegisterSequence(56, new SequenceSceneTransition("BASE_Home"));
        }
    }
}
