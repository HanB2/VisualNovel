using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;
using DongLife.Controls.Computer;

namespace DongLife.Scenes.GameScenes
{
    public class CMP_Base : VNScene
    {
        private Desktop desktop;
        private Image explicitImage;
        private ControlAnimator explicitAnimator;

        public CMP_Base() : base("CMP_Base")
        {
            desktop = new Desktop(this);
            explicitImage = new Image("Textures/Misc/explicit.png");
            explicitAnimator = new ControlAnimator();
            explicitImage.AddChild(explicitAnimator);

            AddChild(desktop);
            AddChild(explicitImage);

            //Base Sequence
            Sequences.RegisterSequence(0, NO_ACTOR, "Hrmmm... wonder what I should do on the computer...");
            Sequences.RegisterSequence(1, new SequenceStageTransition(0));

            //Porn Icon
            Sequences.RegisterSequence(10, NO_ACTOR, "Rule 34 Lounge always has the best porn!  Wonder what I should watch...");
            Sequences.RegisterSequence(11, new SequenceStageTransition(10));

            Sequences.RegisterSequence(12, NO_ACTOR, "I really like Vin Diesel... let's whip it out!");
            Sequences.RegisterSequence(13, new SequenceStageTransition(100));

            Sequences.RegisterSequence(14, NO_ACTOR, "I really like Saitama... let's whip it out!");
            Sequences.RegisterSequence(15, new SequenceStageTransition(100));

            Sequences.RegisterSequence(16, NO_ACTOR, "I really like Shia Laboof... let's whip it out!");
            Sequences.RegisterSequence(17, new SequenceStageTransition(200));

            Sequences.RegisterSequence(18, NO_ACTOR, "I really like Keanu Reeves... let's whip it out!");
            Sequences.RegisterSequence(19, new SequenceStageTransition(300));

            //Homework Icon
            Sequences.RegisterSequence(20, NO_ACTOR, "I guess I'll do some homework... need to decide on the essay subject.");
            Sequences.RegisterSequence(21, new SequenceStageTransition(20));

            //News Icon
            Sequences.RegisterSequence(30, NO_ACTOR, "Guh... the local news is always so biased against Giant Dong people.");
            Sequences.RegisterSequence(31, new SequenceStageTransition(30));

            //Good Masterbate
            Sequences.RegisterSequence(100, new SequenceSpecial("Explicit"));
            ((SequenceSpecial)Sequences.Sequences[100]).OnSequenceExecution += (sender, e) =>
            {
                explicitAnimator.AnimateFade(1f, 400f);
                MessageBox.SetText("Uuughhhh!!!...");
                Sequences.SetStage(101);
            };
            Sequences.RegisterSequence(101, NO_ACTOR, "Man... that was good!");
            Sequences.RegisterSequence(102, NO_ACTOR, "I should really get to class...");
            Sequences.RegisterSequence(103, new SequenceSpecial("GoToClass"));
            ((SequenceSpecial)Sequences.Sequences[103]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("SCHL_Base");
            };

            //Death Masterbate
            Sequences.RegisterSequence(200, new SequenceSpecial("Explicit"));
            ((SequenceSpecial)Sequences.Sequences[200]).OnSequenceExecution += (sender, e) =>
            {
                explicitAnimator.AnimateFade(1f, 400f);
                MessageBox.SetText("Uuughhhh!!!...");
                Sequences.SetStage(201);
            };
            Sequences.RegisterSequence(201, NO_ACTOR, "Oh god... what's going on?!");
            Sequences.RegisterSequence(202, NO_ACTOR, "So much is coming out!!");
            Sequences.RegisterSequence(203, NO_ACTOR, "Someone make it stop!!!");
            Sequences.RegisterSequence(204, new SequenceSpecial("Drown"));
            ((SequenceSpecial)Sequences.Sequences[204]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("BEND_DrownedExplicit");
            };

            //Matrix
            Sequences.RegisterSequence(300, new SequenceSpecial("Explicit"));
            ((SequenceSpecial)Sequences.Sequences[300]).OnSequenceExecution += (sender, e) =>
            {
                explicitAnimator.AnimateFade(1f, 400f);
                MessageBox.SetText("Let's get the show on the road...");
                Sequences.SetStage(301);
            };
            Sequences.RegisterSequence(301, NO_ACTOR, "Wait... what's going on?");
            Sequences.RegisterSequence(302, NO_ACTOR, "This isn't porn!");
            Sequences.RegisterSequence(303, new SequenceSpecial("Matrix"));
            ((SequenceSpecial)Sequences.Sequences[303]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("CMP_Matrix");
            };

            //Suicide Homework
            Sequences.RegisterSequence(400, NO_ACTOR, "I guess I can write about my illness...");
            Sequences.RegisterSequence(401, NO_ACTOR, "*tap* *tap* *tap*");
            Sequences.RegisterSequence(402, NO_ACTOR, "This is really depressing...");
            Sequences.RegisterSequence(403, NO_ACTOR, "*tap* *tap* *tap*");
            Sequences.RegisterSequence(404, NO_ACTOR, "I... I don't know if I can keep this up.");
            Sequences.RegisterSequence(405, NO_ACTOR, "*tap* *tap* *tap*");
            Sequences.RegisterSequence(406, NO_ACTOR, "*sniff*");
            Sequences.RegisterSequence(407, NO_ACTOR, "Screw this!  Fuck this gay earth!");
            Sequences.RegisterSequence(408, new SequenceSpecial("Suicide"));
            ((SequenceSpecial)Sequences.Sequences[408]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("BEND_Suicide");
            };

            //Nic Cage Homework
            Sequences.RegisterSequence(500, NO_ACTOR, "Nicolas Cage is a pretty interesting person.");
            Sequences.RegisterSequence(501, NO_ACTOR, "*tap* *tap* *tap*");
            Sequences.RegisterSequence(502, NO_ACTOR, "Man, this essay is really good so far!  I wonder if Nic would appreciate it...");
            Sequences.RegisterSequence(503, NO_ACTOR, "*knock* *knock* *knock*");
            Sequences.RegisterSequence(504, NO_ACTOR, "What?  Someone is at the door?");
            Sequences.RegisterSequence(505, new SequenceSpecial("Nic"));
            ((SequenceSpecial)Sequences.Sequences[505]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("CMP_Nic");
            };

            //Regular Ass Homework
            Sequences.RegisterSequence(600, NO_ACTOR, "This essay will be my magnum opus!");
            Sequences.RegisterSequence(601, NO_ACTOR, "*tap* *tap* *tap*");
            Sequences.RegisterSequence(602, NO_ACTOR, "Wow!  This has to be the best essay I have ever typed!  I can't wait to get to school.");
            Sequences.RegisterSequence(603, new SequenceSpecial("School"));
            ((SequenceSpecial)Sequences.Sequences[603]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.CurrentTheme = MessageBox.Themes.Normal;
                Manager.ChangeScene("SCHL_Base");
            };
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            desktop.ResetDesktop();
            explicitImage.SetAlpha(0f);
        }
    }
}
