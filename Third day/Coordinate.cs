using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third_day
{
    public struct Coordinate
    {
        public int X, Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void Move(char instrustion)
        {
            switch (instrustion)
            {
                case '^':
                    Y++;
                    break;
                case 'v':
                    Y--;
                    break;
                case '>':
                    X++;
                    break;
                case '<':
                    X--;
                    break;
                default:
                    break;
            }
        }
    }
}
