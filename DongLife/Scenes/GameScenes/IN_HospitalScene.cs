﻿using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;
using Minalear;

namespace DongLife.Scenes.GameScenes
{
    public class IN_HospitalScene : VNScene
    {
        private CharacterCreator creator;
        private Actor doctor, player;
        private Actor mother, father;

        public IN_HospitalScene() : base("IN_HospitalScene")
        {
            background = new Background(@"Textures/Backgrounds/hospital_room.png");

            creator = new CharacterCreator();
            creator.Enabled = false;
            creator.Visible = false;
            creator.CharacterCreated += CharacterCreatedEvent;

            doctor = new Actor("Doctor", @"Textures/Actors/doctor.png");
            doctor.NormalScale = 0.6f;
            doctor.FocusScale = 0.65f;
            doctor.CurrentScale = 0.65f;

            mother = ActorFactory.CreateActor("Mother");
            father = ActorFactory.CreateActor("Father");

            doctor.Animator.AnimationEnd += Animator_AnimationEnd;
            mother.Animator.AnimationEnd += Animator_AnimationEnd;
            father.Animator.AnimationEnd += Animator_AnimationEnd;

            player = ActorFactory.CreateActor("Player");
            player.Position = new Vector2(300f, 650f);
            player.Visible = false;
            player.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);

            AddChild(background);
            AddChild(creator);

            RegisterActor(doctor);
            RegisterActor(player);
            RegisterActor(mother);
            RegisterActor(father);

            #region Sequences
            Sequences.RegisterSequence(00, new SequenceMessage("Doctor", "Hello!  I seem to have misplaced all of my documentation about you, so... what is your name?"));
            Sequences.RegisterSequence(01, new SequenceStageTransition(0));
            Sequences.RegisterSequence(02, new SequenceMessage("Doctor", "Nice to meet you {PLAYERNAME}.  Now... can you describe your physical appearance?  I seem to have misplaced my glasses."));
            Sequences.RegisterSequence(03, new SequenceMessage(NO_ACTOR, "PRETEND THERE IS A CHARACTER CREATOR HERE!"));
            Sequences.RegisterSequence(04, new SequenceSpecial("SpawnPlayer"));
            (Sequences.Sequences[04] as SequenceSpecial).OnSequenceExecution += (sender, e) =>
            {
                doctor.Animator.AnimateMove(new Vector2(800f, doctor.PosY), 250f);

                player.Visible = true;
                player.Animator.AnimateFade(1f, 250f);
            };
            Sequences.RegisterSequence(05, new SequenceMessage("Doctor", "Wow... that sounds hideous."));
            Sequences.RegisterSequence(06, new SequenceMessage("Player", "Hey, fuck you pal.  Where are my parents?"));
            Sequences.RegisterSequence(07, new SequenceMessage("Doctor", "Don't be testy now.  You're parents are dead and gone.  You have been placed into a foster home by the state."));
            Sequences.RegisterSequence(08, new SequenceMessage("Player", "Wut!?"));
            Sequences.RegisterSequence(09, new SequenceMessage("Doctor", "Yea, people typically don't survive 37 shots to the head.  Your new foster parents are outside and they're excited to meet you."));
            Sequences.RegisterSequence(10, new SequenceMessage("Player", "Wut!?"));
            Sequences.RegisterSequence(11, new SequenceSpecial("SpawnParents"));
            (Sequences.Sequences[11] as SequenceSpecial).OnSequenceExecution += (sender, e) =>
            {
                doctor.Animator.AnimateFade(0f, 250f);

                mother.Visible = true;
                father.Visible = true;

                mother.Animator.AnimateFade(1f, 500f);
                father.Animator.AnimateFade(1f, 500f);
            };
            Sequences.RegisterSequence(12, new SequenceMessage("Father", "Hello, son.  I'm your new father."));
            Sequences.RegisterSequence(13, new SequenceMessage("Player", "Wut!?"));
            Sequences.RegisterSequence(14, new SequenceMessage("Father", "Calm the fuck down."));
            Sequences.RegisterSequence(15, new SequenceMessage("Mother", "Calm down honey, you're scaring the lad."));
            Sequences.RegisterSequence(16, new SequenceMessage("Player", "WHY DO YOU HAVE A FISH HEAD?!"));
            Sequences.RegisterSequence(17, new SequenceMessage("Father", "What?!  This fucker is a racist!  I knew this was a bad idea."));
            Sequences.RegisterSequence(18, new SequenceMessage("Mother", "Calm down dear, he's just... confused is all.  He's not a racist."));
            Sequences.RegisterSequence(19, new SequenceMessage("Player", "HOW DO YOU EVEN FUNCTION?!"));
            Sequences.RegisterSequence(20, new SequenceMessage("Father", "Alright, fuck this kid.  I'll be out in the car."));
            Sequences.RegisterSequence(21, new SequenceSpecial("FatherStormsOut"));
            (Sequences.Sequences[21] as SequenceSpecial).OnSequenceExecution += (sender, e) =>
            {
                father.Animator.AnimateFade(0f, 100f);
                mother.Animator.AnimateMove(new Vector2(880f, mother.PosY), 250f);
            };
            Sequences.RegisterSequence(22, new SequenceMessage("Mother", "Don't worry, he's just a little hot headed.  But my... you seem to be a strapping young man ;)"));
            Sequences.RegisterSequence(23, new SequenceDecision("Player",
                "WHY DOES HE HAVE A FISH FOR A HEAD?!",
                "Who are you people?",
                "Where am I?"));
            (Sequences.Sequences[23] as SequenceDecision).Choice += (sender, e) =>
            {
                if (e == 0)
                    Sequences.SetStage(100);
                else if (e == 1)
                    Sequences.SetStage(200);
                else if (e == 2)
                    Sequences.SetStage(300);

                Sequences.ExecuteSequence(this);
            };

