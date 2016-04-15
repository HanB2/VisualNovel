using System.Collections.Generic;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Code;

namespace DongLife.Controls
{
    public class CharacterCreator : Control
    {
        private GeoButton submitButton;
        private TextInput textInput;

        private Player female, male;
        private Accessory[] hats, maleShirts, femaleShirts, misc;

        //Accessory Pointers, -1 represents no accessory
        private int selectedHat = -1;
        private int selectedShirt = -1;
        private int selectedMisc = -1;

        private bool maleSelected = false;

        public CharacterCreator()
        {
            DrawOrder = 0f;
            Size = new Vector2(1280, 720); //Fix mouse down bug

            //UI Setup
            GeoPanel panel = new GeoPanel();
            panel.Position = new Vector2(10f, 10f);
            panel.Size = new Vector2(1260f, 700f);

            submitButton = new GeoButton();
            submitButton.Position = new Vector2(600f, 480f);
            submitButton.Size = new Vector2(640f, 32f);
            submitButton.ButtonPressed += SubmitButton_ButtonPressed;

            textInput = new TextInput(640, 48);
            textInput.Position = new Vector2(600f, 50f);

            CreatorOptionSelector genderOption = new CreatorOptionSelector("Body Type",  new Vector2(600, 120), 640);
            CreatorOptionSelector hatOption =    new CreatorOptionSelector("Hat",        new Vector2(600, 192), 640);
            CreatorOptionSelector shirtOption =  new CreatorOptionSelector("Shirt",      new Vector2(600, 264), 640);
            CreatorOptionSelector accOption =    new CreatorOptionSelector("Accessory",  new Vector2(600, 336), 640);
            CreatorOptionSelector colorOption =  new CreatorOptionSelector("Skin Color", new Vector2(600, 408), 640);

            genderOption.SelectionChanged += GenderOption_SelectionChanged;
            hatOption.SelectionChanged += HatOption_SelectionChanged;
            shirtOption.SelectionChanged += ShirtOption_SelectionChanged;
            accOption.SelectionChanged += AccOption_SelectionChanged;
            colorOption.SelectionChanged += ColorOption_SelectionChanged;

            female = ActorFactory.CreateActor("Player") as Player;
            female.TexturePath = @"Textures/Actors/player_female.png";
            male = ActorFactory.CreateActor("Player") as Player;

            female.Position = new Vector2(female.Position.X, 380);
            female.CurrentScale = 0.65f;
            male.Position = new Vector2(male.Position.X, 380);
            male.CurrentScale = 0.65f;

            AddChild(panel);
            AddChild(textInput);
            AddChild(genderOption);
            AddChild(hatOption);
            AddChild(shirtOption);
            AddChild(accOption);
            AddChild(colorOption);
            AddChild(submitButton);

            initAccessories();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (maleSelected)
                male.Draw(spriteBatch);
            else
                female.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager content)
        {
            female.LoadContent(content);
            male.LoadContent(content);

            loadAccessories(content);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            female.UnloadContent();
            male.UnloadContent();

            unloadAccessories();

            base.UnloadContent();
        }

