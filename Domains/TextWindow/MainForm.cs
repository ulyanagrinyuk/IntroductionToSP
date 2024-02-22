using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextWindow
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		Module DrawModule {  get; set; }
		object drawer;
		
		public MainForm()
		{
			InitializeComponent();
		}
		public MainForm(Module module, object targetWindow) : this()
		{
			InitializeComponent();
			DrawModule = module;
			drawer = targetWindow;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			DrawModule.GetType("TextdRAWER.MainForm").GetMethod("SetText").Invoke(drawer, new object[] {textBox1.Text});
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			DrawModule.GetType("TextDrawer.MainForm").GetMethod("Move").Invoke
				(
				drawer, 
				new object[] 
				{
					new Point(this.Location.X, this.Location.Y + this.Height),
					this.Width
				}
				);
		}
	}
}
