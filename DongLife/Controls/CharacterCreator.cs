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
                {
                    GameManager.TexturePath = @"Textures/Actors/player_male.png";
                    GameManager.Gender = "Male";
                }
                else
                {
                    GameManager.TexturePath = @"Textures/Actors/player_female.png";
                    GameManager.Gender = "Female";
                }

                GameManager.HatIndex = selectedHat;
                GameManager.ShirtIndex = selectedShirt;
                GameManager.MiscIndex = selectedMisc;

                CharacterCreated(this);
            }
        }
        private void loadAccessories(ContentManager content)
        {
            for (int i = 0; i < AccessoryManager.Hats.Length; i++)
                AccessoryManager.Hats[i].LoadContent(content);

            for (int i = 0; i < AccessoryManager.Misc.Length; i++)
                AccessoryManager.Misc[i].LoadContent(content);

            for (int i = 0; i < AccessoryManager.MaleShirts.Length; i++)
                AccessoryManager.MaleShirts[i].LoadContent(content);

            for (int i = 0; i < AccessoryManager.FemaleShirts.Length; i++)
                AccessoryManager.FemaleShirts[i].LoadContent(content);
        }
        private void unloadAccessories()
        {
            for (int i = 0; i < AccessoryManager.Hats.Length; i++)
                AccessoryManager.Hats[i].UnloadContent();

            for (int i = 0; i < AccessoryManager.Misc.Length; i++)
                AccessoryManager.Misc[i].UnloadContent();

            for (int i = 0; i < AccessoryManager.MaleShirts.Length; i++)
                AccessoryManager.MaleShirts[i].UnloadContent();

            for (int i = 0; i < AccessoryManager.FemaleShirts.Length; i++)
                AccessoryManager.FemaleShirts[i].UnloadContent();
        }

        private void GenderOption_SelectionChanged(object sender, int value)
        {
            maleSelected = !maleSelected;
        }
        private void HatOption_SelectionChanged(object sender, int value)
        {
            if (selectedHat != -1)
            {
                male.RemoveAccessory(AccessoryManager.Hats[selectedHat]);
                female.RemoveAccessory(AccessoryManager.Hats[selectedHat]);
            }

            selectedHat += value;
            if (selectedHat >= AccessoryManager.Hats.Length)
                selectedHat = -1;
            if (selectedHat < -1)
                selectedHat = AccessoryManager.Hats.Length - 1;

            if (selectedHat != -1)
            {
                male.EquipAccessory(AccessoryManager.Hats[selectedHat]);
                female.EquipAccessory(AccessoryManager.Hats[selectedHat]);
            }
        }
        private void ShirtOption_SelectionChanged(object sender, int value)
        {
            if (selectedShirt != -1)
            {
                male.RemoveAccessory(AccessoryManager.MaleShirts[selectedShirt]);
                female.RemoveAccessory(AccessoryManager.FemaleShirts[selectedShirt]);
            }

            selectedShirt += value;
            if (selectedShirt >= AccessoryManager.MaleShirts.Length)
                selectedShirt = -1;
            if (selectedShirt < -1)
                selectedShirt = AccessoryManager.MaleShirts.Length - 1;

            if (selectedShirt != -1)
            {
                male.EquipAccessory(AccessoryManager.MaleShirts[selectedShirt]);
                female.EquipAccessory(AccessoryManager.FemaleShirts[selectedShirt]);
            }
        }
        private void AccOption_SelectionChanged(object sender, int value)
        {
            if (selectedMisc != -1)
            {
                male.RemoveAccessory(AccessoryManager.Misc[selectedMisc]);
                female.RemoveAccessory(AccessoryManager.Misc[selectedMisc]);
            }

            selectedMisc += value;
            if (selectedMisc >= AccessoryManager.Misc.Length)
                selectedMisc = -1;
            if (selectedMisc < -1)
                selectedMisc = AccessoryManager.Misc.Length - 1;

            if (selectedMisc != -1)
            {
                male.EquipAccessory(AccessoryManager.Misc[selectedMisc]);
                female.EquipAccessory(AccessoryManager.Misc[selectedMisc]);
            }
        }
        private void ColorOption_SelectionChanged(object sender, int value)
        {

        }

        public delegate void CharacterCreatedDelegate(object sender);
        public event CharacterCreatedDelegate CharacterCreated;
    }
}
