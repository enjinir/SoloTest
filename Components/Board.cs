using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloTest.Components
{

    class Board : TableLayoutPanel
    {
        
        public Board()
        {
            Width = Height = 350;
            ColumnCount = RowCount = 7;
            BackColor = Color.Transparent;
            InitHoles();
        }


       public List<Hole> Holes = new List<Hole>();

        public void InitHoles()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    HoleState s = HoleState.NotAvailable;
                    if ((i < 2 || i > 4) && (j < 2 || j > 4))
                        s = HoleState.NotAvailable;
                    else if (i == 3 && j == 3)    
                        s = HoleState.Empty;
                    else
                        s = HoleState.Occupied;

                    Hole h = new Hole(this, i, j, s);
                    this.Holes.Add(h);
                    this.Controls.Add(h);
                }
            }
        }

        public void Reset()
        {
            foreach (Hole h in Holes)
            {
                HoleState s;
                if ((h.X < 2 || h.X > 4) && (h.Y < 2 || h.Y > 4))
                    s = HoleState.NotAvailable;
                else if (h.X == 3 && h.Y == 3)
                    s = HoleState.Empty;
                else
                    s = HoleState.Occupied;
                h.State = s;
            }
        }

        public void ClearClicks()
        {
            foreach (Hole h in Holes)
            {
                if (h.State == HoleState.Ticked)
                {
                    h.State = HoleState.Occupied;
                }
                if (h.State == HoleState.Available)
                {
                    h.State = HoleState.Empty;
                }
            }
        }

        public bool HasAvailableMoves()
        {
            foreach (Hole h in Holes.FindAll(o => o.State.Equals(HoleState.Occupied) || o.State.Equals(HoleState.Ticked)))
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    Hole vertical = GetHole(h.X + i, h.Y);
                    Hole vertical2 = GetHole(h.X + (2 * i), h.Y);
                    Hole horizontal = GetHole(h.X, h.Y + i);
                    Hole horizontal2 = GetHole(h.X, h.Y + (2 * i));

                    if (vertical != null && vertical2 != null)
                    {
                        if (vertical.State == HoleState.Occupied && (vertical2.State == HoleState.Empty || vertical2.State == HoleState.Available))
                        {
                            return true;
                        }
                    }

                    if (horizontal != null && horizontal2 != null)
                    {
                        if (horizontal.State == HoleState.Occupied && (horizontal2.State == HoleState.Empty || horizontal2.State == HoleState.Available))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public Hole GetHole(int X, int Y)
        {
            Hole hole;
            
            try
            {
                hole = this.Holes.Find(h => h.X == X && h.Y == Y);
            }
            catch (Exception)
            {
                hole = null;
            }

            return hole;
        }
    }      
}
