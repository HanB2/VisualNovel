using System.Collections.Generic;
using OpenTK;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Controls;

namespace DongLife.Code
{
    public class Player : Actor
    {
        private List<Accessory> accessories;

        public Player() : base("Player", GameManager.TexturePath)
        {
            accessories = new List<Accessory>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < accessories.Count; i++)
                accessories[i].Draw(spriteBatch, this);
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < accessories.Count; i++)
                accessories[i].LoadContent(content);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            for (int i = 0; i < accessories.Count; i++)
                accessories[i].UnloadContent();

            base.UnloadContent();
        }

        public void EquipAccessory(Accessory accessory)
        {
            accessories.Add(accessory);
        }
        public void RemoveAccessory(Accessory accessory)
        {
            accessories.Remove(accessory);
        }
    }
}
