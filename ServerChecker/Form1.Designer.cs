
namespace ServerChecker
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRefresh = new System.Windows.Forms.Button();
			this.sRefreshTime = new System.Windows.Forms.Label();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Status = new System.Windows.Forms.DataGridViewImageColumn();
			this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRefresh.Location = new System.Drawing.Point(216, 192);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(56, 57);
			this.btnRefresh.TabIndex = 0;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// sRefreshTime
			// 
			this.sRefreshTime.AutoSize = true;
			this.sRefreshTime.Location = new System.Drawing.Point(161, 237);
			this.sRefreshTime.Name = "sRefreshTime";
			this.sRefreshTime.Size = new System.Drawing.Size(49, 12);
			this.sRefreshTime.TabIndex = 1;
			this.sRefreshTime.Text = "00:00:00";
			// 
			// dataGridView
			// 
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.Names});
			this.dataGridView.Location = new System.Drawing.Point(2, 2);
			this.dataGridView.Margin = new System.Windows.Forms.Padding(10);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowHeadersWidth = 60;
			this.dataGridView.RowTemplate.Height = 32;
			this.dataGridView.Size = new System.Drawing.Size(278, 184);
			this.dataGridView.TabIndex = 3;
			// 
			// Status
			// 
			this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Status.DividerWidth = 1;
			this.Status.HeaderText = "Status";
			this.Status.MinimumWidth = 47;
			this.Status.Name = "Status";
			this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Status.Width = 47;
			// 
			// Names
			// 
			this.Names.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Names.HeaderText = "Name";
			this.Names.Name = "Names";
			this.Names.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.sRefreshTime);
			this.Controls.Add(this.btnRefresh);
			this.Name = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label sRefreshTime;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewImageColumn Status;
		private System.Windows.Forms.DataGridViewTextBoxColumn Names;
	}
}

