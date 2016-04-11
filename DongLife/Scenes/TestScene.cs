using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI;
using Minalear.UI.Controls;
using DongLife.Code;
using OpenTK.Input;

namespace DongLife.Scenes
{
    public class TestScene : Scene
    {
        private GeoRenderer renderer;
        private Background background;
        
        private GeoPanel panel;
        private GeoInput input;
        private CreatorOptions genderOption, shirtOption, accOption, hatOption, colorOption;

        private Texture2D maleTexture, femaleTexture;
        private bool isMale = true;

        private Accessory[] maleShirts, femaleShirts;
        private Accessory[] accessories;
        private Accessory[] hats;
        private Color4[] skinColors;

        int shirtID = 0;
        int accID = 0;
        int hatID = 0;
        int colorID = 0;

        public TestScene() : base("TestScene")
        {
            background = new Background(@"Textures/Backgrounds/hospital_room.png");
            background.DrawOrder = 1f;

            maleShirts = new Accessory[5];
            femaleShirts = new Accessory[5];
            accessories = new Accessory[7];
            hats = new Accessory[7];
            skinColors = new Color4[15];

            initContent();
        }

        public override void LoadContent(ContentManager content)
        {
            renderer = new GeoRenderer(
                content.LoadShader(@"Shaders/geovert.glsl", @"Shaders/geofrag.glsl"),
                Manager.Game.Window.Width, Manager.Game.Window.Height);

            maleTexture = content.LoadTexture2D(@"Textures/Actors/player_male.png");
            femaleTexture = content.LoadTexture2D(@"Textures/Actors/player_female.png");

            //UI Setup
            panel = new GeoPanel(renderer);
            panel.Position = new Vector2(10, 10);
            panel.Size = new Vector2(1260, 700);

            input = new GeoInput(renderer, 640, 48);
            input.Position = new Vector2(600, 50);

            genderOption = new CreatorOptions(renderer, "Body Type", new Vector2(600, 120), 640);
            hatOption = new CreatorOptions(renderer, "Hat", new Vector2(600, 192), 640);
            shirtOption = new CreatorOptions(renderer, "Shirt", new Vector2(600, 264), 640);
            accOption = new CreatorOptions(renderer, "Accessory", new Vector2(600, 336), 640);
            colorOption = new CreatorOptions(renderer, "Skin Color", new Vector2(600, 408), 640);

            genderOption.OptionSelected += GenderOption_OptionSelected;
            hatOption.OptionSelected += HatOption_OptionSelected;
            shirtOption.OptionSelected += ShirtOption_OptionSelected;
            accOption.OptionSelected += AccOption_OptionSelected;
            colorOption.OptionSelected += ColorOption_OptionSelected;

            AddChild(background);
            AddChild(panel);
            AddChild(input);

            AddChild(genderOption);
            AddChild(hatOption);
            AddChild(shirtOption);
            AddChild(accOption);
            AddChild(colorOption);

            loadAccessories(content);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            maleTexture.Dispose();
            femaleTexture.Dispose();

            unloadAccessories();

            base.UnloadContent();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (isMale)
            {
                spriteBatch.Draw(maleTexture, new RectangleF(75, 100, 400, 548), skinColors[colorID]);
                maleShirts[shirtID].Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(femaleTexture, new RectangleF(75, 100, 400, 548), skinColors[colorID]);
                femaleShirts[shirtID].Draw(spriteBatch);
            }

            accessories[accID].Draw(spriteBatch);
            hats[hatID].Draw(spriteBatch);
        }

