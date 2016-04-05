using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;
using Minalear;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Date : VNScene
    {
        private Actor player, principal, principalAlt;
        private Texture2D staticFilter;

        private bool timeParadox = false;
        private bool filterVisible = false;
        private float staticTimer = 0f;
        private float nextBlip = 500f;
        private float blipLength = 400f;

        public SCHL_Date() : base("SCHL_Date")
        {
            background = new Background(@"");

            player = ActorFactory.CreateActor("Player");
            principal = ActorFactory.CreateActor("Principal");
            principalAlt = ActorFactory.CreateActor("Principal");
            principalAlt.Name = "PrincipalAlt";
            principalAlt.TexturePath = "Textures/Actors/principal_shia.png";

            AddChild(background);
            RegisterActor(player);
            RegisterActor(principal);
            RegisterActor(principalAlt);

            //Movie Date
            Sequences.RegisterSequence(0, "Principal", "Man... Pacific Rim 2 is really good.");
            Sequences.RegisterSequence(1, "Player", "I know, these giant robots really know how to put a good fucking to these giant monsters.");
            Sequences.RegisterSequence(2, "Principal", "So what do you think we should do now?");
            Sequences.RegisterSequence(3, new SequenceDecision("Player",
                "Seduce the Principal.",
                "Imply the Principal owes you sex."));
            ((SequenceDecision)Sequences.Sequences[3]).Choice += (sender, e) =>
            {
                if (e == 0) //Seduction
                    Sequences.SetStage(4);
                else if (e == 1) //Extort Sex
                    Sequences.SetStage(40);

                Sequences.ExecuteSequence(this);
            };

            //Seduction
            Sequences.RegisterSequence(4, "Principal", "What do you think I am?  A floozy!");
            Sequences.RegisterSequence(5, new SequenceSceneTransition("BEND_DateDeath"));

            //Baseball Date
            Sequences.RegisterSequence(10, "Principal", "Man... This baseball game is really good.");
            Sequences.RegisterSequence(11, "Player", "I know, these baseball players really know how to baseball.");
            Sequences.RegisterSequence(12, new SequenceStageTransition(2));

            //Bowling Date
            Sequences.RegisterSequence(20, "Principal", "Wow, I completely kicked your ass!  I'm surprised you even hit a single pin!");
            Sequences.RegisterSequence(21, "Player", "Pfft... I let you win.");
            Sequences.RegisterSequence(22, "Principal", "A 260 to 44 is hardly letting me win.");
            Sequences.RegisterSequence(23, "Player", "Okay, well I'm terrible at bowling, so what?  My stats aren't very high.");
            Sequences.RegisterSequence(24, new SequenceStageTransition(50));

            //Laser Tag
            Sequences.RegisterSequence(30, "Principal", "Wow, I completely kicked your ass!  You didn't even hit me once!");
            Sequences.RegisterSequence(31, "Player", "Pfft... I let you win.");
            Sequences.RegisterSequence(32, "Principal", "Yea.  I'm sure.");
            Sequences.RegisterSequence(33, "Player", "Okay fine, you're a god at laser tag.  What was I supposed to do?");
            Sequences.RegisterSequence(34, "Principal", "It's okay, I'll teach you the ropes sometime... :)");
            Sequences.RegisterSequence(35, new SequenceStageTransition(50));

            //Extort Sex
            Sequences.RegisterSequence(40, "Player", "Well, since I paid good money for this date, I think you owe me a blowie.");
            Sequences.RegisterSequence(41, "Principal", "I think you're right!");
            Sequences.RegisterSequence(42, NO_ACTOR, "*You two perform the dirty right there in front of everyone.  Many patrons leave while a few stay to watch*");
            Sequences.RegisterSequence(43, "Player", "Wow!  That was really great.  Where did you learn to do that?");
            Sequences.RegisterSequence(44, "Principal", "Cambodia... :)");
            Sequences.RegisterSequence(45, new SequenceStageTransition(60));

            //Romantic Dialog
            Sequences.RegisterSequence(50, "Principal", "You're a really special person, you know that?");
            Sequences.RegisterSequence(51, "Player", "You are too...");
            Sequences.RegisterSequence(52, "Principal", "I think I love you.");
            Sequences.RegisterSequence(53, "Player", "I have really strong feelings for you too, but are you sure this is love?");
            Sequences.RegisterSequence(54, "Principal", "What else could it be?  I haven't seen your dong this hard all night.");
            Sequences.RegisterSequence(55, "Player", "You're right... it gets really hard when I'm in love.");
            Sequences.RegisterSequence(56, new SequenceStageTransition(60));

            //Revelation
            Sequences.RegisterSequence(60, "Principal", "I can't lie to you anymore... I'm not what I seem to be.");
            Sequences.RegisterSequence(61, "Player", "What do you mean, baby?");
            Sequences.RegisterSequence(62, "Principal", "I'm...");
            Sequences.RegisterSequence(63, "Principal", "*sniff*");
            Sequences.RegisterSequence(64, new SequenceSpecial("ShiaReveal"));
            ((SequenceSpecial)Sequences.Sequences[64]).OnSequenceExecution += (sender, e) =>
            {
                SetActorFocus(NO_ACTOR);
                principal.Animator.FadeOut(800f);
                principalAlt.Animator.FadeIn(800f);

                Sequences.SetStage(65);
            };
            Sequences.RegisterSequence(65, "PrincipalAlt", "I'm Shia LeBeouf.");
            Sequences.RegisterSequence(66, "Player", "OH GOD!  What is wrong with your neck?!");
            Sequences.RegisterSequence(67, "PrincipalAlt", "Don't worry about my neck baby... I just need to know if you're okay with me being this hideous creature.");
            Sequences.RegisterSequence(68, new SequenceDecision("Player",
                "Accept Shia's Love.",
                "Deny Shia's Love."));
            ((SequenceDecision)Sequences.Sequences[68]).Choice += (sender, e) =>
            {
                if (e == 0) //Accept shia love
                {
                    if (GameManager.DeniedFeelings)
                    {
                        timeParadox = true;
                        Sequences.SetStage(90); //Time Paradox
                    }
                    else
                        Sequences.SetStage(70); //Regular accept
                }
                else if (e == 1) //Deny shia love
                {
                    Sequences.SetStage(80);
                    GameManager.DeniedFeelings = true;
                }

                Sequences.ExecuteSequence(this);
            };

            //Accept
            Sequences.RegisterSequence(70, "Player", "I'm more than okay with you being a Shia LeBeouf!  I will always love you, no matter what!");
            Sequences.RegisterSequence(71, "PrincipalAlt", "Really?  Oh, you make me the happiest Shia LeBeouf on Earth!");
            Sequences.RegisterSequence(72, "PrincipalAlt", "Quick, run away with me, my little rapscallion!  Leave this shitty life behind and let's find our own destiny!");
            Sequences.RegisterSequence(73, "Player", "Yea!  Let's go!");
            Sequences.RegisterSequence(74, new SequenceSceneTransition("GEND_GrowOldShia"));

            //Deny
            Sequences.RegisterSequence(80, "Player", "A Shia LeBeouf?!  What the fuck do you think I am?  A dog fucker?!  I can't believe you would lie to me like this...");
            Sequences.RegisterSequence(81, "PrincipalAlt", "I guess I understand that.  I am a dirty beast, not worthy of anyone's love...  I will miss you... my little rapscallion. :'(");
            Sequences.RegisterSequence(82, "Player", "I'm going home!  Loser!");
            Sequences.RegisterSequence(83, new SequenceSceneTransition("BASE_Home"));

            //Time Paradox
            Sequences.RegisterSequence(90, "Player", "I͢͞'̨҉̛͜͝m͏҉̸͟ ̡̛m̸̢͝͡o̢҉r͢ę̕ ͟͢t̷̡̛h̕҉a̶̴͢n̵͡ ̢͡͞o͠҉͘k̶͝͡a̵̸̵y̵̢̢͏ ̢͟͝w҉̶͞i̸̛ţ̴͠͏̶h̡̕͡͡͡ ͏̶̨̕͏y̵̨̧̕͢ǫ̧͜͠u̴̢͟ ͝͡b̶̀é̛i̢n̢͡͏́͝g̷̢̕ ̡á͏̛́͘ ̷̛S̷̕ḩ̷̧͢i̡͞͞҉ą͜͡ ̵̧͞͞L̸̡̕͞͠e̶̴̸̸B̴̕͢e̸͘͟o҉̶̢́͘ù̕͠͝f̶͝͏̴͏!̀͏͏̵͟ ̴̴̨́ ͜҉̢̨I̴̢͡ ̡͏̶͝w̶̷̛͘í̴̛̕͝l͝͡͏̛l̵͘͏ ̷͝a̛̛l̢̕w̷҉a̷̧̛ý̸̴͝s̸͘͟͏ ̷͢͠͠l̡̕͡͡ó͜͝v̸e͏ ́͘͜y͘͟͝ó̴̕͟͟ư̧͜͢͠,̸͘ ̵͟ņ́͟͢͝o̷͝ ̷m̧̧͜a̶͝͠͝t̵͞͞t̸̨̢͝e͠͏r̷̨̧͜͠ ̵̡͟͝w̶͠h̨̢̧͞a̵̵t̸͟!̕͜͢");
            Sequences.RegisterSequence(91, new SequenceSceneTransition("GEND_TimeParadox"));

        }

        public override void OnEnter()
        {
            base.OnEnter();

            principalAlt.DrawColor = new OpenTK.Graphics.Color4(1f, 1f, 1f, 0f);
            timeParadox = false;
        }

        public override void LoadContent(ContentManager content)
        {
            staticFilter = content.LoadTexture2D(@"Textures/Props/static.png");
            switch (GameManager.ChosenDate)
            {
                case "MOVIES":
                    background.Path = @"Textures/Backgrounds/movie_theater.png";
                    Sequences.SetStage(0);
                    break;
                case "BASEBALL":
                    background.Path = @"Textures/Backgrounds/baseball_game.png";
                    Sequences.SetStage(10);
                    break;
                case "BOWLING":
                    background.Path = @"Textures/Backgrounds/bowling_alley.png";
                    Sequences.SetStage(20);
                    break;
                case "LASER":
                    background.Path = @"Textures/Backgrounds/laser_tag.png";
                    Sequences.SetStage(30);
                    break;
            }

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            staticFilter.Dispose();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (filterVisible)
            {
                spriteBatch.Draw(staticFilter, Vector2.Zero, OpenTK.Graphics.Color4.White);
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (timeParadox)
            {
                staticTimer += (float)gameTime.ElapsedTime.TotalMilliseconds;
                if (filterVisible)
                {
                    if (staticTimer >= blipLength)
                    {
                        staticTimer = 0f;
                        filterVisible = !filterVisible;
                        blipLength = RNG.NextFloat(300f, 1750f);
                    }
                }
                else
                {
                    if (staticTimer >= nextBlip)
                    {
                        staticTimer = 0f;
                        filterVisible = !filterVisible;
                        nextBlip = RNG.NextFloat(200f, 500f);
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
