﻿using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Drawing;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;
using Utility;

namespace UIControl . Main
{
    public partial class ReportControl :UserControl1
    {
        public ReportControl ( )
        {
            InitializeComponent ( );
            DrawBtn ( );
        }

        void DrawBtn ( )
        {
            foreach ( Control control in this . Controls )
            {
                if ( control . GetType ( ) == typeof ( Button ) )
                    control . Paint += Control_Paint;
            }
        }

        private void Control_Paint ( object sender ,PaintEventArgs e )
        {
            DrawsHelper . Draw ( e . ClipRectangle ,e . Graphics ,18 ,false ,Color . DarkSeaGreen ,Color . LightGreen );

            base . OnPaint ( e );

            Graphics g = e . Graphics;

            Button btn = sender as Button;
            StringFormat strF = new StringFormat ( );
            strF . Alignment = StringAlignment . Center;
            strF . LineAlignment = StringAlignment . Center;
            g . DrawString ( btn . Text ,new Font ( "微软雅黑" ,14 ,FontStyle . Bold ) ,new SolidBrush ( Color . Black ) ,btn . ClientRectangle ,strF );
        }

        private void ReportControl_SizeChanged ( object sender ,EventArgs e )
        {
            DrawLocation ( );
        }
        void DrawLocation ( )
        {
            int x = this . Width / 2;
            int y = 20;
            labelControl1 . Location = new Point ( x - labelControl1 . Width / 2 ,y + labelControl1 . Height / 2 );
            button2 . Location = new Point ( x - button2 . Width / 2 ,y + labelControl1 . Height + 50 );
            button1 . Location = new Point ( x - button1 . Width / 2 ,y + labelControl1 . Height + button2 . Height + 90 );
            button3 . Location = new Point ( x - button3 . Width / 2 ,y + labelControl1 . Height + button2 . Height + button1 . Height + 90 + 45 );
        }

    }
}
