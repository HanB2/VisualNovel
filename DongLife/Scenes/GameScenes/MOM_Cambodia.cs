using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class MOM_Cambodia : VNScene
    {
        private Actor player, mother, shiaMother;

        public MOM_Cambodia() : base("MOM_Cambodia")
        {
            background = new Background(@"Textures/Backgrounds/cambodia.png");

            player = ActorFactory.CreateActor("Player");
            mother = ActorFactory.CreateActor("SexyMother");
            mother.PosX += 150f;
            shiaMother = ActorFactory.CreateActor("ShiaMother");
            shiaMother.PosX += 150f;
            
            shiaMother.Animator.AnimationEnd += Mother_AnimationEnd;

            AddChild(background);
            RegisterActor(player);
            RegisterActor(mother);
            RegisterActor(shiaMother);

            Sequences.RegisterSequence(0, "Player", "Wow... we're finally in Cambodia.");
            Sequences.RegisterSequence(1, "Mother", "That was such a long trip, I'm glad we made it safely.");
            Sequences.RegisterSequence(2, "Player", "So what do we do now?");
            Sequences.RegisterSequence(3, "Mother", "I... I have not been entirely honest with you... I have a secret that I must tell you before we live out our lives together...");
            Sequences.RegisterSequence(4, new SequenceSpecial("MomReveal"));
            ((SequenceSpecial)Sequences.Sequences[4]).OnSequenceExecution += (sender, e) =>
            {
                mother.Animator.FadeOut(1500f);
                shiaMother.Animator.FadeIn(1500f);
            };
            Sequences.RegisterSequence(5, "ShiaMother", "My name is actually Shia LaBeouf and I love you.");
            Sequences.RegisterSequence(6, new SequenceDecision("Player",
                "Accept Shia's Love.",
                "Deny Shia's Love."));
            ((SequenceDecision)Sequences.Sequences[6]).Choice += (sender, e) =>
            {
                if (e == 0) //Accept Love
                    Sequences.SetStage(10);
                else if (e == 1) //Deny Love
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //Accept Love
            Sequences.RegisterSequence(10, "ShiaMother", "I love you!");
            Sequences.RegisterSequence(11, new SequenceSceneTransition("GEND_TvStar_Shia"));

            //Deny Love
            Sequences.RegisterSequence(20, "ShiaMother", "Well, I guess you will just have to die. ¯\\_(ツ)_/¯");
            Sequences.RegisterSequence(21, new SequenceSceneTransition("BEND_EatenAlive_Shia"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            shiaMother.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }

        private void Mother_AnimationEnd(ControlAnimator.AnimationModes finishedMode)
        {
            if (finishedMode == ControlAnimator.AnimationModes.FadeIn && Sequences.GetCurrentSequence().SequenceStage == 4)
            {
                Sequences.SetStage(5);
                Sequences.ExecuteSequence(this);
            }
        }
    }
}
