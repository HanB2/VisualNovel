using System.Collections.Generic;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class CharacterCreator : Control
    {
        private GeoRenderer renderer;
        private GeoButton submitButton;

        public CharacterCreator()
        {
            DrawOrder = 0f;

            //UI Setup
            GeoPanel panel = new GeoPanel();
            panel.Position = new Vector2(10f, 10f);
            panel.Size = new Vector2(1260f, 700f);

            submitButton = new GeoButton();
            submitButton.Position = new Vector2(600f, 480f);
            submitButton.Size = new Vector2(640f, 32f);
            submitButton.ButtonPressed += SubmitButton_ButtonPressed;

            TextInput textInput = new TextInput(640, 48);
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

        public override void LoadContent(ContentManager content)
        {
            this.renderer = new GeoRenderer(
                content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                GameSettings.WindowWidth, GameSettings.WindowHeight);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            this.renderer.Dispose();

            base.UnloadContent();
        }

        private void SubmitButton_ButtonPressed(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (CharacterCreated != null)
                CharacterCreated(this);
        }

        public delegate void CharacterCreatedDelegate(object sender);
        public event CharacterCreatedDelegate CharacterCreated;
    }
}