            //WHY DOES HE HAVE A FISH FOR A HEAD?!
            Sequences.RegisterSequence(100, new SequenceMessage("Player", "Why... why is his head a giant fish?"));
            Sequences.RegisterSequence(101, new SequenceMessage("Mother", "Well, he is a Fishman."));
            Sequences.RegisterSequence(102, new SequenceMessage("Player", "Fishman?"));
            Sequences.RegisterSequence(103, new SequenceMessage("Mother", "Yes, like... half man, half fish.  His people have been persecuted for many years and there are only a handful left.  He's very sensitive about that fact."));
            Sequences.RegisterSequence(104, new SequenceMessage("Mother", "Me being human and him being just a Fishman, we were unable to produce offspring, which makes him even more upset."));
            Sequences.RegisterSequence(105, new SequenceMessage("Player", "Can humans and fishpeople not reproduce?"));
            Sequences.RegisterSequence(106, new SequenceMessage("Mother", "No, his penis is just too small. ;)"));
            Sequences.RegisterSequence(107, new SequenceStageTransition(23));

            //Who are you people?
            Sequences.RegisterSequence(200, new SequenceMessage("Player", "Who are you people?"));
            Sequences.RegisterSequence(201, new SequenceMessage("Mother", "We're your new foster parents.  Me and my hubby have been wanting a child for years now, but we have not been able to produce any ourselves.  So we decided to just adopt one."));
            Sequences.RegisterSequence(202, new SequenceMessage("Player", "But I'm 37, why on Earth do I need foster parents?"));
            Sequences.RegisterSequence(203, new SequenceMessage("Mother", "The state decided they didn't trust you enough to not harass police with your... package... :)"));
            Sequences.RegisterSequence(204, new SequenceMessage("Player", "That's not even how it happened... whatever."));
            Sequences.RegisterSequence(205, new SequenceStageTransition(23));

            //Where am I?
            Sequences.RegisterSequence(300, new SequenceMessage("Player", "Where am I?"));
            Sequences.RegisterSequence(301, new SequenceMessage("Mother", "You have been hospitalized due to your recent run-in with the police.  The doctor couldn't save your parents, but thankfully your massive... gift absorbed most of the damage."));
            Sequences.RegisterSequence(302, new SequenceMessage("Player", "This thing has only ever gotten me into trouble.  I absolutely hate it."));
            Sequences.RegisterSequence(303, new SequenceMessage("Mother", "Now young man, so many people would dream of having such a gift.  You should be thankful."));
            Sequences.RegisterSequence(304, new SequenceMessage("Player", "It's not a gift, it's an absolute curse!"));
            Sequences.RegisterSequence(305, new SequenceMessage("Mother", "Don't be so down on yourself.  Here, let me take you home and I can give you a special treat to calm your nerves. ;)"));
            Sequences.RegisterSequence(306, new SequenceSceneTransition("IN_Home"));
            #endregion
        }

        public override void OnEnter()
        {
            creator.Enabled = true;
            creator.Visible = true;

            player.Visible = false;
            mother.Visible = false;
            father.Visible = false;
            player.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);

            doctor.Position = new Vector2(640, 425);
            mother.Position = new Vector2(725, 500);
            father.Position = new Vector2(980, 425);

            MessageBox.Visible = false;

            base.OnEnter();
        }

        private void Animator_AnimationEnd(object sender, Animations.AnimationTypes finishedMode)
        {
            if (Sequences.GetCurrentSequence().SequenceStage == 4 && finishedMode == Animations.AnimationTypes.Move)
            {
                Sequences.ProgressStage();
                Sequences.ExecuteSequence(this);
            }
            else if (Sequences.GetCurrentSequence().SequenceStage == 11 && finishedMode == Animations.AnimationTypes.Fade)
            {
                Sequences.ProgressStage();
                Sequences.ExecuteSequence(this);
                doctor.Visible = false;
            }
            else if (Sequences.GetCurrentSequence().SequenceStage == 21 && finishedMode == Animations.AnimationTypes.Fade)
            {
                Sequences.ProgressStage();
                Sequences.ExecuteSequence(this);
                father.Visible = false;
            }
        }
        private void TextInput_OnSubmitText(object sender, string text)
        {
            GameSettings.PlayerName = text.Trim();
            Sequences.SetStage(2);
            Sequences.ExecuteSequence(this);

            creator.Enabled = false;
            creator.Visible = false;
        }
        private void CharacterCreatedEvent(object sender)
        {

        }
    }
}
