using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK.Input;
using Minalear;
using UIPrototype.Controls;

namespace UIPrototype
{
    public class UIScene : Control
    {
        private string name;
        private UISceneManager manager;

        public UIScene(string name)
        {
            this.name = name;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        #region Properties
        public string Name
        {
            get { return this.name; }
        }
        public UISceneManager Manager
        {
            get { return this.manager; }
            set { this.manager = value; }
        }
        #endregion
    }
}
