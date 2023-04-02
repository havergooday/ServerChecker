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
		List<ServerInfo> servers = new List<ServerInfo>();
		public Form1()
		{
			InitializeComponent();

			sRefreshTime.Text = DateTime.Now.ToString("hh:mm:ss");
			CheckData();
		}

		private void CheckData()
		{
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
					servers = JsonConvert.DeserializeObject<List<ServerInfo>>(File.ReadAllText(path));

					int index = 0;
					dataGridView.Rows.Add(servers.Count - 1);
					foreach (ServerInfo server in servers)
					{
						dataGridView.Rows[index].Cells["names"].Value = server.Name;
						dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.uncheckedImage;
						CheckServer(index, server.Url);
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
			RefreshData();
		}

		private void RefreshData()
		{
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				row.Cells["status"].Value = Properties.Resources.uncheckedImage;
			}

			int index = 0;
			foreach (ServerInfo server in servers)
			{
				dataGridView.Rows[index].Cells["names"].Value = server.Name;
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.uncheckedImage;
				CheckServer(index, server.Url);
				index++;
			}
		}
	}

	public class ServerInfo
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}

}
