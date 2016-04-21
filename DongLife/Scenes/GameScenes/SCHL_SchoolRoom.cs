using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_SchoolRoom : VNScene
    {
        private Actor player, teacher, jaeger;

        public SCHL_SchoolRoom() : base("SCHL_SchoolRoom")
        {
            background = new Background(@"Textures/Backgrounds/school_room.png");

            player = ActorFactory.CreateActor("Player");
            teacher = ActorFactory.CreateActor("Teacher");
            //jaeger = ActorFactory.CreateActor("Jaeger_Prime");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(teacher);

            Sequences.RegisterSequence(0, "Player", "Hey teach!  Sorry I am late!");
            Sequences.RegisterSequence(1, "Teacher", "Stop being a little bitch and come in on time!");
            Sequences.RegisterSequence(2, "Player", "Type what you want.");
            Sequences.RegisterSequence(3, new SequenceDecision("Player",
                "Tell the teacher he is hot.",
                "Tell the teacher he is cold."));
            ((SequenceDecision)Sequences.Sequences[3]).Choice += (sender, e) =>
            {
                if (e == 0) //Teacher is hot
                    Sequences.SetStage(10);
                else if (e == 1) //Teacher is cold
                    Sequences.SetStage(20);
                Sequences.ExecuteSequence(this);
            };

            //Teacher is hot
            Sequences.RegisterSequence(10, "Teacher", "Fuck off faggot. HOT.");
            Sequences.RegisterSequence(11, new SequenceStageTransition(0));

            //Teacher is cold
            Sequences.RegisterSequence(20, "Teacher", "Thank you faggot. COLD.");
            Sequences.RegisterSequence(21, new SequenceStageTransition(3));
        }

        public override void OnEnter()
        {
            base.OnEnter();


        }
    }
}
