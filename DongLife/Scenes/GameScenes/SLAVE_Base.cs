using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SLAVE_Base : VNScene
    {
        private ShekelsCounter shekelsCounter;
        private FatigueBar fatigueBar;
        private Actor player, guard, headhoncho, shopOwner, playerDisguise;
        private int shekels = 0;
        private int dongLevel = 0;
        private int timesSlept = 0;
        private int fatigue = 100;

        public SLAVE_Base() : base("SLAVE_Base")
        {
            background = new Background(@"Textures/Backgrounds/slave_camp.png");

            shekelsCounter = new ShekelsCounter();
            shekelsCounter.Position = new Vector2(GameSettings.WindowWidth / 2 + 180f, GameSettings.WindowHeight - 330f);

            fatigueBar = new FatigueBar();
            fatigueBar.Position = new Vector2(GameSettings.WindowWidth / 2 - 320f, GameSettings.WindowHeight / 2 + 95f);

            player = ActorFactory.CreateActor("Player");
            playerDisguise = ActorFactory.CreateActor("Player");
            playerDisguise.Name = "PlayerDisguised";
            playerDisguise.TexturePath = @"Textures/Actors/head_honcho_skin.png";

            guard = new Actor("Guard", @"Textures/Actors/slave_guard.png");
            guard.Position = new Vector2(GameSettings.WindowWidth / 2 + 250f, GameSettings.WindowHeight / 2 + 150f);
            guard.CurrentScale = 1.25f;
            guard.NormalScale = 1.25f;
            guard.FocusScale = 1.5f;

            headhoncho = new Actor("HeadHoncho", @"Textures/Actors/head_honcho.png");
            headhoncho.Position = new Vector2(GameSettings.WindowWidth / 2 + 250f, GameSettings.WindowHeight / 2 + 150f);
            headhoncho.CurrentScale = 0.8f;
            headhoncho.NormalScale = 0.8f;
            headhoncho.FocusScale = 0.9f;

            shopOwner = new Actor("ShopOwner", @"Textures/Actors/shop_owner.png");
            shopOwner.Position = new Vector2(GameSettings.WindowWidth / 2 + 250f, GameSettings.WindowHeight / 2 + 150f);
            shopOwner.CurrentScale = 1f;
            shopOwner.NormalScale = 0.8f;
            shopOwner.FocusScale = 1f;

            AddChild(background);
            AddChild(shekelsCounter);
            AddChild(fatigueBar);
            RegisterActor(player);
            RegisterActor(playerDisguise);
            RegisterActor(guard);
            RegisterActor(headhoncho);
            RegisterActor(shopOwner);

            Sequences.RegisterSequence(0, "Player", "Where am I?");
            Sequences.RegisterSequence(1, "Guard", "Welcome to Camp La Fuckya!");
            Sequences.RegisterSequence(2, "Player", "Camp La Fuckwhat?");
            Sequences.RegisterSequence(3, "Guard", "Camp La Fuckya!");
            Sequences.RegisterSequence(4, "Player", "Okay... where is Camp La Fuckya?");
            Sequences.RegisterSequence(5, "Guard", "Cambodia!");
            Sequences.RegisterSequence(6, "Player", "What am I doing here?");
            Sequences.RegisterSequence(7, "Guard", "You're a sex slave now!");
            Sequences.RegisterSequence(8, "Player", "What?!");
            Sequences.RegisterSequence(9, "Guard", "You've been sold to us to perform sexual acts for all of our clients!");
            Sequences.RegisterSequence(10, "Player", "But... I don't want to be a sex slave...");
            Sequences.RegisterSequence(11, "Guard", "Well, then I guess I get to kill you!");
            Sequences.RegisterSequence(12, new SequenceDecision("Player",
                "Accept new life as a slave.",
                "Attempt to fight the guard."));
            ((SequenceDecision)Sequences.Sequences[12]).Choice += (sender, e) =>
            {
                if (e == 0) //Accept life
                    Sequences.SetStage(20);
                else if (e == 1) //Attempt to fight
                    Sequences.SetStage(13);

                Sequences.ExecuteSequence(this);
            };

            //Attempt to fight the guard
            Sequences.RegisterSequence(13, "Player", "I will destroy you!");
            Sequences.RegisterSequence(14, "Guard", "You know I have a gun, right?");
            Sequences.RegisterSequence(15, "Player", "Yaaarrrr!!!");
            Sequences.RegisterSequence(16, NO_ACTOR, "*bang*");
            Sequences.RegisterSequence(17, new SequenceSceneTransition("BEND_SlaveGunDeath"));

            //Accept life
            Sequences.RegisterSequence(20, "Player", "Welp, the slave life might not be so bad after all!");
            Sequences.RegisterSequence(21, "Guard", "That's the spirit!");
            Sequences.RegisterSequence(22, new SequenceSpecial("StartOfLoop"));
            ((SequenceSpecial)Sequences.Sequences[22]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus("Guard");
                MessageBox.SetText("Now get to work!");

                guard.Animator.FadeOut(800f);
                shekelsCounter.Animator.FadeIn(800f);
                fatigueBar.Animator.FadeIn(800f);
                Sequences.SetStage(30);
            };

            //Slave Loop
            Sequences.RegisterSequence(30, "Player", "Well.  Time to get to work.");
            Sequences.RegisterSequence(31, new SequenceDecision("Player",
                "Go to work.",
                "Go to shop.",
                "Sleep."));
            ((SequenceDecision)Sequences.Sequences[31]).Choice += (sender, e) =>
            {
                if (e == 0) //Go to work
                {
                    shekels += 10;
                    shekelsCounter.SetAmount(shekels);

                    fatigue -= 25;
                    fatigueBar.SetAmount(fatigue / 100f);

                    if (fatigue <= 0f)
                    {
                        Manager.SetScene("BEND_SlaveWorkToDeath");
                    }

                    if (shekels < 100)
                    {
                        SetActorFocus(NO_ACTOR);
                        MessageBox.SetText("*You worked hard for hours, earning yourself 10 shekels*");
                        Sequences.SetStage(31);
                    }
                    else
                    {
                        Manager.SetScene("BEND_SlaveDeath");
                    }
                }
                else if (e == 1) //Go to shop
                {
                    MessageBox.SetText("*You enter into the shop*");
                    shopOwner.Animator.FadeIn(800f);
                    Sequences.SetStage(40);
                }
                else if (e == 2) //Sleep
                {
                    timesSlept++;
                    fatigue = 100;
                    fatigueBar.SetAmount(fatigue / 100f);

                    if (timesSlept >= 5)
                    {
                        SetActorFocus(NO_ACTOR);
                        MessageBox.SetText("What do you think you're doing?!");
                        guard.Animator.FadeIn(800f);

                        Sequences.SetStage(35);
                    }
                    else
                    {
                        Sequences.SetStage(32);
                        Sequences.ExecuteSequence(this);
                    }
                }
            };

            //Sleep
            Sequences.RegisterSequence(32, NO_ACTOR, "*You take a nice little snooze*");
            Sequences.RegisterSequence(33, new SequenceStageTransition(31));

            //Sleep too much
            Sequences.RegisterSequence(35, "Guard", "Hey!  You're a sex worker!  Not a sex sleeper!");
            Sequences.RegisterSequence(36, "Guard", "Well, I guess I get to put you down now... Exciting!");
            Sequences.RegisterSequence(37, "Player", "Wait!  No!!!");
            Sequences.RegisterSequence(38, new SequenceSceneTransition("BEND_SlaveSleepDeath"));

            //Shop
            Sequences.RegisterSequence(40, "ShopOwner", "Welcome to Camp Lafuckya Super Shoppe!  What can I do for ya?");
            Sequences.RegisterSequence(41, new SequenceDecision("Player",
                "Purchase Dong Upgrade.",
                "What is this place?"));
            ((SequenceDecision)Sequences.Sequences[41]).Choice += (sender, e) =>
            {
                if (e == 0) //Purchase Dong Upgrade
                {
                    if (shekels >= 15)
                        Sequences.SetStage(42);
                    else
                        Sequences.SetStage(45);
                }
                else if (e == 1) //What is this place?
                {
                    Sequences.SetStage(50);
                }

                Sequences.ExecuteSequence(this);
            };

            //Purchase the dong upgrade
            Sequences.RegisterSequence(42, "Player", "I would like to purchase one dong upgrade please!");
            Sequences.RegisterSequence(43, "ShopOwner", "Here ya go, lad!  Only 15 shekels.");
            Sequences.RegisterSequence(44, new SequenceSpecial("BoughtUpgrade"));
            ((SequenceSpecial)Sequences.Sequences[44]).OnSequenceExecution += (sender, e) =>
            {
                shekels -= 15;
                shekelsCounter.SetAmount(shekels);

                dongLevel++;
                SetActorFocus(NO_ACTOR);
                shopOwner.Animator.FadeOut(800f);
                MessageBox.SetText("Your current dong level is: " + dongLevel.ToString());

                if (dongLevel < 3)
                    Sequences.SetStage(31);
                else
                {
                    headhoncho.Animator.FadeIn(800f);
                    shekelsCounter.Animator.FadeOut(800f);
                    fatigueBar.Animator.FadeOut(800f);
                    Sequences.SetStage(60);
                }
            };

            //Can't afford the dong upgrade
            Sequences.RegisterSequence(45, "Player", "I would like to purchase one dong upgrade please!");
            Sequences.RegisterSequence(46, "ShopOwner", "Get out of here with ya nonsense!  You can't afford no dong upgrade!  Costs 15 shekels.");
            Sequences.RegisterSequence(47, new SequenceSpecial("BoughtUpgrade"));
            ((SequenceSpecial)Sequences.Sequences[47]).OnSequenceExecution += (sender, e) =>
            {
                shopOwner.Animator.FadeOut(800f);

                Sequences.SetStage(31);
                //Sequences.ExecuteSequence(this);
            };

            //What is this place
            Sequences.RegisterSequence(50, "ShopOwner", "Well this is the shoppe for Camp Lafuckya.  Here, ya can buy upgrades.");
            Sequences.RegisterSequence(51, "Player", "Upgrades for what?");
            Sequences.RegisterSequence(52, "ShopOwner", "Can you not read the dialog options, ya whippersnapper?  You upgrade ya dong!");
            Sequences.RegisterSequence(53, "Player", "What the fuck are you talking about?");
            Sequences.RegisterSequence(54, "ShopOwner", "It's fairly simple, ya see?  Ya upgrade yar dong and ya will gain renown.  Enough renown, ya get a visit from the camp's head honcho, ya see?");
            Sequences.RegisterSequence(55, "Player", "Oh okay!  Now I see.");
            Sequences.RegisterSequence(56, "ShopOwner", "Good!  Now buy something or get the fuck outta my store!");
            Sequences.RegisterSequence(57, new SequenceStageTransition(41));

            //Head Honcho visit
            Sequences.RegisterSequence(60, "HeadHoncho", "Hey there {PLAYERNAME}!");
            Sequences.RegisterSequence(61, "Player", "Who are you?!");
            Sequences.RegisterSequence(62, "HeadHoncho", "I am the Head Honcho!  How's Camp Lafuckya treating you?");
            Sequences.RegisterSequence(63, "Player", "Well, it's hard work, but I think I'm getting used to it.");
            Sequences.RegisterSequence(64, "HeadHoncho", "Good!  I hate naysayers.  Really nice dong by the way.");
            Sequences.RegisterSequence(65, new SequenceDecision("Player",
                "Kill the Head Honcho.",
                "Play along."));
            ((SequenceDecision)Sequences.Sequences[65]).Choice += (sender, e) =>
            {
                if (e == 0) //Kill Head Honcho
                {
                    Sequences.SetStage(70);
                }
                else if (e == 1) //Play along
                {
                    Sequences.SetStage(66);
                }

                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(66, "Player", "Thanks!");
            Sequences.RegisterSequence(67, "HeadHoncho", "Well... nice talking to you!");
            Sequences.RegisterSequence(68, new SequenceSceneTransition("BEND_SlaveDeath"));

            //Kill Head Honcho
            Sequences.RegisterSequence(70, "Player", "Take this!");
            Sequences.RegisterSequence(71, "HeadHoncho", "What?");
            Sequences.RegisterSequence(72, NO_ACTOR, "*With your massive dong updgraded to level 3, you overwhelmed the Head Honcho, killing him instantly*");
            Sequences.RegisterSequence(73, new SequenceSpecial("HeadHonchoDead"));
            ((SequenceSpecial)Sequences.Sequences[73]).OnSequenceExecution += (sender, e) =>
            {
                MessageBox.SetText("!BLURGH!");
                headhoncho.Animator.FadeOut(800f);
                Sequences.SetStage(74);
            };
            Sequences.RegisterSequence(74, "Player", "I did it!  Now what?");
            Sequences.RegisterSequence(75, new SequenceDecision("Player",
                "Use the Head Honcho's skin as a disguise.",
                "Cheese it!"));
            ((SequenceDecision)Sequences.Sequences[75]).Choice += (sender, e) =>
            {
                if (e == 0) //Use his skin
                {
                    Sequences.SetStage(80);
                }
                else if (e == 1) //Cheese it!
                {
                    Sequences.SetStage(76);
                }

                Sequences.ExecuteSequence(this);
            };

            //Cheese it
            Sequences.RegisterSequence(76, "Player", "I'm getting the fuck out of here!");
            Sequences.RegisterSequence(77, new SequenceSpecial("GuardsKillPlayer"));
            ((SequenceSpecial)Sequences.Sequences[77]).OnSequenceExecution += (sender, e) =>
            {
                guard.Animator.FadeIn(800f);
                Sequences.SetStage(78);
            };
            Sequences.RegisterSequence(78, "Guard", "Stop that runaway slave!");
            Sequences.RegisterSequence(79, new SequenceSceneTransition("BEND_SlaveDieByGuards"));

            //Use his skin
            Sequences.RegisterSequence(80, new SequenceSpecial("HeadHonchoDead"));
            ((SequenceSpecial)Sequences.Sequences[80]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus(NO_ACTOR);
                MessageBox.SetText("Time to get cutting!");

                player.Animator.FadeOut(800f);
                playerDisguise.Animator.FadeIn(800f);

                Sequences.SetStage(81);
            };
            Sequences.RegisterSequence(81, "PlayerDisguised", "This is oddly comfortable!");
            Sequences.RegisterSequence(82, new SequenceSceneTransition("GEND_HeadHoncho"));
        }

        public override void OnEnter()
        {
            base.OnEnter();

            headhoncho.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            shopOwner.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            shekelsCounter.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            fatigueBar.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            playerDisguise.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
        }
    }
}
