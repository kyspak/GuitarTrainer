using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;
using System.Data.SqlClient;
namespace GT
{

    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        MediaPlayer Amch = new MediaPlayer();
        MediaPlayer Ach = new MediaPlayer();
        MediaPlayer Emch = new MediaPlayer();
        MediaPlayer Ech = new MediaPlayer();
        MediaPlayer Pachka = new MediaPlayer();
        int[,] A = new int[3, 2] { { 1, 1 }, { 1, 2 }, { 1, 3 } };
        int[,] Am = new int[3, 2] { { 0, 1 }, { 1, 2 }, { 1, 3 } };
        int[,] Em = new int[2, 2] { { 1, 3 }, { 1, 4 } };
        int[,] E = new int[3, 2] { { 1, 3 }, { 1, 4 }, { 0, 2 } };
        int[,] C = new int[3, 2] { { 2, 4 }, { 1, 3 }, { 0, 1 } };
        int[,] D = new int[3, 2] { { 1, 2 }, { 2, 1 }, { 1, 0 } };
        string Buttonname;
        string b;
        string Chord;
        Image[,] img = new Image[19, 6];
        Button[,] but = new Button[18, 6];
        int n, m;
        int count = 0;
        bool check(int[,] Chord, int n, int m)
        {
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j - 1 == 0)
                    {
                        if (img[Chord[i, j - 1], Chord[i, j]].Visibility != Visibility.Visible)
                        {
                            k++;
                            break;
                        }

                    }
                }
            }
            if (k > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void PlayChord()
        {
            if (count == 0)
            {
                Ach.Position = new TimeSpan(0, 0, 0);
                Ach.Play();
            }
            else if (count == 1)
            {
                Ech.Position = new TimeSpan(0, 0, 0);
                Ech.Play();
            }
            else if (count == 2)
            {
                Amch.Position = new TimeSpan(0, 0, 0);
                Amch.Play();
            }
            else if (count == 3)
            {
                Emch.Position = new TimeSpan(0, 0, 0);
                Emch.Play();
            }
        }
        void Ques()
        {
            if (Chordshow.Content.ToString() == "A" && count == 1)
            {
                if (check(A, 3, 2) == false)
                {
                    MessageBox.Show("mistake");
                    count = count - 1;

                }
                else
                {
                    Chordshow.Content = "E";
                    PlayChord();
                }
            }
            if (Chordshow.Content.ToString() == "E" && count == 2)
            {
                if (check(E, 3, 2) == false)
                {
                    MessageBox.Show("mistake");
                    count = count - 1;

                }
                else
                {
                    Chordshow.Content = "Am";
                    PlayChord();
                }
            }
            if (Chordshow.Content.ToString() == "Am" && count == 3)
            {
                if (check(Am, 3, 2) == false)
                {
                    MessageBox.Show("mistake");
                    count = count - 1;
                }
                else
                {
                    Chordshow.Content = "Em";
                    PlayChord();
                }
            }
            if (Chordshow.Content.ToString() == "Em" && count == 4)
            {
                if (check(Em, 2, 2) == false)
                {
                    MessageBox.Show("mistake");
                    count = count - 1;
                }
                else
                {
                    MessageBox.Show("Succes", "Вы правильно ответили на все вопросы");
                    Chordshow.Content = "A";
                    count = 0;
                    PlayChord();
                }
            }
        }
        void devis()//Выключение видимости точек
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    img[i, j].Visibility = Visibility.Hidden;
                }

            }
        }
        void blockbutton()//выключение всех кнопок
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    but[i, j].IsEnabled = false;
                }
            }
        }
        void unblockbutton()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    but[i, j].IsEnabled = true;
                }
            }
        }
        void Showimg(string name)
        {

            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 6; j++)
                {

                    b = but[i, j].Name;
                    if (Buttonname == b)
                    {
                        n = i;
                        m = j;
                        break;
                    }

                }

            }

            if (n > 15)
            {
                for (int i = 15; i < 18; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (i == n && j == m)
                        {
                            continue;
                        }
                        else
                        {
                            but[i, j].IsEnabled = false;
                        }

                    }

                }
                if (img[n, m].Visibility == Visibility.Visible)
                {
                    for (int i = 15; i < 18; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            but[i, j].IsEnabled = true;
                        }

                    }
                }
            }

            if (img[n, m].Visibility == Visibility.Hidden)
            {
                img[n, m].Visibility = Visibility.Visible;
            }
            else
            {
                img[n, m].Visibility = Visibility.Hidden;
            }
        }

        public Main()
        {
            InitializeComponent();

        }
        protected override void OnContentRendered(EventArgs e)
        {
            MainWindow wind = new MainWindow();
            wind.Close();
            img[0, 0] = _11;
            img[0, 1] = _21;
            img[0, 2] = _31;
            img[0, 3] = _41;
            img[0, 4] = _51;
            img[0, 5] = _61;
            img[1, 0] = _12;
            img[1, 1] = _22;
            img[1, 2] = _32;
            img[1, 3] = _42;
            img[1, 4] = _52;
            img[1, 5] = _62;
            img[2, 0] = _13;
            img[2, 1] = _23;
            img[2, 2] = _33;
            img[2, 3] = _43;
            img[2, 4] = _53;
            img[2, 5] = _63;
            img[3, 0] = _1_Copy16;
            img[3, 1] = _1_Copy15;
            img[3, 2] = _1_Copy14;
            img[3, 3] = _1_Copy13;
            img[3, 4] = _1_Copy12;
            img[3, 5] = _1_Copy11;
            img[4, 0] = _1_Copy22;
            img[4, 1] = _1_Copy21;
            img[4, 2] = _1_Copy20;
            img[4, 3] = _1_Copy19;
            img[4, 4] = _1_Copy18;
            img[4, 5] = _1_Copy17;
            img[5, 0] = _1_Copy28;
            img[5, 1] = _1_Copy27;
            img[5, 2] = _1_Copy26;
            img[5, 3] = _1_Copy25;
            img[5, 4] = _1_Copy24;
            img[5, 5] = _1_Copy23;
            img[6, 0] = _1_Copy5;
            img[6, 1] = _1_Copy4;
            img[6, 2] = _1_Copy3;
            img[6, 3] = _1_Copy2;
            img[6, 4] = _1_Copy1;
            img[6, 5] = _1_Copy;
            img[7, 0] = _1_Copy30;
            img[7, 1] = _1_Copy29;
            img[7, 2] = _1_Copy9;
            img[7, 3] = _1_Copy8;
            img[7, 4] = _1_Copy7;
            img[7, 5] = _1_Copy6;
            img[8, 0] = _1_Copy36;
            img[8, 1] = _1_Copy35;
            img[8, 2] = _1_Copy34;
            img[8, 3] = _1_Copy33;
            img[8, 4] = _1_Copy32;
            img[8, 5] = _1_Copy31;
            img[9, 0] = _1_Copy42;
            img[9, 1] = _1_Copy41;
            img[9, 2] = _1_Copy40;
            img[9, 3] = _1_Copy39;
            img[9, 4] = _1_Copy38;
            img[9, 5] = _1_Copy37;
            img[10, 0] = _1_Copy48;
            img[10, 1] = _1_Copy47;
            img[10, 2] = _1_Copy46;
            img[10, 3] = _1_Copy45;
            img[10, 4] = _1_Copy44;
            img[10, 5] = _1_Copy43;
            img[11, 0] = _1_Copy54;
            img[11, 1] = _1_Copy53;
            img[11, 2] = _1_Copy52;
            img[11, 3] = _1_Copy51;
            img[11, 4] = _1_Copy50;
            img[11, 5] = _1_Copy49;
            img[12, 0] = _1_Copy60;
            img[12, 1] = _1_Copy59;
            img[12, 2] = _1_Copy58;
            img[12, 3] = _1_Copy57;
            img[12, 4] = _1_Copy56;
            img[12, 5] = _1_Copy55;
            img[13, 0] = _1_Copy66;
            img[13, 1] = _1_Copy65;
            img[13, 2] = _1_Copy64;
            img[13, 3] = _1_Copy63;
            img[13, 4] = _1_Copy62;
            img[13, 5] = _1_Copy61;
            img[14, 0] = _1_Copy72;
            img[14, 1] = _1_Copy71;
            img[14, 2] = _1_Copy70;
            img[14, 3] = _1_Copy69;
            img[14, 4] = _1_Copy68;
            img[14, 5] = _1_Copy67;
            img[15, 0] = _1_Copy78;
            img[15, 1] = _1_Copy77;
            img[15, 2] = _1_Copy76;
            img[15, 3] = _1_Copy75;
            img[15, 4] = _1_Copy74;
            img[15, 5] = _1_Copy73;
            img[16, 0] = barr1;
            img[16, 1] = barr2;
            img[16, 2] = barr3;
            img[16, 3] = barr4;
            img[16, 4] = barr5;
            img[16, 5] = barr6;
            img[17, 0] = barr7;
            img[17, 1] = barr8;
            img[17, 2] = barr9;
            img[17, 3] = barr10;
            img[17, 4] = barr11;
            img[17, 5] = barr12;
            img[18, 0] = cl1;
            img[18, 1] = cl2;
            img[18, 2] = cl3;
            img[18, 3] = cl4;
            img[18, 4] = cl5;
            img[18, 5] = cl6;



            but[0, 0] = b11;
            but[0, 1] = b12;
            but[0, 2] = b13;
            but[0, 3] = b14;
            but[0, 4] = b15;
            but[0, 5] = b16;
            but[1, 0] = b21;
            but[1, 1] = b22;
            but[1, 2] = b23;
            but[1, 3] = b24;
            but[1, 4] = b25;
            but[1, 5] = b26;
            but[2, 0] = b31;
            but[2, 1] = b32;
            but[2, 2] = b33;
            but[2, 3] = b34;
            but[2, 4] = b35;
            but[2, 5] = b36;
            but[3, 0] = b41;
            but[3, 1] = b42;
            but[3, 2] = b43;
            but[3, 3] = b44;
            but[3, 4] = b45;
            but[3, 5] = b46;
            but[4, 0] = b51;
            but[4, 1] = b52;
            but[4, 2] = b53;
            but[4, 3] = b54;
            but[4, 4] = b55;
            but[4, 5] = b56;
            but[5, 0] = b51_Copy;
            but[5, 1] = b52_Copy;
            but[5, 2] = b53_Copy;
            but[5, 3] = b54_Copy;
            but[5, 4] = b55_Copy;
            but[5, 5] = b56_Copy;
            but[6, 0] = b51_Copy1;
            but[6, 1] = b52_Copy1;
            but[6, 2] = b53_Copy1;
            but[6, 3] = b54_Copy1;
            but[6, 4] = b55_Copy1;
            but[6, 5] = b56_Copy1;
            but[7, 0] = b51_Copy2;
            but[7, 1] = b52_Copy2;
            but[7, 2] = b53_Copy2;
            but[7, 3] = b54_Copy2;
            but[7, 4] = b55_Copy2;
            but[7, 5] = b56_Copy2;
            but[8, 0] = b51_Copy3;
            but[8, 1] = b52_Copy3;
            but[8, 2] = b53_Copy3;
            but[8, 3] = b54_Copy3;
            but[8, 4] = b55_Copy3;
            but[8, 5] = b56_Copy3;
            but[9, 0] = b51_Copy4;
            but[9, 1] = b52_Copy4;
            but[9, 2] = b53_Copy4;
            but[9, 3] = b54_Copy4;
            but[9, 4] = b55_Copy4;
            but[9, 5] = b56_Copy4;
            but[10, 0] = b51_Copy5;
            but[10, 1] = b52_Copy5;
            but[10, 2] = b53_Copy5;
            but[10, 3] = b54_Copy5;
            but[10, 4] = b55_Copy5;
            but[10, 5] = b56_Copy5;
            but[11, 0] = b51_Copy6;
            but[11, 1] = b52_Copy6;
            but[11, 2] = b53_Copy6;
            but[11, 3] = b54_Copy6;
            but[11, 4] = b55_Copy6;
            but[11, 5] = b56_Copy6;
            but[12, 0] = b51_Copy7;
            but[12, 1] = b52_Copy7;
            but[12, 2] = b53_Copy7;
            but[12, 3] = b54_Copy7;
            but[12, 4] = b55_Copy7;
            but[12, 5] = b56_Copy7;
            but[13, 0] = b51_Copy8;
            but[13, 1] = b52_Copy8;
            but[13, 2] = b53_Copy8;
            but[13, 3] = b54_Copy8;
            but[13, 4] = b55_Copy8;
            but[13, 5] = b56_Copy8;
            but[14, 0] = b51_Copy9;
            but[14, 1] = b52_Copy9;
            but[14, 2] = b53_Copy9;
            but[14, 3] = b54_Copy9;
            but[14, 4] = b55_Copy9;
            but[14, 5] = b56_Copy9;
            but[15, 0] = b51_Copy10;
            but[15, 1] = b52_Copy10;
            but[15, 2] = b53_Copy10;
            but[15, 3] = b54_Copy10;
            but[15, 4] = b55_Copy10;
            but[15, 5] = b56_Copy10;
            but[16, 0] = br1;
            but[16, 1] = br2;
            but[16, 2] = br3;
            but[16, 3] = br4;
            but[16, 4] = br5;
            but[16, 5] = br6;
            but[17, 0] = br7;
            but[17, 1] = br8;
            but[17, 2] = br9;
            but[17, 3] = br10;
            but[17, 4] = br11;
            but[17, 5] = br12;
            devis();
            blockbutton();
            stm.IsEnabled = false;
            Chordshow.Content = "A";
            Chordshow.Visibility = Visibility.Hidden;
            CheckChord1.Visibility = Visibility.Hidden;
            Ach.Open(new Uri("C:\\GT\\GT\\A.mp3", UriKind.RelativeOrAbsolute));
            Amch.Open(new Uri("C:\\GT\\GT\\Am.mp3", UriKind.RelativeOrAbsolute));
            Ech.Open(new Uri("C:\\GT\\GT\\E.mp3", UriKind.RelativeOrAbsolute));
            Emch.Open(new Uri("C:\\GT\\GT\\Em.mp3", UriKind.RelativeOrAbsolute));
            Pachka.Open(new Uri("C:\\GT\\GT\\Pachka.mp3", UriKind.RelativeOrAbsolute));
            PlayChord1.IsEnabled = false;
            musstop.IsEnabled = false;
        }
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerpachka = new DispatcherTimer();
        protected void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (tm.IsEnabled == false)
            {
                PlayChord1.IsEnabled = true;
            }
            else
            {
                show.IsEnabled = true;
            }
            CheckChord1.IsEnabled = true;
            timer.Stop();
        }

        void ShowChord(int[,] Chord, int n, int m)
        {
            devis();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j - 1 == 0)
                    {
                        img[Chord[i, j - 1], Chord[i, j]].Visibility = Visibility.Visible;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Owner.Show();
            this.Close();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Chord = CheckChord.SelectedItem.ToString();
            show.IsEnabled = false;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Start();
            if (Chord == "System.Windows.Controls.ComboBoxItem: Em")
            {
                ShowChord(Em, 2, 2);
                Emch.Position = new TimeSpan(0, 0, 0);
                Emch.Play();
            }
            else if (Chord == "System.Windows.Controls.ComboBoxItem: Am")
            {
                ShowChord(Am, 3, 2);
                Amch.Position = new TimeSpan(0, 0, 0);
                Amch.Play();
            }
            else if (Chord == "System.Windows.Controls.ComboBoxItem: A")
            {
                ShowChord(A, 3, 2);
                Ach.Position = new TimeSpan(0, 0, 0);
                Ach.Play();
            }
            else if (Chord == "System.Windows.Controls.ComboBoxItem: E")
            {
                ShowChord(E, 3, 2);
                Ech.Position = new TimeSpan(0, 0, 0);
                Ech.Play();
            }
        }

        private void tm_Click(object sender, RoutedEventArgs e)
        {
            devis();
            unblockbutton();
            tm.IsEnabled = false;
            stm.IsEnabled = true;
            show.IsEnabled = false;
            CheckChord.IsEnabled = false;
            if (CheckSound.IsChecked == false)
            {
                Chordshow.Visibility = Visibility.Visible;
            }
            CheckChord1.Visibility = Visibility.Visible;
            PlayChord();

            CheckChord1.IsEnabled = false;
            PlayChord1.IsEnabled = false;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Start();
            musplay.IsEnabled = false;
        }


        private void stm_Click(object sender, RoutedEventArgs e)
        {
            devis();
            blockbutton();
            tm.IsEnabled = true;
            stm.IsEnabled = false;
            show.IsEnabled = true;
            CheckChord.IsEnabled = true;
            Chordshow.Visibility = Visibility.Hidden;
            PlayChord1.IsEnabled = false;
            CheckChord1.Visibility = Visibility.Hidden;
            musplay.IsEnabled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            count++;
            Ques();
            //PlayChord();
            devis();
            CheckChord1.IsEnabled = false;
            PlayChord1.IsEnabled = false;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Start();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PlayChord();
            CheckChord1.IsEnabled = false;
            PlayChord1.IsEnabled = false;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Start();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            chord_count = 0;
            Pachka.Play();
            timerpachka.Interval = TimeSpan.FromSeconds(1);
            timerpachka.Tick += new EventHandler(Chord_tick);
            timerpachka.Start();
            tm.IsEnabled = false;
            show.IsEnabled = false;
            CheckChord.IsEnabled = false;
            CheckSound.IsEnabled = false;
            musplay.IsEnabled = false;
            musstop.IsEnabled = true;
        }
        int chord_count;
        int[,] temp = new int[0,0];
        int counter = 0;
        private void Chord_change()
        {
            if (chord_count == 0)
            {
                counter++;
                temp = Em;
                ShowChord(temp, temp.GetLength(0), temp.GetLength(1));
            }
            else if (chord_count == 1)
            {
                temp = Am;
                ShowChord(temp, temp.GetLength(0), temp.GetLength(1));
            }
            else if (chord_count == 2)
            {
                temp = C;
                ShowChord(temp, temp.GetLength(0), temp.GetLength(1));
            }
            else if (chord_count == 3)
            {
                timerpachka.Interval = TimeSpan.FromSeconds(1.27);
                temp = D;
                ShowChord(temp, temp.GetLength(0), temp.GetLength(1));
                chord_count = -1;
            }

        }
        private void Chord_tick(object sender, EventArgs e)
        {
            timerpachka.Interval = TimeSpan.FromSeconds(1.28);
            if (counter % 10 == 5)
            {
                timerpachka.Interval = TimeSpan.FromSeconds(1.27);
            }
            Chord_change();
            chord_count++;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Pachka.Stop();
            timerpachka.Stop();
            timerpachka.Tick -= new EventHandler(Chord_tick);
            devis();
           //counter = 0;
            temp = Em;
            chord_count = -1;
            tm.IsEnabled = true;
            show.IsEnabled = true;
            CheckChord.IsEnabled = true;
            CheckSound.IsEnabled = true;
            musplay.IsEnabled = true;
            musstop.IsEnabled = false;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Window1 wind1 = new Window1();
            wind1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Buttonname = (sender as Button).Name;
            Showimg(Buttonname);
            
        }
    }
}
