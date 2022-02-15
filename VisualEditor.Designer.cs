
namespace MySCADA
{
    partial class VisualEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualEditor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolButton = new System.Windows.Forms.ToolStripButton();
            this.toolLabel = new System.Windows.Forms.ToolStripButton();
            this.toolCheckbox = new System.Windows.Forms.ToolStripButton();
            this.toolAutocomplete = new System.Windows.Forms.ToolStripButton();
            this.toolTextBox = new System.Windows.Forms.ToolStripButton();
            this.toolRadioButton = new System.Windows.Forms.ToolStripButton();
            this.toolDatePicker = new System.Windows.Forms.ToolStripButton();
            this.pnControls = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButton,
            this.toolLabel,
            this.toolCheckbox,
            this.toolAutocomplete,
            this.toolTextBox,
            this.toolRadioButton,
            this.toolDatePicker});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(104, 426);
            this.toolStrip1.TabIndex = 24;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolButton
            // 
            this.toolButton.Image = ((System.Drawing.Image)(resources.GetObject("toolButton.Image")));
            this.toolButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButton.Name = "toolButton";
            this.toolButton.Size = new System.Drawing.Size(101, 20);
            this.toolButton.Text = "Button";
            this.toolButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolButton.Click += new System.EventHandler(this.toolButton_Click);
            // 
            // toolLabel
            // 
            this.toolLabel.Image = ((System.Drawing.Image)(resources.GetObject("toolLabel.Image")));
            this.toolLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLabel.Name = "toolLabel";
            this.toolLabel.Size = new System.Drawing.Size(101, 20);
            this.toolLabel.Text = "Label";
            this.toolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolLabel.Click += new System.EventHandler(this.toolLabel_Click);
            // 
            // toolCheckbox
            // 
            this.toolCheckbox.Image = ((System.Drawing.Image)(resources.GetObject("toolCheckbox.Image")));
            this.toolCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolCheckbox.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolCheckbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCheckbox.Name = "toolCheckbox";
            this.toolCheckbox.Size = new System.Drawing.Size(101, 20);
            this.toolCheckbox.Text = "Checkbox";
            this.toolCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolCheckbox.Click += new System.EventHandler(this.toolCheckbox_Click);
            // 
            // toolAutocomplete
            // 
            this.toolAutocomplete.Image = ((System.Drawing.Image)(resources.GetObject("toolAutocomplete.Image")));
            this.toolAutocomplete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolAutocomplete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolAutocomplete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAutocomplete.Name = "toolAutocomplete";
            this.toolAutocomplete.Size = new System.Drawing.Size(101, 20);
            this.toolAutocomplete.Text = "Autocomplete";
            this.toolAutocomplete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolAutocomplete.Click += new System.EventHandler(this.toolAutocomplete_Click);
            // 
            // toolTextBox
            // 
            this.toolTextBox.Image = ((System.Drawing.Image)(resources.GetObject("toolTextBox.Image")));
            this.toolTextBox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTextBox.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolTextBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTextBox.Name = "toolTextBox";
            this.toolTextBox.Size = new System.Drawing.Size(101, 20);
            this.toolTextBox.Text = "Textbox";
            this.toolTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTextBox.Click += new System.EventHandler(this.toolTextBox_Click);
            // 
            // toolRadioButton
            // 
            this.toolRadioButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRadioButton.Image = ((System.Drawing.Image)(resources.GetObject("toolRadioButton.Image")));
            this.toolRadioButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolRadioButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolRadioButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRadioButton.Name = "toolRadioButton";
            this.toolRadioButton.Size = new System.Drawing.Size(101, 20);
            this.toolRadioButton.Text = "Radiobutton";
            this.toolRadioButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolRadioButton.Click += new System.EventHandler(this.toolRadioButton_Click);
            // 
            // toolDatePicker
            // 
            this.toolDatePicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDatePicker.Image = ((System.Drawing.Image)(resources.GetObject("toolDatePicker.Image")));
            this.toolDatePicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolDatePicker.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolDatePicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDatePicker.Name = "toolDatePicker";
            this.toolDatePicker.Size = new System.Drawing.Size(101, 20);
            this.toolDatePicker.Text = "Date picker";
            this.toolDatePicker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolDatePicker.Click += new System.EventHandler(this.toolDatePicker_Click);
            // 
            // pnControls
            // 
            this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnControls.Location = new System.Drawing.Point(104, 24);
            this.pnControls.Name = "pnControls";
            this.pnControls.Size = new System.Drawing.Size(696, 426);
            this.pnControls.TabIndex = 25;
            this.pnControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseDown);
            this.pnControls.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseMove);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(670, 24);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(130, 426);
            this.propertyGrid1.TabIndex = 26;
            // 
            // VisualEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.pnControls);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VisualEditor";
            this.Text = "Visual Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolButton;
        private System.Windows.Forms.ToolStripButton toolLabel;
        private System.Windows.Forms.ToolStripButton toolCheckbox;
        private System.Windows.Forms.ToolStripButton toolAutocomplete;
        private System.Windows.Forms.ToolStripButton toolTextBox;
        private System.Windows.Forms.ToolStripButton toolRadioButton;
        private System.Windows.Forms.ToolStripButton toolDatePicker;
        private System.Windows.Forms.Panel pnControls;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Timer timer1;
    }
}