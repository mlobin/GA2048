using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GATest
{
    public partial class Form1 : Form
    {
        List<List<Label>> field = new List<List<Label>>();
        //List<List<int>> fieldInt = new List<List<int>>();
        List<Player> players = new List<Player>();
        public Form1()
        {

            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                List<int> tmpInt = new List<int>();
                for (int j = 0; j < 4; j++)
                    tmpInt.Add(0);
                //fieldInt.Add(tmpInt);
                field.Add(new List<Label>());
            }
            field[0].Add(label1);
            field[0].Add(label2);
            field[0].Add(label3);
            field[0].Add(label4);
            field[1].Add(label5);
            field[1].Add(label6);
            field[1].Add(label7);
            field[1].Add(label8);
            field[2].Add(label9);
            field[2].Add(label10);
            field[2].Add(label11);
            field[2].Add(label12);
            field[3].Add(label13);
            field[3].Add(label14);
            field[3].Add(label15);
            field[3].Add(label16);
            List<List<int>> fieldInt = new List<List<int>>();
            for (int cnt = 0; cnt < 4; cnt++)
            {
                fieldInt.Add(new List<int>());
                for (int cnt1 = 0; cnt1 < 4; cnt1++)
                {
                    fieldInt[cnt].Add(0);
                }
            }
            drawField(fieldInt);

        }

        public static List<List<int>> AddTile(List<List<int>> fieldInt)
        {
            Random r = new Random();
            int next = r.Next(16);
            int cnt = 0;
            List<int> n = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                if (fieldInt[i / 4][i % 4] == 0) n.Add(i);
            }
            cnt = n[r.Next(n.Count)];
            fieldInt[cnt/4][cnt%4] = r.Next(100) <= 20 ? 4 : 2;
            return fieldInt;
        }

        public void drawField(List<List<int>> fieldInt)
        {
            for (int i = 0; i < 16; i++)
                field[i / 4][i % 4].Text = fieldInt[i / 4][i % 4].ToString();
        }

        public static List<List<int>> MoveTL(int pos, List<List<int>> fieldInt, bool show=false, Form1 f = null)
        {
            
             switch (pos)
            {
                case 1:
                    for (int i = 0; i < 4; i++)
                        for (int j = 1; j < 4; j++)
                        {
                            int t = j;
                            if (fieldInt[i][j] != 0)
                            {
                                while (t > 0 && fieldInt[i][t-1] == 0)
                                    t--;
                                if (t != j)
                                {
                                    fieldInt[i][t] = fieldInt[i][j];
                                    fieldInt[i][j] = 0;
                                }
                                if (t>0 && fieldInt[i][t - 1] == fieldInt[i][t])
                                    {
                                        fieldInt[i][t - 1] = fieldInt[i][t - 1] * 2;
                                        fieldInt[i][t] = 0;
                                    }
                            }
                        }
                    break;
                case 2:
                    for (int j = 0; j < 4; j++)
                        for (int i = 2; i >=0; i--)
                        {
                            int t = i;
                            if (fieldInt[i][j] != 0)
                            {
                                while (t < 3 && fieldInt[t+1][j] == 0)
                                    t++;
                                if (t != i)
                                {
                                    fieldInt[t][j] = fieldInt[i][j];
                                    fieldInt[i][j] = 0;
                                }
                                if (t < 3 && fieldInt[t+1][j] == fieldInt[t][j])
                                {
                                    fieldInt[t+1][j] = fieldInt[t+1][j] * 2;
                                    fieldInt[t][j] = 0;
                                }
                            }
                        }
                    break;
                case 3:
                    for (int i = 1; i < 4; i++)
                        for (int j = 0; j < 4; j++)
                        {
                            int t = i;
                            if (fieldInt[i][j] != 0)
                            {
                                while (t > 0 && fieldInt[t-1][i] == 0)
                                    t--;
                                if (t != i)
                                {
                                    fieldInt[t][j] = fieldInt[i][j];
                                    fieldInt[i][j] = 0;
                                }
                                if (t > 0 && fieldInt[t-1][j] == fieldInt[t][j])
                                {
                                    fieldInt[t-1][j] = fieldInt[t-1][j] * 2;
                                    fieldInt[t][j] = 0;
                                }
                            }
                        }

                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                        for (int j = 2; j >=0; j--)
                        {
                            int t = j;
                            if (fieldInt[i][j] != 0)
                            {
                                while (t < 3 && fieldInt[i][t+1] == 0)
                                    t++;
                                if (t != j)
                                {
                                    fieldInt[i][t] = fieldInt[i][j];
                                    fieldInt[i][j] = 0;
                                }
                                if (t<3 && fieldInt[i][t + 1] == fieldInt[i][t])
                                    {
                                        fieldInt[i][t + 1] = fieldInt[i][t + 1] * 2;
                                        fieldInt[i][t] = 0;
                                    }
                            }
                        }
                    break;
            }
                fieldInt = AddTile(fieldInt);

            if (show) f.drawField(fieldInt);
            return fieldInt;

        }

        public void KillAndBreed()
        {
            Random r = new Random();
            List<Player> t = new List<Player>(players);
            while (t.Count > 20)
            {
                int a = r.Next(t.Count), b = r.Next(t.Count);
                while (a==b)
                    a = r.Next(t.Count);
                if (t[a].score > t[b].score)
                    t.RemoveAt(b);
                else t.RemoveAt(a);
            }
            while (t.Count < 100)
            {
                int a = r.Next(players.Count), b = r.Next(players.Count);
                while (a == b)
                    a = r.Next(players.Count);
                t.Add(Player.Breed(players[a], players[b]));
            }
            for (int i = 0;i<100;i++)
            {
                if (r.Next(100) > 79)
                    t[i].Mutate();
            }
            players = new List<Player>(t);
        }

        public void Teach()
        {

            var a = MessageBox.Show("Дальше?", "Показать результаты?", MessageBoxButtons.YesNoCancel);
            Setup();
            while (a!=DialogResult.Cancel)
            {
                for (int i = 0; i < 20;i++)
                {
                    KillAndBreed();
                }
                a = MessageBox.Show("Дальше?", "Показать результаты?", MessageBoxButtons.YesNoCancel);
                if (a == DialogResult.Yes)
                {
                    int max = 0;
                    for (int i = 0; i < 100; i++)
                        max = max < players[i].score ? players[i].score : max;
                    MessageBox.Show(max.ToString());
                }
                for (int i = 0; i < 100; i++)
                    players[i].Add();
            }
        }
        public void Setup()
        {
            for (int i = 0; i < 20; i++)
                players.Add(new Player());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Left)
                MoveTL(1,);
            if (e.KeyCode == Keys.Down)
                MoveTL(2);
            if (e.KeyCode == Keys.Up)
                MoveTL(3);
            if (e.KeyCode == Keys.Right)
                MoveTL(4);
                */
            Teach();
        }
    }

    public class Player
    {
        List<int> moves;
        public int score;
        public Player(List<int> a)
        {
            moves = a;
            int scoresum = 0;
            for (int i = 0; i < 10; i++)
                scoresum += GetScore();
            score = scoresum / 10;
        }

        public Player()
        {
            Random r = new Random();
            moves = new List<int>();
            for (int i = 0; i < 100; i++)
                moves.Add(r.Next(1, 5));
            int scoresum = 0;
            for (int i = 0; i < 10; i++)
                scoresum += GetScore();
            score = scoresum / 10;
        }

        public static Player Breed(Player p1, Player p2)
        {
            Random r = new Random();
            r.Next(1, 2);
            List<int> t = new List<int>();
            for (int i = 0; i < p1.moves.Count && i < p2.moves.Count; i++)
                t.Add(r.Next(1, 3) == 2 ? p1.moves[i] : p2.moves[i]);
            return new Player(t);
        }

        public void Mutate()
        {
            Random r = new Random();
            int i = moves.Count;
            while (i-- > 0)
                if (r.Next(1, 11) < 3)
                    moves[i] = r.Next(1, 5);
        }

        public int GetScore()
        {
            List<List<int>> fieldInt = new List<List<int>>();
            for (int cnt = 0; cnt < 4; cnt++)
            {
                fieldInt.Add(new List<int>());
                for (int cnt1 = 0; cnt1 < 4; cnt1++)
                    fieldInt[cnt].Add(0);
            }
            int score = 0;
            for (int i = 0; i < moves.Count; i++)
                try
                { fieldInt = Form1.MoveTL(moves[i], fieldInt, false); }
                catch { break; }
            for (int i = 0; i < 16; i++)
                score += fieldInt[i / 4][i % 4];
            return score;
        }

        public void Add()
        {
            Random r = new Random();
            List<int> ts = new List<int>();
            for (int i = 10; i > 0; i--)
                moves.Add(r.Next(1, 5));
        }
        
    }
}
