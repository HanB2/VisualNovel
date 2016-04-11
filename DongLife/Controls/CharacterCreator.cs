using System.Collections.Generic;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;

namespace DongLife.Controls
{
    public class CharacterCreator : Control
    {
        private GeoRenderer renderer;
        private List<IGeoDrawable> geometryControls;

        public CharacterCreator()
        {
            this.geometryControls = new List<IGeoDrawable>();

            //UI Setup
            GeoPanel panel = new GeoPanel();
            panel.Position = new Vector2(10f, 10f);
            panel.Size = new Vector2(1260f, 700f);

            TextInput textInput = new TextInput(640, 48);
            textInput.Position = new Vector2(600f, 50f);

            CreatorOptionSelector genderOption = new CreatorOptionSelector("Body Type",  new OpenTK.Vector2(600, 120), 640);
            CreatorOptionSelector hatOption =    new CreatorOptionSelector("Hat",        new OpenTK.Vector2(600, 192), 640);
            CreatorOptionSelector shirtOption =  new CreatorOptionSelector("Shirt",      new OpenTK.Vector2(600, 264), 640);
            CreatorOptionSelector accOption =    new CreatorOptionSelector("Accessory",  new OpenTK.Vector2(600, 336), 640);
            CreatorOptionSelector colorOption =  new CreatorOptionSelector("Skin Color", new OpenTK.Vector2(600, 408), 640);

            AddChild(textInput);
            //AddChild for gender options since they have a regular control
            AddChild(genderOption);
            AddChild(hatOption);
            AddChild(shirtOption);
            AddChild(accOption);
            AddChild(colorOption);

            registerGeoControl(panel);
            registerGeoControl(genderOption);
            registerGeoControl(hatOption);
            registerGeoControl(shirtOption);
            registerGeoControl(accOption);
            registerGeoControl(colorOption);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < this.geometryControls.Count; i++)
            {
                this.geometryControls[i].Draw(this.renderer);
            }
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.geometryControls.Count; i++)
            {
                this.geometryControls[i].Draw(this.renderer);
            }

            base.Update(gameTime);
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

        private void registerGeoControl(IGeoDrawable control)
        {
            this.geometryControls.Add(control);
            this.geometryControls.Sort();
        }
    }
}
