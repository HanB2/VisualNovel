using Minalear;

namespace DongLife.Controls
{
    public interface IGeoDrawable
    {
        void Draw(GeoRenderer renderer);
        void Update(GameTime gameTime);
    }
}
