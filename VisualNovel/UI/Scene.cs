using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK.Input;
using Minalear.UI.Controls;

namespace Minalear.UI
{
    public class Scene : Control
    {
        private string name;
        private SceneManager manager;

        public Scene(string name)
        {
            this.name = name;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        #region Properties
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public SceneManager Manager
        {
            get { return this.manager; }
            set { this.manager = value; }
        }
        #endregion
    }
}
