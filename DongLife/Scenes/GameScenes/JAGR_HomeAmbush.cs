using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class JAGR_HomeAmbush : VNScene
    {
        private Actor player, prime, metroid, iron, kaiju;

        public JAGR_HomeAmbush() : base("JAGR_HomeAmbush")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

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

            Sequences.RegisterSequence(0, "Player", "Well that was a weird day...");
            Sequences.RegisterSequence(1, "Player", "Time to catch some Zzzzs.");
            Sequences.RegisterSequence(2, new SequenceSpecial("PlayerFadesOut"));
            ((SequenceSpecial)Sequences.Sequences[2]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus(NO_ACTOR);
                MessageBox.SetText("*You sleep for several hours*");

                player.Animator.AnimateFade(0f, 600f);

                Sequences.SetStage(3);
            };
            Sequences.RegisterSequence(3, NO_ACTOR, "*Figures appear in your door way*");
            Sequences.RegisterSequence(4, new SequenceSpecial("GangComesIn"));
            ((SequenceSpecial)Sequences.Sequences[4]).OnSequenceExecution += (sender, e) =>
            {
                prime.Animator.AnimateFade(1f, 950f);
                metroid.Animator.AnimateFade(1f, 950f);
                iron.Animator.AnimateFade(1f, 950f);
                kaiju.Animator.AnimateFade(1f, 950f);

                Sequences.SetStage(5);
            };
            Sequences.RegisterSequence(5, "JaegerPrime", "Wake up, Fat Cock!");
            Sequences.RegisterSequence(6, new SequenceSpecial("PlayerFadesIn"));
            ((SequenceSpecial)Sequences.Sequences[6]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Player");
                MessageBox.SetText("Whaaa...?");

                player.Animator.AnimateFade(1f, 600f);

                Sequences.SetStage(7);
            };
            Sequences.RegisterSequence(7, "Player", "What's going on?");
            Sequences.RegisterSequence(8, "JaegerPrime", "You betrayed your own kind, {JAEGERNAME}.  How dare you.");
            Sequences.RegisterSequence(9, "MetroidJaeger", "You sold us out, mang!");
            Sequences.RegisterSequence(10, "IronJaeger", "You're going to pay for that!");
            Sequences.RegisterSequence(11, "KaijuJaeger", "Yea!  What they said!");
            Sequences.RegisterSequence(12, "JaegerPrime", "You're going to die for your crimes against the Jaeger Gang, scum!");
            Sequences.RegisterSequence(13, "Player", "No wait!  I can explain!");
            Sequences.RegisterSequence(14, "JaegerPrime", "Too late!");
            Sequences.RegisterSequence(15, new SequenceSceneTransition("BEND_JaegerDeath"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            prime.SetAlpha(0f);
            metroid.SetAlpha(0f);
            iron.SetAlpha(0f);
            kaiju.SetAlpha(0f);
        }
    }
}
