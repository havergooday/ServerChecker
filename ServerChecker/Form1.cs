using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
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

			// Load custom images


			sRefreshTime.Text = DateTime.Now.ToString("hh:mm:ss");
			string folderPath = Application.StartupPath;
			string path = folderPath + "/ServerList.json";
			if (!File.Exists(path))
			{
				// Create a file to write to.
				sRefreshTime.Text = "File not Exist:\n" + path;
			}
			else
			{
				try
				{
					List<ServerInfo> servers = JsonConvert.DeserializeObject<List<ServerInfo>>(File.ReadAllText(path));

					int index = 0;
					foreach (ServerInfo server in servers)
					{
						int rowIndex = dataGridView.Rows.Add();
						dataGridView.Rows[rowIndex].Cells["names"].Value = server.Name;
						CheckServer(index, server.Url);
						//serverListBox.Items.Add(server.Name);
						index++;
					}

				}
				catch (JsonException ex)
				{
					Console.WriteLine($"Error deserializing JSON: {ex.Message}");
				}
			}
		}

		private async void CheckServer(int index, string url)
		{
			var isServerActive = await CheckServerStatusAsync(url);
			if (isServerActive)
			{
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.checkedImage;
			}
			else
			{
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.uncheckedImage;
			}
		}
		private async Task<bool> CheckServerStatusAsync(string url)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var response = await httpClient.GetAsync(url);
					if (response.IsSuccessStatusCode)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (HttpRequestException)
			{
				return false;
			}
		}
		
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			sRefreshTime.Text = DateTime.Now.ToString("hh:mm:ss");

		}
	}

	public class ServerInfo
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}

}
