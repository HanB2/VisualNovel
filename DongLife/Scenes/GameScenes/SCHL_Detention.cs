using System;
using OpenTK;
using DongLife.Code;
using DongLife.Controls;

namespace DongLife.Scenes.GameScenes
{
    public class SCHL_Detention : VNScene
    {
        public SCHL_Detention() : base("SCHL_Detention")
        {
            background = new Background(@"Textures/Backgrounds/detention.png");

            AddChild(background);
        }
    }
}
