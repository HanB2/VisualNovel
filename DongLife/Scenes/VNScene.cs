using System;
using System.Collections.Generic;
using Minalear.UI;
using DongLife.Controls;
using DongLife.Code;
using OpenTK.Input;

namespace DongLife.Scenes
{
    public class VNScene : Scene
    {
        private static MessageBox messageBox;

        private SequenceHandler sequences;
        private Dictionary<string, Actor> actors;
        protected Background background;

        public const string NO_ACTOR = "NONE";

        public VNScene(string sceneName) : base(sceneName)
        {
            this.sequences = new SequenceHandler();
            this.actors = new Dictionary<string, Actor>();
            AddChild(messageBox);
        }

        public override void OnEnter()
        {
            foreach (Actor actor in this.actors.Values)
                actor.Reset();

            MessageBox.TextFinished += MessageBox_TextFinished;
            MessageBox.OptionSelected += MessageBox_OptionSelected;

            Sequences.ExecuteSequence(this);

            base.OnEnter();
        }
        public override void OnExit()
        {
            MessageBox.TextFinished -= MessageBox_TextFinished;
            MessageBox.OptionSelected -= MessageBox_OptionSelected;
            Sequences.SetStage(0);

            base.OnExit();
        }

        public void RegisterActor(Actor actor)
        {
            actors.Add(actor.Name, actor);
            AddChild(actor);
        }
        public void SetActorFocus(string actorName, bool soleFocus = true)
        {
            if (soleFocus)
            {
                foreach (Actor actor in actors.Values)
                {
                    if (actor.Name != actorName && actor.HasFocus)
                        actor.SetFocus(false);
                }
            }
            if (actorName != "NONE" && !actors[actorName].HasFocus)
                actors[actorName].SetFocus(true);
        }

        //TODO: Allow clicking the messagebox to skip animations
        public override void OnMouseUp(MouseButtonEventArgs e)
        {
            foreach (Actor actor in actors.Values)
            {
                if (actor.Animator.Animating)
                    actor.Animator.ForceEndAllAnimations();
            }

            base.OnMouseUp(e);
        }

        protected virtual void MessageBox_TextFinished(object sender, EventArgs args)
        {
            Sequences.ExecuteSequence(this);
        }
        protected virtual void MessageBox_OptionSelected(object sender, int optionID)
        {
            Sequences.ExecuteChoice(this, optionID);
        }

        public SequenceHandler Sequences
        {
            get { return this.sequences; }
            protected set { this.sequences = value; }
        }
        public Background Background
        {
            get { return this.background; }
            set { this.background = value; }
        }
        public static MessageBox MessageBox
        {
            get { return messageBox; }
            set { messageBox = value; }
        }
    }
}
