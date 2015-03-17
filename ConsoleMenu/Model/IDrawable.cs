using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.Model
{
    interface IDrawable
    {
        String Title { get; set; }
        void Draw();
    }
}
