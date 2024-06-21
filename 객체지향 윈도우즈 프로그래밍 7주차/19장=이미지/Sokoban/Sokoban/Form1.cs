using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class Form1 : Form
    {
        const int MAXSTAGE = 3;
        const int BW = 32;
        const int BH = 32;

        StringBuilder [] ns = new StringBuilder[18];
        int nStage;
        int nx, ny;
        int nMove;
        Bitmap[] hBit = new Bitmap[5];

        string[,] arStage = {
            {
            "####################",
            "####################",
            "####################",
            "#####   ############",
            "#####O  ############",
            "#####  O############",
            "###  O O ###########",
            "### # ## ###########",
            "#   # ## #####  ..##",
            "# O  O   @      ..##",
            "##### ### # ##  ..##",
            "#####     ##########",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################"
            },
            {
            "####################",
            "####################",
            "####################",
            "####################",
            "####..  #     ######",
            "####..  # O  O  ####",
            "####..  #O####  ####",
            "####..    @ ##  ####",
            "####..  # #  O #####",
            "######### ##O O ####",
            "###### O  O O O ####",
            "######    #     ####",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################"
            },
            {
            "####################",
            "####################",
            "####################",
            "####################",
            "##########     @####",
            "########## O#O #####",
            "########## O  O#####",
            "###########O O #####",
            "########## O # #####",
            "##....  ## O  O  ###",
            "###...    O  O   ###",
            "##....  ############",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################",
            "####################"
            },
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(900, BH * 18);
            hBit[0] = Properties.Resources.wall;
            hBit[1] = Properties.Resources.pack;
            hBit[2] = Properties.Resources.target;
            hBit[3] = Properties.Resources.empty;
            hBit[4] = Properties.Resources.man;
            nStage = 0;

            InitStage();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int x, y;
            int iBit = 0;

            for (y = 0; y < 18; y++)
            {
                for (x = 0; x < 20; x++)
                {
                    switch (ns[y][x])
                    {
                        case '#':
                            iBit = 0;
                            break;
                        case 'O':
                            iBit = 1;
                            break;
                        case '.':
                            iBit = 2;
                            break;
                        case ' ':
                            iBit = 3;
                            break;
                    }
                    e.Graphics.DrawImage(hBit[iBit], x * BW, y * BH);
                }
            }
            e.Graphics.DrawImage(hBit[4], nx * BW, ny * BH);

            e.Graphics.DrawString("SOKOBAN", Font, Brushes.Black, 700, 10);
            e.Graphics.DrawString("Q:종료, R:다시 시작", Font, Brushes.Black, 700, 30);
            e.Graphics.DrawString("N:다음, P:이전", Font, Brushes.Black, 700, 50);
            e.Graphics.DrawString("스테이지 : " + (nStage + 1), Font, Brushes.Black, 700, 70);
            e.Graphics.DrawString("이동 회수 : " + nMove, Font, Brushes.Black, 700, 90);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    MoveMan(e.KeyCode);
                    if (TestEnd() == true)
                    {
                        MessageBox.Show((nStage + 1) + " 스테이지를 풀었습니다." +
                            "다음 스테이지로 이동합니다");
                        goto case Keys.N;
                    }
                    break;
                case Keys.Q:
                    Close();
                    break;
                case Keys.R:
                    InitStage();
                    break;
                case Keys.N:
                    if (nStage < MAXSTAGE - 1)
                    {
                        nStage++;
                        InitStage();
                    }
                    break;
                case Keys.P:
                    if (nStage > 0)
                    {
                        nStage--;
                        InitStage();
                    }
                    break;
            }
        }

        private void InitStage()
        {
            int x, y;

            for (y = 0; y < 18; y++)
            {
                ns[y] = new StringBuilder(arStage[nStage, y]);
            }
 
            for (y = 0; y < 18; y++)
            {
                for (x = 0; x < 20; x++)
                {
                    if (ns[y][x] == '@')
                    {
                        nx = x;
                        ny = y;
                        ns[y][x] = ' ';
                    }
                }
            }
            nMove = 0;
            Invalidate();
        }

        private void MoveMan(Keys dir)
        {
            int dx = 0, dy = 0;

            switch (dir)
            {
                case Keys.Left:
                    dx = -1;
                    break;
                case Keys.Right:
                    dx = 1;
                    break;
                case Keys.Up:
                    dy = -1;
                    break;
                case Keys.Down:
                    dy = 1;
                    break;
            }

            if (ns[ny + dy][nx + dx] != '#')
            {
                if (ns[ny + dy][nx + dx] == 'O')
                {
                    if (ns[ny + dy * 2][nx + dx * 2] == ' ' || ns[ny + dy * 2][nx + dx * 2] == '.')
                    {
                        if (arStage[nStage,ny + dy][nx + dx] == '.')
                        {
                            ns[ny + dy][nx + dx] = '.';
                        }
                        else
                        {
                            ns[ny + dy][nx + dx] = ' ';
                        }
                        ns[ny + dy * 2][nx + dx * 2] = 'O';
                    }
                    else
                    {
                        return;
                    }
                }
                nx += dx;
                ny += dy;
                nMove++;
                Invalidate();
            }
        }
    
        private bool TestEnd()
        {
            int x, y;

            for (y = 0; y < 18; y++)
            {
                for (x = 0; x < 20; x++)
                {
                    if (arStage[nStage, y][x] == '.' && ns[y][x] != 'O')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}