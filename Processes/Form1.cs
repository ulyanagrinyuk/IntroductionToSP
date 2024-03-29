﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Processes
{
	public partial class Form1 : Form
	{
		List<Process> process_list;
		public Form1()
		{
			InitializeComponent();
			process_list = new List<Process>();
			richTextBoxProcessName.Text = "notepad";
			//InitProcess();
			lvProcesses.Columns.Add("PID");
			lvProcesses.Columns.Add("Name");
		}
		//void Form1_Closing(object sender, CancelEventArgs e)
		//{
		//	myProcess.CloseMainWindow();
		//	myProcess.Close();
		//}
		void InitProcess()
		{
			AllignText();
			myProcess = new Process();
			myProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(richTextBoxProcessName.Text);
			myProcess.Start();
			//myProcess = new System.Diagnostics.Process(richTextBoxProcessName.Text);
			//process_list.Add(myProcess);
			lvProcesses.Items.Add(myProcess.Id.ToString());
			lvProcesses.Items[lvProcesses.Items.Count - 1].SubItems.Add(myProcess.ProcessName);
		}
		void AllignText()
		{
			richTextBoxProcessName.SelectAll();
			richTextBoxProcessName.SelectionAlignment = HorizontalAlignment.Center;
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			InitProcess();
			//myProcess.Start();
			//Info();
			//this.TopMost = true;

		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			//if (process_list.Count > 0)
			if(lvProcesses.Items.Count > 0)
			{
				try
				{
					//myProcess = process_list.Last();
					myProcess = Process.GetProcessById(Convert.ToInt32(lvProcesses.Items[lvProcesses.Items.Count - 1].Text));
					myProcess.CloseMainWindow();              //закрывает процесс
					myProcess.Close();                       //освобождает ресурсы, занимаемые процессом
															//process_list.RemoveAt(process_list.Count - 1);
													     	//lvProcesses.Items[lvProcesses.Items.Count-1].SubItems.Clear();
					//lvProcesses.Items.RemoveByKey(myProcess.Id.ToString());
					lvProcesses.Items.RemoveAt(lvProcesses.Items.Count - 1);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//process_list.RemoveAt(process_list.Count - 1);
					//lvProcesses.Items.RemoveByKey(myProcess.Id.ToString());
					lvProcesses.Items.RemoveAt(lvProcesses.Items.Count - 1);
				}
					Info();
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

			while(process_list.Count > 0)
			{
				try
				{
					process_list.First().CloseMainWindow();
					process_list.Last().CloseMainWindow();
					process_list.RemoveAt(0);
				}
				catch(Exception ex)
				{
					process_list.RemoveAt(0);
				}
			}
		}

		void Info()
		{
			if (process_list.Count > 0)
			{
			myProcess = process_list.First();
				labelProcessInfo.Text = $"Total process count: {process_list.Count}";
				labelProcessInfo.Text += $"PID:				{myProcess.Id}\n";
				labelProcessInfo.Text += $"Base priority:	{myProcess.BasePriority}\n";
				labelProcessInfo.Text += $"Priority class:	{myProcess.PriorityClass}\n";
				labelProcessInfo.Text += $"Start time:		{myProcess.StartTime}\n";
				labelProcessInfo.Text += $"Total CPU time:	{myProcess.TotalProcessorTime}\n";
				labelProcessInfo.Text += $"User  CPU time:	{myProcess.UserProcessorTime}\n";
				labelProcessInfo.Text += $"User  CPU time:	{myProcess.UserProcessorTime}\n";
				labelProcessInfo.Text += $"Session ID:		{myProcess.SessionId}\n";
				labelProcessInfo.Text += $"Name:			{myProcess.ProcessName}\n";
				labelProcessInfo.Text += $"Affinity:		{myProcess.ProcessorAffinity}\n";
				labelProcessInfo.Text += $"Threads:			{myProcess.Threads.Count}\n";
			}
			else labelProcessInfo.Text = "Нет запущенных процессов";
		}
		void RemoveLost()
		{

		}
	}
}