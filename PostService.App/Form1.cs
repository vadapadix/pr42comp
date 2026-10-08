using PostService.Dtos;
using System.Net.Http.Json;

namespace PostService.App
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("http://localhost:5028") };

        public Form1()
        {
            InitializeComponent();
            gridPostings.AutoGenerateColumns = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadPostingAsync();
        }

        private async Task LoadPostingAsync()
        {
            try
            {
                var postings = await _httpClient.GetFromJsonAsync<PostingGetDto[]>("postings");
                gridPostings.DataSource = postings;
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Не вдалося отримати дані, перевірте чи запущено PostService", "Помилка зв'язку", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            using var dialog = new PostingEditForm(_httpClient);
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

            using var dialog = new PostingEditForm(_httpClient, selectedPosting);
            if (dialog.ShowDialog() == DialogResult.OK)
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

            var response = await _httpClient.DeleteAsync($"postings/{selectedPosting.Id}");
            await LoadPostingAsync();
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Не вдалося видалити відправлення, помилка: {(int)response.StatusCode}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await LoadPostingAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using var dialog = new PostingEditForm(_httpClient);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                await LoadPostingAsync();
            }
        }
    }
}
