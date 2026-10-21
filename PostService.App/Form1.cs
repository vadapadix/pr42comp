using PostService.Dtos;
using System.Net;

namespace PostService.App
{
    public partial class Form1 : Form
    {
        private readonly IPostServiceClient _postServiceClient;

        public Form1()
        {
            InitializeComponent();
            gridPostings.AutoGenerateColumns = false;
            _postServiceClient = new PostServiceClient();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadPostingAsync();
        }

        private async Task LoadPostingAsync()
        {
            bool retry = true;
            while (retry)
            {
                try
                {
                    var postings = await _postServiceClient.GetPostingsAsync();
                    gridPostings.DataSource = postings;
                    retry = false;
                }
                catch (HttpRequestException e) when (e.StatusCode == null)
                {
                    var result = MessageBox.Show(
                        "Не вдалося з'єднатися з сервером. Повторити спробу?",
                        "Сервер недоступний",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Warning
                    );
                    if (result != DialogResult.Retry)
                    {
                        retry = false;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            using var dialog = new PostingEditForm(_postServiceClient);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadPostingAsync();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridPostings.CurrentRow?.DataBoundItem is not PostingGetDto selectedPosting)
            {
                MessageBox.Show("Виберіть відправлення для редагування", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new PostingEditForm(_postServiceClient, selectedPosting);
            if (dialog.ShowDialog() == DialogResult.OK || dialog.RecordNotFound)
            {
                await LoadPostingAsync();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridPostings.CurrentRow?.DataBoundItem is not PostingGetDto selectedPosting)
            {
                MessageBox.Show("Виберіть відправлення яке треба видалити", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Ви дійсно бажаєте видалити відправлення #{selectedPosting.Id} ({selectedPosting.Content})? Дія є незворотною.",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            bool retry = true;
            while (retry)
            {
                try
                {
                    await _postServiceClient.DeletePostingAsync(selectedPosting.Id);
                    await LoadPostingAsync();
                    retry = false;
                }
                catch (HttpRequestException ex) when (ex.StatusCode == null)
                {
                    var result = MessageBox.Show(
                        "Не вдалося з'єднатися з сервером. Повторити спробу?",
                        "Сервер недоступний",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Warning
                    );
                    if (result != DialogResult.Retry)
                    {
                        retry = false;
                    }
                }
                catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Відправлення вже не існує. Список буде оновлено.", "Не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPostingAsync();
                    retry = false;
                }
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await LoadPostingAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using var dialog = new PostingEditForm(_postServiceClient);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadPostingAsync();
            }
        }
    }
}
