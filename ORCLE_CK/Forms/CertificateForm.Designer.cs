using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class CertificateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.certificatesListView = new ListView();
            this.certificatePreviewPanel = new Panel();
            this.lblStudentName = new Label();
            this.lblCourseName = new Label();
            this.lblCompletionDate = new Label();
            this.lblGrade = new Label();
            this.btnDownload = new Button();
            this.btnPrint = new Button();
            this.btnRefresh = new Button();

            this.SuspendLayout();

            // Form
            this.Text = $"Chứng chỉ - {currentUser.FullName}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;

            // Certificates List
            var lblListTitle = new Label
            {
                Text = "Danh sách chứng chỉ",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(200, 25)
            };

            this.certificatesListView.Location = new Point(20, 50);
            this.certificatesListView.Size = new Size(450, 500);
            this.certificatesListView.View = View.Details;
            this.certificatesListView.FullRowSelect = true;
            this.certificatesListView.GridLines = true;
            this.certificatesListView.Font = new Font("Microsoft Sans Serif", 9F);

            this.certificatesListView.Columns.Add("Khóa học", 200);
            this.certificatesListView.Columns.Add("Hoàn thành", 120);
            this.certificatesListView.Columns.Add("Điểm", 80);
            this.certificatesListView.Columns.Add("Trạng thái", 80);

            this.certificatesListView.SelectedIndexChanged += CertificatesListView_SelectedIndexChanged;

            // Certificate Preview Panel
            this.certificatePreviewPanel.Location = new Point(490, 50);
            this.certificatePreviewPanel.Size = new Size(470, 500);
            this.certificatePreviewPanel.BackColor = Color.White;
            this.certificatePreviewPanel.BorderStyle = BorderStyle.FixedSingle;
            this.certificatePreviewPanel.Padding = new Padding(20);

            var lblPreviewTitle = new Label
            {
                Text = "Xem trước chứng chỉ",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(0, 0),
                Size = new Size(200, 25)
            };

            // Certificate Content
            var lblCertificateTitle = new Label
            {
                Text = "CHỨNG NHẬN HOÀN THÀNH KHÓA HỌC",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 50),
                Size = new Size(430, 30)
            };

            var lblCertifyText = new Label
            {
                Text = "Chứng nhận rằng",
                Font = new Font("Microsoft Sans Serif", 12F),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 100),
                Size = new Size(430, 25)
            };

            this.lblStudentName.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblStudentName.ForeColor = Color.DarkRed;
            this.lblStudentName.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStudentName.Location = new Point(0, 130);
            this.lblStudentName.Size = new Size(430, 30);

            var lblCompletedText = new Label
            {
                Text = "đã hoàn thành khóa học",
                Font = new Font("Microsoft Sans Serif", 12F),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 170),
                Size = new Size(430, 25)
            };

            this.lblCourseName.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.lblCourseName.ForeColor = Color.DarkBlue;
            this.lblCourseName.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCourseName.Location = new Point(0, 200);
            this.lblCourseName.Size = new Size(430, 30);

            this.lblCompletionDate.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblCompletionDate.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCompletionDate.Location = new Point(0, 250);
            this.lblCompletionDate.Size = new Size(430, 20);

            this.lblGrade.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblGrade.ForeColor = Color.DarkGreen;
            this.lblGrade.TextAlign = ContentAlignment.MiddleCenter;
            this.lblGrade.Location = new Point(0, 280);
            this.lblGrade.Size = new Size(430, 25);

            var lblSignature = new Label
            {
                Text = "Hệ thống E-Learning Management",
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 350),
                Size = new Size(430, 20)
            };

            this.certificatePreviewPanel.Controls.Add(lblPreviewTitle);
            this.certificatePreviewPanel.Controls.Add(lblCertificateTitle);
            this.certificatePreviewPanel.Controls.Add(lblCertifyText);
            this.certificatePreviewPanel.Controls.Add(this.lblStudentName);
            this.certificatePreviewPanel.Controls.Add(lblCompletedText);
            this.certificatePreviewPanel.Controls.Add(this.lblCourseName);
            this.certificatePreviewPanel.Controls.Add(this.lblCompletionDate);
            this.certificatePreviewPanel.Controls.Add(this.lblGrade);
            this.certificatePreviewPanel.Controls.Add(lblSignature);

            // Action Buttons
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(20, 570);
            this.btnRefresh.Size = new Size(100, 30);
            this.btnRefresh.BackColor = Color.Blue;
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Click += BtnRefresh_Click;

            this.btnDownload.Text = "Tải xuống";
            this.btnDownload.Location = new Point(490, 570);
            this.btnDownload.Size = new Size(100, 30);
            this.btnDownload.BackColor = Color.Green;
            this.btnDownload.ForeColor = Color.White;
            this.btnDownload.Click += BtnDownload_Click;

            this.btnPrint.Text = "In";
            this.btnPrint.Location = new Point(600, 570);
            this.btnPrint.Size = new Size(100, 30);
            this.btnPrint.BackColor = Color.Orange;
            this.btnPrint.ForeColor = Color.White;
            this.btnPrint.Click += BtnPrint_Click;

            // Add controls
            this.Controls.Add(lblListTitle);
            this.Controls.Add(this.certificatesListView);
            this.Controls.Add(this.certificatePreviewPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnPrint);

            this.ResumeLayout(false);
        }

        #endregion
    }
}