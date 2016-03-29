using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes
{
    public class IN_Home : VNScene
    {
        private Actor player, mother, father;

        public IN_Home() : base("IN_Home")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            mother = ActorFactory.CreateActor("Mother");
            father = ActorFactory.CreateActor("Father");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(mother);
            RegisterActor(father);

            father.Animator.AnimationEnd += Animator_AnimationEnd;
            mother.Animator.AnimationEnd += Animator_AnimationEnd;

            //Sequences
            Sequences.RegisterSequence(0, "Mother", "Well this is your room.  If you need anything, don't be afraid to ask.");
            Sequences.RegisterSequence(1, "Father", "I'm going to bed.");
            Sequences.RegisterSequence(2, new SequenceSpecial("FatherStormsOut"));
            (Sequences.Sequences[2] as SequenceSpecial).OnSequenceExecution += (sender, id) =>
            {
                father.Animator.FadeOut(350f);
            };
            Sequences.RegisterSequence(3, "Mother", "Don't mind him, I'm sure he'll come around.");
            Sequences.RegisterSequence(4, new SequenceDecision("Player",
                "I still can't get over his fucking head.",
                "So what do I do now?"));
            (Sequences.Sequences[4] as SequenceDecision).Choice += (sender, e) =>
            {
                if (e == 0)
                    Sequences.SetStage(10);
                else if (e == 1)
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //I still can't get over his fucking head
            Sequences.RegisterSequence(10, "Mother", "You'll get used to it.");
            Sequences.RegisterSequence(11, new SequenceStageTransition(30));

            //So what do I do now?
            Sequences.RegisterSequence(20, "Mother", "Well, you start your new school tomorrow.  You'll be attending Littlewood High.");
            Sequences.RegisterSequence(21, new SequenceStageTransition(30));

            Sequences.RegisterSequence(30, "Mother", "Now... didn't I promise a little gift?  Would you like me to massage your oh so poor shoulders, my dear?");
            Sequences.RegisterSequence(31, new SequenceDecision("Player",
                "I would really appreciate it.",
                "Um... no thanks."));
            ((SequenceDecision)Sequences.Sequences[31]).Choice += (sender, e) =>
            {
                if (e == 0)
                    Sequences.SetStage(10);
                else if (e == 1)
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //I would really appreciate it (foster mother seduction branch)
            Sequences.RegisterSequence(40, "Mother", "Well... get over here then :)");
            Sequences.RegisterSequence(41, new SequenceSceneTransition("HOME")); //Transition to MOTHER scene

            //Um... no thanks
            Sequences.RegisterSequence(50, "Mother", "Oh... well okay then.  Let me know if you need anything.");
            Sequences.RegisterSequence(51, new SequenceSceneTransition("HOME")); //Transition to Home (normal branch)
        }

        public override void OnEnter()
        {
            mother.Position = new Vector2(725, 500);
            father.Position = new Vector2(980, 425);

            base.OnEnter();
        }

        private void Animator_AnimationEnd(ControlAnimator.AnimationModes finishedMode)
        {
            if (Sequences.GetCurrentSequence().SequenceStage == 2 && 
                finishedMode == ControlAnimator.AnimationModes.FadeOut)
            {
                Sequences.ProgressStage();
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
