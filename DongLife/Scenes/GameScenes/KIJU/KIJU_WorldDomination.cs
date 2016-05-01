using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class KIJU_WorldDomination : VNScene
    {
        private Actor player, professor;

        public KIJU_WorldDomination() : base("KIJU_WorldDomination")
        {
            background = new Background(@"Textures/Backgrounds/pawns_of_war.png");

            player = ActorFactory.CreateActor("Player");
            professor = ActorFactory.CreateActor("Teacher");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(professor);

            Sequences.RegisterSequence(0, NO_ACTOR, "As the gears of war churned, Professor Kaiju was able to successfully defeat the world's armies through determination.");
            Sequences.RegisterSequence(1, new SequenceSpecial("SpawnActors"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                player.Animator.AnimateFade(1f, 800f);
                professor.Animator.AnimateFade(1f, 800f);

                Sequences.SetStage(2);
            };
            Sequences.RegisterSequence(2, "Teacher", "We did it {PLAYERNAME}!  We have rid the world of racists!  We shall build a new utopia for our peoples!  I shall now be known as King Professor Kaiju!");
            Sequences.RegisterSequence(3, new SequenceDecision("Player",
                "Overthrow King Professor Kaiju.",
                "Rule alongside King Professor Kaiju."));
            ((SequenceDecision)Sequences.Sequences[3]).Choice += (sender, e) =>
            {
                if (e == 0) //Overthrow
                    Sequences.SetStage(10);
                else if (e == 1) //Alongside
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //Overthrow
            Sequences.RegisterSequence(10, "Player", "King Professor Kaiju!  I challenge you to a duel!");
            Sequences.RegisterSequence(11, "Teacher", "Oh no!  My one true weakness!  I am allergic to challenges!");
            Sequences.RegisterSequence(12, new SequenceSceneTransition("GEND_KingKaiju"));

            //Rule alongside
            Sequences.RegisterSequence(20, "Player", "I will be with you, King Professor Kaiju, until the very end!");
            Sequences.RegisterSequence(21, "Teacher", "That's good, {PLAYERNAME}... That's very good...");
            Sequences.RegisterSequence(22, "Teacher", "I may have some use for you yet...");
            Sequences.RegisterSequence(23, new SequenceSceneTransition("GEND_KaijuSlave"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            player.SetAlpha(0f);
            professor.SetAlpha(0f);
        }
    }
}
