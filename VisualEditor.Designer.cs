
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolButton = new System.Windows.Forms.ToolStripButton();
            this.toolLabel = new System.Windows.Forms.ToolStripButton();
            this.toolCheckbox = new System.Windows.Forms.ToolStripButton();
            this.toolAutocomplete = new System.Windows.Forms.ToolStripButton();
            this.toolTextBox = new System.Windows.Forms.ToolStripButton();
            this.toolRadioButton = new System.Windows.Forms.ToolStripButton();
            this.toolDatePicker = new System.Windows.Forms.ToolStripButton();
            this.toolImage = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDeleteSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlsSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.tlsViewCode = new System.Windows.Forms.ToolStripButton();
            this.tlsRun = new System.Windows.Forms.ToolStripButton();
            this.pnControls = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolDatePicker,
            this.toolImage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(104, 419);
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
            // toolImage
            // 
            this.toolImage.Image = ((System.Drawing.Image)(resources.GetObject("toolImage.Image")));
            this.toolImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolImage.Name = "toolImage";
            this.toolImage.Size = new System.Drawing.Size(101, 20);
            this.toolImage.Text = "Image";
            this.toolImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxPaste,
            this.ctxDeleteAll});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(139, 80);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::MySCADA.Properties.Resources.paste;
            this.ctxPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(138, 38);
            this.ctxPaste.Text = "&Paste";
            this.ctxPaste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxPaste.Click += new System.EventHandler(this.ctxPaste_Click);
            // 
            // ctxDeleteAll
            // 
            this.ctxDeleteAll.Image = global::MySCADA.Properties.Resources.DeleteAll;
            this.ctxDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxDeleteAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxDeleteAll.Name = "ctxDeleteAll";
            this.ctxDeleteAll.Size = new System.Drawing.Size(138, 38);
            this.ctxDeleteAll.Text = "&Delete all";
            this.ctxDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxDeleteAll.Click += new System.EventHandler(this.ctxDeleteAll_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(597, 31);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(203, 419);
            this.propertyGrid1.TabIndex = 26;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCopy,
            this.ctxCut,
            this.ctxBringToFront,
            this.ctxSendToBack,
            this.ctxDeleteSelected});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(170, 194);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::MySCADA.Properties.Resources.Copy;
            this.ctxCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(169, 38);
            this.ctxCopy.Text = "&Copy";
            this.ctxCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxCopy.Click += new System.EventHandler(this.ctxCopy_Click);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::MySCADA.Properties.Resources.Cut;
            this.ctxCut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxCut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(169, 38);
            this.ctxCut.Text = "C&ut";
            this.ctxCut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxCut.Click += new System.EventHandler(this.ctxCut_Click);
            // 
            // ctxBringToFront
            // 
            this.ctxBringToFront.Image = global::MySCADA.Properties.Resources.BringtoFront;
            this.ctxBringToFront.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxBringToFront.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxBringToFront.Name = "ctxBringToFront";
            this.ctxBringToFront.Size = new System.Drawing.Size(169, 38);
            this.ctxBringToFront.Text = "&Bring To Front";
            this.ctxBringToFront.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxBringToFront.Click += new System.EventHandler(this.ctxBringToFront_Click);
            // 
            // ctxSendToBack
            // 
            this.ctxSendToBack.Image = global::MySCADA.Properties.Resources.SendtoBack;
            this.ctxSendToBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxSendToBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxSendToBack.Name = "ctxSendToBack";
            this.ctxSendToBack.Size = new System.Drawing.Size(169, 38);
            this.ctxSendToBack.Text = "&Send To Back";
            this.ctxSendToBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxSendToBack.Click += new System.EventHandler(this.ctxSendToBack_Click);
            // 
            // ctxDeleteSelected
            // 
            this.ctxDeleteSelected.Image = global::MySCADA.Properties.Resources.DeleteS;
            this.ctxDeleteSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxDeleteSelected.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ctxDeleteSelected.Name = "ctxDeleteSelected";
            this.ctxDeleteSelected.Size = new System.Drawing.Size(169, 38);
            this.ctxDeleteSelected.Text = "&Delete selected";
            this.ctxDeleteSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ctxDeleteSelected.Click += new System.EventHandler(this.ctxDeleteSelected_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsSaveChanges,
            this.tlsViewCode,
            this.tlsRun});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // tlsSaveChanges
            // 
            this.tlsSaveChanges.Image = ((System.Drawing.Image)(resources.GetObject("tlsSaveChanges.Image")));
            this.tlsSaveChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlsSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlsSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsSaveChanges.Name = "tlsSaveChanges";
            this.tlsSaveChanges.Size = new System.Drawing.Size(106, 28);
            this.tlsSaveChanges.Text = "Save changes";
            this.tlsSaveChanges.ToolTipText = "Save changes";
            this.tlsSaveChanges.Click += new System.EventHandler(this.tlsSaveChanges_Click);
            // 
            // tlsViewCode
            // 
            this.tlsViewCode.Image = ((System.Drawing.Image)(resources.GetObject("tlsViewCode.Image")));
            this.tlsViewCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlsViewCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlsViewCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsViewCode.Name = "tlsViewCode";
            this.tlsViewCode.Size = new System.Drawing.Size(81, 28);
            this.tlsViewCode.Text = "View code";
            this.tlsViewCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlsViewCode.Click += new System.EventHandler(this.tlsViewCode_Click);
            // 
            // tlsRun
            // 
            this.tlsRun.Image = ((System.Drawing.Image)(resources.GetObject("tlsRun.Image")));
            this.tlsRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlsRun.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlsRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsRun.Name = "tlsRun";
            this.tlsRun.Size = new System.Drawing.Size(48, 28);
            this.tlsRun.Text = "Run";
            this.tlsRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlsRun.Click += new System.EventHandler(this.tlsRun_Click);
            // 
            // pnControls
            // 
            this.pnControls.BackColor = System.Drawing.SystemColors.Window;
            this.pnControls.ContextMenuStrip = this.contextMenuStrip2;
            this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnControls.Location = new System.Drawing.Point(104, 31);
            this.pnControls.Name = "pnControls";
            this.pnControls.Size = new System.Drawing.Size(696, 419);
            this.pnControls.TabIndex = 25;
            this.pnControls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseDown);
            this.pnControls.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnControls_MouseMove);
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
            this.Name = "VisualEditor";
            this.Text = "Visual Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VisualEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolButton;
        private System.Windows.Forms.ToolStripButton toolLabel;
        private System.Windows.Forms.ToolStripButton toolCheckbox;
        private System.Windows.Forms.ToolStripButton toolAutocomplete;
        private System.Windows.Forms.ToolStripButton toolTextBox;
        private System.Windows.Forms.ToolStripButton toolRadioButton;
        private System.Windows.Forms.ToolStripButton toolDatePicker;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton toolImage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxCopy;
        private System.Windows.Forms.ToolStripMenuItem ctxCut;
        private System.Windows.Forms.ToolStripMenuItem ctxBringToFront;
        private System.Windows.Forms.ToolStripMenuItem ctxSendToBack;
        private System.Windows.Forms.ToolStripMenuItem ctxDeleteSelected;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem ctxPaste;
        private System.Windows.Forms.ToolStripMenuItem ctxDeleteAll;
        private System.Windows.Forms.ToolStrip menuStrip1;
        private System.Windows.Forms.ToolStripButton tlsSaveChanges;
        private System.Windows.Forms.ToolStripButton tlsViewCode;
        private System.Windows.Forms.Panel pnControls;
        private System.Windows.Forms.ToolStripButton tlsRun;
    }
}