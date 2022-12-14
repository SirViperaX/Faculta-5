using System.Security.Cryptography.X509Certificates;

namespace Faculta_5
{
    public partial class mainForm : Form
    {
        ListBox listBox;
        int ncs;
        Random rnd = new Random();
        public mainForm()
        {
            InitializeComponent();

        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            inputForm myIF = new inputForm();
            myIF.ShowDialog();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox = new ListBox();
            listBox.Width = 300;
            listBox.Height = 500;
            listBox.Top = 50;
            listBox.Left = 12;
            listBox.Parent = this;
            listBox.Font = new Font("Arial", 20, FontStyle.Regular);
            Engine.h1 = Engine.Load(@"D:\VS Projects\Faculta 5\Resources\Help\h1.txt");
            Engine.h2 = Engine.Load(@"D:\VS Projects\Faculta 5\Resources\Help\h2.txt");
            Engine.h3 = Engine.Load(@"D:\VS Projects\Faculta 5\Resources\Help\h3.txt");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        


        public void Refresh()
        {
            int tmp = NumberTest();
            listBox.Items.Add(Engine.nu + " " + tmp);
            Engine.History.Add(new T(Engine.nu, tmp));
            textBox1.Text = Engine.nc.ToString();
        }

        public int Hint()
        {
            int pn;
            bool ok;
            do
            {
                pn = GenerateNumber4();
                ok = true;

                foreach (T t in Engine.History)
                {
                    if (NumberTest(pn, t.nr) != t.pow)
                    {
                        ok = false;
                        break;
                    }
                }
            } while (!ok);
            return pn;
        }

        public void NewGame()
        {
            Engine.nc = GenerateNumber4();
            Engine.History.Clear();
        }
        public int GenerateNumber1()
        {
            bool ok;
            int x;
            do
            {
                ok = true;
                x = rnd.Next(1000, 10000);
                int c1 = x % 10;
                int c2 = (x / 10) % 10;
                int c3 = (x / 100) % 10;
                int c4 = (x / 1000) % 10;
                if (c1 == c2 || c1 == c3 || c1 == c4 || c2 == c3 || c2 == c4 || c3 == c4)
                    ok = false;
            } while (!ok);
            return x;
        }
        public int GenerateNumber2()
        {
            int x;
            int c1 = rnd.Next(1,10);
            int c2, c3, c4;
            do
            {
                c2 = rnd.Next(10);
                
            }while(c1==c2);
            bool ok;
            do
            {
                ok = true;
                c3 = rnd.Next(10);
                if (c1 == c3 || c2 == c3)
                    ok = false;

            } while (!ok);
            do
            {
                ok = true;
                c4 = rnd.Next(10);
                if ( c1 == c4 || c2 == c4 || c3 == c4)
                    ok = false;

            } while (!ok);
            return c1 * 1000 + c2 * 100 + c3 * 10 + c4;

        }

        public int GenerateNumber3()
        {
            int[] v = new int[10];
            int x;
            do
            {
                for (int i = 0; i < 4; i++)
                {
                    v[i] = i + 1;
                }
                for (int i = 0; i < v.Length; i++)
                {
                    int t = rnd.Next(10);
                    int aux = v[i];
                    v[i] = v[t];
                    v[t] = aux;
                }
                x = 0;
                for (int i = 0; i < v.Length; i++)
                {
                    switch (v[i])
                    {
                        case 1:
                            x += i * 1000;
                            break;
                        case 2:
                            x += i * 100;
                            break;
                        case 3:
                            x += i * 10;
                            break;
                        case 4:
                            x += i;
                            break;
                    }
                }
            } while (x < 1000);
            return x;
        }

        public int GenerateNumber4()
        {
            List<int> intregi = new List<int>();
            for(int i =0; i < 10; i++)
            {
                intregi.Add(i);
            }
            int t = rnd.Next(1, 10);
            int x = intregi[t] * 1000;
            intregi.RemoveAt(t);
            t = rnd.Next(intregi.Count);
            x += intregi[t]*100;
            intregi.RemoveAt(t);
            t = rnd.Next(intregi.Count);
            x += intregi[t] * 10;
            intregi.RemoveAt(t);
            t = rnd.Next(intregi.Count);
            x += intregi[t];

            return x;
        }

        private int NumberTest(int x, int y)
        {
            int nc_copy = x;
            int[] nComputer = new int[4];
            for (int i = 0; i < 4; i++)
            {
                nComputer[i] = nc_copy % 10;
                nc_copy /= 10;
            }
            int nu_copy = y;
            int[] nUser = new int[4];
            for (int i = 0; i < 4; i++)
            {
                nUser[i] = nu_copy % 10;
                nu_copy /= 10;
            }
            int p = 0, c = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (nUser[i] == nComputer[j] && i == j)
                        c++;
                    if (nUser[i] == nComputer[j] && j != i)
                        p++;
                }
            }
            return 100 + (c * 10) + p;
        }
        private int NumberTest()
        {
            int nc_copy = Engine.nc;
            int[] nComputer = new int[4];
            for(int i =0;i<4;i++)
            {
                nComputer[i] = nc_copy % 10;
                nc_copy /= 10;
            }
            int nu_copy = Engine.nu;
            int[] nUser = new int[4];
            for (int i = 0; i < 4; i++)
            {
                nUser[i] = nu_copy % 10;
                nu_copy /= 10;
            }
            int p = 0, c = 0;
            for(int i = 0;i<4;i++)
            {
                for(int j=0; j<4;j++)
                {
                    if (nUser[i] == nComputer[j] && i == j)
                        c++;
                    if (nUser[i] == nComputer[j] && j != i)
                        p++;
                }
            }
            return 100 + (c * 10) + p ;
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void hintButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = Hint().ToString();
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            HelpForm myIF = new HelpForm();
            myIF.ShowDialog();
        }
    }
}