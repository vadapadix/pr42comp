namespace PostService.App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            gridPostings = new DataGridView();
            toolStrip1 = new ToolStrip();
            btnDelete = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnReload = new ToolStripButton();
            Id = new DataGridViewTextBoxColumn();
            From = new DataGridViewTextBoxColumn();
            To = new DataGridViewTextBoxColumn();
            Content = new DataGridViewTextBoxColumn();
            DeliveryType = new DataGridViewTextBoxColumn();
            Weight = new DataGridViewTextBoxColumn();
            Width = new DataGridViewTextBoxColumn();
            Height = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            CreatredAt = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gridPostings
            // 
            gridPostings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPostings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPostings.Columns.AddRange(new DataGridViewColumn[] { Id, From, To, Content, DeliveryType, Weight, Width, Height, Value, Price, CreatredAt });
            gridPostings.Location = new Point(12, 35);
            gridPostings.Name = "gridPostings";
            gridPostings.Size = new Size(776, 403);
            gridPostings.TabIndex = 0;
            gridPostings.CellContentClick += dataGridView1_CellContentClick;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnDelete, toolStripSeparator1, btnReload });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 32);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnDelete
            // 
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDelete.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(98, 29);
            btnDelete.Text = "Видалити";
            btnDelete.Click += btnDelete_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 32);
            // 
            // btnReload
            // 
            btnReload.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnReload.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnReload.Image = (Image)resources.GetObject("btnReload.Image");
            btnReload.ImageTransparentColor = Color.Magenta;
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(92, 29);
            btnReload.Text = "Оновити";
            btnReload.Click += btnReload_Click;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Ід";
            Id.Name = "Id";
            Id.Visible = false;
            // 
            // From
            // 
            From.DataPropertyName = "From";
            From.HeaderText = "Від";
            From.Name = "From";
            // 
            // To
            // 
            To.DataPropertyName = "To";
            To.HeaderText = "До";
            To.Name = "To";
            // 
            // Content
            // 
            Content.DataPropertyName = "Content";
            Content.HeaderText = "В посилці";
            Content.Name = "Content";
            // 
            // DeliveryType
            // 
            DeliveryType.DataPropertyName = "DeliveryType";
            DeliveryType.HeaderText = "Тип відправлення";
            DeliveryType.Name = "DeliveryType";
            // 
            // Weight
            // 
            Weight.DataPropertyName = "Weight";
            Weight.HeaderText = "Вага";
            Weight.Name = "Weight";
            // 
            // Width
            // 
            Width.DataPropertyName = "Width";
            Width.HeaderText = "Ширина";
            Width.Name = "Width";
            // 
            // Height
            // 
            Height.DataPropertyName = "Height";
            Height.HeaderText = "Висота";
            Height.Name = "Height";
            // 
            // Value
            // 
            Value.DataPropertyName = "Value";
            Value.HeaderText = "Значення";
            Value.Name = "Value";
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            Price.HeaderText = "Ціна";
            Price.Name = "Price";
            // 
            // CreatredAt
            // 
            CreatredAt.DataPropertyName = "CreatedAt";
            CreatredAt.HeaderText = "Дата відправлення";
            CreatredAt.Name = "CreatredAt";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip1);
            Controls.Add(gridPostings);
            Name = "Form1";
            Text = "Доставка";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridPostings;
        private ToolStrip toolStrip1;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnReload;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn From;
        private DataGridViewTextBoxColumn To;
        private DataGridViewTextBoxColumn Content;
        private DataGridViewTextBoxColumn DeliveryType;
        private DataGridViewTextBoxColumn Weight;
        private DataGridViewTextBoxColumn Width;
        private DataGridViewTextBoxColumn Height;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn CreatredAt;
    }
}
