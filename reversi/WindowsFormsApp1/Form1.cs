using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int num;
        Button[,] board;
        int[,] numboard;
        int[,] preNumboard;
        int[,] prepreNumboard;
        int[,] preprepreNumboard;
        int turn = 1;
        public Form1()
        {
            InitializeComponent();
        }
        int value(int[,] ezer)   
        {
            int i = 0;
            for (int shura = 1; shura < num - 1; shura++)
                for (int amuda = 1; amuda < num - 1; amuda++)
                {
                    if (ezer[shura, amuda] == 1)
                        i--;
                    else if (ezer[shura, amuda] == 2)
                        i++;
                }
            for (int shura = 1; shura < num - 1; shura++)
            {
                if (ezer[shura, 0] == 1)
                    i = i - 20;
                else if (ezer[shura, 0] == 2)
                    i = i + 20;
                if (ezer[shura, num - 1] == 1)
                    i = i - 20;
                else if (ezer[shura, num - 1] == 2)
                    i = i + 20;
            }
            for (int amuda = 1; amuda < num - 1; amuda++)
            {
                if (ezer[0, amuda] == 1)
                    i = i - 20;
                else if (ezer[0, amuda] == 2)
                    i = i + 20;
                if (ezer[num - 1, amuda] == 1)
                    i = i - 20;
                else if (ezer[num - 1, amuda] == 2)
                    i = i + 20;
            }
            if (ezer[0, 0] == 1)
                i = i - 70;
            else if (ezer[0, 0] == 2)
                i = i + 70;
            if (ezer[0, num - 1] == 1)
                i = i - 70;
            else if (ezer[0, num - 1] == 2)
                i = i + 70;
            if (ezer[num - 1, 0] == 1)
                i = i - 70;
            else if (ezer[num - 1, 0] == 2)
                i = i + 70;
            if (ezer[num - 1, num - 1] == 1)
                i = i - 70;
            else if (ezer[num - 1, num - 1] == 2)
                i = i + 70;

            return i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = textBox1.Text;
                if (int.Parse(s) <= 3)
                    num = 4;
                else
                    num = int.Parse(s);
            }
            catch
            {
                num = 8;
            }
            board = new Button[num, num];
            numboard = new int[num, num];
            int btnsize = 50;
            int r = 0, c, i = 0;
            while (r < num)
            {
                c = 0;
                while (c < num)
                {
                    #region בניית לוח
                    board[r, c] = new Button();
                    board[r, c].Size = new Size(btnsize, btnsize);
                    board[r, c].Location = new Point(c * btnsize, r * btnsize);
                    board[r, c].BackColor = Color.FromArgb(230, 230, 230);
                    board[r, c].Tag = i.ToString();
                    board[r, c].Click += Form1_Click;
                    panel1.Controls.Add(board[r, c]);
                    #endregion

                    i++;
                    c++;
                }
                r++;
            }
            panel1.Width = btnsize * board.GetLength(1) + 5;
            panel1.Height = btnsize * board.GetLength(1) + 5;
            this.Height = btnsize * board.GetLength(1) + 170;
            this.Width = btnsize * board.GetLength(1) + 40;

            textBox2.Text = "Black turn";
            try
            {
                board[num / 2, num / 2].BackColor = Color.Black;
                numboard[num / 2, num / 2] = 1;
                board[num / 2 - 1, num / 2 - 1].BackColor = Color.Black;
                numboard[num / 2 - 1, num / 2 - 1] = 1;
                board[num / 2, num / 2 - 1].BackColor = Color.Blue;
                numboard[num / 2, num / 2 - 1] = 2;
                board[num / 2 - 1, num / 2].BackColor = Color.Blue;
                numboard[num / 2 - 1, num / 2] = 2;
                numboard[num / 2 - 1, num / 2 + 1] = 3;
                numboard[num / 2 + 1, num / 2 - 1] = 3;
                numboard[num / 2, num / 2 - 2] = 3;
                numboard[num / 2 - 2, num / 2] = 3;
                board[num / 2 - 1, num / 2 + 1].BackColor = Color.LightGreen;
                board[num / 2 + 1, num / 2 - 1].BackColor = Color.LightGreen;
                board[num / 2, num / 2 - 2].BackColor = Color.LightGreen;
                board[num / 2 - 2, num / 2].BackColor = Color.LightGreen;
            }
            catch { }

            preNumboard = new int[num,num];
            prepreNumboard = new int[num, num];
            preprepreNumboard = new int[num, num];
            for (int shura = 0; shura < num; shura++) 
                for (int amuda = 0; amuda < num; amuda++)
                {
                    preNumboard[shura, amuda] = numboard[shura, amuda];
                    prepreNumboard[shura, amuda] = numboard[shura, amuda];
                    preprepreNumboard[shura, amuda] = numboard[shura, amuda];
                }
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            string n = (((Button)(sender)).Tag).ToString();
            int s = int.Parse(n);

            if (numboard[s / num, s % num] == 3)
            {
                for (int shura = 0; shura < num; shura++)
                    for (int amuda = 0; amuda < num; amuda++)
                    {
                        preprepreNumboard[shura, amuda] = prepreNumboard[shura, amuda];
                        prepreNumboard[shura, amuda] = preNumboard[shura, amuda];
                        preNumboard[shura, amuda] = numboard[shura, amuda];
                    }

                #region player's turn
                if (turn == 1)
                {
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num + i] == 2; i++)
                            if (numboard[s / num + i + 1, s % num + i + 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num + i] = 1;
                                    board[s / num + i, s % num + i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num - i] == 2; i++)
                            if (numboard[s / num - i - 1, s % num - i - 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num - i] = 1;
                                    board[s / num - i, s % num - i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num + i] == 2; i++)
                            if (numboard[s / num - i - 1, s % num + i + 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num + i] = 1;
                                    board[s / num - i, s % num + i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num - i] == 2; i++)
                            if (numboard[s / num + i + 1, s % num - i - 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num - i] = 1;
                                    board[s / num + i, s % num - i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num] == 2; i++)
                            if (numboard[s / num + i + 1, s % num] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num] = 1;
                                    board[s / num + i, s % num].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num] == 2; i++)
                            if (numboard[s / num - i - 1, s % num] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num] = 1;
                                    board[s / num - i, s % num].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num, s % num + i] == 2; i++)
                            if (numboard[s / num, s % num + i + 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num, s % num + i] = 1;
                                    board[s / num, s % num + i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num, s % num - i] == 2; i++)
                            if (numboard[s / num, s % num - i - 1] == 1)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num, s % num - i] = 1;
                                    board[s / num, s % num - i].BackColor = Color.Black;
                                }
                    }
                    catch { }
                }
                #endregion 
                #region player's turn
                if (turn == 2)
                {
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num + i] == 1; i++)
                            if (numboard[s / num + i + 1, s % num + i + 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num + i] = 2;
                                    board[s / num + i, s % num + i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num - i] == 1; i++)
                            if (numboard[s / num - i - 1, s % num - i - 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num - i] = 2;
                                    board[s / num - i, s % num - i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num + i] == 1; i++)
                            if (numboard[s / num - i - 1, s % num + i + 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num + i] = 2;
                                    board[s / num - i, s % num + i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num - i] == 1; i++)
                            if (numboard[s / num + i + 1, s % num - i - 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num - i] = 2;
                                    board[s / num + i, s % num - i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num + i, s % num] == 1; i++)
                            if (numboard[s / num + i + 1, s % num] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num + i, s % num] = 2;
                                    board[s / num + i, s % num].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num - i, s % num] == 1; i++)
                            if (numboard[s / num - i - 1, s % num] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num - i, s % num] = 2;
                                    board[s / num - i, s % num].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num, s % num + i] == 1; i++)
                            if (numboard[s / num, s % num + i + 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num, s % num + i] = 2;
                                    board[s / num, s % num + i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                    try
                    {
                        for (int i = 1; numboard[s / num, s % num - i] == 1; i++)
                            if (numboard[s / num, s % num - i - 1] == 2)
                                for (; i >= 0; i--)
                                {
                                    numboard[s / num, s % num - i] = 2;
                                    board[s / num, s % num - i].BackColor = Color.Blue;
                                }
                    }
                    catch { }
                }
                #endregion


                for (int i = 0; i < num * num; i++)
                {
                    if (numboard[i / num, i % num] == 3)
                    {
                        numboard[i / num, i % num] = 0;
                        board[i / num, i % num].BackColor = Color.FromArgb(230, 230, 230);
                    }
                    board[i / num, i % num].Text = " ";
                }

                int bluewinstate = 0, blackwinstate = 0, all = 0;
                if (turn == 1)
                {
                    turn = 2;
                    #region blue checker
                    for (int r = 0; r < num; r++)
                        for (int c = 0; c < num; c++)
                        {
                            if (numboard[r, c] == 1)
                            {
                                if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 1; i++)
                                    {
                                        if (numboard[r - i - 1, c - i - 1] == 2)
                                        {
                                            numboard[r + 1, c + 1] = 3;
                                            board[r + 1, c + 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 &&  c + i + 1 <= num - 1 && numboard[r + i, c + i] == 1; i++)
                                    {
                                        if (numboard[r + i + 1, c + i + 1] == 2)
                                        {
                                            numboard[r - 1, c - 1] = 3;
                                            board[r - 1, c - 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 1; i++)
                                    {
                                        if (numboard[r + i + 1, c - i - 1] == 2)
                                        {
                                            numboard[r - 1, c + 1] = 3;
                                            board[r - 1, c + 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 1; i++)
                                    {
                                        if (numboard[r - i - 1, c + i + 1] == 2)
                                        {
                                            numboard[r + 1, c - 1] = 3;
                                            board[r + 1, c - 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (r != 0 && numboard[r - 1, c] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 1; i++)
                                    {
                                        if (numboard[r + i + 1, c] == 2)
                                        {
                                            numboard[r - 1, c] = 3;
                                            board[r - 1, c].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (r != num - 1 && numboard[r + 1, c] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 1; i++)
                                    {
                                        if (numboard[r - i - 1, c] == 2)
                                        {
                                            numboard[r + 1, c] = 3;
                                            board[r + 1, c].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (c != num - 1 && numboard[r, c + 1] == 0)
                                {
                                    for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 1; i++)
                                    {
                                        if (numboard[r, c - i - 1] == 2)
                                        {
                                            numboard[r, c + 1] = 3;
                                            board[r, c + 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                                if (c != 0 && numboard[r, c - 1] == 0)
                                {
                                    for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 1; i++)
                                    {
                                        if (numboard[r, c + i + 1] == 2)
                                        {
                                            numboard[r, c - 1] = 3;
                                            board[r, c - 1].BackColor = Color.LightBlue;
                                        }
                                    }
                                }
                            }
                        }
#endregion
                    #region win 
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 3)
                            bluewinstate++;
                    }
                    if (bluewinstate == 0)
                    {
                        #region checks options for black (4)
                        for (int r = 0; r < num; r++)
                            for (int c = 0; c < num; c++)
                                if (numboard[r, c] == 2)
                                {
                                    try
                                    {
                                        if (numboard[r + 1, c + 1] == 0)
                                            for (int i = 0; numboard[r - i, c - i] == 2; i++)
                                                if (numboard[r - i - 1, c - i - 1] == 1)
                                                {
                                                    numboard[r + 1, c + 1] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c - 1] == 0)
                                            for (int i = 0; numboard[r + i, c + i] == 2; i++)
                                                if (numboard[r + i + 1, c + i + 1] == 1)
                                                {
                                                    numboard[r - 1, c - 1] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c + 1] == 0)
                                            for (int i = 0; numboard[r + i, c - i] == 2; i++)
                                                if (numboard[r + i + 1, c - i - 1] == 1)
                                                {
                                                    numboard[r - 1, c + 1] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r + 1, c - 1] == 0)
                                            for (int i = 0; numboard[r - i, c + i] == 2; i++)
                                                if (numboard[r - i - 1, c + i + 1] == 1)
                                                {
                                                    numboard[r + 1, c - 1] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c] == 0)
                                            for (int i = 0; numboard[r + i, c] == 2; i++)
                                                if (numboard[r + i + 1, c] == 1)
                                                {
                                                    numboard[r - 1, c] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r + 1, c] == 0)
                                            for (int i = 0; numboard[r - i, c] == 2; i++)
                                                if (numboard[r - i - 1, c] == 1)
                                                {
                                                    numboard[r + 1, c] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r, c + 1] == 0)
                                            for (int i = 0; numboard[r, c - i] == 2; i++)
                                                if (numboard[r, c - i - 1] == 1)
                                                {
                                                    numboard[r, c + 1] = 4;
                                                }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r, c - 1] == 0)
                                            for (int i = 0; numboard[r, c + i] == 2; i++)
                                                if (numboard[r, c + i + 1] == 1)
                                                {
                                                    numboard[r, c - 1] = 4;
                                                }
                                    }
                                    catch { }
                                }
                        #endregion
                        for (int j = 0; j < num * num; j++)
                        {
                            if (numboard[j / num, j % num] == 4)
                                blackwinstate++;
                            if (numboard[j / num, j % num] == 1 || numboard[j / num, j % num] == 2)
                                all++;
                        }
                        if (blackwinstate == 0)
                        {
                            int bluecheck = 0;
                            int blackcheck = 0;
                            for (int i = 0; i < num * num; i++)
                            {
                                if (numboard[i / num, i % num] == 0)
                                    numboard[i / num, i % num] = -1;
                                if (numboard[i / num, i % num] == 1)
                                    blackcheck++;
                                if (numboard[i / num, i % num] == 2)
                                    bluecheck++;
                            }
                            if (blackcheck > bluecheck)
                                MessageBox.Show("Congratiolations black player, you won!!!");
                            if (bluecheck > blackcheck)
                                MessageBox.Show("Congratiolations blue player, you won!!!");
                            if (bluecheck == blackcheck)
                                MessageBox.Show("It's a tie");
                            textBox2.Text = (" ");
                        }
                        for (int j = 0; j < num * num; j++)
                        {
                            if (numboard[j / num, j % num] == 4)
                                numboard[j / num, j % num] = 0;
                        }
                    }
                    #endregion
                }
                else
                {
                    turn = 1;
                    #region black checker
                    for (int r = 0; r < num; r++)
                        for (int c = 0; c < num; c++)
                        {
                            if (numboard[r, c] == 2)
                            {
                                if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 2; i++)
                                    {
                                        if (numboard[r - i - 1, c - i - 1] == 1)
                                        {
                                            numboard[r + 1, c + 1] = 3;
                                            board[r + 1, c + 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 && c + i + 1 <= num - 1 && numboard[r + i, c + i] == 2; i++)
                                    {
                                        if (numboard[r + i + 1, c + i + 1] == 1)
                                        {
                                            numboard[r - 1, c - 1] = 3;
                                            board[r - 1, c - 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 2; i++)
                                    {
                                        if (numboard[r + i + 1, c - i - 1] == 1)
                                        {
                                            numboard[r - 1, c + 1] = 3;
                                            board[r - 1, c + 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 2; i++)
                                    {
                                        if (numboard[r - i - 1, c + i + 1] == 1)
                                        {
                                            numboard[r + 1, c - 1] = 3;
                                            board[r + 1, c - 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (r != 0 && numboard[r - 1, c] == 0)
                                {
                                    for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 2; i++)
                                    {
                                        if (numboard[r + i + 1, c] == 1)
                                        {
                                            numboard[r - 1, c] = 3;
                                            board[r - 1, c].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (r != num - 1 && numboard[r + 1, c] == 0)
                                {
                                    for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 2; i++)
                                    {
                                        if (numboard[r - i - 1, c] == 1)
                                        {
                                            numboard[r + 1, c] = 3;
                                            board[r + 1, c].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (c != num - 1 && numboard[r, c + 1] == 0)
                                {
                                    for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 2; i++)
                                    {
                                        if (numboard[r, c - i - 1] == 1)
                                        {
                                            numboard[r, c + 1] = 3;
                                            board[r, c + 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                                if (c != 0 && numboard[r, c - 1] == 0)
                                {
                                    for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 2; i++)
                                    {
                                        if (numboard[r, c + i + 1] == 1)
                                        {
                                            numboard[r, c - 1] = 3;
                                            board[r, c - 1].BackColor = Color.LightGreen;
                                        }
                                    }
                                }
                            }
                        }
                    #endregion
                    #region win
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 3)
                            blackwinstate++;
                    }
                    if (blackwinstate == 0)
                    {
                        #region checks options for blue (4)
                        for (int r = 0; r < num; r++)
                            for (int c = 0; c < num; c++)
                            {
                                if (numboard[r, c] == 1)
                                {
                                    try
                                    {
                                        if (numboard[r + 1, c + 1] == 0)
                                        {
                                            for (int i = 0; numboard[r - i, c - i] == 1; i++)
                                            {
                                                if (numboard[r - i - 1, c - i - 1] == 2)
                                                {
                                                    numboard[r + 1, c + 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c - 1] == 0)
                                        {
                                            for (int i = 0; numboard[r + i, c + i] == 1; i++)
                                            {
                                                if (numboard[r + i + 1, c + i + 1] == 2)
                                                {
                                                    numboard[r - 1, c - 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c + 1] == 0)
                                        {
                                            for (int i = 0; numboard[r + i, c - i] == 1; i++)
                                            {
                                                if (numboard[r + i + 1, c - i - 1] == 2)
                                                {
                                                    numboard[r - 1, c + 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r + 1, c - 1] == 0)
                                        {
                                            for (int i = 0; numboard[r - i, c + i] == 1; i++)
                                            {
                                                if (numboard[r - i - 1, c + i + 1] == 2)
                                                {
                                                    numboard[r + 1, c - 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r - 1, c] == 0)
                                        {
                                            for (int i = 0; numboard[r + i, c] == 1; i++)
                                            {
                                                if (numboard[r + i + 1, c] == 2)
                                                {
                                                    numboard[r - 1, c] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r + 1, c] == 0)
                                        {
                                            for (int i = 0; numboard[r - i, c] == 1; i++)
                                            {
                                                if (numboard[r - i - 1, c] == 2)
                                                {
                                                    numboard[r + 1, c] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r, c + 1] == 0)
                                        {
                                            for (int i = 0; numboard[r, c - i] == 1; i++)
                                            {
                                                if (numboard[r, c - i - 1] == 2)
                                                {
                                                    numboard[r, c + 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    try
                                    {
                                        if (numboard[r, c - 1] == 0)
                                        {
                                            for (int i = 0; numboard[r, c + i] == 1; i++)
                                            {
                                                if (numboard[r, c + i + 1] == 2)
                                                {
                                                    numboard[r, c - 1] = 4;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                        #endregion
                        for (int j = 0; j < num * num; j++)
                        {
                            if (numboard[j / num, j % num] == 4)
                                bluewinstate++;
                            if (numboard[j / num, j % num] == 1 || numboard[j / num, j % num] == 2)
                                all++;
                        }
                        if (bluewinstate == 0)
                        {
                            int bluecheck = 0;
                            int blackcheck = 0;
                            for (int i = 0; i < num * num; i++)
                            {
                                if (numboard[i / num, i % num] == 0)
                                    numboard[i / num, i % num] = -1;
                                if (numboard[i / num, i % num] == 1)
                                    blackcheck++;
                                if (numboard[i / num, i % num] == 2)
                                    bluecheck++;
                            }
                            if (blackcheck > bluecheck)
                                MessageBox.Show("Congratiolations black player, you won!!!");
                            if (bluecheck > blackcheck)
                                MessageBox.Show("Congratiolations blue player, you won!!!");
                            if (bluecheck == blackcheck)
                                MessageBox.Show("It's a tie");
                            textBox2.Text = (" ");
                        }
                        for (int j = 0; j < num * num; j++)
                        {
                            if (numboard[j / num, j % num] == 4)
                                numboard[j / num, j % num] = 0;
                        }
                    }
                    #endregion
                }

                if (turn == 1)
                {
                    textBox2.Text = ("Black turn");
                    textBox2.ForeColor = Color.Black;
                }
                else
                {
                    textBox2.Text = ("Blue turn");
                    textBox2.ForeColor = Color.Blue;
                }
            } // מבצע מהלכים
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < num * num; i++)
            {
                if (numboard[i / num, i % num] == 3)
                {
                    numboard[i / num, i % num] = 0;
                    board[i / num, i % num].BackColor = Color.FromArgb(230, 230, 230);
                }
            }
            if (turn == 1)
            {
                turn++;
                textBox2.Text = ("Blue turn");
                textBox2.ForeColor = Color.Blue;
                #region blue checker
                for (int r = 0; r < num; r++)
                    for (int c = 0; c < num; c++)
                    {
                        if (numboard[r, c] == 1)
                        {
                            if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c - i - 1] == 2)
                                    {
                                        numboard[r + 1, c + 1] = 3;
                                        board[r + 1, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c + i + 1 <= num - 1 && numboard[r + i, c + i] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c + i + 1] == 2)
                                    {
                                        numboard[r - 1, c - 1] = 3;
                                        board[r - 1, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c - i - 1] == 2)
                                    {
                                        numboard[r - 1, c + 1] = 3;
                                        board[r - 1, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c + i + 1] == 2)
                                    {
                                        numboard[r + 1, c - 1] = 3;
                                        board[r + 1, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && numboard[r - 1, c] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c] == 2)
                                    {
                                        numboard[r - 1, c] = 3;
                                        board[r - 1, c].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != num - 1 && numboard[r + 1, c] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c] == 2)
                                    {
                                        numboard[r + 1, c] = 3;
                                        board[r + 1, c].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (c != num - 1 && numboard[r, c + 1] == 0)
                            {
                                for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 1; i++)
                                {
                                    if (numboard[r, c - i - 1] == 2)
                                    {
                                        numboard[r, c + 1] = 3;
                                        board[r, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (c != 0 && numboard[r, c - 1] == 0)
                            {
                                for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 1; i++)
                                {
                                    if (numboard[r, c + i + 1] == 2)
                                    {
                                        numboard[r, c - 1] = 3;
                                        board[r, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                        }
                    }
                #endregion
            }
            else
            {
                turn--;
                textBox2.Text = ("Black turn");
                textBox2.ForeColor = Color.Black;
                #region black checker
                for (int r = 0; r < num; r++)
                    for (int c = 0; c < num; c++)
                    {
                        if (numboard[r, c] == 2)
                        {
                            if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c - i - 1] == 1)
                                    {
                                        numboard[r + 1, c + 1] = 3;
                                        board[r + 1, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c + i + 1 <= num - 1 && numboard[r + i, c + i] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c + i + 1] == 1)
                                    {
                                        numboard[r - 1, c - 1] = 3;
                                        board[r - 1, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c - i - 1] == 1)
                                    {
                                        numboard[r - 1, c + 1] = 3;
                                        board[r - 1, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c + i + 1] == 1)
                                    {
                                        numboard[r + 1, c - 1] = 3;
                                        board[r + 1, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && numboard[r - 1, c] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c] == 1)
                                    {
                                        numboard[r - 1, c] = 3;
                                        board[r - 1, c].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != num - 1 && numboard[r + 1, c] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c] == 1)
                                    {
                                        numboard[r + 1, c] = 3;
                                        board[r + 1, c].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (c != num - 1 && numboard[r, c + 1] == 0)
                            {
                                for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 2; i++)
                                {
                                    if (numboard[r, c - i - 1] == 1)
                                    {
                                        numboard[r, c + 1] = 3;
                                        board[r, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (c != 0 && numboard[r, c - 1] == 0)
                            {
                                for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 2; i++)
                                {
                                    if (numboard[r, c + i + 1] == 1)
                                    {
                                        numboard[r, c - 1] = 3;
                                        board[r, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                        }
                    }
                #endregion
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (turn == 2)
            {
                #region computer
                int[,] ezer1 = new int[num, num];
                int[,] ezer2 = new int[num, num];
                int[,] ezer3 = new int[num, num];

                int Max = -999999, Min = 999999, Max2 = -99999, y = -1, x = -1;

                for (int shura = 0; shura < num; shura++)
                    for (int amuda = 0; amuda < num; amuda++)
                    {
                        if (numboard[shura, amuda] == 3)
                        {
                            for (int shu = 0; shu < num; shu++)
                                for (int amu = 0; amu < num; amu++)
                                {
                                    ezer1[shu, amu] = numboard[shu, amu];
                                }
                            #region turn
                            for (int i = 1; shura + i + 1 <= num - 1 && amuda + i + 1 <= num - 1 && ezer1[shura + i, amuda + i] == 1; i++)
                                if (ezer1[shura + i + 1, amuda + i + 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda + i] = 2;

                            for (int i = 1; shura - i - 1 >= 0 && amuda - i - 1 >= 0 && ezer1[shura - i, amuda - i] == 1; i++)
                                if (ezer1[shura - i - 1, amuda - i - 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda - i] = 2;

                            for (int i = 1; shura - i - 1 >= 0 && amuda + i + 1 <= num - 1 && ezer1[shura - i, amuda + i] == 1; i++)
                                if (ezer1[shura - i - 1, amuda + i + 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda + i] = 2;

                            for (int i = 1; shura + i + 1 <= num - 1 && amuda - i - 1 >= 0 && ezer1[shura + i, amuda - i] == 1; i++)
                                if (ezer1[shura + i + 1, amuda - i - 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda - i] = 2;

                            for (int i = 1; shura + i + 1 <= num - 1 && ezer1[shura + i, amuda] == 1; i++)
                                if (ezer1[shura + i + 1, amuda] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda] = 2;

                            for (int i = 1; shura - i - 1 >= 0 && ezer1[shura - i, amuda] == 1; i++)
                                if (ezer1[shura - i - 1, amuda] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda] = 2;

                            for (int i = 1; amuda + i + 1 <= num - 1 && ezer1[shura, amuda + i] == 1; i++)
                                if (ezer1[shura, amuda + i + 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura, amuda + i] = 2;

                            for (int i = 1; amuda - i - 1 >= 0 && ezer1[shura, amuda - i] == 1; i++)
                                if (ezer1[shura, amuda - i - 1] == 2)
                                    for (; i >= 0; i--)
                                        ezer1[shura, amuda - i] = 2;
                            #endregion

                            Min = 999999;
                            for (int shu = 0; shu < num; shu++)
                            {
                                for (int amu = 0; amu < num; amu++)
                                {
                                    if (ezer1[shu, amu] == 3)
                                    {
                                        ezer1[shu, amu] = 0;
                                    }
                                }
                            } // deletes 3 

                            for (int shu = 0; shu < num; shu++)
                                for (int amu = 0; amu < num; amu++)
                                {
                                    for (int s = 0; s < num; s++)
                                        for (int a = 0; a < num; a++)
                                        {
                                            ezer2[s, a] = ezer1[s, a];
                                        }
                                    int diditwork = 0;
                                    #region turn
                                    if (ezer2[shu, amu] == 0)
                                    {
                                        for (int i = 1; shu + i + 1 <= num - 1 && amu + i + 1 <= num - 1 && ezer2[shu + i, amu + i] == 2; i++)
                                            if (ezer2[shu + i + 1, amu + i + 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu + i] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && amu - i - 1 >= 0 && ezer2[shu - i, amu - i] == 2; i++)
                                            if (ezer2[shu - i - 1, amu - i - 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu - i] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && amu + i + 1 <= num - 1 && ezer2[shu - i, amu + i] == 2; i++)
                                            if (ezer2[shu - i - 1, amu + i + 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu + i] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu + i + 1 <= num - 1 && amu - i - 1 >= 0 && ezer2[shu + i, amu - i] == 2; i++)
                                            if (ezer2[shu + i + 1, amu - i - 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu - i] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu + i + 1 <= num - 1 && ezer2[shu + i, amu] == 2; i++)
                                            if (ezer2[shu + i + 1, amu] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && ezer2[shu - i, amu] == 2; i++)
                                            if (ezer2[shu - i - 1, amu] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; amu + i + 1 <= num - 1 && ezer2[shu, amu + i] == 2; i++)
                                            if (ezer2[shu, amu + i + 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu, amu + i] = 1;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; amu - i - 1 >= 0 && ezer2[shu, amu - i] == 2; i++)
                                            if (ezer2[shu, amu - i - 1] == 1)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu, amu - i] = 1;
                                                diditwork = 1;
                                            }
                                    }
                                    #endregion
                                    if (diditwork == 1)
                                    {
                                        Max2 = -99999;
                                        for (int row = 0; row < num; row++)
                                            for (int col = 0; col < num; col++)
                                            {
                                                for (int s = 0; s < num; s++)
                                                    for (int a = 0; a < num; a++)
                                                    {
                                                        ezer3[s, a] = ezer2[s, a];
                                                    }
                                                int diditwork2 = 0;
                                                #region turn
                                                if (ezer3[row, col] == 0)
                                                {
                                                    for (int i = 1; row + i + 1 <= num - 1 && col + i + 1 <= num - 1 && ezer3[row + i, col + i] == 1; i++)
                                                        if (ezer3[row + i + 1, col + i + 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col + i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && col - i - 1 >= 0 && ezer3[row - i, col - i] == 1; i++)
                                                        if (ezer3[row - i - 1, col - i - 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col - i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && col + i + 1 <= num - 1 && ezer3[row - i, col + i] == 1; i++)
                                                        if (ezer3[row - i - 1, col + i + 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col + i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row + i + 1 <= num - 1 && col - i - 1 >= 0 && ezer3[row + i, col - i] == 1; i++)
                                                        if (ezer3[row + i + 1, col - i - 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col - i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row + i + 1 <= num - 1 && ezer3[row + i, col] == 1; i++)
                                                        if (ezer3[row + i + 1, col] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && ezer3[row - i, col] == 1; i++)
                                                        if (ezer3[row - i - 1, col] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; col + i + 1 <= num - 1 && ezer3[row, col + i] == 1; i++)
                                                        if (ezer3[row, col + i + 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row, col + i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; col - i - 1 >= 0 && ezer3[row, col - i] == 1; i++)
                                                        if (ezer3[row, col - i - 1] == 2)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row, col - i] = 2;
                                                            diditwork2 = 1;
                                                        }
                                                }
                                                #endregion
                                                if (diditwork2 == 1)
                                                {
                                                    int current = value(ezer3);
                                                    if (current > Max2)
                                                    {
                                                        Max2 = current;
                                                    }
                                                }
                                            }
                                        if (Max2 < Min)
                                        {
                                            Min = Max2;
                                        }
                                    }
                                }
                            if (Min > Max)
                            {
                                Max = Min;
                                y = shura;
                                x = amuda;
                            }
                        }
                    }
                if (y != -1)
                {
                    for (int shura = 0; shura < num; shura++)
                        for (int amuda = 0; amuda < num; amuda++)
                        {
                            preprepreNumboard[shura, amuda] = prepreNumboard[shura, amuda];
                            prepreNumboard[shura, amuda] = preNumboard[shura, amuda];
                            preNumboard[shura, amuda] = numboard[shura, amuda];
                        }
                    #region does turn
                    for (int i = 1; y + i + 1 <= num - 1 && x + i + 1 <= num - 1 && numboard[y + i, x + i] == 1; i++)
                        if (numboard[y + i + 1, x + i + 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x + i] = 2;
                                board[y + i, x + i].BackColor = Color.Blue;
                            }
                    for (int i = 1; y - i - 1 >= 0 && x - i - 1 >= 0 && numboard[y - i, x - i] == 1; i++)
                        if (numboard[y - i - 1, x - i - 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x - i] = 2;
                                board[y - i, x - i].BackColor = Color.Blue;
                            }
                    for (int i = 1; y - i - 1 >= 0 && x + i + 1 <= num - 1 && numboard[y - i, x + i] == 1; i++)
                        if (numboard[y - i - 1, x + i + 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x + i] = 2;
                                board[y - i, x + i].BackColor = Color.Blue;
                            }
                    for (int i = 1; y + i + 1 <= num - 1 && x - i - 1 >= 0 && numboard[y + i, x - i] == 1; i++)
                        if (numboard[y + i + 1, x - i - 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x - i] = 2;
                                board[y + i, x - i].BackColor = Color.Blue;
                            }
                    for (int i = 1; y + i + 1 <= num - 1 && numboard[y + i, x] == 1; i++)
                        if (numboard[y + i + 1, x] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x] = 2;
                                board[y + i, x].BackColor = Color.Blue;
                            }
                    for (int i = 1; y - i - 1 >= 0 && numboard[y - i, x] == 1; i++)
                        if (numboard[y - i - 1, x] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x] = 2;
                                board[y - i, x].BackColor = Color.Blue;
                            }
                    for (int i = 1; x + i + 1 <= num - 1 && numboard[y, x + i] == 1; i++)
                        if (numboard[y, x + i + 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y, x + i] = 2;
                                board[y, x + i].BackColor = Color.Blue;
                            }
                    for (int i = 1; x - i - 1 >= 0 && numboard[y, x - i] == 1; i++)
                        if (numboard[y, x - i - 1] == 2)
                            for (; i >= 0; i--)
                            {
                                numboard[y, x - i] = 2;
                                board[y, x - i].BackColor = Color.Blue;
                            }
                    #endregion
                }
                #endregion

                #region shit after the computer turn
                for (int shura = 0; shura < num; shura++)
                    for (int amuda = 0; amuda < num; amuda++)
                    {
                        if (numboard[shura, amuda] == 3)
                        {
                            numboard[shura, amuda] = 0;
                            board[shura, amuda].BackColor = Color.FromArgb(230, 230, 230);
                        }
                    }
                int bluewinstate = 0, blackwinstate = 0, all = 0;
                turn = 1;
                #region black checker
                for (int r = 0; r < num; r++)
                    for (int c = 0; c < num; c++)
                    {
                        if (numboard[r, c] == 2)
                        {
                            if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c - i - 1] == 1)
                                    {
                                        numboard[r + 1, c + 1] = 3;
                                        board[r + 1, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c + i + 1 <= num - 1 && numboard[r + i, c + i] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c + i + 1] == 1)
                                    {
                                        numboard[r - 1, c - 1] = 3;
                                        board[r - 1, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c - i - 1] == 1)
                                    {
                                        numboard[r - 1, c + 1] = 3;
                                        board[r - 1, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c + i + 1] == 1)
                                    {
                                        numboard[r + 1, c - 1] = 3;
                                        board[r + 1, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != 0 && numboard[r - 1, c] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 2; i++)
                                {
                                    if (numboard[r + i + 1, c] == 1)
                                    {
                                        numboard[r - 1, c] = 3;
                                        board[r - 1, c].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (r != num - 1 && numboard[r + 1, c] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 2; i++)
                                {
                                    if (numboard[r - i - 1, c] == 1)
                                    {
                                        numboard[r + 1, c] = 3;
                                        board[r + 1, c].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (c != num - 1 && numboard[r, c + 1] == 0)
                            {
                                for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 2; i++)
                                {
                                    if (numboard[r, c - i - 1] == 1)
                                    {
                                        numboard[r, c + 1] = 3;
                                        board[r, c + 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                            if (c != 0 && numboard[r, c - 1] == 0)
                            {
                                for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 2; i++)
                                {
                                    if (numboard[r, c + i + 1] == 1)
                                    {
                                        numboard[r, c - 1] = 3;
                                        board[r, c - 1].BackColor = Color.LightGreen;
                                    }
                                }
                            }
                        }
                    }
                #endregion
                #region win
                for (int j = 0; j < num * num; j++)
                {
                    if (numboard[j / num, j % num] == 3)
                        blackwinstate++;
                }
                if (blackwinstate == 0)
                {
                    #region checks options for blue (4)
                    for (int r = 0; r < num; r++)
                        for (int c = 0; c < num; c++)
                        {
                            if (numboard[r, c] == 1)
                            {
                                try
                                {
                                    if (numboard[r + 1, c + 1] == 0)
                                    {
                                        for (int i = 0; numboard[r - i, c - i] == 1; i++)
                                        {
                                            if (numboard[r - i - 1, c - i - 1] == 2)
                                            {
                                                numboard[r + 1, c + 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c - 1] == 0)
                                    {
                                        for (int i = 0; numboard[r + i, c + i] == 1; i++)
                                        {
                                            if (numboard[r + i + 1, c + i + 1] == 2)
                                            {
                                                numboard[r - 1, c - 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c + 1] == 0)
                                    {
                                        for (int i = 0; numboard[r + i, c - i] == 1; i++)
                                        {
                                            if (numboard[r + i + 1, c - i - 1] == 2)
                                            {
                                                numboard[r - 1, c + 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r + 1, c - 1] == 0)
                                    {
                                        for (int i = 0; numboard[r - i, c + i] == 1; i++)
                                        {
                                            if (numboard[r - i - 1, c + i + 1] == 2)
                                            {
                                                numboard[r + 1, c - 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c] == 0)
                                    {
                                        for (int i = 0; numboard[r + i, c] == 1; i++)
                                        {
                                            if (numboard[r + i + 1, c] == 2)
                                            {
                                                numboard[r - 1, c] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r + 1, c] == 0)
                                    {
                                        for (int i = 0; numboard[r - i, c] == 1; i++)
                                        {
                                            if (numboard[r - i - 1, c] == 2)
                                            {
                                                numboard[r + 1, c] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r, c + 1] == 0)
                                    {
                                        for (int i = 0; numboard[r, c - i] == 1; i++)
                                        {
                                            if (numboard[r, c - i - 1] == 2)
                                            {
                                                numboard[r, c + 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r, c - 1] == 0)
                                    {
                                        for (int i = 0; numboard[r, c + i] == 1; i++)
                                        {
                                            if (numboard[r, c + i + 1] == 2)
                                            {
                                                numboard[r, c - 1] = 4;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    #endregion
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 4)
                            bluewinstate++;
                        if (numboard[j / num, j % num] == 1 || numboard[j / num, j % num] == 2)
                            all++;
                    }
                    if (bluewinstate == 0)
                    {
                        int bluecheck = 0;
                        int blackcheck = 0;
                        for (int i = 0; i < num * num; i++)
                        {
                            if (numboard[i / num, i % num] == 0)
                                numboard[i / num, i % num] = -1;
                            if (numboard[i / num, i % num] == 1)
                                blackcheck++;
                            if (numboard[i / num, i % num] == 2)
                                bluecheck++;
                        }
                        if (blackcheck > bluecheck)
                            MessageBox.Show("Congratiolations black player, you won!!!");
                        if (bluecheck > blackcheck)
                            MessageBox.Show("Congratiolations blue player, you won!!!");
                        if (bluecheck == blackcheck)
                            MessageBox.Show("It's a tie");
                        textBox2.Text = (" ");
                    }
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 4)
                            numboard[j / num, j % num] = 0;
                    }
                }
                #endregion

                textBox2.Text = ("Black turn");
                textBox2.ForeColor = Color.Black;
                #endregion
            }
            else
            {
                #region computer
                int[,] ezer1 = new int[num, num];
                int[,] ezer2 = new int[num, num];
                int[,] ezer3 = new int[num, num];

                int Min = 999999, Max = -999999, Min2 = 99999, y = -1, x = -1;

                for (int shura = 0; shura < num; shura++)
                    for (int amuda = 0; amuda < num; amuda++)
                    {
                        if (numboard[shura, amuda] == 3)
                        {
                            for (int shu = 0; shu < num; shu++)
                                for (int amu = 0; amu < num; amu++)
                                {
                                    ezer1[shu, amu] = numboard[shu, amu];
                                }
                            #region turn
                            for (int i = 1; shura + i + 1 <= num - 1 && amuda + i + 1 <= num - 1 && ezer1[shura + i, amuda + i] == 2; i++)
                                if (ezer1[shura + i + 1, amuda + i + 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda + i] = 1;

                            for (int i = 1; shura - i - 1 >= 0 && amuda - i - 1 >= 0 && ezer1[shura - i, amuda - i] == 2; i++)
                                if (ezer1[shura - i - 1, amuda - i - 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda - i] = 1;

                            for (int i = 1; shura - i - 1 >= 0 && amuda + i + 1 <= num - 1 && ezer1[shura - i, amuda + i] == 2; i++)
                                if (ezer1[shura - i - 1, amuda + i + 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda + i] = 1;

                            for (int i = 1; shura + i + 1 <= num - 1 && amuda - i - 1 >= 0 && ezer1[shura + i, amuda - i] == 2; i++)
                                if (ezer1[shura + i + 1, amuda - i - 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda - i] = 1;

                            for (int i = 1; shura + i + 1 <= num - 1 && ezer1[shura + i, amuda] == 2; i++)
                                if (ezer1[shura + i + 1, amuda] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura + i, amuda] = 1;

                            for (int i = 1; shura - i - 1 >= 0 && ezer1[shura - i, amuda] == 2; i++)
                                if (ezer1[shura - i - 1, amuda] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura - i, amuda] = 1;

                            for (int i = 1; amuda + i + 1 <= num - 1 && ezer1[shura, amuda + i] == 2; i++)
                                if (ezer1[shura, amuda + i + 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura, amuda + i] = 1;

                            for (int i = 1; amuda - i - 1 >= 0 && ezer1[shura, amuda - i] == 2; i++)
                                if (ezer1[shura, amuda - i - 1] == 1)
                                    for (; i >= 0; i--)
                                        ezer1[shura, amuda - i] = 1;
                            #endregion

                            Max = -999999;
                            for (int shu = 0; shu < num; shu++)
                            {
                                for (int amu = 0; amu < num; amu++)
                                {
                                    if (ezer1[shu, amu] == 3)
                                    {
                                        ezer1[shu, amu] = 0;
                                    }
                                }
                            } // deletes 3 

                            for (int shu = 0; shu < num; shu++)
                                for (int amu = 0; amu < num; amu++)
                                {
                                    for (int s = 0; s < num; s++)
                                        for (int a = 0; a < num; a++)
                                        {
                                            ezer2[s, a] = ezer1[s, a];
                                        }
                                    int diditwork = 0;
                                    #region turn
                                    if (ezer2[shu, amu] == 0)
                                    {
                                        for (int i = 1; shu + i + 1 <= num - 1 && amu + i + 1 <= num - 1 && ezer2[shu + i, amu + i] == 1; i++)
                                            if (ezer2[shu + i + 1, amu + i + 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu + i] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && amu - i - 1 >= 0 && ezer2[shu - i, amu - i] == 1; i++)
                                            if (ezer2[shu - i - 1, amu - i - 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu - i] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && amu + i + 1 <= num - 1 && ezer2[shu - i, amu + i] == 1; i++)
                                            if (ezer2[shu - i - 1, amu + i + 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu + i] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu + i + 1 <= num - 1 && amu - i - 1 >= 0 && ezer2[shu + i, amu - i] == 1; i++)
                                            if (ezer2[shu + i + 1, amu - i - 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu - i] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu + i + 1 <= num - 1 && ezer2[shu + i, amu] == 1; i++)
                                            if (ezer2[shu + i + 1, amu] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu + i, amu] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; shu - i - 1 >= 0 && ezer2[shu - i, amu] == 1; i++)
                                            if (ezer2[shu - i - 1, amu] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu - i, amu] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; amu + i + 1 <= num - 1 && ezer2[shu, amu + i] == 1; i++)
                                            if (ezer2[shu, amu + i + 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu, amu + i] = 2;
                                                diditwork = 1;
                                            }
                                        for (int i = 1; amu - i - 1 >= 0 && ezer2[shu, amu - i] == 1; i++)
                                            if (ezer2[shu, amu - i - 1] == 2)
                                            {
                                                for (; i >= 0; i--)
                                                    ezer2[shu, amu - i] = 2;
                                                diditwork = 1;
                                            }
                                    }
                                    #endregion
                                    if (diditwork == 1)
                                    {
                                        Min2 = 99999;
                                        for (int row = 0; row < num; row++)
                                            for (int col = 0; col < num; col++)
                                            {
                                                for (int s = 0; s < num; s++)
                                                    for (int a = 0; a < num; a++)
                                                    {
                                                        ezer3[s, a] = ezer2[s, a];
                                                    }
                                                int diditwork2 = 0;
                                                #region turn
                                                if (ezer3[row, col] == 0)
                                                {
                                                    for (int i = 1; row + i + 1 <= num - 1 && col + i + 1 <= num - 1 && ezer3[row + i, col + i] == 2; i++)
                                                        if (ezer3[row + i + 1, col + i + 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col + i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && col - i - 1 >= 0 && ezer3[row - i, col - i] == 2; i++)
                                                        if (ezer3[row - i - 1, col - i - 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col - i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && col + i + 1 <= num - 1 && ezer3[row - i, col + i] == 2; i++)
                                                        if (ezer3[row - i - 1, col + i + 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col + i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row + i + 1 <= num - 1 && col - i - 1 >= 0 && ezer3[row + i, col - i] == 2; i++)
                                                        if (ezer3[row + i + 1, col - i - 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col - i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row + i + 1 <= num - 1 && ezer3[row + i, col] == 2; i++)
                                                        if (ezer3[row + i + 1, col] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row + i, col] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; row - i - 1 >= 0 && ezer3[row - i, col] == 2; i++)
                                                        if (ezer3[row - i - 1, col] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row - i, col] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; col + i + 1 <= num - 1 && ezer3[row, col + i] == 2; i++)
                                                        if (ezer3[row, col + i + 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row, col + i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                    for (int i = 1; col - i - 1 >= 0 && ezer3[row, col - i] == 2; i++)
                                                        if (ezer3[row, col - i - 1] == 1)
                                                        {
                                                            for (; i >= 0; i--)
                                                                ezer3[row, col - i] = 1;
                                                            diditwork2 = 1;
                                                        }
                                                }
                                                #endregion
                                                if (diditwork2 == 1)
                                                {
                                                    int current = value(ezer3);
                                                    if (current < Min2)
                                                    {
                                                        Min2 = current;
                                                    }
                                                }
                                            }
                                        if (Min2 > Max)
                                        {
                                            Max = Min2;
                                        }
                                    }
                                }
                            if (Max < Min)
                            {
                                Min = Max;
                                y = shura;
                                x = amuda;
                            }
                        }
                    }
                if (y != -1)
                {
                    for (int shura = 0; shura < num; shura++)
                        for (int amuda = 0; amuda < num; amuda++)
                        {
                            preprepreNumboard[shura, amuda] = prepreNumboard[shura, amuda];
                            prepreNumboard[shura, amuda] = preNumboard[shura, amuda];
                            preNumboard[shura, amuda] = numboard[shura, amuda];
                        }

                    #region does turn
                    for (int i = 1; y + i + 1 <= num - 1 && x + i + 1 <= num - 1 && numboard[y + i, x + i] == 2; i++)
                        if (numboard[y + i + 1, x + i + 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x + i] = 1;
                                board[y + i, x + i].BackColor = Color.Black;
                            }
                    for (int i = 1; y - i - 1 >= 0 && x - i - 1 >= 0 && numboard[y - i, x - i] == 2; i++)
                        if (numboard[y - i - 1, x - i - 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x - i] = 1;
                                board[y - i, x - i].BackColor = Color.Black;
                            }
                    for (int i = 1; y - i - 1 >= 0 && x + i + 1 <= num - 1 && numboard[y - i, x + i] == 2; i++)
                        if (numboard[y - i - 1, x + i + 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x + i] = 1;
                                board[y - i, x + i].BackColor = Color.Black;
                            }
                    for (int i = 1; y + i + 1 <= num - 1 && x - i - 1 >= 0 && numboard[y + i, x - i] == 2; i++)
                        if (numboard[y + i + 1, x - i - 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x - i] = 1;
                                board[y + i, x - i].BackColor = Color.Black;
                            }
                    for (int i = 1; y + i + 1 <= num - 1 && numboard[y + i, x] == 2; i++)
                        if (numboard[y + i + 1, x] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y + i, x] = 1;
                                board[y + i, x].BackColor = Color.Black;
                            }
                    for (int i = 1; y - i - 1 >= 0 && numboard[y - i, x] == 2; i++)
                        if (numboard[y - i - 1, x] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y - i, x] = 1;
                                board[y - i, x].BackColor = Color.Black;
                            }
                    for (int i = 1; x + i + 1 <= num - 1 && numboard[y, x + i] == 2; i++)
                        if (numboard[y, x + i + 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y, x + i] = 1;
                                board[y, x + i].BackColor = Color.Black;
                            }
                    for (int i = 1; x - i - 1 >= 0 && numboard[y, x - i] == 2; i++)
                        if (numboard[y, x - i - 1] == 1)
                            for (; i >= 0; i--)
                            {
                                numboard[y, x - i] = 1;
                                board[y, x - i].BackColor = Color.Black;
                            }
                    #endregion
                }
                #endregion

                #region shit after the computer turn
                for (int shura = 0; shura < num; shura++)
                    for (int amuda = 0; amuda < num; amuda++)
                    {
                        if (numboard[shura, amuda] == 3)
                        {
                            numboard[shura, amuda] = 0;
                            board[shura, amuda].BackColor = Color.FromArgb(230, 230, 230);
                        }
                    }
                int bluewinstate = 0, blackwinstate = 0, all = 0;
                turn = 2;
                #region blue checker
                for (int r = 0; r < num; r++)
                    for (int c = 0; c < num; c++)
                    {
                        if (numboard[r, c] == 1)
                        {
                            if (r != num - 1 && c != num - 1 && numboard[r + 1, c + 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c - i - 1 >= 0 && numboard[r - i, c - i] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c - i - 1] == 2)
                                    {
                                        numboard[r + 1, c + 1] = 3;
                                        board[r + 1, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && c != 0 && numboard[r - 1, c - 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c + i + 1 <= num - 1 && numboard[r + i, c + i] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c + i + 1] == 2)
                                    {
                                        numboard[r - 1, c - 1] = 3;
                                        board[r - 1, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && c != num - 1 && numboard[r - 1, c + 1] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && c - i - 1 >= 0 && numboard[r + i, c - i] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c - i - 1] == 2)
                                    {
                                        numboard[r - 1, c + 1] = 3;
                                        board[r - 1, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != num - 1 && c != 0 && numboard[r + 1, c - 1] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && c + i + 1 <= num - 1 && numboard[r - i, c + i] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c + i + 1] == 2)
                                    {
                                        numboard[r + 1, c - 1] = 3;
                                        board[r + 1, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != 0 && numboard[r - 1, c] == 0)
                            {
                                for (int i = 0; r + i + 1 <= num - 1 && numboard[r + i, c] == 1; i++)
                                {
                                    if (numboard[r + i + 1, c] == 2)
                                    {
                                        numboard[r - 1, c] = 3;
                                        board[r - 1, c].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (r != num - 1 && numboard[r + 1, c] == 0)
                            {
                                for (int i = 0; r - i - 1 >= 0 && numboard[r - i, c] == 1; i++)
                                {
                                    if (numboard[r - i - 1, c] == 2)
                                    {
                                        numboard[r + 1, c] = 3;
                                        board[r + 1, c].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (c != num - 1 && numboard[r, c + 1] == 0)
                            {
                                for (int i = 0; c - i - 1 >= 0 && numboard[r, c - i] == 1; i++)
                                {
                                    if (numboard[r, c - i - 1] == 2)
                                    {
                                        numboard[r, c + 1] = 3;
                                        board[r, c + 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                            if (c != 0 && numboard[r, c - 1] == 0)
                            {
                                for (int i = 0; c + i + 1 <= num - 1 && numboard[r, c + i] == 1; i++)
                                {
                                    if (numboard[r, c + i + 1] == 2)
                                    {
                                        numboard[r, c - 1] = 3;
                                        board[r, c - 1].BackColor = Color.LightBlue;
                                    }
                                }
                            }
                        }
                    }
                #endregion
                #region win 
                for (int j = 0; j < num * num; j++)
                {
                    if (numboard[j / num, j % num] == 3)
                        bluewinstate++;
                }
                if (bluewinstate == 0)
                {
                    #region checks options for black (4)
                    for (int r = 0; r < num; r++)
                        for (int c = 0; c < num; c++)
                            if (numboard[r, c] == 2)
                            {
                                try
                                {
                                    if (numboard[r + 1, c + 1] == 0)
                                        for (int i = 0; numboard[r - i, c - i] == 2; i++)
                                            if (numboard[r - i - 1, c - i - 1] == 1)
                                            {
                                                numboard[r + 1, c + 1] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c - 1] == 0)
                                        for (int i = 0; numboard[r + i, c + i] == 2; i++)
                                            if (numboard[r + i + 1, c + i + 1] == 1)
                                            {
                                                numboard[r - 1, c - 1] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c + 1] == 0)
                                        for (int i = 0; numboard[r + i, c - i] == 2; i++)
                                            if (numboard[r + i + 1, c - i - 1] == 1)
                                            {
                                                numboard[r - 1, c + 1] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r + 1, c - 1] == 0)
                                        for (int i = 0; numboard[r - i, c + i] == 2; i++)
                                            if (numboard[r - i - 1, c + i + 1] == 1)
                                            {
                                                numboard[r + 1, c - 1] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r - 1, c] == 0)
                                        for (int i = 0; numboard[r + i, c] == 2; i++)
                                            if (numboard[r + i + 1, c] == 1)
                                            {
                                                numboard[r - 1, c] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r + 1, c] == 0)
                                        for (int i = 0; numboard[r - i, c] == 2; i++)
                                            if (numboard[r - i - 1, c] == 1)
                                            {
                                                numboard[r + 1, c] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r, c + 1] == 0)
                                        for (int i = 0; numboard[r, c - i] == 2; i++)
                                            if (numboard[r, c - i - 1] == 1)
                                            {
                                                numboard[r, c + 1] = 4;
                                            }
                                }
                                catch { }
                                try
                                {
                                    if (numboard[r, c - 1] == 0)
                                        for (int i = 0; numboard[r, c + i] == 2; i++)
                                            if (numboard[r, c + i + 1] == 1)
                                            {
                                                numboard[r, c - 1] = 4;
                                            }
                                }
                                catch { }
                            }
                    #endregion
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 4)
                            blackwinstate++;
                        if (numboard[j / num, j % num] == 1 || numboard[j / num, j % num] == 2)
                            all++;
                    }
                    if (blackwinstate == 0)
                    {
                        int bluecheck = 0;
                        int blackcheck = 0;
                        for (int i = 0; i < num * num; i++)
                        {
                            if (numboard[i / num, i % num] == 0)
                                numboard[i / num, i % num] = -1;
                            if (numboard[i / num, i % num] == 1)
                                blackcheck++;
                            if (numboard[i / num, i % num] == 2)
                                bluecheck++;
                        }
                        if (blackcheck > bluecheck)
                            MessageBox.Show("Congratiolations black player, you won!!!");
                        if (bluecheck > blackcheck)
                            MessageBox.Show("Congratiolations blue player, you won!!!");
                        if (bluecheck == blackcheck)
                            MessageBox.Show("It's a tie");
                        textBox2.Text = (" ");
                    }
                    for (int j = 0; j < num * num; j++)
                    {
                        if (numboard[j / num, j % num] == 4)
                            numboard[j / num, j % num] = 0;
                    }
                }
                #endregion

                textBox2.Text = ("Blue turn");
                textBox2.ForeColor = Color.Blue;
                #endregion
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int shura = 0; shura < num; shura++)
                for (int amuda = 0; amuda < num; amuda++)
                {
                    numboard[shura, amuda] = preNumboard[shura, amuda];
                    if(numboard[shura,amuda] == 0)
                        board[shura,amuda].BackColor = Color.FromArgb(230, 230, 230);
                    if (numboard[shura, amuda] == 1)
                        board[shura, amuda].BackColor = Color.Black;
                    if (numboard[shura, amuda] == 2)
                        board[shura, amuda].BackColor = Color.Blue;
                    if (numboard[shura, amuda] == 3)
                    {
                        if (turn == 1)
                            board[shura, amuda].BackColor = Color.LightBlue;
                        else
                            board[shura, amuda].BackColor = Color.LightGreen;
                    }
                    preNumboard[shura, amuda] = prepreNumboard[shura, amuda];
                    prepreNumboard[shura, amuda] = preprepreNumboard[shura, amuda];
                    board[shura, amuda].Text = "";
                }
            if (turn == 1)
                turn = 2;
            else
                turn = 1;

        } // Undo

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}