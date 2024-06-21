using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Fighter
{
    public partial class Form1 : Form
    {
        const int MAXENEMY = 20;
        const int MAXBALL = 20;
        const int MAXBULLET = 5;
        const int BALLSPEED = 3;
        const int BULSPEED = 7;
        const int BULGAP = 40;
        const int FIGHTERSPEED = 10;

        struct tag_Enemy
        {
	        public bool exist;
            public int Type;
            public int x, y;
            public int Delta;
            public int Speed;
        } 
        tag_Enemy[] Enemy = new tag_Enemy[MAXENEMY];

        struct tag_Ball
        {
            public bool exist;
            public int x, y;
        } 
        tag_Ball[] Ball = new tag_Ball[MAXBALL];
        
        struct tag_Bullet
        {
            public bool exist;
            public int x, y;
        } 
        tag_Bullet[] Bullet = new tag_Bullet[MAXBULLET];

        int fx;
        const int fy = 420;
        Bitmap hFighter, hFBullet, hEBullet;
        Bitmap[] hEnemy = new Bitmap[3];
        Bitmap hBit = new Bitmap(640, 480);

        const int fw = 30;
        const int fh = 32;
        const int bw = 6;
        const int bh = 12;
        int ew(int t) { return t == 0 ? 30 : 26; }
        int eh(int t) { return t == 0 ? 32 : 20; }
        Random R = new Random();

        [DllImport("User32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(640, 480);
            hFighter = Properties.Resources.fighter;
            hEnemy[0] = Properties.Resources.enemy1;
            hEnemy[1] = Properties.Resources.enemy2;
            hEnemy[2] = Properties.Resources.enemy3;
            hFBullet = Properties.Resources.fbullet;
            hEBullet = Properties.Resources.ebullet;

            StartGame();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    StartGame();
                    break;
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (hBit != null)
            {
                e.Graphics.DrawImage(hBit, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 배경색으로 지우지 않음
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i, j, maxy;
            tag_Enemy en;
            tag_Bullet b;
            int w, h;
            Rectangle enemyrt, bulrt, ballrt, frt, irt;

            // 더블 버퍼링 준비
            Graphics G = Graphics.FromImage(hBit);
            G.Clear(Color.Black);

            // 키보드 입력 처리. GetKeyState Win32 함수 사용
            if (GetKeyState((int)Keys.Left) < 0)
            {
                fx -= FIGHTERSPEED;
                fx = Math.Max(fw / 2, fx);
            }
            if (GetKeyState((int)Keys.Right) < 0)
            {
                fx += FIGHTERSPEED;
                fx = Math.Min(ClientSize.Width - fw / 2, fx);
            }
            if (GetKeyState((int)Keys.Space) < 0)
            {
                for (i = 0, maxy = -1; i < MAXBULLET; i++)
                {
                    if (Bullet[i].exist == true) maxy = Math.Max(Bullet[i].y, maxy);
                }
                for (i = 0; i < MAXBULLET; i++)
                {
                    if (Bullet[i].exist == false) break;
                }
                if (i != MAXBULLET && fy - maxy > BULGAP)
                {
                    Bullet[i].exist = true;
                    Bullet[i].x = fx;
                    Bullet[i].y = fy - bh;
                }
            }

            // 총알 이동 및 출력
            for (i = 0; i < MAXBULLET; i++)
            {
                if (Bullet[i].exist == false) continue;
                if (Bullet[i].y > 0)
                {
                    Bullet[i].y -= BULSPEED;
                    G.DrawImage(hFBullet, Bullet[i].x - bw / 2, Bullet[i].y);
                }
                else
                {
                    Bullet[i].exist = false;
                }
            }

            // 파이터 출력
            G.DrawImage(hFighter, fx - fw / 2, fy);

            // 적군 생성
            if (R.Next(20) == 0)
            {
                for (i = 0; i < MAXENEMY && Enemy[i].exist == true; i++) { ; }
                if (i != MAXENEMY)
                {
                    Enemy[i].Type = R.Next(3);
                    if (R.Next(2) == 1)
                    {
                        Enemy[i].x = ew(0) / 2;
                        Enemy[i].Delta = 1;
                    }
                    else
                    {
                        Enemy[i].x = ClientSize.Width - ew(0) / 2;
                        Enemy[i].Delta = -1;
                    }
                    Enemy[i].y = R.Next(200) + 50;
                    Enemy[i].Speed = R.Next(4) + 3;
                    Enemy[i].exist = true;
                }
            }

            // 적군 이동 및 그림. 적군 총알 발사
            for (i = 0; i < MAXENEMY; i++)
            {
                if (Enemy[i].exist == false) continue;
                Enemy[i].x += Enemy[i].Speed * Enemy[i].Delta;
                if (Enemy[i].x < 0 || Enemy[i].x > ClientSize.Width)
                {
                    Enemy[i].exist = false;
                }
                else
                {
                    G.DrawImage(hEnemy[Enemy[i].Type], Enemy[i].x - 
                        ew(Enemy[i].Type) / 2, Enemy[i].y);
                }
                if (R.Next(40) == 0)
                {
                    for (j = 0; j < MAXBALL && Ball[j].exist == true; j++) { ;}
                    if (j != MAXBALL)
                    {
                        Ball[j].x = Enemy[i].x + ew(Enemy[i].Type) / 2;
                        Ball[j].y = Enemy[i].y;
                        Ball[j].exist = true;
                    }
                }
            }

            // 적군 총알 이동 및 그림
            for (i = 0; i < MAXBALL; i++)
            {
                if (Ball[i].exist == false) continue;
                if (Ball[i].y < fy)
                {
                    Ball[i].y += BALLSPEED;
                    G.DrawImage(hEBullet, Ball[i].x - 3, Ball[i].y);
                }
                else
                {
                    Ball[i].exist = false;
                }
            }

            // 적군과 총알의 충돌 판정
            for (i = 0; i < MAXENEMY; i++)
            {
                if (Enemy[i].exist == false) continue;
                en = Enemy[i];
                w = ew(Enemy[i].Type);
                h = eh(Enemy[i].Type);
                enemyrt = new Rectangle(en.x - w / 2, en.y, w, h);
                for (j = 0; j < MAXBULLET; j++)
                {
                    if (Bullet[j].exist == false) continue;
                    b = Bullet[j];
                    bulrt = new Rectangle(b.x - bw / 2, b.y, bw, bh);
                    irt = Rectangle.Intersect(enemyrt, bulrt);
                    if (irt.IsEmpty == false)
                    {
                        Enemy[i].exist = false;
                        Bullet[j].exist = false;
                    }
                }
            }
            
            // 적군 총알과 아군의 충돌 판정
            frt = new Rectangle(fx - fw / 2, fy, fw, fh);
            for (i = 0; i < MAXBALL; i++)
            {
                if (Ball[i].exist == false) continue;
                ballrt = new Rectangle(Ball[i].x - bw / 2, Ball[i].y, bw, bh);
                irt = Rectangle.Intersect(frt, ballrt);
                if (irt.IsEmpty == false)
                {
                    timer1.Stop();
                }
            }

            Invalidate();
        }

        void StartGame()
        {
            fx = 320;
            for (int i = 0; i < MAXBULLET; i++) Bullet[i].exist = false;
            for (int i = 0; i < MAXBALL; i++) Ball[i].exist = false;
            for (int i = 0; i < MAXENEMY; i++) Enemy[i].exist = false;
            timer1.Start();
        }
    }
}