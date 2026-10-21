using System.Net;
using PostService.CommonTypes;
using PostService.Dtos;

namespace PostService.App;

public partial class PostingEditForm : Form
{
    public bool RecordNotFound { get; private set; }

    private readonly IPostServiceClient _postServiceClient;
    private readonly int? _editingId;
    private readonly float _editingPrice;

    public PostingEditForm(IPostServiceClient postServiceClient)
    {
        InitializeComponent();
        _postServiceClient = postServiceClient;
        _editingId = null;
        _editingPrice = 0f;

        Text = "Нове відправлення";
        cmbDeliveryType.DataSource = Enum.GetValues(typeof(DeliveryType));
    }

    public PostingEditForm(IPostServiceClient postServiceClient, PostingGetDto posting)
    {
        InitializeComponent();
        _postServiceClient = postServiceClient;
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
        btnCancel.Enabled = false;

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

                bool retry = true;
                while (retry)
                {
                    try
                    {
                        await _postServiceClient.CreatePostingAsync(postDto);
                        DialogResult = DialogResult.OK;
                        Close();
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

                bool retry = true;
                while (retry)
                {
                    try
                    {
                        await _postServiceClient.UpdatePostingAsync(_editingId.Value, putDto);
                        DialogResult = DialogResult.OK;
                        Close();
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
                        RecordNotFound = true;
                        MessageBox.Show("Відправлення вже не існує. Список буде оновлено.", "Не знайдено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.Cancel;
                        Close();
                        retry = false;
                    }
                }
            }
        }
        catch (PostingValidationException ex)
        {
            var messages = new List<string>();
            foreach (var field in ex.Errors)
            {
                messages.AddRange(field.Value);
            }
            MessageBox.Show(string.Join(Environment.NewLine, messages), "Перевірте введені дані");
        }
        finally
        {
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
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
