using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class CMP_Nic : VNScene
    {
        private Actor player, nic;

        public CMP_Nic() : base("CMP_Nic")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            nic = new Actor("Nic", @"Textures/Actors/nic_cage.png");
            nic.CurrentScale = 0.5f;
            nic.FocusScale = 0.65f;
            nic.NormalScale = 0.5f;
            nic.Position = new Vector2(950, 325);

            AddChild(background);
            RegisterActor(player);
            RegisterActor(nic);

            Sequences.RegisterSequence(0, "Player", "Who goes there?!");
            Sequences.RegisterSequence(1, new SequenceSpecial("NicAppears"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                nic.Animator.AnimateFade(1f, 800f);
                SetActorFocus("Nic");
                MessageBox.SetText("It is me.");

                Sequences.SetStage(2);
            };
            Sequences.RegisterSequence(2, "Nic", "Nicolas Cage.");
            Sequences.RegisterSequence(3, "Player", "Holy shit!  It really is you!");
            Sequences.RegisterSequence(4, "Nic", "I heard you were writing a very flattering paper about me...");
            Sequences.RegisterSequence(5, "Player", "I mean... yes, I was.  But I don't think it really describes your awesomeness adequately.");
            Sequences.RegisterSequence(6, "Nic", "I'll be the judge of that...");
            Sequences.RegisterSequence(7, NO_ACTOR, "*Nic Cage reads your essay*");
            Sequences.RegisterSequence(8, "Nic", "Wow!  This is really good.  Thank you, {PLAYERNAME}.");
            Sequences.RegisterSequence(9, "Player", "You are welcome, sir.  It is a true honor to meet you.");
            Sequences.RegisterSequence(10, "Nic", "How about you come with me back to my home?  You can be my lover and compatriot.");
            Sequences.RegisterSequence(11, new SequenceDecision("Player",
                "Yes!",
                "Why of course!",
                "I would love to!",
                "I would be honored!",
                "Anything for you Nic Cage!",
                "I love you."));
            ((SequenceDecision)Sequences.Sequences[11]).Choice += (sender, e) =>
            {
                //Decisions are all the same
                Sequences.SetStage(12);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(12, "Nic", "Then come with me, {PLAYERNAME}.  Our destiny awaits!");
            Sequences.RegisterSequence(13, new SequenceSceneTransition("GEND_Nic"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            nic.SetAlpha(0f);
        }
    }
}
