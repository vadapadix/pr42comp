using System.Net.Http.Json;
using PostService.CommonTypes;
using PostService.Dtos;

namespace PostService.App;

public partial class PostingEditForm : Form
{
    private readonly HttpClient _httpClient;
    private readonly int? _editingId;
    private readonly float _editingPrice;

    public PostingEditForm(HttpClient httpClient)
    {
        InitializeComponent();
        _httpClient = httpClient;
        _editingId = null;
        _editingPrice = 0f;

        Text = "Нове відправлення";
        cmbDeliveryType.DataSource = Enum.GetValues(typeof(DeliveryType));
    }

    public PostingEditForm(HttpClient httpClient, PostingGetDto posting)
    {
        InitializeComponent();
        _httpClient = httpClient;
        _editingId = posting.Id;
        _editingPrice = posting.Price;

        Text = $"Редагування відправлення #{posting.Id}";
        cmbDeliveryType.DataSource = Enum.GetValues(typeof(DeliveryType));

        txtFrom.Text = posting.From;
        txtTo.Text = posting.To;
        txtContent.Text = posting.Content;
        cmbDeliveryType.SelectedItem = posting.DeliveryType;
        numWeight.Value = (decimal)posting.Weight;
        numWidth.Value = (decimal)posting.Width;
        numHeight.Value = (decimal)posting.Height;
        numDepth.Value = (decimal)posting.Depth;
        numValue.Value = (decimal)(posting.Value ?? 0);
    }

    private bool ValidateForm()
    {
        errorProvider1.Clear();
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(txtFrom.Text))
        {
            errorProvider1.SetError(txtFrom, "Вкажіть відправника");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(txtTo.Text))
        {
            errorProvider1.SetError(txtTo, "Вкажіть отримувача");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(txtContent.Text))
        {
            errorProvider1.SetError(txtContent, "Вкажіть опис вантажу");
            isValid = false;
        }

        if (numWeight.Value <= 0)
        {
            errorProvider1.SetError(numWeight, "Вага має бути більше 0");
            isValid = false;
        }

        return isValid;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            return;
        }

        btnSave.Enabled = false;

        try
        {
            if (_editingId == null)
            {
                var postDto = new PostingPostDto
                {
                    From = txtFrom.Text.Trim(),
                    To = txtTo.Text.Trim(),
                    Content = txtContent.Text.Trim(),
                    DeliveryType = (DeliveryType)cmbDeliveryType.SelectedItem!,
                    Weight = (float)numWeight.Value,
                    Width = (float)numWidth.Value,
                    Height = (float)numHeight.Value,
                    Depth = (float)numDepth.Value,
                    Value = numValue.Value > 0 ? (float)numValue.Value : null
                };

                var response = await _httpClient.PostAsJsonAsync("postings", postDto);
                if (response.IsSuccessStatusCode)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show($"Помилка створення: {response.StatusCode}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var putDto = new PostingPutDto
                {
                    Id = _editingId.Value,
                    From = txtFrom.Text.Trim(),
                    To = txtTo.Text.Trim(),
                    Content = txtContent.Text.Trim(),
                    DeliveryType = (DeliveryType)cmbDeliveryType.SelectedItem!,
                    Weight = (float)numWeight.Value,
                    Width = (float)numWidth.Value,
                    Height = (float)numHeight.Value,
                    Depth = (float)numDepth.Value,
                    Value = numValue.Value > 0 ? (float)numValue.Value : null,
                    Price = _editingPrice
                };

                var response = await _httpClient.PutAsJsonAsync($"postings/{_editingId.Value}", putDto);
                if (response.IsSuccessStatusCode)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show($"Помилка оновлення: {response.StatusCode}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Помилка зв'язку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnSave.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void PostingEditForm_Load(object sender, EventArgs e)
    {

    }
}
