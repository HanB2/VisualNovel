using System;

namespace Minalear
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
