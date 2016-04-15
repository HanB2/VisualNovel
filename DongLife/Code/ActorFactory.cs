using OpenTK;

namespace DongLife.Code
{
    public static class ActorFactory
    {
        private static Player playerRef;

        public static void Init()
        {
            playerRef = new Player();
            playerRef.Position = new Vector2(300f, 650f);
            playerRef.NormalScale = 1f;
            playerRef.FocusScale = 1.25f;
            playerRef.CurrentScale = 1.25f;
            playerRef.DrawColor = GameManager.PlayerColor;

            attachAccessories(playerRef);
        }
        public static void UpdatePlayerModel()
        {
            playerRef.TexturePath = GameManager.TexturePath;
            playerRef.Position = new Vector2(300f, 650f);
            playerRef.NormalScale = 1f;
            playerRef.FocusScale = 1.25f;
            playerRef.CurrentScale = 1.25f;
            playerRef.DrawColor = GameManager.PlayerColor;

            attachAccessories(playerRef);
        }

        public static Actor CreateActor(string actorName)
        {
            switch (actorName)
            {
                case "Player":
                    playerRef.Position = new Vector2(300f, 650f);
                    return playerRef;
                case "PlayerNew":
                    Player player = new Player();
                    player.Position = new Vector2(300f, 650f);
                    player.NormalScale = 1f;
                    player.FocusScale = 1.25f;
                    player.CurrentScale = 1.25f;

                    attachAccessories(player);

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
                    mother.Position = new Vector2(725, 500);
                    mother.NormalScale = 1f;
                    mother.FocusScale = 1.25f;
                    mother.NormalScale = 1f;

                    return mother;
                case "SexyMother":
                    Actor sexyMother = new Actor("Mother", @"Textures/Actors/foster_mother_sexy.png");
                    sexyMother.Position = new Vector2(725, 500);
                    sexyMother.NormalScale = 1f;
                    sexyMother.FocusScale = 1.25f;
                    sexyMother.NormalScale = 1f;

                    return sexyMother;
                case "ShiaMother":
                    Actor shiaMother = new Actor("ShiaMother", @"Textures/Actors/shia_mother.png");
                    shiaMother.Position = new Vector2(725, 500);
                    shiaMother.NormalScale = 1f;
                    shiaMother.FocusScale = 1.25f;
                    shiaMother.NormalScale = 1f;

                    return shiaMother;
                case "Janitor":
                    Actor janitor = new Actor("Janitor", @"Textures/Actors/janitor_normal.png");
                    janitor.Position = new Vector2(GameSettings.WindowWidth / 2 + 350f, GameSettings.WindowHeight / 2 + 75f);
                    janitor.CurrentScale = 0.65f;
                    janitor.NormalScale = 0.65f;
                    janitor.FocusScale = 0.75f;

                    return janitor;
                case "Principal":
                    Actor principal = new Actor("Principal", @"Textures/Actors/principal_normal.png");
                    principal.Position = new Vector2(GameSettings.WindowWidth / 2 + 300f, GameSettings.WindowHeight / 2 + 75f);
                    principal.CurrentScale = 0.75f;
                    principal.NormalScale = 0.75f;
                    principal.FocusScale = 0.9f;

                    return principal;
                case "Father":
                    Actor father = new Actor("Father", @"Textures/Actors/foster_father.png");
                    father.Position = new Vector2(980, 425);
                    father.NormalScale = 0.75f;
                    father.FocusScale = 0.8f;
                    father.NormalScale = 0.75f;

                    return father;
            }

            throw new System.ArgumentException("Invalid Actor: " + actorName);
        }

        private static void attachAccessories(Player player)
        {
            if (GameManager.HatIndex != -1)
                player.EquipAccessory(AccessoryManager.Hats[GameManager.HatIndex]);
            if (GameManager.ShirtIndex != -1)
            {
                if (GameManager.Gender == "Male")
                    player.EquipAccessory(AccessoryManager.MaleShirts[GameManager.ShirtIndex]);
                else
                    player.EquipAccessory(AccessoryManager.FemaleShirts[GameManager.ShirtIndex]);
            }
            if (GameManager.MiscIndex != -1)
                player.EquipAccessory(AccessoryManager.Misc[GameManager.MiscIndex]);

            //player.DrawColor = GameManager.PlayerColor;
        }
    }
}
