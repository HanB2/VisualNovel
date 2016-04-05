using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SLAVE_HomeAmbush : VNScene
    {
        private Actor player, janitor;

        public SLAVE_HomeAmbush() : base("SLAVE_HomeAmbush")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            janitor = ActorFactory.CreateActor("Janitor");
            janitor.TexturePath = "Textures/Actors/janitor_death.png";
            janitor.Position = new Vector2(GameSettings.WindowWidth / 2, janitor.PosY);

            player = ActorFactory.CreateActor("Player");
            player.Position = new Vector2(GameSettings.WindowWidth / 2, player.PosY);

            AddChild(background);
            RegisterActor(janitor);
            RegisterActor(player);

            Sequences.RegisterSequence(0, "Player", "Well that was a weird day... time to go to bed.");
            Sequences.RegisterSequence(1, new SequenceSpecial("PlayerSleeps"));
            ((SequenceSpecial)Sequences.Sequences[1]).OnSequenceExecution += (sender, e) =>
            {
                player.Animator.FadeOut(800f);
                Sequences.SetStage(2);
            };
            Sequences.RegisterSequence(2, NO_ACTOR, "*Hours pass as you sleep when someone slips into your room, unnoticed*");
            Sequences.RegisterSequence(3, "Janitor", "You're mine, you piece of shit...");
            Sequences.RegisterSequence(4, new SequenceSpecial("JanitorDrugsYou"));
            ((SequenceSpecial)Sequences.Sequences[4]).OnSequenceExecution += (sender, e) =>
            {
                janitor.Animator.FadeIn(800f);

                SetActorFocus("Janitor");
                MessageBox.SetText("Take this!");

                Sequences.SetStage(5);
            };
            Sequences.RegisterSequence(5, new SequenceSceneTransition("SLAVE_Base"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            janitor.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            player.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 1f);
        }
    }
}
