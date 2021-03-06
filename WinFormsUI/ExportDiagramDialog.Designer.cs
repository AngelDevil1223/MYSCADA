namespace Dataweb.NShape.WinFormsUI {

	partial class ExportDiagramDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing){
				if (components != null) {
					components.Dispose();
					components = null;
				}
				if (_imgAttribs != null) {
					_imgAttribs.Dispose();
					_imgAttribs = null;
				}
				if (_colorLabelBackBrush != null) {
					_colorLabelBackBrush.Dispose();
					_colorLabelBackBrush = null;
				}
				if (_colorLabelFrontBrush != null) {
					_colorLabelFrontBrush.Dispose();
					_colorLabelFrontBrush = null;
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.previewPanel = new System.Windows.Forms.Panel();
			this.FileFormatOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.bmpRadioButton = new System.Windows.Forms.RadioButton();
			this.jpgRadioButton = new System.Windows.Forms.RadioButton();
			this.pngRadioButton = new System.Windows.Forms.RadioButton();
			this.emfRadioButton = new System.Windows.Forms.RadioButton();
			this.emfPlusRadioButton = new System.Windows.Forms.RadioButton();
			this.DestinationOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.qualityTrackBar = new System.Windows.Forms.TrackBar();
			this.qualityLabel = new System.Windows.Forms.Label();
			this.dpiComboBox = new System.Windows.Forms.ComboBox();
			this.dpiLabel = new System.Windows.Forms.Label();
			this.browseButton = new System.Windows.Forms.Button();
			this.filePathTextBox = new System.Windows.Forms.TextBox();
			this.toFileRadioButton = new System.Windows.Forms.RadioButton();
			this.toClipboardRadioButton = new System.Windows.Forms.RadioButton();
			this.ContentOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.backColorCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.marginUpDown = new System.Windows.Forms.NumericUpDown();
			this.exportDiagramRadioButton = new System.Windows.Forms.RadioButton();
			this.exportAllRadioButton = new System.Windows.Forms.RadioButton();
			this.exportSelectedRadioButton = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.colorLabel = new System.Windows.Forms.Label();
			this.withBackgroundCheckBox = new System.Windows.Forms.CheckBox();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.previewCheckBox = new System.Windows.Forms.CheckBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.exportButton = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.visibleLayersOnlyCheckBox = new System.Windows.Forms.CheckBox();
			this.FileFormatOptionsGroupBox.SuspendLayout();
			this.DestinationOptionsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).BeginInit();
			this.ContentOptionsGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.marginUpDown)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// previewPanel
			// 
			this.previewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.previewPanel.BackColor = System.Drawing.Color.Transparent;
			this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.previewPanel.Location = new System.Drawing.Point(3, 19);
			this.previewPanel.Name = "previewPanel";
			this.previewPanel.Size = new System.Drawing.Size(278, 184);
			this.previewPanel.TabIndex = 0;
			this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
			// 
			// FileFormatOptionsGroupBox
			// 
			this.FileFormatOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FileFormatOptionsGroupBox.Controls.Add(this.descriptionLabel);
			this.FileFormatOptionsGroupBox.Controls.Add(this.bmpRadioButton);
			this.FileFormatOptionsGroupBox.Controls.Add(this.jpgRadioButton);
			this.FileFormatOptionsGroupBox.Controls.Add(this.pngRadioButton);
			this.FileFormatOptionsGroupBox.Controls.Add(this.emfRadioButton);
			this.FileFormatOptionsGroupBox.Controls.Add(this.emfPlusRadioButton);
			this.FileFormatOptionsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.FileFormatOptionsGroupBox.Name = "FileFormatOptionsGroupBox";
			this.FileFormatOptionsGroupBox.Size = new System.Drawing.Size(272, 250);
			this.FileFormatOptionsGroupBox.TabIndex = 0;
			this.FileFormatOptionsGroupBox.TabStop = false;
			this.FileFormatOptionsGroupBox.Text = "Image Format Options";
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionLabel.AutoEllipsis = true;
			this.descriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.descriptionLabel.Location = new System.Drawing.Point(6, 143);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(260, 104);
			this.descriptionLabel.TabIndex = 5;
			// 
			// bmpRadioButton
			// 
			this.bmpRadioButton.AutoSize = true;
			this.bmpRadioButton.Location = new System.Drawing.Point(6, 111);
			this.bmpRadioButton.Name = "bmpRadioButton";
			this.bmpRadioButton.Size = new System.Drawing.Size(195, 17);
			this.bmpRadioButton.TabIndex = 4;
			this.bmpRadioButton.Text = "BMP - Uncompressed Bitmap Image";
			this.bmpRadioButton.UseVisualStyleBackColor = true;
			this.bmpRadioButton.CheckedChanged += new System.EventHandler(this.bmpRadioButton_CheckedChanged);
			// 
			// jpgRadioButton
			// 
			this.jpgRadioButton.AutoSize = true;
			this.jpgRadioButton.Location = new System.Drawing.Point(6, 88);
			this.jpgRadioButton.Name = "jpgRadioButton";
			this.jpgRadioButton.Size = new System.Drawing.Size(186, 17);
			this.jpgRadioButton.TabIndex = 3;
			this.jpgRadioButton.Text = "JPEG - Compressed Bitmap Image";
			this.jpgRadioButton.UseVisualStyleBackColor = true;
			this.jpgRadioButton.CheckedChanged += new System.EventHandler(this.jpgRadioButton_CheckedChanged);
			// 
			// pngRadioButton
			// 
			this.pngRadioButton.AutoSize = true;
			this.pngRadioButton.Location = new System.Drawing.Point(6, 65);
			this.pngRadioButton.Name = "pngRadioButton";
			this.pngRadioButton.Size = new System.Drawing.Size(225, 17);
			this.pngRadioButton.TabIndex = 2;
			this.pngRadioButton.Text = "PNG - Lossless Compressed Bitmap Image";
			this.pngRadioButton.UseVisualStyleBackColor = true;
			this.pngRadioButton.CheckedChanged += new System.EventHandler(this.pngRadioButton_CheckedChanged);
			// 
			// emfRadioButton
			// 
			this.emfRadioButton.AutoSize = true;
			this.emfRadioButton.Location = new System.Drawing.Point(6, 42);
			this.emfRadioButton.Name = "emfRadioButton";
			this.emfRadioButton.Size = new System.Drawing.Size(161, 17);
			this.emfRadioButton.TabIndex = 1;
			this.emfRadioButton.Text = "EMF (Classic) - Vector Image";
			this.emfRadioButton.UseVisualStyleBackColor = true;
			this.emfRadioButton.CheckedChanged += new System.EventHandler(this.emfRadioButton_CheckedChanged);
			// 
			// emfPlusRadioButton
			// 
			this.emfPlusRadioButton.AutoSize = true;
			this.emfPlusRadioButton.Location = new System.Drawing.Point(6, 19);
			this.emfPlusRadioButton.Name = "emfPlusRadioButton";
			this.emfPlusRadioButton.Size = new System.Drawing.Size(233, 17);
			this.emfPlusRadioButton.TabIndex = 0;
			this.emfPlusRadioButton.Text = "EMF (Plus Dual) - High Quality Vector Image";
			this.emfPlusRadioButton.UseVisualStyleBackColor = true;
			this.emfPlusRadioButton.CheckedChanged += new System.EventHandler(this.emfPlusRadioButton_CheckedChanged);
			// 
			// DestinationOptionsGroupBox
			// 
			this.DestinationOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DestinationOptionsGroupBox.Controls.Add(this.qualityTrackBar);
			this.DestinationOptionsGroupBox.Controls.Add(this.qualityLabel);
			this.DestinationOptionsGroupBox.Controls.Add(this.dpiComboBox);
			this.DestinationOptionsGroupBox.Controls.Add(this.dpiLabel);
			this.DestinationOptionsGroupBox.Controls.Add(this.browseButton);
			this.DestinationOptionsGroupBox.Controls.Add(this.filePathTextBox);
			this.DestinationOptionsGroupBox.Controls.Add(this.toFileRadioButton);
			this.DestinationOptionsGroupBox.Controls.Add(this.toClipboardRadioButton);
			this.DestinationOptionsGroupBox.Location = new System.Drawing.Point(0, 256);
			this.DestinationOptionsGroupBox.Name = "DestinationOptionsGroupBox";
			this.DestinationOptionsGroupBox.Size = new System.Drawing.Size(272, 150);
			this.DestinationOptionsGroupBox.TabIndex = 1;
			this.DestinationOptionsGroupBox.TabStop = false;
			this.DestinationOptionsGroupBox.Text = "Export Options";
			// 
			// qualityTrackBar
			// 
			this.qualityTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.qualityTrackBar.AutoSize = false;
			this.qualityTrackBar.LargeChange = 10;
			this.qualityTrackBar.Location = new System.Drawing.Point(71, 118);
			this.qualityTrackBar.Margin = new System.Windows.Forms.Padding(0);
			this.qualityTrackBar.Maximum = 100;
			this.qualityTrackBar.Name = "qualityTrackBar";
			this.qualityTrackBar.Size = new System.Drawing.Size(195, 28);
			this.qualityTrackBar.SmallChange = 5;
			this.qualityTrackBar.TabIndex = 2;
			this.qualityTrackBar.TickFrequency = 10;
			this.qualityTrackBar.Value = 75;
			this.qualityTrackBar.ValueChanged += new System.EventHandler(this.qualityTrackBar_ValueChanged);
			// 
			// qualityLabel
			// 
			this.qualityLabel.AutoSize = true;
			this.qualityLabel.Location = new System.Drawing.Point(6, 126);
			this.qualityLabel.Name = "qualityLabel";
			this.qualityLabel.Size = new System.Drawing.Size(39, 13);
			this.qualityLabel.TabIndex = 2;
			this.qualityLabel.Text = "Quality";
			// 
			// dpiComboBox
			// 
			this.dpiComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dpiComboBox.FormattingEnabled = true;
			this.dpiComboBox.Location = new System.Drawing.Point(79, 91);
			this.dpiComboBox.Name = "dpiComboBox";
			this.dpiComboBox.Size = new System.Drawing.Size(51, 21);
			this.dpiComboBox.TabIndex = 0;
			this.dpiComboBox.SelectedIndexChanged += new System.EventHandler(this.dpiComboBox_SelectedIndexChanged);
			this.dpiComboBox.SelectedValueChanged += new System.EventHandler(this.dpiComboBox_SelectedValueChanged);
			this.dpiComboBox.TextChanged += new System.EventHandler(this.dpiComboBox_TextChanged);
			// 
			// dpiLabel
			// 
			this.dpiLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dpiLabel.AutoSize = true;
			this.dpiLabel.Location = new System.Drawing.Point(6, 94);
			this.dpiLabel.Name = "dpiLabel";
			this.dpiLabel.Size = new System.Drawing.Size(57, 13);
			this.dpiLabel.TabIndex = 1;
			this.dpiLabel.Text = "Resolution";
			// 
			// browseButton
			// 
			this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.browseButton.Location = new System.Drawing.Point(242, 63);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(24, 23);
			this.browseButton.TabIndex = 3;
			this.browseButton.Text = "...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// filePathTextBox
			// 
			this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filePathTextBox.Location = new System.Drawing.Point(6, 65);
			this.filePathTextBox.Name = "filePathTextBox";
			this.filePathTextBox.Size = new System.Drawing.Size(230, 20);
			this.filePathTextBox.TabIndex = 2;
			this.filePathTextBox.TextChanged += new System.EventHandler(this.filePathTextBox_TextChanged);
			// 
			// toFileRadioButton
			// 
			this.toFileRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toFileRadioButton.AutoSize = true;
			this.toFileRadioButton.Location = new System.Drawing.Point(6, 42);
			this.toFileRadioButton.Name = "toFileRadioButton";
			this.toFileRadioButton.Size = new System.Drawing.Size(86, 17);
			this.toFileRadioButton.TabIndex = 1;
			this.toFileRadioButton.Text = "Export to File";
			this.toFileRadioButton.UseVisualStyleBackColor = true;
			this.toFileRadioButton.CheckedChanged += new System.EventHandler(this.toFileRadioButton_CheckedChanged);
			// 
			// toClipboardRadioButton
			// 
			this.toClipboardRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toClipboardRadioButton.AutoSize = true;
			this.toClipboardRadioButton.Location = new System.Drawing.Point(6, 19);
			this.toClipboardRadioButton.Name = "toClipboardRadioButton";
			this.toClipboardRadioButton.Size = new System.Drawing.Size(114, 17);
			this.toClipboardRadioButton.TabIndex = 0;
			this.toClipboardRadioButton.Text = "Export to Clipboard";
			this.toClipboardRadioButton.UseVisualStyleBackColor = true;
			this.toClipboardRadioButton.CheckedChanged += new System.EventHandler(this.toClipboardRadioButton_CheckedChanged);
			// 
			// ContentOptionsGroupBox
			// 
			this.ContentOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ContentOptionsGroupBox.Controls.Add(this.visibleLayersOnlyCheckBox);
			this.ContentOptionsGroupBox.Controls.Add(this.backColorCheckBox);
			this.ContentOptionsGroupBox.Controls.Add(this.label1);
			this.ContentOptionsGroupBox.Controls.Add(this.marginUpDown);
			this.ContentOptionsGroupBox.Controls.Add(this.exportDiagramRadioButton);
			this.ContentOptionsGroupBox.Controls.Add(this.exportAllRadioButton);
			this.ContentOptionsGroupBox.Controls.Add(this.exportSelectedRadioButton);
			this.ContentOptionsGroupBox.Controls.Add(this.button1);
			this.ContentOptionsGroupBox.Controls.Add(this.colorLabel);
			this.ContentOptionsGroupBox.Controls.Add(this.withBackgroundCheckBox);
			this.ContentOptionsGroupBox.Location = new System.Drawing.Point(3, 209);
			this.ContentOptionsGroupBox.Name = "ContentOptionsGroupBox";
			this.ContentOptionsGroupBox.Size = new System.Drawing.Size(278, 197);
			this.ContentOptionsGroupBox.TabIndex = 2;
			this.ContentOptionsGroupBox.TabStop = false;
			this.ContentOptionsGroupBox.Text = "Content Options";
			// 
			// backColorCheckBox
			// 
			this.backColorCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.backColorCheckBox.AutoSize = true;
			this.backColorCheckBox.Location = new System.Drawing.Point(6, 172);
			this.backColorCheckBox.Name = "backColorCheckBox";
			this.backColorCheckBox.Size = new System.Drawing.Size(143, 17);
			this.backColorCheckBox.TabIndex = 12;
			this.backColorCheckBox.Text = "Image Background Color";
			this.backColorCheckBox.UseVisualStyleBackColor = true;
			this.backColorCheckBox.CheckedChanged += new System.EventHandler(this.backColorCheckBox_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 124);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Margin";
			// 
			// marginUpDown
			// 
			this.marginUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.marginUpDown.Location = new System.Drawing.Point(51, 122);
			this.marginUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
			this.marginUpDown.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
			this.marginUpDown.Name = "marginUpDown";
			this.marginUpDown.Size = new System.Drawing.Size(55, 20);
			this.marginUpDown.TabIndex = 10;
			this.marginUpDown.ValueChanged += new System.EventHandler(this.marginUpDown_ValueChanged);
			this.marginUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.marginUpDown_KeyUp);
			// 
			// exportDiagramRadioButton
			// 
			this.exportDiagramRadioButton.AutoSize = true;
			this.exportDiagramRadioButton.Location = new System.Drawing.Point(6, 65);
			this.exportDiagramRadioButton.Name = "exportDiagramRadioButton";
			this.exportDiagramRadioButton.Size = new System.Drawing.Size(128, 17);
			this.exportDiagramRadioButton.TabIndex = 9;
			this.exportDiagramRadioButton.TabStop = true;
			this.exportDiagramRadioButton.Text = "Export Diagram Sheet";
			this.exportDiagramRadioButton.UseVisualStyleBackColor = true;
			this.exportDiagramRadioButton.CheckedChanged += new System.EventHandler(this.exportDiagramRadioButton_CheckedChanged);
			// 
			// exportAllRadioButton
			// 
			this.exportAllRadioButton.AutoSize = true;
			this.exportAllRadioButton.Location = new System.Drawing.Point(6, 42);
			this.exportAllRadioButton.Name = "exportAllRadioButton";
			this.exportAllRadioButton.Size = new System.Drawing.Size(108, 17);
			this.exportAllRadioButton.TabIndex = 8;
			this.exportAllRadioButton.TabStop = true;
			this.exportAllRadioButton.Text = "Export All Shapes";
			this.exportAllRadioButton.UseVisualStyleBackColor = true;
			this.exportAllRadioButton.CheckedChanged += new System.EventHandler(this.exportAllRadioButton_CheckedChanged);
			// 
			// exportSelectedRadioButton
			// 
			this.exportSelectedRadioButton.AutoSize = true;
			this.exportSelectedRadioButton.Location = new System.Drawing.Point(6, 19);
			this.exportSelectedRadioButton.Name = "exportSelectedRadioButton";
			this.exportSelectedRadioButton.Size = new System.Drawing.Size(139, 17);
			this.exportSelectedRadioButton.TabIndex = 7;
			this.exportSelectedRadioButton.TabStop = true;
			this.exportSelectedRadioButton.Text = "Export Selected Shapes";
			this.exportSelectedRadioButton.UseVisualStyleBackColor = true;
			this.exportSelectedRadioButton.CheckedChanged += new System.EventHandler(this.exportSelectedRadioButton_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(184, 168);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(26, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.selectBackColor_Click);
			// 
			// colorLabel
			// 
			this.colorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.colorLabel.BackColor = System.Drawing.Color.White;
			this.colorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.colorLabel.Location = new System.Drawing.Point(155, 168);
			this.colorLabel.Name = "colorLabel";
			this.colorLabel.Size = new System.Drawing.Size(23, 23);
			this.colorLabel.TabIndex = 4;
			this.colorLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.backColorLabel_Paint);
			// 
			// withBackgroundCheckBox
			// 
			this.withBackgroundCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.withBackgroundCheckBox.AutoSize = true;
			this.withBackgroundCheckBox.Location = new System.Drawing.Point(6, 148);
			this.withBackgroundCheckBox.Name = "withBackgroundCheckBox";
			this.withBackgroundCheckBox.Size = new System.Drawing.Size(181, 17);
			this.withBackgroundCheckBox.TabIndex = 3;
			this.withBackgroundCheckBox.Text = "Export with Diagram Background";
			this.withBackgroundCheckBox.UseVisualStyleBackColor = true;
			this.withBackgroundCheckBox.CheckedChanged += new System.EventHandler(this.withBackgroundCheckBox_CheckedChanged);
			// 
			// previewCheckBox
			// 
			this.previewCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.previewCheckBox.AutoSize = true;
			this.previewCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.previewCheckBox.Checked = true;
			this.previewCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.previewCheckBox.Location = new System.Drawing.Point(187, 0);
			this.previewCheckBox.Name = "previewCheckBox";
			this.previewCheckBox.Size = new System.Drawing.Size(94, 17);
			this.previewCheckBox.TabIndex = 4;
			this.previewCheckBox.Text = "Show Preview";
			this.previewCheckBox.UseVisualStyleBackColor = true;
			this.previewCheckBox.CheckedChanged += new System.EventHandler(this.previewCheckBox_CheckedChanged);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(497, 434);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(416, 434);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// exportButton
			// 
			this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exportButton.Location = new System.Drawing.Point(335, 434);
			this.exportButton.Name = "exportButton";
			this.exportButton.Size = new System.Drawing.Size(75, 23);
			this.exportButton.TabIndex = 7;
			this.exportButton.Text = "Export";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.FileFormatOptionsGroupBox);
			this.splitContainer1.Panel1.Controls.Add(this.DestinationOptionsGroupBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.previewCheckBox);
			this.splitContainer1.Panel2.Controls.Add(this.previewPanel);
			this.splitContainer1.Panel2.Controls.Add(this.ContentOptionsGroupBox);
			this.splitContainer1.Size = new System.Drawing.Size(560, 406);
			this.splitContainer1.SplitterDistance = 275;
			this.splitContainer1.TabIndex = 8;
			// 
			// visibleLayersOnlyCheckBox
			// 
			this.visibleLayersOnlyCheckBox.AutoSize = true;
			this.visibleLayersOnlyCheckBox.Location = new System.Drawing.Point(6, 92);
			this.visibleLayersOnlyCheckBox.Name = "visibleLayersOnlyCheckBox";
			this.visibleLayersOnlyCheckBox.Size = new System.Drawing.Size(196, 17);
			this.visibleLayersOnlyCheckBox.TabIndex = 13;
			this.visibleLayersOnlyCheckBox.Text = "Export only Shapes of Visible Layers";
			this.visibleLayersOnlyCheckBox.UseVisualStyleBackColor = true;
			this.visibleLayersOnlyCheckBox.CheckedChanged += new System.EventHandler(this.visibleLayersOnlyCheckBox_CheckedChanged);
			// 
			// ExportDiagramDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(584, 469);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.Name = "ExportDiagramDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export Diagram";
			this.FileFormatOptionsGroupBox.ResumeLayout(false);
			this.FileFormatOptionsGroupBox.PerformLayout();
			this.DestinationOptionsGroupBox.ResumeLayout(false);
			this.DestinationOptionsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).EndInit();
			this.ContentOptionsGroupBox.ResumeLayout(false);
			this.ContentOptionsGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.marginUpDown)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel previewPanel;
		private System.Windows.Forms.GroupBox FileFormatOptionsGroupBox;
		private System.Windows.Forms.RadioButton bmpRadioButton;
		private System.Windows.Forms.RadioButton jpgRadioButton;
		private System.Windows.Forms.RadioButton pngRadioButton;
		private System.Windows.Forms.RadioButton emfRadioButton;
		private System.Windows.Forms.RadioButton emfPlusRadioButton;
		private System.Windows.Forms.Label descriptionLabel;
		private System.Windows.Forms.GroupBox DestinationOptionsGroupBox;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.TextBox filePathTextBox;
		private System.Windows.Forms.RadioButton toFileRadioButton;
		private System.Windows.Forms.RadioButton toClipboardRadioButton;
		private System.Windows.Forms.GroupBox ContentOptionsGroupBox;
		private System.Windows.Forms.Label dpiLabel;
		private System.Windows.Forms.ComboBox dpiComboBox;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label colorLabel;
		private System.Windows.Forms.CheckBox withBackgroundCheckBox;
		private System.Windows.Forms.RadioButton exportDiagramRadioButton;
		private System.Windows.Forms.RadioButton exportAllRadioButton;
		private System.Windows.Forms.RadioButton exportSelectedRadioButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown marginUpDown;
		private System.Windows.Forms.CheckBox previewCheckBox;
		private System.Windows.Forms.TrackBar qualityTrackBar;
		private System.Windows.Forms.Label qualityLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.CheckBox backColorCheckBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.CheckBox visibleLayersOnlyCheckBox;
	}
}