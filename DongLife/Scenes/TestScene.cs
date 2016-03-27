using OpenTK;
using DongLife.Code;

namespace DongLife.Scenes
{
    public class TestScene : VNScene
    {
        private Actor player, professor;

        public TestScene() : base("TestScene")
        {
            background = new Background(@"Textures/Backgrounds/school.png");

            player = ActorFactory.CreateActor("Player");
            player.Position = new Vector2(300f, 650f);

            professor = ActorFactory.CreateActor("Professor");
            professor.Position = new Vector2(1080f, 550f);

            AddChild(background);
            RegisterActor(player);
            RegisterActor(professor);

            //Sequences
            SequenceDecision decision01 = new SequenceDecision("Player");
            decision01.SetDialogueOptions(
                "Ask the professor if he would like some dank ass kush.",
                "Ask the professor if he has graded the latest assignment.",
                "Ask the professor if you could bang his wife while their infant son watched.");
            decision01.Choice += (sender, e) =>
            {
                if (e == 0)
                    Sequences.SetStage(1);
                else if (e == 1)
                    Sequences.SetStage(7);
                else if (e == 2)
                    Sequences.SetStage(13);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(0, decision01);

            //Branch 01
            Sequences.RegisterSequence(001, new SequenceMessage("Player",
                "Why hello Professor Kaiju, sir!\n" +
                "What a glorious day today is, amirite? Would you like to purchase some kick ass reefer?"));
            Sequences.RegisterSequence(2, new SequenceMessage("Professor",
                "I could not do such a thing {PLAYERNAME}!  It would be against the school rules and would tarnish my Kaiju honor!\n" +
                "However, my son is a giant pothead and would love to partake in the kush."));
            Sequences.RegisterSequence(3, new SequenceMessage("Player", "Isn't your son like... two?"));
            Sequences.RegisterSequence(4, new SequenceMessage("Professor", "Yes!"));
            Sequences.RegisterSequence(5, new SequenceMessage("Player", "Okay."));
            Sequences.RegisterSequence(6, new SequenceSceneTransition("RenderScene"));

            //Branch 02
            Sequences.RegisterSequence(7, new SequenceMessage("Player",
                "Why hello Professor Kaiju, sir!\n" +
                "Have you, perhaps, graded the assignment yet?  I would really like to know my grade."));
            Sequences.RegisterSequence(8, new SequenceMessage("Professor",
                "Ah yes!  {PLAYERNAME}!  What a pleasent surprise!  I did grade your paper and it was absolutely atrocious!"));
            Sequences.RegisterSequence(9, new SequenceMessage("Player", "Oh... That's... okay."));
            Sequences.RegisterSequence(10, new SequenceMessage("Professor", "Don't worry, {PLAYERNAME}, hopefully someday you won't write utter fucking garbage and actually get a decent grade."));
            Sequences.RegisterSequence(11, new SequenceMessage("Player", "Thanks, I guess."));
            Sequences.RegisterSequence(12, new SequenceStageTransition(0));

            //Branch 03
            Sequences.RegisterSequence(13, new SequenceMessage("Player",
                "Why hello Professor Kaiju, sir!\n" +
                "Can I perform the cunninlingus on your wife while your infant son watched from the corner crying?"));
            Sequences.RegisterSequence(14, new SequenceMessage("Professor",
                "Hrmmm... That is indeed an interesting proposition, Mr. {PLAYERNAME}.  How about you shove my fat cock up your ass instead?"));
            Sequences.RegisterSequence(15, new SequenceMessage("Player",
                "I'm going to take that as a no."));
            Sequences.RegisterSequence(16, new SequenceMessage("Professor",
                "Yea, go fuck off faggot."));
            Sequences.RegisterSequence(17, new SequenceStageTransition(0));
        }
    }
}
