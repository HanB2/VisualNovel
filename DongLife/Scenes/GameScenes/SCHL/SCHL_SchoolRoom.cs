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
            jaeger = ActorFactory.CreateActor("JaegerPrime");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(teacher);
            RegisterActor(jaeger);

            #region Sequences
            Sequences.RegisterSequence(0, "Player", "Hey!  I'm here, sorry I'm late!");
            Sequences.RegisterSequence(1, "Teacher", "First day of class and you're already late.  Not a good start for the year, now is it?");
            Sequences.RegisterSequence(2, "Player", "Again, I'm sorry... my mom was being weird and there was this janitor...");
            Sequences.RegisterSequence(3, "Teacher", "No excuses.  Take a seat and pay attention.");
            Sequences.RegisterSequence(4, "Teacher", "Now class, turn in your textbooks to page 347 and begin taking notes.");
            Sequences.RegisterSequence(5, "JaegerPrime", "Fuck you, Prof!");
            Sequences.RegisterSequence(6, new SequenceSpecial("JaegerFadeIn"));
            ((SequenceSpecial)Sequences.Sequences[6]).OnSequenceExecution += (sender, e) =>
            {
                jaeger.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(7);
            };
            Sequences.RegisterSequence(7, "Teacher", "Jaeger Prime... I swear to god if you interrupt my lecture one more time, you will be severely punished!");
            Sequences.RegisterSequence(8, "JaegerPrime", "Bite me!");
            Sequences.RegisterSequence(9, "Teacher", "That's it!  To the principal's office with you!");
            Sequences.RegisterSequence(10, "JaegerPrime", "Whatevs...");
            Sequences.RegisterSequence(11, new SequenceSpecial("JaegerFadeOut"));
            ((SequenceSpecial)Sequences.Sequences[11]).OnSequenceExecution += (sender, e) =>
            {
                jaeger.Animator.AnimateFade(0f, 800f);
                Sequences.SetStage(12);
            };
            Sequences.RegisterSequence(12, "Teacher", "Okay... With that out of the way, let us return to the lesson.");
            Sequences.RegisterSequence(13, NO_ACTOR, "*HOURS PASS*");
            Sequences.RegisterSequence(14, "Teacher", "And that is our lesson for the day.  Thank you class for your undivided attention, please be here on time tomorrow.");
            Sequences.RegisterSequence(15, "Teacher", "{PLAYERNAME}, please see me after class, if you will.");
            Sequences.RegisterSequence(16, new SequenceDecision("Player", 
                "Meet the professor after class.",
                "Bail on his reptilian ass."));
            ((SequenceDecision)Sequences.Sequences[16]).Choice += (sender, e) =>
            {
                if (e == 0) //Meet the professor
                    Sequences.SetStage(20);
                else if (e == 1) //Bail
                    Sequences.SetStage(17);

                Sequences.ExecuteSequence(this);
            };

            //Bail and meet Jaeger Prime
            Sequences.RegisterSequence(17, "Player", "Screw this loser professor, I'm out!");
            Sequences.RegisterSequence(18, new SequenceSceneTransition("SCHL_Hallway"));

            //Meet the professor
            Sequences.RegisterSequence(20, "Teacher", "Thank you for seeing me, {PLAYERNAME}.  Most of the students here typically don't respect me enough to stay.");
            Sequences.RegisterSequence(21, new SequenceSpecial("CheckHomework"));
            ((SequenceSpecial)Sequences.Sequences[21]).OnSequenceExecution += (sender, e) =>
            {
                if (GameManager.CompletedHomework)
                {
                    GameManager.CompletedHomework = false;
                    Sequences.SetStage(22);
                }
                else
                    Sequences.SetStage(30);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(22, "Teacher", "I wanted to tell you that I finished grading your essay on the Meaning of Life.");
            Sequences.RegisterSequence(23, "Teacher", "Well... since you're a new student, I'll be lenient and give you a D.  It was absolutely atrocious.  Do better next time, why don't you?");
            Sequences.RegisterSequence(24, new SequenceStageTransition(30));

            Sequences.RegisterSequence(30, "Teacher", "Now, onto why I asked you stay after class.  I read the news about your run in with the police.  Mind telling me what happened?");
            Sequences.RegisterSequence(31, "Player", "No one typically believes me when I say anything... I doubt you will either.");
            Sequences.RegisterSequence(32, "Teacher", "Try me.");
            Sequences.RegisterSequence(33, "Player", "Well... we were driving home from dinner when the police pulled over our car.  They mistook my giant bulge as a weapon and told us to stand down.");
            Sequences.RegisterSequence(34, "Player", "My father tried talking to them, but it was too late.  They opened fire and I don't remember much after that.  I just remember waking up in a hospital room with a new family.");
            Sequences.RegisterSequence(35, "Teacher", "That's absolutely terrible, {PLAYERNAME}!  The paper made it sound like it was all your fault!");
            Sequences.RegisterSequence(36, "Player", "Most people don't take too kindly with us Giant Dongitus carrying folk.  It's a hard life.");
            Sequences.RegisterSequence(37, "Teacher", "You're talking to a Kaiju, {PLAYERNAME}.  I know my fair share of prejudice.");
            Sequences.RegisterSequence(38, "Teacher", "You know what, {PLAYERNAME}?  How about you join me and my family tonight for dinner?  We can discuss your situation more in depth.");
            Sequences.RegisterSequence(39, new SequenceDecision("Player",
                "Sure, why not?",
                "I think I'll pass."));
            ((SequenceDecision)Sequences.Sequences[39]).Choice += (sender, e) =>
            {
                if (e == 0) //Yes
                    Sequences.SetStage(40);
                else if (e == 1) //No
                    Sequences.SetStage(50);

                Sequences.ExecuteSequence(this);
            };

            //Yes
            Sequences.RegisterSequence(40, "Teacher", "Wonderful!  Follow me, {PLAYERNAME}.");
            Sequences.RegisterSequence(41, new SequenceSceneTransition("KIJU_Home"));

            //No
            Sequences.RegisterSequence(50, "Teacher", "Well that is too bad, {PLAYERNAME}.  I was sure we would hit it off well.  Well, I will see you tomorrow.");
            Sequences.RegisterSequence(51, "Teacher", "Hopefully on time, yes?");
            Sequences.RegisterSequence(52, new SequenceSceneTransition("BASE_Home"));

            Sequences.RegisterSequence(100, "Player", "Oh wait, I killed my professor.  There's no class!  Woo!");
            Sequences.RegisterSequence(101, new SequenceSceneTransition("BASE_Home"));


            //After first day
            Sequences.RegisterSequence(200, "Teacher", "Welcome students, please turn in your textbook to page 347 and begin the assignment.");
            Sequences.RegisterSequence(201, "JaegerPrime", "Fuck you, Prof!");
            Sequences.RegisterSequence(202, new SequenceSpecial("JaegerFadeIn"));
            ((SequenceSpecial)Sequences.Sequences[202]).OnSequenceExecution += (sender, e) =>
            {
                jaeger.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(203);
            };
            Sequences.RegisterSequence(203, "Teacher", "Jaeger Prime... I swear to god if you interrupt my lecture one more time, you will be severely punished!");
            Sequences.RegisterSequence(204, "JaegerPrime", "Bite me!");
            Sequences.RegisterSequence(205, "Teacher", "That's it!  To the principal's office with you!");
            Sequences.RegisterSequence(206, "JaegerPrime", "Whatevs...");
            Sequences.RegisterSequence(207, new SequenceSpecial("JaegerFadeOut"));
            ((SequenceSpecial)Sequences.Sequences[207]).OnSequenceExecution += (sender, e) =>
            {
                jaeger.Animator.AnimateFade(0f, 800f);
                Sequences.SetStage(208);
            };
            Sequences.RegisterSequence(208, "Teacher", "Okay... With that out of the way, let us return to the lesson.");
            Sequences.RegisterSequence(209, NO_ACTOR, "*HOURS PASS*");
            Sequences.RegisterSequence(210, "Teacher", "And that is our lesson for the day.  Thank you class for your undivided attention, please be here on time tomorrow.");
            Sequences.RegisterSequence(211, "Player", "Hey, professor!");
            Sequences.RegisterSequence(212, "Teacher", "Hello, {PLAYERNAME}!  Would you be interested in coming over for dinner again?  Baby Kaiju really likes you, even though he doesn't show it.");
            Sequences.RegisterSequence(213, new SequenceDecision("Player",
                "Yes!",
                "I'll pass this time."));
            ((SequenceDecision)Sequences.Sequences[213]).Choice += (sender, e) =>
            {
                if (e == 0) //Yes
                    Sequences.SetStage(214);
                else if (e == 1) //Nah
                    Sequences.SetStage(220);

                Sequences.ExecuteSequence(this);
            };

            //Yes
            Sequences.RegisterSequence(214, "Teacher", "Alright, let us go then!");
            Sequences.RegisterSequence(215, new SequenceSceneTransition("KIJU_Home"));

            //Nah
            Sequences.RegisterSequence(220, "Teacher", "That's a damn shame.  You are welcome anytime.  Have a nice day, {PLAYERNAME}.");
            Sequences.RegisterSequence(221, new SequenceSceneTransition("BASE_Home"));
            #endregion
        }

        public override void OnEnter()
        {
            base.OnEnter();

            jaeger.SetAlpha(0f);
            jaeger.Position = new Vector2(640, 510);

            if (GameManager.TeacherDied)
            {
                teacher.SetAlpha(0f);
                Sequences.SetStage(100);

                Sequences.ExecuteSequence(this);
            }
            else if (GameManager.AttendedClass)
            {
                Sequences.SetStage(200);
                Sequences.ExecuteSequence(this);
            }
            else
                GameManager.AttendedClass = true;
        }
    }
}
