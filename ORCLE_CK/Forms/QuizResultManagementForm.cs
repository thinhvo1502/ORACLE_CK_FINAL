using ORCLE_CK.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class QuizResultManagementForm : Form
    {
        private readonly QuizService quizService;
        private readonly int quizId;
        private ListView listViewResults;
        private Button btnRefresh, btnViewDetail;

        public QuizResultManagementForm(int quizId)
        {
            this.quizId = quizId;
            quizService = new QuizService();
            InitializeComponent();
            LoadResults();
        }

        

        private void LoadResults()
        {
            listViewResults.Items.Clear();
            // Implementation to load quiz results
            // This would require additional service methods and models
        }

        private void BtnViewDetail_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedItems.Count == 0) return;

            // Implementation to show detailed quiz result
            MessageBox.Show("Xem chi tiết kết quả quiz", "Thông báo");
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadResults();
        }
    }
}
