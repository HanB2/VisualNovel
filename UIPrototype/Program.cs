using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIPrototype
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (MainGame game = new MainGame())
            {
                game.Run();
            }
        }
    }
}