        private void SubmitButton_ButtonPressed(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (CharacterCreated != null)
            {
                GameManager.PlayerName = textInput.Text.Trim();

                if (maleSelected)
                    GameManager.TexturePath = @"Textures/Actors/player_male.png";
                else
                    GameManager.TexturePath = @"Textures/Actors/player_female.png";

                CharacterCreated(this);
            }
        }
        private void initAccessories()
        {
            hats = new Accessory[7];
            maleShirts = new Accessory[5];
            femaleShirts = new Accessory[5];
            misc = new Accessory[7];

            hats[0] = new Accessory("Textures/Accessories/Hats/hat_01.png", new Vector2(4, -277f));
            hats[1] = new Accessory("Textures/Accessories/Hats/hat_02.png", new Vector2(4, -300f));
            hats[2] = new Accessory("Textures/Accessories/Hats/hat_03.png", new Vector2(8, -285f));
            hats[3] = new Accessory("Textures/Accessories/Hats/hat_04.png", new Vector2(2, -305f));
            hats[4] = new Accessory("Textures/Accessories/Hats/hat_05.png", new Vector2(4, -304f));
            hats[5] = new Accessory("Textures/Accessories/Hats/hat_06.png", new Vector2(6, -305f));
            hats[6] = new Accessory("Textures/Accessories/Hats/hat_07.png", new Vector2(3, -300f));

            maleShirts[0] = new Accessory("Textures/Accessories/MaleShirts/mshirt_01.png", new Vector2(6f, -120f));
            maleShirts[1] = new Accessory("Textures/Accessories/MaleShirts/mshirt_02.png", new Vector2(6f, -120f));
            maleShirts[2] = new Accessory("Textures/Accessories/MaleShirts/mshirt_03.png", new Vector2(6f, -120f));
            maleShirts[3] = new Accessory("Textures/Accessories/MaleShirts/mshirt_04.png", new Vector2(6f, -120f));
            maleShirts[4] = new Accessory("Textures/Accessories/MaleShirts/mshirt_05.png", new Vector2(6f, -120f));

            femaleShirts[0] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_01.png", new Vector2(7f, -114f));
            femaleShirts[1] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_02.png", new Vector2(7f, -114f));
            femaleShirts[2] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_03.png", new Vector2(7f, -114f));
            femaleShirts[3] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_04.png", new Vector2(7f, -114f));
            femaleShirts[4] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_05.png", new Vector2(7f, -114f));

            misc[0] = new Accessory("Textures/Accessories/Misc/acc_01.png", new Vector2(5f, -195f));
            misc[1] = new Accessory("Textures/Accessories/Misc/acc_02.png", new Vector2(30f, -117f));
            misc[2] = new Accessory("Textures/Accessories/Misc/acc_03.png", new Vector2(-240f, -140f));
            misc[3] = new Accessory("Textures/Accessories/Misc/acc_04.png", new Vector2(-190f, 40f));
            misc[4] = new Accessory("Textures/Accessories/Misc/acc_05.png", new Vector2(65f, -220f));
            misc[5] = new Accessory("Textures/Accessories/Misc/acc_06.png", new Vector2(5f, -265f));
            misc[6] = new Accessory("Textures/Accessories/Misc/acc_07.png", new Vector2(7f, -258f));
        }
        private void loadAccessories(ContentManager content)
        {
            for (int i = 0; i < hats.Length; i++)
                hats[i].LoadContent(content);

            for (int i = 0; i < misc.Length; i++)
                misc[i].LoadContent(content);

            for (int i = 0; i < maleShirts.Length; i++)
                maleShirts[i].LoadContent(content);

            for (int i = 0; i < femaleShirts.Length; i++)
                femaleShirts[i].LoadContent(content);
        }
        private void unloadAccessories()
        {
            for (int i = 0; i < hats.Length; i++)
                hats[i].UnloadContent();

            for (int i = 0; i < misc.Length; i++)
                misc[i].UnloadContent();

            for (int i = 0; i < maleShirts.Length; i++)
                maleShirts[i].UnloadContent();

            for (int i = 0; i < femaleShirts.Length; i++)
                femaleShirts[i].UnloadContent();
        }

        private void GenderOption_SelectionChanged(object sender, int value)
        {
            maleSelected = !maleSelected;
        }
        private void HatOption_SelectionChanged(object sender, int value)
        {
            if (selectedHat != -1)
            {
                male.RemoveAccessory(hats[selectedHat]);
                female.RemoveAccessory(hats[selectedHat]);
            }

            selectedHat += value;
            if (selectedHat >= hats.Length)
                selectedHat = -1;
            if (selectedHat < -1)
                selectedHat = hats.Length - 1;

            if (selectedHat != -1)
            {
                male.EquipAccessory(hats[selectedHat]);
                female.EquipAccessory(hats[selectedHat]);
            }
        }
        private void ShirtOption_SelectionChanged(object sender, int value)
        {
            if (selectedShirt != -1)
            {
                male.RemoveAccessory(maleShirts[selectedShirt]);
                female.RemoveAccessory(femaleShirts[selectedShirt]);
            }

            selectedShirt += value;
            if (selectedShirt >= maleShirts.Length)
                selectedShirt = -1;
            if (selectedShirt < -1)
                selectedShirt = maleShirts.Length - 1;

            if (selectedShirt != -1)
            {
                male.EquipAccessory(maleShirts[selectedShirt]);
                female.EquipAccessory(femaleShirts[selectedShirt]);
            }
        }
        private void AccOption_SelectionChanged(object sender, int value)
        {
            if (selectedMisc != -1)
            {
                male.RemoveAccessory(misc[selectedMisc]);
                female.RemoveAccessory(misc[selectedMisc]);
            }

            selectedMisc += value;
            if (selectedMisc >= misc.Length)
                selectedMisc = -1;
            if (selectedMisc < -1)
                selectedMisc = misc.Length - 1;

            if (selectedMisc != -1)
            {
                male.EquipAccessory(misc[selectedMisc]);
                female.EquipAccessory(misc[selectedMisc]);
            }
        }
        private void ColorOption_SelectionChanged(object sender, int value)
        {

        }

        public delegate void CharacterCreatedDelegate(object sender);
        public event CharacterCreatedDelegate CharacterCreated;
    }
}
