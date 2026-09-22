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
                MessageBox.Show("Не вдалося отримати дані, перевірте чи запущено PostService");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridPostings.CurrentRow?.DataBoundItem is not PostingGetDto selectedPosting)
            {
                MessageBox.Show("Виберіть відправлення яке треба видалити");
                return;
            }
            var response = await _httpClient.DeleteAsync($"postings/{selectedPosting.Id}");
            await LoadPostingAsync();
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Не вдалося видалити відправлення, помилка: {(int)response.StatusCode}");
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await LoadPostingAsync();
        }
    }
}
