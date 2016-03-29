using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class MOM_Base : VNScene
    {
        private Actor player, mother;

        public MOM_Base() : base("MOM_Base")
        {
            background = new Background(@"Textures/Backgrounds/bedroom.png");

            player = ActorFactory.CreateActor("Player");
            mother = ActorFactory.CreateActor("Mother");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(mother);

            Sequences.RegisterSequence(0, "Mother", "Hey honey, how are you adjusting to your new home?");
            Sequences.RegisterSequence(1, "Player", "I'm adjusting okay I guess, was seeing what you were up to.");
            Sequences.RegisterSequence(2, "Mother", "Ohh, interested in me, eh?  That offer for a massage is still open if you want ;)  We can talk while I rub you down.");
            Sequences.RegisterSequence(3, new SequenceDecision("Player", 
                "That actually sounds good, yes please!",
                "I think I'll have to pass.  Maybe another time."));
            ((SequenceDecision)Sequences.Sequences[3]).Choice += (sender, e) =>
            {
                if (e == 0) //Yes
                    Sequences.SetStage(10);
                else if (e == 1) //No
                    Sequences.SetStage(20);

                Sequences.ExecuteSequence(this);
            };

            //Yes
            Sequences.RegisterSequence(10, "Mother", "OOohhh! I'm so excited, follow me, baby.");
            Sequences.RegisterSequence(11, new SequenceSceneTransition("MOM_Seduction"));

            //No
            Sequences.RegisterSequence(20, "Mother", "Oh, that's a shame.  Well I have some housework to take care of, why don't you go get ready for school?");
            Sequences.RegisterSequence(21, new SequenceSceneTransition("BASE_Home"));
        }
    }
}
