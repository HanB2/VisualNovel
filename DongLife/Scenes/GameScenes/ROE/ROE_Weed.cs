using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class ROE_Weed : VNScene
    {
        private Actor player, weedLord;
        private Image bloodshotAura;
        private Image bong;
        private ControlAnimator auraAnimator;
        private ControlAnimator bongAnimator;

        private float currentOpacity = 0f;
        private int dankness = 0;

        public ROE_Weed() : base("ROE_Weed")
        {
            background = new Background(@"Textures/Backgrounds/weed_lord_stage.png");

            player = ActorFactory.CreateActor("Player");
            weedLord = ActorFactory.CreateActor("WeedLord");

            bloodshotAura = new Image(@"Textures/Misc/bloodshot_aura.png");
            bloodshotAura.Size = new Vector2(GameSettings.WindowWidth, GameSettings.WindowHeight);
            bloodshotAura.Position = Vector2.Zero;
            bloodshotAura.SetAlpha(0f);
            auraAnimator = new ControlAnimator();
            bloodshotAura.AddChild(auraAnimator);

            bong = new Image(@"Textures/Props/bong.png");
            bong.AutoSize = true;
            bong.Position = new Vector2(GameSettings.WindowWidth / 2f, GameSettings.WindowHeight / 2f);
            bong.SetAlpha(0f);
            bongAnimator = new ControlAnimator();
            bong.AddChild(bongAnimator);

            AddChild(background);
            AddChild(bloodshotAura);
            RegisterActor(player);
            RegisterActor(weedLord);

            #region Sequences
            Sequences.RegisterSequence(0, "Player", "Who the heck are you?");
            Sequences.RegisterSequence(1, "WeedLord", "danny dankvito, who u??");
            Sequences.RegisterSequence(2, "Player", "I am {PLAYERNAME}.");
            Sequences.RegisterSequence(3, "WeedLord", "ay yo, u smoke??");

            Sequences.RegisterSequence(4, new SequenceDecision("Player", 
                "'ell yeah",
                "nah brah"));
            ((SequenceDecision)Sequences.Sequences[4]).Choice += (sender, e) =>
            {
                if (e == 0) //Hell yeah
                    Sequences.SetStage(20);
                else if (e == 1) //Nah brah
                    Sequences.SetStage(10);
                Sequences.ExecuteSequence(this);
            };

            //Nah Brah
            Sequences.RegisterSequence(10, "WeedLord", "then wat tha fuk u doin' hurr??");
            Sequences.RegisterSequence(11, new SequenceSceneTransition("BEND_Weed"));

            //Hell Yeah
            Sequences.RegisterSequence(20, new SequenceSpecial("BongFade"));
            ((SequenceSpecial)Sequences.Sequences[20]).OnSequenceExecution += (sender, e) =>
            {
                bongAnimator.AnimateFade(1f, 500f);
                Sequences.SetStage(21);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(21, "WeedLord", "take a hit brah");
            Sequences.RegisterSequence(22, new SequenceDecision("Player",
                "-take a hit-"));
            ((SequenceDecision)Sequences.Sequences[22]).Choice += (sender, e) =>
            {
                Sequences.SetStage(23);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(23, "WeedLord", "cum on, mah grammy can hit harder than that");

            Sequences.RegisterSequence(24, new SequenceDecision("Player",
                "-take a hit-",
                "-take a bigger hit-",
                "-drink it-"));
            ((SequenceDecision)Sequences.Sequences[24]).Choice += (sender, e) =>
            {
                if (e == 0) //Take a hit
                {
                    Sequences.SetStage(30);
                    AddAuraFade(0.2f);
                    dankness += 1;
                }
                else if (e == 1) //Take a bigger hit
                {
                    Sequences.SetStage(40);
                    AddAuraFade(0.4f);
                    dankness += 2;
                }
                else if (e == 2) //Drink that bitch
                {
                    Sequences.SetStage(70);
                    Sequences.ExecuteSequence(this);
                }

                if (dankness >= 4) //You fucking die
                {
                    Sequences.SetStage(50);
                }
                else if (dankness == 3) //Good shit
                {
                    Sequences.SetStage(60);
                }

                Sequences.ExecuteSequence(this);
            };

            //Take a hit (non ending)
            Sequences.RegisterSequence(30, "WeedLord", "weeeeeeaaaaaaak!!  hit it again!");
            Sequences.RegisterSequence(31, new SequenceStageTransition(24));

            //Take a bigger hit (non ending)
            Sequences.RegisterSequence(40, "WeedLord", "daaaaaaamn boiiiii, thats the shit rite there");
            Sequences.RegisterSequence(41, new SequenceStageTransition(24));

            //Overdose
            Sequences.RegisterSequence(50, NO_ACTOR, "You smoked so much weed, you wouldn't believe, and you got more ass than a toilet seat.");
            Sequences.RegisterSequence(51, NO_ACTOR, "And you died.");
            Sequences.RegisterSequence(52, new SequenceSceneTransition("BEND_Overdose"));

            //Proper Baked
            Sequences.RegisterSequence(60, "WeedLord", "ahh shit son, we high as fuk. u wanna get some micky-D??");
            Sequences.RegisterSequence(61, "Player", "Hell yeah!");
            Sequences.RegisterSequence(62, new SequenceSceneTransition("GEND_Weed"));

            //Drink It
            Sequences.RegisterSequence(70, "WeedLord", "bro wat tha fuk, u drank the bong water");
            Sequences.RegisterSequence(71, "WeedLord", "tha fuks 'wrong wit u");
            Sequences.RegisterSequence(72, "WeedLord", "fuk this, im outyy");
            Sequences.RegisterSequence(73, new SequenceSpecial("WeedLordFade"));
            ((SequenceSpecial)Sequences.Sequences[73]).OnSequenceExecution += (sender, e) =>
            {
                bongAnimator.AnimateFade(0f, 250f);
                weedLord.Animator.AnimateFade(0f, 250f);
                Sequences.SetStage(74);
                Sequences.ExecuteSequence(this);
            };
            Sequences.RegisterSequence(74, "Player", "Fuck.");
            Sequences.RegisterSequence(75, new SequenceSceneTransition("BASE_Home"));

            #endregion
        }

        public override void OnEnter()
        {
            base.OnEnter();

            currentOpacity = 0f;
            bloodshotAura.SetAlpha(currentOpacity);
            bong.SetAlpha(0f);
            dankness = 0;

            weedLord.SetAlpha(1f);
        }

        public void AddAuraFade(float amount)
        {
            currentOpacity += amount;
            if (currentOpacity > 1f) currentOpacity = 1f;
            bongAnimator.AnimateFade(currentOpacity, 1000f);
        }
    }
}
