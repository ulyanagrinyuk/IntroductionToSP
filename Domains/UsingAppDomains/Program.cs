﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace UsingAppDomains
{
	internal static class Program
	{
		static AppDomain drawer;
		static AppDomain textWindow;

		static Assembly drawerAsm;
		static Assembly textWindowAsm;

		static Form DrawerForm;
		static Form TextWindowForm;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		[LoaderOptimization(LoaderOptimization.MultiDomain)]
		static void Main()
		{
			Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Form1());
			drawer = AppDomain.CreateDomain("Drawer");
			textWindow = AppDomain.CreateDomain("TextWindow");
			drawerAsm = drawer.Load(AssemblyName.GetAssemblyName("TextDrawer.exe"));
			textWindowAsm = textWindow.Load(AssemblyName.GetAssemblyName("TextName"));
			
			DrawerForm =  Activator.CreateInstance(drawerAsm.GetType("TextDrawer.exe")) as Form;
			TextWindowForm = Activator.CreateInstance
				(
				textWindowAsm.GetType("TextWindow.MainForm"),
				new object[]
				{
					drawerAsm.GetModule("TextDrawer.exe"), DrawerForm
				}
				) as Form;
				(new Thread(new ThreadStart(RunVisualzer))).Start();
				(new Thread(new ThreadStart(RunDrawer))).Start();

			drawer.DomainUnload += new EventHandler(Drawer_DomainUnload);
		}
		static void Drawer_DomainUnload(object sender, EventArgs e)
		{
			MessageBox.Show($"Domain {(sender as AppDomain).FriendlyName} has been unloaded");

		}
		static void RunDrawer()
		{
			DrawerForm.ShowDialog();
			AppDomain.Unload(drawer);
		}
		static void RunVisualzer()
		{
			TextWindowForm.ShowDialog();
			Application.Exit();
		}
	}
}
