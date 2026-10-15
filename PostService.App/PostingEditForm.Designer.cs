namespace PostService.App
{
    partial class PostingEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostingEditForm));
            lblFrom = new Label();
            txtFrom = new TextBox();
            lblTo = new Label();
            txtTo = new TextBox();
            lblContent = new Label();
            txtContent = new TextBox();
            lblDeliveryType = new Label();
            cmbDeliveryType = new ComboBox();
            lblWeight = new Label();
            numWeight = new NumericUpDown();
            lblWidth = new Label();
            numWidth = new NumericUpDown();
            lblHeight = new Label();
            numHeight = new NumericUpDown();
            lblDepth = new Label();
            numDepth = new NumericUpDown();
            lblValue = new Label();
            numValue = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            errorProvider1 = new ErrorProvider(components);
            toolStrip1 = new ToolStrip();
            toolStripLabel2 = new ToolStripLabel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)numWeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(20, 27);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(77, 17);
            lblFrom.TabIndex = 19;
            lblFrom.Text = "Відправник:";
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(20, 47);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(347, 25);
            txtFrom.TabIndex = 18;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(20, 75);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(77, 17);
            lblTo.TabIndex = 17;
            lblTo.Text = "Отримувач:";
            // 
            // txtTo
            // 
            txtTo.Location = new Point(20, 95);
            txtTo.Name = "txtTo";
            txtTo.Size = new Size(347, 25);
            txtTo.TabIndex = 16;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(20, 243);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(41, 17);
            lblContent.TabIndex = 15;
            lblContent.Text = "Вміст:";
            // 
            // txtContent
            // 
            txtContent.Location = new Point(20, 263);
            txtContent.Multiline = true;
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(347, 98);
            txtContent.TabIndex = 14;
            // 
            // lblDeliveryType
            // 
            lblDeliveryType.AutoSize = true;
            lblDeliveryType.Location = new Point(20, 129);
            lblDeliveryType.Name = "lblDeliveryType";
            lblDeliveryType.Size = new Size(89, 17);
            lblDeliveryType.TabIndex = 13;
            lblDeliveryType.Text = "Тип доставки:";
            // 
            // cmbDeliveryType
            // 
            cmbDeliveryType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeliveryType.Location = new Point(20, 149);
            cmbDeliveryType.Name = "cmbDeliveryType";
            cmbDeliveryType.Size = new Size(167, 25);
            cmbDeliveryType.TabIndex = 12;
            // 
            // lblWeight
            // 
            lblWeight.AutoSize = true;
            lblWeight.Location = new Point(300, 188);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(60, 17);
            lblWeight.TabIndex = 11;
            lblWeight.Text = "Вага (кг):";
            // 
            // numWeight
            // 
            numWeight.DecimalPlaces = 2;
            numWeight.Location = new Point(300, 208);
            numWeight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numWeight.Name = "numWeight";
            numWeight.Size = new Size(67, 25);
            numWeight.TabIndex = 10;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(20, 188);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(87, 17);
            lblWidth.TabIndex = 9;
            lblWidth.Text = "Ширина (см):";
            // 
            // numWidth
            // 
            numWidth.DecimalPlaces = 1;
            numWidth.Location = new Point(20, 208);
            numWidth.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(67, 25);
            numWidth.TabIndex = 8;
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(211, 188);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(78, 17);
            lblHeight.TabIndex = 7;
            lblHeight.Text = "Висота (см):";
            // 
            // numHeight
            // 
            numHeight.DecimalPlaces = 1;
            numHeight.Location = new Point(211, 208);
            numHeight.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(67, 25);
            numHeight.TabIndex = 6;
            // 
            // lblDepth
            // 
            lblDepth.AutoSize = true;
            lblDepth.Location = new Point(113, 188);
            lblDepth.Name = "lblDepth";
            lblDepth.Size = new Size(92, 17);
            lblDepth.TabIndex = 5;
            lblDepth.Text = "Довжина (см):";
            // 
            // numDepth
            // 
            numDepth.DecimalPlaces = 1;
            numDepth.Location = new Point(113, 208);
            numDepth.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numDepth.Name = "numDepth";
            numDepth.Size = new Size(67, 25);
            numDepth.TabIndex = 4;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.Location = new Point(222, 129);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(91, 17);
            lblValue.TabIndex = 3;
            lblValue.Text = "Цінність (грн):";
            // 
            // numValue
            // 
            numValue.DecimalPlaces = 2;
            numValue.Location = new Point(224, 149);
            numValue.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numValue.Name = "numValue";
            numValue.Size = new Size(143, 25);
            numValue.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(328, 26);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 35);
            btnSave.TabIndex = 1;
            btnSave.Text = "Зберегти";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(195, 26);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 35);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel2 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(450, 28);
            toolStrip1.TabIndex = 20;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            toolStripLabel2.ForeColor = SystemColors.HotTrack;
            toolStripLabel2.Image = (Image)resources.GetObject("toolStripLabel2.Image");
            toolStripLabel2.ImageScaling = ToolStripItemImageScaling.None;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(136, 25);
            toolStripLabel2.Text = "PostService";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnSave);
            panel1.Location = new Point(0, 367);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 78);
            panel1.TabIndex = 21;
            // 
            // PostingEditForm
            // 
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            ClientSize = new Size(450, 440);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Controls.Add(numValue);
            Controls.Add(lblValue);
            Controls.Add(numDepth);
            Controls.Add(lblDepth);
            Controls.Add(numHeight);
            Controls.Add(lblHeight);
            Controls.Add(numWidth);
            Controls.Add(lblWidth);
            Controls.Add(numWeight);
            Controls.Add(lblWeight);
            Controls.Add(cmbDeliveryType);
            Controls.Add(lblDeliveryType);
            Controls.Add(txtContent);
            Controls.Add(lblContent);
            Controls.Add(txtTo);
            Controls.Add(lblTo);
            Controls.Add(txtFrom);
            Controls.Add(lblFrom);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PostingEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Створення нового відправлення";
            Load += PostingEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)numWeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblFrom;
        private TextBox txtFrom;
        private Label lblTo;
        private TextBox txtTo;
        private Label lblContent;
        private TextBox txtContent;
        private Label lblDeliveryType;
        private ComboBox cmbDeliveryType;
        private Label lblWeight;
        private NumericUpDown numWeight;
        private Label lblWidth;
        private NumericUpDown numWidth;
        private Label lblHeight;
        private NumericUpDown numHeight;
        private Label lblDepth;
        private NumericUpDown numDepth;
        private Label lblValue;
        private NumericUpDown numValue;
        private Button btnSave;
        private Button btnCancel;
        private ErrorProvider errorProvider1;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel2;
        private Panel panel1;
    }
}
