using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Controls;

namespace DongLife.Code
{
    public static class ActorFactory
    {
        public static Actor CreateActor(string actorName)
        {
            switch (actorName)
            {
                case "Player":
                    Actor player = new Actor("Player", @"Textures/Actors/player_male.png");
                    player.Position = new Vector2(300f, 650f);
                    player.NormalScale = 1f;
                    player.FocusScale = 1.25f;
                    player.CurrentScale = 1.25f;

                    return player;
                case "Professor":
                    Actor professor = new Actor("Professor", @"Textures/Actors/professor_kaiju.png");
                    professor.Position = new Vector2(1080f, 550f);
                    professor.NormalScale = 1f;
                    professor.FocusScale = 1.25f;
                    professor.CurrentScale = 1f;

                    return professor;
                case "Mother":
                    Actor mother = new Actor("Mother", @"Textures/Actors/foster_mother.png");
                    mother.NormalScale = 1f;
                    mother.FocusScale = 1.25f;
                    mother.NormalScale = 1f;

                    return mother;
                case "Father":
                    Actor father = new Actor("Father", @"Textures/Actors/foster_father.png");
                    father.NormalScale = 0.75f;
                    father.FocusScale = 0.8f;
                    father.NormalScale = 0.75f;

                    return father;
            }

            throw new System.ArgumentException("Invalid Actor: " + actorName);
        }
    }
}
