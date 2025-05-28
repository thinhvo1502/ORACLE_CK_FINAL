using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class EditQuizQuestionForm
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
            // Initialize controls
            txtQuestion = new TextBox();
            txtOptionA = new TextBox();
            txtOptionB = new TextBox();
            txtOptionC = new TextBox();
            txtOptionD = new TextBox();
            cmbCorrectAnswer = new ComboBox();
            numPoints = new NumericUpDown();
            numOrder = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();

            // Form setup
            this.Text = "Chỉnh sửa câu hỏi";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Question
            var lblQuestion = new Label { Text = "Câu hỏi:", Location = new System.Drawing.Point(20, 20) };
            txtQuestion.Location = new System.Drawing.Point(20, 50);
            txtQuestion.Size = new System.Drawing.Size(540, 60);
            txtQuestion.Multiline = true;

            // Options
            var lblOptionA = new Label { Text = "Đáp án A:", Location = new System.Drawing.Point(20, 120) };
            txtOptionA.Location = new System.Drawing.Point(20, 150);
            txtOptionA.Size = new System.Drawing.Size(540, 23);

            var lblOptionB = new Label { Text = "Đáp án B:", Location = new System.Drawing.Point(20, 180) };
            txtOptionB.Location = new System.Drawing.Point(20, 210);
            txtOptionB.Size = new System.Drawing.Size(540, 23);

            var lblOptionC = new Label { Text = "Đáp án C:", Location = new System.Drawing.Point(20, 240) };
            txtOptionC.Location = new System.Drawing.Point(20, 270);
            txtOptionC.Size = new System.Drawing.Size(540, 23);

            var lblOptionD = new Label { Text = "Đáp án D:", Location = new System.Drawing.Point(20, 300) };
            txtOptionD.Location = new System.Drawing.Point(20, 330);
            txtOptionD.Size = new System.Drawing.Size(540, 23);

            // Correct Answer
            var lblCorrectAnswer = new Label { Text = "Đáp án đúng:", Location = new System.Drawing.Point(20, 360) };
            cmbCorrectAnswer.Location = new System.Drawing.Point(20, 390);
            cmbCorrectAnswer.Size = new System.Drawing.Size(100, 23);
            cmbCorrectAnswer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCorrectAnswer.Items.AddRange(new object[] { "A", "B", "C", "D" });

            // Points
            var lblPoints = new Label { Text = "Điểm:", Location = new System.Drawing.Point(140, 360) };
            numPoints.Location = new System.Drawing.Point(140, 390);
            numPoints.Size = new System.Drawing.Size(100, 23);
            numPoints.Minimum = 1;
            numPoints.Maximum = 100;

            // Order
            var lblOrder = new Label { Text = "Thứ tự:", Location = new System.Drawing.Point(260, 360) };
            numOrder.Location = new System.Drawing.Point(260, 390);
            numOrder.Size = new System.Drawing.Size(100, 23);
            numOrder.Minimum = 1;
            numOrder.Maximum = 1000;

            // Buttons
            btnSave.Text = "Lưu";
            btnSave.Location = new System.Drawing.Point(400, 420);
            btnSave.Size = new System.Drawing.Size(75, 30);
            btnSave.Click += BtnSave_Click;

            btnCancel.Text = "Hủy";
            btnCancel.Location = new System.Drawing.Point(485, 420);
            btnCancel.Size = new System.Drawing.Size(75, 30);
            btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.AddRange(new Control[] {
                lblQuestion, txtQuestion,
                lblOptionA, txtOptionA,
                lblOptionB, txtOptionB,
                lblOptionC, txtOptionC,
                lblOptionD, txtOptionD,
                lblCorrectAnswer, cmbCorrectAnswer,
                lblPoints, numPoints,
                lblOrder, numOrder,
                btnSave, btnCancel
            });
        }


        #endregion
    }
}