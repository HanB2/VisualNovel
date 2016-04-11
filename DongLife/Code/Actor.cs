using OpenTK;
using OpenTK.Graphics;
using Minalear;
using Minalear.UI.Controls;
using DongLife.Controls;

namespace DongLife.Code
{
    public class Actor : Control
    {
        private string name;
        private Texture2D actorTexture;
        private string texturePath;

        private ControlAnimator animator;

        private Vector2 origin;
        private float normalScale = 1f;
        private float focusScale = 1.25f;
        private float currentScale = 1f;

        private bool hasFocus;

        public Actor(string name, string texturePath)
        {
            this.name = name;
            this.texturePath = texturePath;
            this.DrawOrder = 0.5f;

            this.origin = new Vector2(0.5f, 0.5f);
            this.normalScale = 1f;
            this.focusScale = 1f;

            this.hasFocus = true;

            animator = new ControlAnimator();
            AddChild(animator);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
            {
                spriteBatch.Draw(actorTexture, Position, this.DrawColor, 0f, origin, currentScale, RenderFlags.None);
            }
            else
            {
                spriteBatch.Draw(actorTexture, Position, this.DrawColor, 0f, origin, currentScale, RenderFlags.Blur | RenderFlags.Desaturate);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void SetFocus(bool focus)
        {
            hasFocus = focus;
            if (hasFocus)
                animator.AnimateActorZoom(focusScale, 100f);
            else
                animator.AnimateActorZoom(normalScale, 100f);
        }

        public override void LoadContent(ContentManager content)
        {
            actorTexture = content.LoadTexture2D(texturePath);
            Size = new Vector2(actorTexture.Width, actorTexture.Height);

            base.LoadContent(content);
        }
        public override void UnloadContent()
        {
            actorTexture.Delete();

            base.UnloadContent();
        }

        public void Reset()
        {
            //Reset Alpha
            Color4 color = this.DrawColor;
            color.A = 1f;
            this.DrawColor = color;

            //Reset Zoom
            HasFocus = false;
            CurrentScale = NormalScale;
        }

        #region Properties
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public ControlAnimator Animator
        {
            get { return this.animator; }
            set { this.animator = value; }
        }
        public string TexturePath
        {
            get { return this.texturePath; }
            set { this.texturePath = value; }
        }
        public float NormalScale
        {
            get { return this.normalScale; }
            set { this.normalScale = value; }
        }
        public float FocusScale
        {
            get { return this.focusScale; }
            set { this.focusScale = value; }
        }
        public float CurrentScale
        {
            get { return this.currentScale; }
            set { this.currentScale = value; }
        }
        public bool HasFocus
        {
            get { return this.hasFocus; }
            set { SetFocus(value); }
        }
        #endregion
    }
}