        private void GenderOption_OptionSelected(object sender, int value)
        {
            isMale = !isMale;
        }
        private void HatOption_OptionSelected(object sender, int value)
        {
            hatID += value;
            if (hatID < 0)
                hatID = hats.Length - 1;
            if (hatID >= hats.Length)
                hatID = 0;
        }
        private void ShirtOption_OptionSelected(object sender, int value)
        {
            shirtID += value;
            if (shirtID < 0)
                shirtID = maleShirts.Length - 1;
            if (shirtID >= maleShirts.Length)
                shirtID = 0;
        }
        private void AccOption_OptionSelected(object sender, int value)
        {
            accID += value;
            if (accID < 0)
                accID = accessories.Length - 1;
            if (accID >= accessories.Length)
                accID = 0;
        }
        private void initContent()
        {
            hats[0] = new Accessory("Textures/Accessories/Hats/hat_01.png", new Vector2(118, -15), new Vector2(75, 100));
            hats[1] = new Accessory("Textures/Accessories/Hats/hat_02.png", new Vector2(118, -45), new Vector2(75, 100));
            hats[2] = new Accessory("Textures/Accessories/Hats/hat_03.png", new Vector2(120, -30), new Vector2(75, 100));
            hats[3] = new Accessory("Textures/Accessories/Hats/hat_04.png", new Vector2(116, -45), new Vector2(75, 100));
            hats[4] = new Accessory("Textures/Accessories/Hats/hat_05.png", new Vector2(118, -45), new Vector2(75, 100));
            hats[5] = new Accessory("Textures/Accessories/Hats/hat_06.png", new Vector2(118, -45), new Vector2(75, 100));
            hats[6] = new Accessory("Textures/Accessories/Hats/hat_07.png", new Vector2(118, -45), new Vector2(75, 100));

            accessories[0] = new Accessory("Textures/Accessories/Misc/acc_01.png", new Vector2(170, 85), new Vector2(75, 100));
            accessories[1] = new Accessory("Textures/Accessories/Misc/acc_02.png", new Vector2(170, 85), new Vector2(75, 100));
            accessories[2] = new Accessory("Textures/Accessories/Misc/acc_03.png", new Vector2(-50, 40), new Vector2(75, 100));
            accessories[3] = new Accessory("Textures/Accessories/Misc/acc_04.png", new Vector2(-20, 120), new Vector2(75, 100));
            accessories[4] = new Accessory("Textures/Accessories/Misc/acc_05.png", new Vector2(220, 47), new Vector2(75, 100));
            accessories[5] = new Accessory("Textures/Accessories/Misc/acc_06.png", new Vector2(174, 35), new Vector2(75, 100));
            accessories[6] = new Accessory("Textures/Accessories/Misc/acc_07.png", new Vector2(174, 35), new Vector2(75, 100));

            maleShirts[0] = new Accessory("Textures/Accessories/MaleShirts/mshirt_01.png", new Vector2(115, 83), new Vector2(75, 100));
            maleShirts[1] = new Accessory("Textures/Accessories/MaleShirts/mshirt_02.png", new Vector2(115, 83), new Vector2(75, 100));
            maleShirts[2] = new Accessory("Textures/Accessories/MaleShirts/mshirt_03.png", new Vector2(115, 83), new Vector2(75, 100));
            maleShirts[3] = new Accessory("Textures/Accessories/MaleShirts/mshirt_04.png", new Vector2(115, 83), new Vector2(75, 100));
            maleShirts[4] = new Accessory("Textures/Accessories/MaleShirts/mshirt_05.png", new Vector2(115, 83), new Vector2(75, 100));

            femaleShirts[0] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_01.png", new Vector2(140, 90), new Vector2(75, 100));
            femaleShirts[1] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_02.png", new Vector2(140, 90), new Vector2(75, 100));
            femaleShirts[2] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_03.png", new Vector2(140, 90), new Vector2(75, 100));
            femaleShirts[3] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_04.png", new Vector2(140, 90), new Vector2(75, 100));
            femaleShirts[4] = new Accessory("Textures/Accessories/FemaleShirts/fshirt_05.png", new Vector2(140, 90), new Vector2(75, 100));

            skinColors[0] = Color4.White;
            skinColors[1] = Color4.Black;
            skinColors[2] = Color4.Brown;
            skinColors[3] = Color4.PeachPuff;
            skinColors[4] = Color4.IndianRed;
            skinColors[5] = Color4.Goldenrod;
            skinColors[6] = Color4.Purple;
            skinColors[7] = Color4.Blue;
            skinColors[8] = Color4.Red;
            skinColors[9] = Color4.Green;
            skinColors[10] = Color4.Yellow;
            skinColors[11] = Color4.Orange;
            skinColors[12] = new Color4(1f, 1f, 1f, 0.1f);
            skinColors[13] = Color4.Turquoise;
            skinColors[14] = Color4.Teal;
        }
        private void ColorOption_OptionSelected(object sender, int value)
        {
            colorID += value;
            if (colorID < 0)
                colorID = skinColors.Length - 1;
            if (colorID >= skinColors.Length)
                colorID = 0;
        }
        private void loadAccessories(ContentManager content)
        {
            for (int i = 0; i < hats.Length; i++)
                hats[i].LoadContent(content);
            for (int i = 0; i < maleShirts.Length; i++)
                maleShirts[i].LoadContent(content);
            for (int i = 0; i < femaleShirts.Length; i++)
                femaleShirts[i].LoadContent(content);
            for (int i = 0; i < accessories.Length; i++)
                accessories[i].LoadContent(content);
        }
        private void unloadAccessories()
        {
            for (int i = 0; i < hats.Length; i++)
                hats[i].UnloadContent();
            for (int i = 0; i < maleShirts.Length; i++)
                maleShirts[i].UnloadContent();
            for (int i = 0; i < femaleShirts.Length; i++)
                femaleShirts[i].UnloadContent();
            for (int i = 0; i < accessories.Length; i++)
                accessories[i].UnloadContent();
        }
    }

