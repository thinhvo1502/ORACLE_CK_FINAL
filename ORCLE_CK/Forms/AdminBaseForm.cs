using System;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public class AdminBaseForm : Form
    {
        protected Panel headerPanel;
        protected Panel contentPanel;
        protected StatusStrip statusStrip;
        protected ToolStripStatusLabel statusLabel;
        protected ToolStripStatusLabel timeLabel;
        protected Timer statusTimer;

        public AdminBaseForm(string title)
        {
            InitializeBaseComponents(title);
            InitializeStatusTimer();
        }

        private void InitializeBaseComponents(string title)
        {
            // Form settings
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Header Panel
            headerPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(0, 120, 215)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            headerPanel.Controls.Add(titleLabel);

            // Content Panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            // Status Strip
            statusStrip = new StatusStrip
            {
                BackColor = Color.FromArgb(240, 240, 240),
                SizingGrip = false
            };

            statusLabel = new ToolStripStatusLabel
            {
                Text = "Sẵn sàng",
                Font = new Font("Segoe UI", 9)
            };

            timeLabel = new ToolStripStatusLabel
            {
                Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Font = new Font("Segoe UI", 9),
                Alignment = ToolStripItemAlignment.Right
            };

            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, timeLabel });

            // Add controls to form
            this.Controls.AddRange(new Control[] { headerPanel, contentPanel, statusStrip });
        }

        private void InitializeStatusTimer()
        {
            statusTimer = new Timer
            {
                Interval = 1000
            };
            statusTimer.Tick += (s, e) => timeLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            statusTimer.Start();
        }

        protected void ShowSuccess(string message)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = Color.FromArgb(0, 150, 136);
        }

        protected void ShowError(string message)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = Color.FromArgb(244, 67, 54);
        }

        protected void ShowInfo(string message)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = Color.FromArgb(0, 120, 215);
        }

        protected void AddControlsToContent(params Control[] controls)
        {
            contentPanel.Controls.AddRange(controls);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            statusTimer?.Stop();
            statusTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
} 