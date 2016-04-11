using System.Collections.Generic;
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
