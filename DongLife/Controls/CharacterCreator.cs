using System.Collections.Generic;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using OpenTK.Input;

namespace DongLife.Controls
{
    public class CharacterCreator : Control
    {
        private GeoButton submitButton;
        private TextInput textInput;

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

            AddChild(panel);
            AddChild(textInput);
            AddChild(genderOption);
            AddChild(hatOption);
            AddChild(shirtOption);
            AddChild(accOption);
            AddChild(colorOption);
            AddChild(submitButton);
        }

        private void SubmitButton_ButtonPressed(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (CharacterCreated != null)
            {
                GameSettings.PlayerName = textInput.Text.Trim();
                CharacterCreated(this);
            }
        }

        public delegate void CharacterCreatedDelegate(object sender);
        public event CharacterCreatedDelegate CharacterCreated;
    }
}
