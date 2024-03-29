﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDrawer
{
	public partial class MainForm : Form
	{
		string text = "Nothing here yet";
		Font font;
		public MainForm()
		{
			InitializeComponent();
			font = new Font("Arial", 48);

			panel1.Paint += Panel1_Paint;
		}
		private void Panel1_Paint(object sender, PaintEventArgs e)
		{
			if(text.Length > 0)
			{
				Image img = new Bitmap(panel1.ClientRectangle.Width, panel1.ClientRectangle.Height);
				Graphics imgDC = Graphics.FromImage(img);
				imgDC.Clear(Color.LightBlue);
				imgDC.DrawString(text, font, Brushes.Black, ClientRectangle, new StringFormat(StringFormatFlags.NoFontFallback));
				e.Graphics.DrawImage(img, 0, 0);
			}
		}
		private void MainForm_Paint(object sender, PaintEventArgs e)
		{
			Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
		}

		private void fontToolStripMenuItemFont_Click(object sender, EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();
			fontDialog.Font = this.font;
			if (fontDialog.ShowDialog() == DialogResult.OK) this.font = fontDialog.Font;
			Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
		}
		public void SetText(string text)
		{
			this.text = text;
			Panel1_Paint(panel1, new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle));
		}
		public void Move(Point newLocation, int width)
		{
			this.Location = newLocation;
			this.Width = width;
		}
	}
}