    public class GeoButton : Control
    {
        private GeoRenderer renderer;
        private ButtonModes currentMode;

        public GeoButton(GeoRenderer renderer)
        {
            this.renderer = renderer;
            this.DrawOrder = 0.1f;
            this.currentMode = ButtonModes.Normal;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            if (currentMode == ButtonModes.Normal)
                renderer.FillRect(Position, Size, new Color4(0.3f, 0.3f, 0.3f, 1f));
            else if (currentMode == ButtonModes.Hover)
                renderer.FillRect(Position, Size, new Color4(0.5f, 0.5f, 0.5f, 1f));
            else
                renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 1f));

            renderer.DrawRect(Position, Size, Color4.White);
            //renderer.FillCircle(Position + Size / 2, 10f, 4, Color4.White);
            renderer.End();
        }

        public override void OnMouseEnter()
        {
            currentMode = ButtonModes.Hover;
        }
        public override void OnMouseLeave()
        {
            currentMode = ButtonModes.Normal;
        }
        public override void OnMouseDown(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Pressed;
        }
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            currentMode = ButtonModes.Hover;
            if (ButtonPressed != null)
                ButtonPressed(this, e);
        }

        public delegate void ButtonPressedDelegate(object sender, MouseButtonEventArgs e);
        public event ButtonPressedDelegate ButtonPressed;
        private enum ButtonModes { Normal, Hover, Pressed }
    }
    public class GeoPanel : Control
    {
        private GeoRenderer renderer;

        public GeoPanel(GeoRenderer renderer)
        {
            this.renderer = renderer;
            this.DrawOrder = 0.9f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Begin();
            renderer.FillRect(Position, Size, new Color4(0.2f, 0.2f, 0.2f, 0.8f));
            renderer.DrawRect(new Vector2(PosX + 1, PosY + 1), new Vector2(Width - 2, Height - 2), Color4.White);
            renderer.End();
        }
    }
    public class GeoInput : Control
    {
        private GeoRenderer renderer;

        private Bitmap bitmap;
        private Graphics graphics;
        private Texture2D renderTexture;
        private Font font;

        private string buffer = string.Empty;
        private const int MAX_BUFFER = 32;

        public GeoInput(GeoRenderer renderer, int width, int height)
        {
            this.renderer = renderer;
            Width = width;
            Height = height;

            DrawOrder = 0.1f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, Color4.White);

            base.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void LoadContent(ContentManager content)
        {
            bitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(bitmap);
            font = new Font("Comic Sans MS", 24f, FontStyle.Regular);

            initTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            bitmap.Dispose();
            graphics.Dispose();
            renderTexture.Dispose();
            font.Dispose();

            buffer = String.Empty;

            base.UnloadContent();
        }

        public override void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (buffer.Length < MAX_BUFFER)
            {
                buffer += e.KeyChar;
                updateTexture();
            }

            base.OnKeyPress(sender, e);
        }
        public override void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.BackSpace && buffer.Length > 0)
            {
                buffer = buffer.Substring(0, buffer.Length - 1);
                updateTexture();
            }

            base.OnKeyUp(sender, e);
        }

        private void initTexture()
        {
            graphics.Clear(Color.FromArgb(new Color4(0f, 0f, 0f, 0.5f).ToArgb()));
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

            bitmap.UnlockBits(data);
        }
        private void updateTexture()
        {
            graphics.Clear(Color.FromArgb(new Color4(0f, 0f, 0f, 0.5f).ToArgb()));
            float width = graphics.MeasureString(buffer, font).Width;
            graphics.DrawString(buffer, font, Brushes.White, Bounds.Width / 2 - width / 2, 0f);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture.UpdateTexture(data.Scan0);

            bitmap.UnlockBits(data);
        }
    }
    public class GeoTextBox : Control
    {
        private string text;
        private float textWidth;

        private Bitmap bitmap;
        private Graphics graphics;
        private Texture2D renderTexture;
        private Font font;

        public GeoTextBox(float width, float height, string text)
        {
            this.text = text;
            Size = new Vector2(width, height);
            DrawOrder = 0.1f;
        }

        public override void LoadContent(ContentManager content)
        {
            bitmap = new Bitmap((int)Width, (int)Height);
            graphics = Graphics.FromImage(bitmap);
            font = new Font("Comic Sans MS", 24f, FontStyle.Regular);

            textWidth = graphics.MeasureString(text, font).Width;

            initTexture();

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            bitmap.Dispose();
            graphics.Dispose();
            font.Dispose();
            renderTexture.Dispose();

            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Bounds, Color4.White);

            base.Draw(spriteBatch);
        }

        private void initTexture()
        {
            graphics.Clear(Color.FromArgb(new Color4(0f, 0f, 0f, 0.5f).ToArgb()));
            graphics.DrawString(text, font, Brushes.White, Width / 2 - textWidth / 2, 0f);

            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            renderTexture = new Texture2D(bitmap.Width, bitmap.Height, data.Scan0);

            bitmap.UnlockBits(data);
        }
    }

    public class CreatorOptions : Control
    {
        private GeoTextBox textBox;
        private GeoButton left, right;

        public CreatorOptions(GeoRenderer renderer, string text, Vector2 position, float width)
        {
            Position = position;
            Size = new Vector2(width, 48);

            left = new GeoButton(renderer);
            left.Size = new Vector2(32, 32);
            left.Position = new Vector2(position.X + 8, position.Y + 8);

            right = new GeoButton(renderer);
            right.Size = new Vector2(32, 32);
            right.Position = new Vector2(position.X + width - 40, position.Y + 8);

            textBox = new GeoTextBox(width, 48, text);
            textBox.Position = position;

            AddChild(left);
            AddChild(right);
            AddChild(textBox);

            DrawOrder = 0f;

            left.ButtonPressed += LeftPressed;
            right.ButtonPressed += RightPressed;
        }

        private void LeftPressed(object sender, MouseButtonEventArgs e)
        {
            if (OptionSelected != null)
                OptionSelected(this, -1);
        }
        private void RightPressed(object sender, MouseButtonEventArgs e)
        {
            if (OptionSelected != null)
                OptionSelected(this, 1);
        }

        public delegate void OptionSelectedDelegate(object sender, int value);
        public event OptionSelectedDelegate OptionSelected;
    }
    public class Accessory : IDrawable
    {
        public string Path;
        public Vector2 Position;
        public Texture2D Texture;

        public Accessory(string path, Vector2 position, Vector2 relativePos)
        {
            this.Path = path;
            this.Position = position + relativePos;
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.LoadTexture2D(Path);
        }
        public void UnloadContent()
        {
            Texture.Dispose();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color4.White, 0f, Vector2.Zero, 0.552977f);
        }
        public void Update(GameTime gameTime) { }
    }
}
