using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class KIJU_Home : VNScene
    {
        private Actor player, professor, baby;

        public KIJU_Home() : base("KIJU_Home")
        {
            background = new Background(@"Textures/Backgrounds/kaiju_dining.png");

            player = ActorFactory.CreateActor("Player");
            professor = ActorFactory.CreateActor("Teacher");
            baby = ActorFactory.CreateActor("BabyKaiju");

            AddChild(background);
            RegisterActor(player);
            RegisterActor(baby);
            RegisterActor(professor);

            #region Sequences
            Sequences.RegisterSequence(0, "Player", "Hey, thank you for having me over Professor Kaiju!  I really appreciate it.");
            Sequences.RegisterSequence(1, "Teacher", "Don't mention it, {PLAYERNAME}.  It is a pleasure to have you over.");
            Sequences.RegisterSequence(2, new SequenceDecision("Player",
                "Wow, you have a very nice house!",
                "Your son is very adorable."));
            ((SequenceDecision)Sequences.Sequences[2]).Choice += (sender, e) =>
            {
                if (e == 0) //Nice house
                    Sequences.SetStage(3);
                else if (e == 1) //Nice baby
                    Sequences.SetStage(6);

                Sequences.ExecuteSequence(this);
            };

            //Nice House
            Sequences.RegisterSequence(3, "Teacher", "Why, thank you {PLAYERNAME}.  Yes, we were able to afford this beautiful home after the insurance payout.");
            Sequences.RegisterSequence(4, "Teacher", "You see, my dearly beloved wife was murdered not too long ago and it was very disheartening.");
            Sequences.RegisterSequence(5, new SequenceStageTransition(10));

            //Nice Baby
            Sequences.RegisterSequence(6, "Teacher", "Why, thank you {PLAYERNAME}.  Yes, this is my son Baby Kaiju.  Say hi to our guest, son.");
            Sequences.RegisterSequence(7, "BabyKaiju", "...");
            Sequences.RegisterSequence(8, "Teacher", "Well, he is very shy.  His mother was murdered not too long ago and it has been very hard on him.");
            Sequences.RegisterSequence(9, new SequenceStageTransition(10));

            Sequences.RegisterSequence(10, "Teacher", "The police investigation into the matter never resulted in much, saddly.  They seemed fairly dismissive of the issue.");
            Sequences.RegisterSequence(11, new SequenceDecision("Player",
                "I didn't ask for your life story.",
                "That is terrible!"));
            ((SequenceDecision)Sequences.Sequences[11]).Choice += (sender, e) =>
            {
                if (e == 0) //Didn't ask
                    Sequences.SetStage(12);
                else if (e == 1) //Terrible
                    Sequences.SetStage(16);

                Sequences.ExecuteSequence(this);
            };

            //Didn't ask
            Sequences.RegisterSequence(12, "Teacher", "What... Okay, I'm sorry.  I didn't mean to bring it up.");
            Sequences.RegisterSequence(13, "Player", "I'm hungry, where is the food?");
            Sequences.RegisterSequence(14, "Teacher", "Yes, let us begin.  I'm going to put Baby Kaiju to bed real quick.  Go ahead and take a seat.");
            Sequences.RegisterSequence(15, new SequenceStageTransition(20));

            //Terrible
            Sequences.RegisterSequence(16, "Teacher", "Yes, it was very tragic.  I know who did it, but we can discuss this more over dinner.");
            Sequences.RegisterSequence(17, "Teacher", "Let me put Baby Kaiju to bed.  Go ahead and take a seat, if you will.");
            Sequences.RegisterSequence(18, new SequenceStageTransition(20));

            //Dinner Sequences
            Sequences.RegisterSequence(20, new SequenceSpecial("ProfessorLeaves"));
            ((SequenceSpecial)Sequences.Sequences[20]).OnSequenceExecution += (sender, e) =>
            {
                professor.Animator.AnimateFade(0f, 800f);
                baby.Animator.AnimateFade(0f, 800f);

                Sequences.SetStage(21);
            };

            Sequences.RegisterSequence(21, "Player", "Well... what should I do before he gets back?");
            Sequences.RegisterSequence(22, new SequenceDecision("Player",
                "Poison his food with odorless poison.",
                "Poison his food with obvious poison.",
                "Wait patiently for him."));
            ((SequenceDecision)Sequences.Sequences[22]).Choice += (sender, e) =>
            {
                if (e == 0) //Odorless poison
                    Sequences.SetStage(30);
                else if (e == 1) //Obvious poison
                    Sequences.SetStage(40);
                else if (e == 2) //Wait patiently
                    Sequences.SetStage(100);

                Sequences.ExecuteSequence(this);
            };

            //Odorless Poison
            Sequences.RegisterSequence(30, "Player", "Take this asshole!");
            Sequences.RegisterSequence(31, new SequenceSpecial("ProfessorComesBack"));
            ((SequenceSpecial)Sequences.Sequences[31]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Teacher");
                MessageBox.SetText("I am back, {PLAYERNAME}!");

                professor.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(32);
            };
            Sequences.RegisterSequence(32, "Teacher", "Well, how is...");
            Sequences.RegisterSequence(33, "Teacher", "...");
            Sequences.RegisterSequence(34, "Teacher", "Did you poison my food?");
            Sequences.RegisterSequence(35, "Player", "Nooo...");
            Sequences.RegisterSequence(36, "Teacher", "You know I'll have to kill you, right?");
            Sequences.RegisterSequence(37, "Player", "No, please don't!");
            Sequences.RegisterSequence(38, new SequenceSceneTransition("BEND_KaijuDeath"));

            //Obvious Poison
            Sequences.RegisterSequence(40, "Player", "Take this asshole!");
            Sequences.RegisterSequence(41, new SequenceSpecial("ProfessorComesBack"));
            ((SequenceSpecial)Sequences.Sequences[41]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Teacher");
                MessageBox.SetText("I am back, {PLAYERNAME}!");

                professor.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(42);
            };
            Sequences.RegisterSequence(42, "Teacher", "Wow, I didn't realize the food smelled THAT good!  Let's dig in!");
            Sequences.RegisterSequence(43, "Player", "This food is real good, Professor Kaiju!");
            Sequences.RegisterSequence(44, "Teacher", "Why thank... thank you {PLAYERNAME}...");
            Sequences.RegisterSequence(45, "Teacher", "...");
            Sequences.RegisterSequence(46, "Teacher", "I don't feel so good...");
            Sequences.RegisterSequence(47, "Teacher", "!HURGH!");
            Sequences.RegisterSequence(48, new SequenceSpecial("KillBaby"));
            ((SequenceSpecial)Sequences.Sequences[48]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Teacher");
                MessageBox.SetText("!BLURGH!");

                GameManager.TeacherDied = true;

                professor.Animator.AnimateFade(0f, 800f);
                Sequences.SetStage(49);
            };
            Sequences.RegisterSequence(49, "Player", "Yes!  Take that you scaly freak!");
            Sequences.RegisterSequence(50, "Player", "Oh wait... he has a kid.  What should I do now?");
            Sequences.RegisterSequence(51, new SequenceDecision("Player",
                "Kill the kid.",
                "Cheese it."));
            ((SequenceDecision)Sequences.Sequences[51]).Choice += (sender, e) =>
            {
                if (e == 0) //Kill the kid
                    Sequences.SetStage(60);
                else if (e == 1) //Cheese it
                    Sequences.SetStage(80);

                Sequences.ExecuteSequence(this);
            };

            //Kill the kid
            Sequences.RegisterSequence(60, "Player", "Hey, Baby Kaiju!  Come down here!");
            Sequences.RegisterSequence(61, new SequenceSpecial("BabyKaijuComes"));
            ((SequenceSpecial)Sequences.Sequences[61]).OnSequenceExecution += (sender, e) =>
            {
                baby.Animator.AnimateFade(1f, 800f);

                SetActorFocus("BabyKaiju");
                MessageBox.SetText("What goin' on?");

                Sequences.SetStage(62);
            };
            Sequences.RegisterSequence(62, "BabyKaiju", "Where daddy?");
            Sequences.RegisterSequence(63, "Player", "I'm your daddy now.  Now die.");
            Sequences.RegisterSequence(64, "BabyKaiju", "No, please!");
            Sequences.RegisterSequence(65, new SequenceSpecial("BabyKaijuComes"));
            ((SequenceSpecial)Sequences.Sequences[65]).OnSequenceExecution += (sender, e) =>
            {
                baby.Animator.AnimateFade(0f, 800f);

                SetActorFocus("BabyKaiju");
                MessageBox.SetText("!BLURGH!");

                Sequences.SetStage(66);
            };
            Sequences.RegisterSequence(66, "Player", "Time to get out of here!");
            Sequences.RegisterSequence(67, new SequenceSceneTransition("BASE_Home"));

            //Cheese it
            Sequences.RegisterSequence(80, "Player", "I gotta get out of here!");
            Sequences.RegisterSequence(81, new SequenceSceneTransition("KIJU_HomeArrest"));

            //Wait Patiently
            Sequences.RegisterSequence(100, "Player", "Well, I'll just hang out until he gets back.");
            Sequences.RegisterSequence(101, new SequenceSpecial("ProfessorComesBack"));
            ((SequenceSpecial)Sequences.Sequences[101]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Teacher");
                MessageBox.SetText("I am back, {PLAYERNAME}!");

                professor.Animator.AnimateFade(1f, 800f);
                Sequences.SetStage(102);
            };
            Sequences.RegisterSequence(102, "Player", "This food is absolutely wonderful, Professor Kaiju!");
            Sequences.RegisterSequence(103, "Teacher", "Yes, I worked very hard to prepare a nice meal for my guest.");
            Sequences.RegisterSequence(104, new SequenceDecision("Player",
                "Inquire about his wife.",
                "Ignore the elephant in the room."));
            ((SequenceDecision)Sequences.Sequences[104]).Choice += (sender, e) =>
            {
                if (e == 0) //Inquire
                    Sequences.SetStage(110);
                else if (e == 1) //Ignore
                    Sequences.SetStage(105);

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(105, NO_ACTOR, "*You two have small talk as the evening progresses*");
            Sequences.RegisterSequence(106, "Player", "Welp, that was a very nice dinner, Professor Kaiju, but I better get going.");
            Sequences.RegisterSequence(107, "Teacher", "It was a very nice dinner, {PLAYERNAME}.  I would love for you to come over again another time.");
            Sequences.RegisterSequence(108, new SequenceSceneTransition("BASE_Home"));

            Sequences.RegisterSequence(110, "Player", "Do you mind telling me more about your wife?");
            Sequences.RegisterSequence(111, "Teacher", "Well... It was only about three months ago when it happened.");
            Sequences.RegisterSequence(112, "Teacher", "A bunch of hooligans from school thought it would be a great prank to light my home on fire.");
            Sequences.RegisterSequence(113, "Player", "Holy shit!");
            Sequences.RegisterSequence(114, "Teacher", "My wife, naturally being allergic to fire, died as soon as the blaze reached our room.  It was really terrible.  I was barely able to save our son from a similar fate. :(");
            Sequences.RegisterSequence(115, new SequenceDecision("Player",
                "Wow, that is a really tragic story.",
                "She deserved it, you dirty reptilian freak."));
            ((SequenceDecision)Sequences.Sequences[115]).Choice += (sender, e) =>
            {
                if (e == 0) //Tragic Story
                    Sequences.SetStage(120);
                else if (e == 1) //Dirty Reptilian Freak
                    Sequences.SetStage(116);

                Sequences.ExecuteSequence(this);
            };

            //Dirty reptilian freak
            Sequences.RegisterSequence(116, "Teacher", "Why you little...");
            Sequences.RegisterSequence(117, new SequenceSceneTransition("BEND_KaijuDeath"));

            //Tragic story
            Sequences.RegisterSequence(120, "Teacher", "Thank you for your sympathy, {PLAYERNAME}.");
            Sequences.RegisterSequence(121, "Player", "You said you knew who did it?");
            Sequences.RegisterSequence(122, "Teacher", "Yes!  It was that gang of Jaegers that hang out behind the school.  They're blatantly racist against Kaijus.");
            Sequences.RegisterSequence(123, "Teacher", "They were caught on our security video that the police 'lost' on accident.  All racists, I must say.");
            Sequences.RegisterSequence(124, "Player", "Wow, that's really terrible!  A similar thing happened to the body cameras of the police that attacked my parents.  They don't really like us non-humans.");
            Sequences.RegisterSequence(125, "Teacher", "No, they do not, {PLAYERNAME}...");
            Sequences.RegisterSequence(126, "Teacher", "I have a proposition {PLAYERNAME}... Being a Kaiju, I am exceptionally good at destroying civilizations...");
            Sequences.RegisterSequence(127, "Teacher", "Would you fight alongside me and destroy this racist world and rebuild it as a utopia of acceptance?");
            Sequences.RegisterSequence(128, new SequenceDecision("Player",
                "Sure.",
                "Nah."));
            ((SequenceDecision)Sequences.Sequences[128]).Choice += (sender, e) =>
            {
                if (e == 0) //Sure
                    Sequences.SetStage(131);
                else if (e == 1) //Nah
                    Sequences.SetStage(129);

                Sequences.ExecuteSequence(this);
            };

            //Nah
            Sequences.RegisterSequence(129, "Teacher", "Oh, well that's a shame.  Come back and talk to me again if you change your mind.");
            Sequences.RegisterSequence(130, new SequenceSceneTransition("BASE_Home"));

            //Sure
            Sequences.RegisterSequence(131, "Teacher", "Great!  Follow me!");
            Sequences.RegisterSequence(132, new SequenceSceneTransition("KIJU_WorldDomination"));


            //Not first dinner
            Sequences.RegisterSequence(200, "Teacher", "Say hi to {PLAYERNAME}, Baby Kaiju.");
            Sequences.RegisterSequence(201, "BabyKaiju", "...hi");
            Sequences.RegisterSequence(202, "Player", "Hi, Baby Kaiju");
            Sequences.RegisterSequence(203, "Teacher", "I'm going to take him and put him to bed, then we'll continue the dinner.");
            Sequences.RegisterSequence(204, new SequenceStageTransition(20));
            #endregion
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (GameManager.HadDinner)
            {
                Sequences.SetStage(200);
                Sequences.ExecuteSequence(this);
            }
            else
                GameManager.HadDinner = true;
        }
    }
}
