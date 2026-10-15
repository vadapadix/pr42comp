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
            btnCreate = new ToolStripButton();
            btnEdit = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnDelete = new ToolStripButton();
            btnReload = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            panel1 = new Panel();
            label1 = new Label();
            button1 = new Button();
            process1 = new System.Diagnostics.Process();
            Id = new DataGridViewTextBoxColumn();
            From = new DataGridViewTextBoxColumn();
            To = new DataGridViewTextBoxColumn();
            Content = new DataGridViewTextBoxColumn();
            DeliveryType = new DataGridViewTextBoxColumn();
            Weight = new DataGridViewTextBoxColumn();
            Width = new DataGridViewTextBoxColumn();
            Height = new DataGridViewTextBoxColumn();
            Depth = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            CreatredAt = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // gridPostings
            // 
            gridPostings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPostings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPostings.Columns.AddRange(new DataGridViewColumn[] { Id, From, To, Content, DeliveryType, Weight, Width, Height, Depth, Value, Price, CreatredAt });
            gridPostings.Location = new Point(17, 88);
            gridPostings.Name = "gridPostings";
            gridPostings.Size = new Size(1043, 347);
            gridPostings.TabIndex = 0;
            gridPostings.CellContentClick += dataGridView1_CellContentClick;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnCreate, btnEdit, toolStripSeparator2, btnDelete, btnReload, toolStripSeparator1, toolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1067, 32);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnCreate
            // 
            btnCreate.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnCreate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(98, 29);
            btnCreate.Text = "Створити";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnEdit
            // 
            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnEdit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(112, 29);
            btnEdit.Text = "Редагувати";
            btnEdit.Click += btnEdit_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 32);
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 32);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(140, 29);
            toolStripButton1.Text = "Про програму";
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(17, 35);
            panel1.Name = "panel1";
            panel1.Size = new Size(1038, 50);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(4, 12);
            label1.Name = "label1";
            label1.Size = new Size(279, 25);
            label1.TabIndex = 1;
            label1.Text = "Список поштових відправлень";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.HotTrack;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.Control;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(829, 3);
            button1.Name = "button1";
            button1.Size = new Size(206, 44);
            button1.TabIndex = 0;
            button1.Text = "Створення відправлення";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // process1
            // 
            process1.StartInfo.CreateNewProcessGroup = false;
            process1.StartInfo.Domain = "";
            process1.StartInfo.LoadUserProfile = false;
            process1.StartInfo.Password = null;
            process1.StartInfo.StandardErrorEncoding = null;
            process1.StartInfo.StandardInputEncoding = null;
            process1.StartInfo.StandardOutputEncoding = null;
            process1.StartInfo.UseCredentialsForNetworkingOnly = false;
            process1.StartInfo.UserName = "";
            process1.SynchronizingObject = this;
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
            // Depth
            // 
            Depth.DataPropertyName = "Depth";
            Depth.HeaderText = "Довжина";
            Depth.Name = "Depth";
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
            ClientSize = new Size(1067, 450);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Controls.Add(gridPostings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Postservice";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridPostings;
        private ToolStrip toolStrip1;
        private ToolStripButton btnCreate;
        private ToolStripButton btnEdit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton1;
        private Panel panel1;
        private Button button1;
        private Label label1;
        private System.Diagnostics.Process process1;
        private ToolStripButton btnDelete;
        private ToolStripButton btnReload;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn From;
        private DataGridViewTextBoxColumn To;
        private DataGridViewTextBoxColumn Content;
        private DataGridViewTextBoxColumn DeliveryType;
        private DataGridViewTextBoxColumn Weight;
        private DataGridViewTextBoxColumn Width;
        private DataGridViewTextBoxColumn Height;
        private DataGridViewTextBoxColumn Depth;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn CreatredAt;
    }
}
