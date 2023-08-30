using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace ServerChecker
{
	public partial class Form1 : Form
	{
		DispatcherTimer timer = new DispatcherTimer();

		List<ToolStripItem> trayItemList = new List<ToolStripItem>();

		List<ServerInfo> servers = new List<ServerInfo>();
		public Form1()
		{
			InitializeComponent();

			sRefreshTime.Text = DateTime.Now.ToString("hh:mm:ss");
			CheckData();

			contextMenuStrip.Items.Add(new ToolStripSeparator());
			ToolStripItem windowButton = contextMenuStrip.Items.Add(" 열기 ");
			windowButton.Click += new EventHandler(TrayModeOff_Click);
			ToolStripItem item = contextMenuStrip.Items.Add(" 종료 ");
			item.Click += new EventHandler(contentMenuStrip_Exit);

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

						ToolStripItem item = contextMenuStrip.Items.Add(index.ToString());
						item.Image = Properties.Resources.checkedImage;
						trayItemList.Add(item);
					}
				}
				catch (JsonException ex)
				{
					Console.WriteLine($"Error deserializing JSON: {ex.Message}");
				}
			}
		}
		private void JustCheckData(object sender, System.EventArgs e)
		{
			foreach (ServerInfo server in servers)
			{
				CheckServer(server.Url);
			}
		}

		private async void CheckServer(int index, string url)
		{
			var isServerActive = await CheckServerStatusAsync(url);
			if (isServerActive)
			{
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.checkedImage;

				trayItemList[index].Image = Properties.Resources.checkedImage;
			}
			else
			{
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.uncheckedImage;

				trayItemList[index].Image = Properties.Resources.uncheckedImage;
			}
		}

		private async void CheckServer(string url)
		{
			var isServerActive = await CheckServerStatusAsync(url);
			if (!isServerActive)
			{
				Console.WriteLine("error!");
				notifyIcon.ShowBalloonTip(1000, "알림", "감시 중인 URL 연결이 비활성화되었습니다.", ToolTipIcon.Error);
				notifyIcon.BalloonTipClicked += new EventHandler(TrayModeOff_Click);
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
			int index = 0;
			foreach (ServerInfo server in servers)
			{
				dataGridView.Rows[index].Cells["names"].Value = server.Name;
				dataGridView.Rows[index].Cells["status"].Value = Properties.Resources.uncheckedImage;
				CheckServer(index, server.Url);
				index++;
			}
		}
		private void InitTimer()
		{
			timer.Stop();

			int tickTime = 1;
			if (!string.IsNullOrEmpty(TrayTime.Text))
			{
				if (Int32.TryParse(TrayTime.Text, out int j))
				{
					tickTime = j;
				}
				else
				{
					Console.WriteLine("String could not be parsed.");
				}
			}
			// 1초 마다 Tick 됩니다.
			Console.WriteLine(tickTime * 60000);
			timer.Interval = TimeSpan.FromMilliseconds(tickTime * 60000); 
			timer.Tick += JustCheckData;
			timer.Start();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			InitTimer();

			this.Visible = false;
			notifyIcon.Visible = true;
		}

		private void TrayModeOff_Click(object sender, EventArgs e)
		{
			this.Visible = true;
			notifyIcon.Visible = false;
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			Console.WriteLine("open");
			RefreshData();
		}

		private void contentMenuStrip_Exit(object sender, EventArgs e)
		{
			this.Close();
		}
	}

	public class ServerInfo
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}

}
