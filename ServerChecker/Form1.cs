using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ServerChecker
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			sRefrshTime.Text = DateTime.Now.ToString("hh:mm:ss");
			string folderPath = Application.StartupPath;
			string path = folderPath + "/ServerList.json";
			if (!File.Exists(path))
			{
				// Create a file to write to.
				sRefrshTime.Text = "File not Exist:\n" + path;
			}
			else
			{
				try
				{
					List<ServerInfo> servers = JsonConvert.DeserializeObject<List<ServerInfo>>(File.ReadAllText(path));
					CheckedListBox checkedListBox = new CheckedListBox();
					for (int i = 0; i < servers.Count; i++)
					{
						checkedListBox.Items.Add(servers[i].Name);
						checkedListBox1.Controls.Add(checkedListBox);
					}
				}
				catch (JsonException ex)
				{
					Console.WriteLine($"Error deserializing JSON: {ex.Message}");
				}
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			sRefrshTime.Text = DateTime.Now.ToString("hh:mm:ss");

		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}


	public class ServerInfo
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}

}
