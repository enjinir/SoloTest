using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloTest.Components
{

    class Hole : PictureBox
    {
        public Board Board;
        private HoleState _state;
        public HoleState State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
                this.setImage();
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Hole(int x, int y)
        {
            this.Click += onClick;
            this.X = x;
            this.Y = y;
            Width = Height = 50;
            Padding = new Padding(0);
            Margin = new Padding(0);
            BackColor = Color.Transparent;
        }
        public Hole(int x, int y, HoleState s)
            : this(x, y)
        {
            State = s;
        }
        public Hole(Board b, int x, int y, HoleState s)
            : this(x, y, s)
        {
            this.Board = b;
        }
        private void onClick(object sender, EventArgs e)
        {
            if (_state == HoleState.Occupied)
            {
                Board.ClearClicks();
                State = HoleState.Ticked;

                for (int i = -1; i <= 1; i += 2)
                {
                    Hole vertical = Board.GetHole(X + i, Y);
                    Hole vertical2 = Board.GetHole(X + (2 * i), Y);
                    Hole horizontal = Board.GetHole(X, Y + i);
                    Hole horizontal2 = Board.GetHole(X, Y + (2 * i));

                    if (vertical != null && vertical2 != null)
                    {
                        if (vertical.State == HoleState.Occupied && vertical2.State == HoleState.Empty)
                        {
                            vertical2.State = HoleState.Available;
                        }
                    }

                    if (horizontal != null && horizontal2 != null)
                    {
                        if (horizontal.State == HoleState.Occupied && horizontal2.State == HoleState.Empty)
                        {
                            horizontal2.State = HoleState.Available;
                        }
                    }
                }
            }
            else if (_state == HoleState.Ticked) 
            {
                State = HoleState.Occupied;
                Board.ClearClicks();
            }
            else if (_state == HoleState.Available)
            {
                State = HoleState.Occupied;

                Hole prev = Board.Holes.Find(h => h.State.Equals(HoleState.Ticked));
                if (prev != null)
                    prev.State = HoleState.Empty;

                int x = (Y - prev.Y) != 0 ? X : (X + prev.X) / 2;
                int y = (X - prev.X) != 0 ? Y : (Y + prev.Y) / 2;

                Hole mid = Board.GetHole(x, y);
                if (mid != null)
                    mid.State = HoleState.Empty;

                Board.ClearClicks();

            }
            else
                Board.ClearClicks();

            if (!Board.HasAvailableMoves())
            {
                int count = Board.Holes.Count(h => h.State.Equals(HoleState.Occupied));
                DialogResult result = MessageBox.Show(string.Format("Congratulations. You've completed the SoloTest game with a score of {0}. Wanna try again?", count), "Game Over!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Board.Reset();
                }
                else
                {
                    Application.Exit();
                }
            }
        }


        private void setImage()
        {
            string folder = "Images";
            string fileName = this.State.ToString() + ".png";
            this.Image = System.Drawing.Image.FromFile(folder + "\\" + fileName);
        }


    }
    enum HoleState
    {
        Empty,
        Occupied,
        NotAvailable,
        Ticked,
        Available
    }
}
