using OpenTK;

namespace DongLife.Code
{
    public static class AccessoryManager
    {
        public static Accessory[] Hats;
        public static Accessory[] MaleShirts;
        public static Accessory[] FemaleShirts;
        public static Accessory[] Misc;

        public static void Init()
        {
            Hats = new Accessory[7];
            MaleShirts = new Accessory[5];
            FemaleShirts = new Accessory[5];
            Misc = new Accessory[7];

            Hats[0] = new Accessory("Textures/Accessories/Hats/hat_01.png", new Vector2(4, -425f), 1);
            Hats[1] = new Accessory("Textures/Accessories/Hats/hat_02.png", new Vector2(4, -470f), 1);
            Hats[2] = new Accessory("Textures/Accessories/Hats/hat_03.png", new Vector2(8, -455f), 1);
            Hats[3] = new Accessory("Textures/Accessories/Hats/hat_04.png", new Vector2(2, -475f), 1);
            Hats[4] = new Accessory("Textures/Accessories/Hats/hat_05.png", new Vector2(4, -474f), 1);
            Hats[5] = new Accessory("Textures/Accessories/Hats/hat_06.png", new Vector2(6, -475f), 1);
            Hats[6] = new Accessory("Textures/Accessories/Hats/hat_07.png", new Vector2(3, -470f), 1);

            MaleShirts[0] = new Accessory("Textures/Accessories/MaleShirts/mshirt_01.png", new Vector2(7f, -184f), 0);
            MaleShirts[1] = new Accessory("Textures/Accessories/MaleShirts/mshirt_02.png", new Vector2(7f, -184f), 0);
            MaleShirts[2] = new Accessory("Textures/Accessories/MaleShirts/mshirt_03.png", new Vector2(7f, -184f), 0);
            MaleShirts[3] = new Accessory("Textures/Accessories/MaleShirts/mshirt_04.png", new Vector2(7f, -184f), 0);
            MaleShirts[4] = new Accessory("Textures/Accessories/MaleShirts/mshirt_05.png", new Vector2(7f, -184f), 0);

            FemaleShirts[0] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_01.png", new Vector2(12f, -176f), 0);
            FemaleShirts[1] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_02.png", new Vector2(12f, -176f), 0);
            FemaleShirts[2] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_03.png", new Vector2(12f, -176f), 0);
            FemaleShirts[3] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_04.png", new Vector2(12f, -176f), 0);
            FemaleShirts[4] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_05.png", new Vector2(12f, -176f), 0);

            Misc[0] = new Accessory("Textures/Accessories/Misc/acc_01.png", new Vector2(8f, -310f), 2);
            Misc[1] = new Accessory("Textures/Accessories/Misc/acc_02.png", new Vector2(55f, -175f), 2);
            Misc[2] = new Accessory("Textures/Accessories/Misc/acc_03.png", new Vector2(-340f, -140f), 2);
            Misc[3] = new Accessory("Textures/Accessories/Misc/acc_04.png", new Vector2(-275f, 40f), 2);
            Misc[4] = new Accessory("Textures/Accessories/Misc/acc_05.png", new Vector2(85f, -350f), 2);
            Misc[5] = new Accessory("Textures/Accessories/Misc/acc_06.png", new Vector2(8f, -410f), 2);
            Misc[6] = new Accessory("Textures/Accessories/Misc/acc_07.png", new Vector2(10f, -395f), 2);
        }
    }
}
