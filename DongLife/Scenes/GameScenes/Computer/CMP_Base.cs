using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;
using DongLife.Controls.Computer;

namespace DongLife.Scenes.GameScenes
{
    public class CMP_Base : VNScene
    {
        private Desktop desktop;

        public CMP_Base() : base("CMP_Base")
        {
            desktop = new Desktop();

            AddChild(desktop);

            Sequences.RegisterSequence(0, NO_ACTOR, "Hello!  Welcome to the shittiest OS on the interwebs.  Here viruses are normal and functioning programs are not!  Also we do not use Lorem Ipsum, but rather random ass ramblings of a mad man.");
            Sequences.RegisterSequence(1, new SequenceStageTransition(0));
        }
    }
}
