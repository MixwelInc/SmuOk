﻿namespace SmuOk.Component
{
    partial class InvDoc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBudg = new System.Windows.Forms.DataGridView();
            this.dgv_btn_folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_InvId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvINN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvLegalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvSumWOVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvSumWithVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvSumFinished = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_InvComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPb = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.txtSpecNameFilter = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.OrderId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteOrder_btn = new System.Windows.Forms.Button();
            this.dgvInvFilling = new System.Windows.Forms.DataGridView();
            this.btnImportInvFilling = new System.Windows.Forms.Button();
            this.btnExportInvFilling = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_InvPosId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PriceWOVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvFilling)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBudg
            // 
            this.dgvBudg.AllowUserToAddRows = false;
            this.dgvBudg.AllowUserToDeleteRows = false;
            this.dgvBudg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBudg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBudg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBudg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_btn_folder,
            this.dgv_InvId,
            this.dgv_InvType,
            this.dgv_InvINN,
            this.dgv_InvLegalName,
            this.dgv_InvNum,
            this.dgv_InvDate,
            this.dgv_InvSumWOVAT,
            this.dgv_InvSumWithVAT,
            this.dgv_InvSumFinished,
            this.dgv_InvComment});
            this.dgvBudg.Location = new System.Drawing.Point(3, 30);
            this.dgvBudg.Name = "dgvBudg";
            this.dgvBudg.ReadOnly = true;
            this.dgvBudg.RowHeadersVisible = false;
            this.dgvBudg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBudg.Size = new System.Drawing.Size(2146, 925);
            this.dgvBudg.TabIndex = 1;
            this.dgvBudg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellContentClick);
            this.dgvBudg.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpec_CellLeave);
            // 
            // dgv_btn_folder
            // 
            this.dgv_btn_folder.HeaderText = "0";
            this.dgv_btn_folder.Image = global::SmuOk.Properties.Resources.shared;
            this.dgv_btn_folder.Name = "dgv_btn_folder";
            this.dgv_btn_folder.ReadOnly = true;
            this.dgv_btn_folder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_btn_folder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgv_btn_folder.Visible = false;
            this.dgv_btn_folder.Width = 28;
            // 
            // dgv_InvId
            // 
            this.dgv_InvId.DataPropertyName = "InvId";
            this.dgv_InvId.HeaderText = "Id счет";
            this.dgv_InvId.Name = "dgv_InvId";
            this.dgv_InvId.ReadOnly = true;
            // 
            // dgv_InvType
            // 
            this.dgv_InvType.DataPropertyName = "InvType";
            this.dgv_InvType.HeaderText = "Вид документа (КП. счет)";
            this.dgv_InvType.Name = "dgv_InvType";
            this.dgv_InvType.ReadOnly = true;
            // 
            // dgv_InvINN
            // 
            this.dgv_InvINN.DataPropertyName = "InvINN";
            this.dgv_InvINN.HeaderText = "ИНН юр. лица по счету";
            this.dgv_InvINN.Name = "dgv_InvINN";
            this.dgv_InvINN.ReadOnly = true;
            // 
            // dgv_InvLegalName
            // 
            this.dgv_InvLegalName.DataPropertyName = "InvLegalName";
            this.dgv_InvLegalName.HeaderText = "Наименование организации";
            this.dgv_InvLegalName.Name = "dgv_InvLegalName";
            this.dgv_InvLegalName.ReadOnly = true;
            // 
            // dgv_InvNum
            // 
            this.dgv_InvNum.DataPropertyName = "InvNum";
            this.dgv_InvNum.HeaderText = "№ счета/КП";
            this.dgv_InvNum.Name = "dgv_InvNum";
            this.dgv_InvNum.ReadOnly = true;
            // 
            // dgv_InvDate
            // 
            this.dgv_InvDate.DataPropertyName = "InvDate";
            this.dgv_InvDate.HeaderText = "Дата";
            this.dgv_InvDate.Name = "dgv_InvDate";
            this.dgv_InvDate.ReadOnly = true;
            // 
            // dgv_InvSumWOVAT
            // 
            this.dgv_InvSumWOVAT.DataPropertyName = "InvSumWOVAT";
            this.dgv_InvSumWOVAT.HeaderText = "Сумма без НДС";
            this.dgv_InvSumWOVAT.Name = "dgv_InvSumWOVAT";
            this.dgv_InvSumWOVAT.ReadOnly = true;
            // 
            // dgv_InvSumWithVAT
            // 
            this.dgv_InvSumWithVAT.DataPropertyName = "InvSumWithVAT";
            this.dgv_InvSumWithVAT.HeaderText = "Сумма с НДС";
            this.dgv_InvSumWithVAT.Name = "dgv_InvSumWithVAT";
            this.dgv_InvSumWithVAT.ReadOnly = true;
            // 
            // dgv_InvSumFinished
            // 
            this.dgv_InvSumFinished.DataPropertyName = "InvSumFinished";
            this.dgv_InvSumFinished.HeaderText = "Сумма разнесена";
            this.dgv_InvSumFinished.Name = "dgv_InvSumFinished";
            this.dgv_InvSumFinished.ReadOnly = true;
            // 
            // dgv_InvComment
            // 
            this.dgv_InvComment.DataPropertyName = "InvComment";
            this.dgv_InvComment.HeaderText = "Комментарий";
            this.dgv_InvComment.Name = "dgv_InvComment";
            this.dgv_InvComment.ReadOnly = true;
            // 
            // lblPb
            // 
            this.lblPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPb.AutoSize = true;
            this.lblPb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPb.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblPb.Location = new System.Drawing.Point(1838, 4);
            this.lblPb.Name = "lblPb";
            this.lblPb.Size = new System.Drawing.Size(67, 13);
            this.lblPb.TabIndex = 69;
            this.lblPb.Text = "==========";
            this.lblPb.Visible = false;
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(1841, 20);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(307, 5);
            this.pb.TabIndex = 68;
            this.pb.Tag = "lblPb";
            this.pb.Visible = false;
            // 
            // txtSpecNameFilter
            // 
            this.txtSpecNameFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtSpecNameFilter.Location = new System.Drawing.Point(3, 4);
            this.txtSpecNameFilter.Name = "txtSpecNameFilter";
            this.txtSpecNameFilter.Size = new System.Drawing.Size(151, 20);
            this.txtSpecNameFilter.TabIndex = 73;
            this.txtSpecNameFilter.Tag = "Шифр...";
            this.txtSpecNameFilter.Enter += new System.EventHandler(this.txtSpecNameFilter_Enter);
            this.txtSpecNameFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSpecNameFilter_KeyUp);
            this.txtSpecNameFilter.Leave += new System.EventHandler(this.txtSpecNameFilter_Leave);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "0";
            this.dataGridViewImageColumn1.Image = global::SmuOk.Properties.Resources.shared;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 28;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnImport.Image = global::SmuOk.Properties.Resources.open;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(1891, 961);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(123, 23);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "Обновить реестр";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.Green;
            this.btnExport.Image = global::SmuOk.Properties.Resources.report_excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(2020, 961);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(128, 23);
            this.btnExport.TabIndex = 33;
            this.btnExport.Text = "Выгрузить реестр";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // OrderId
            // 
            this.OrderId.Location = new System.Drawing.Point(888, 4);
            this.OrderId.Name = "OrderId";
            this.OrderId.Size = new System.Drawing.Size(100, 20);
            this.OrderId.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(789, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Удаление счета";
            // 
            // deleteOrder_btn
            // 
            this.deleteOrder_btn.Location = new System.Drawing.Point(994, 3);
            this.deleteOrder_btn.Name = "deleteOrder_btn";
            this.deleteOrder_btn.Size = new System.Drawing.Size(75, 23);
            this.deleteOrder_btn.TabIndex = 76;
            this.deleteOrder_btn.Text = "Удалить";
            this.deleteOrder_btn.UseVisualStyleBackColor = true;
            this.deleteOrder_btn.Click += new System.EventHandler(this.deleteOrder_btn_Click);
            // 
            // dgvInvFilling
            // 
            this.dgvInvFilling.AllowUserToAddRows = false;
            this.dgvInvFilling.AllowUserToDeleteRows = false;
            this.dgvInvFilling.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvFilling.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInvFilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvFilling.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.dgv_InvPosId,
            this.dgv_No,
            this.dgv_Name,
            this.dgv_Amount,
            this.dgv_Unit,
            this.dgv_PriceWOVAT});
            this.dgvInvFilling.Location = new System.Drawing.Point(1167, 31);
            this.dgvInvFilling.Name = "dgvInvFilling";
            this.dgvInvFilling.ReadOnly = true;
            this.dgvInvFilling.RowHeadersVisible = false;
            this.dgvInvFilling.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvInvFilling.Size = new System.Drawing.Size(981, 924);
            this.dgvInvFilling.TabIndex = 77;
            this.dgvInvFilling.Visible = false;
            // 
            // btnImportInvFilling
            // 
            this.btnImportInvFilling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportInvFilling.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportInvFilling.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnImportInvFilling.FlatAppearance.BorderSize = 0;
            this.btnImportInvFilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportInvFilling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnImportInvFilling.Image = global::SmuOk.Properties.Resources.open;
            this.btnImportInvFilling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportInvFilling.Location = new System.Drawing.Point(1841, 961);
            this.btnImportInvFilling.Name = "btnImportInvFilling";
            this.btnImportInvFilling.Size = new System.Drawing.Size(145, 23);
            this.btnImportInvFilling.TabIndex = 78;
            this.btnImportInvFilling.Text = "Обновить наполнение";
            this.btnImportInvFilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportInvFilling.UseVisualStyleBackColor = true;
            this.btnImportInvFilling.Visible = false;
            // 
            // btnExportInvFilling
            // 
            this.btnExportInvFilling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportInvFilling.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportInvFilling.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnExportInvFilling.FlatAppearance.BorderSize = 0;
            this.btnExportInvFilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportInvFilling.ForeColor = System.Drawing.Color.Green;
            this.btnExportInvFilling.Image = global::SmuOk.Properties.Resources.report_excel;
            this.btnExportInvFilling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportInvFilling.Location = new System.Drawing.Point(1992, 961);
            this.btnExportInvFilling.Name = "btnExportInvFilling";
            this.btnExportInvFilling.Size = new System.Drawing.Size(156, 23);
            this.btnExportInvFilling.TabIndex = 79;
            this.btnExportInvFilling.Text = "Выгрузить наполнение";
            this.btnExportInvFilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportInvFilling.UseVisualStyleBackColor = true;
            this.btnExportInvFilling.Visible = false;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "0";
            this.dataGridViewImageColumn2.Image = global::SmuOk.Properties.Resources.shared;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Visible = false;
            this.dataGridViewImageColumn2.Width = 28;
            // 
            // dgv_InvPosId
            // 
            this.dgv_InvPosId.DataPropertyName = "InvPosId";
            this.dgv_InvPosId.HeaderText = "Id позиции счета";
            this.dgv_InvPosId.Name = "dgv_InvPosId";
            this.dgv_InvPosId.ReadOnly = true;
            // 
            // dgv_No
            // 
            this.dgv_No.DataPropertyName = "No";
            this.dgv_No.HeaderText = "№ п/п";
            this.dgv_No.Name = "dgv_No";
            this.dgv_No.ReadOnly = true;
            // 
            // dgv_Name
            // 
            this.dgv_Name.DataPropertyName = "Name";
            this.dgv_Name.HeaderText = "Наименование";
            this.dgv_Name.Name = "dgv_Name";
            this.dgv_Name.ReadOnly = true;
            // 
            // dgv_Amount
            // 
            this.dgv_Amount.DataPropertyName = "Amount";
            this.dgv_Amount.HeaderText = "Количество";
            this.dgv_Amount.Name = "dgv_Amount";
            this.dgv_Amount.ReadOnly = true;
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.DataPropertyName = "Unit";
            this.dgv_Unit.HeaderText = "Ед. изм.";
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.ReadOnly = true;
            // 
            // dgv_PriceWOVAT
            // 
            this.dgv_PriceWOVAT.DataPropertyName = "PriceWOVAT";
            this.dgv_PriceWOVAT.HeaderText = "Цена за ед. без НДС";
            this.dgv_PriceWOVAT.Name = "dgv_PriceWOVAT";
            this.dgv_PriceWOVAT.ReadOnly = true;
            // 
            // InvDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnImportInvFilling);
            this.Controls.Add(this.btnExportInvFilling);
            this.Controls.Add(this.dgvInvFilling);
            this.Controls.Add(this.deleteOrder_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OrderId);
            this.Controls.Add(this.txtSpecNameFilter);
            this.Controls.Add(this.lblPb);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvBudg);
            this.Name = "InvDoc";
            this.Size = new System.Drawing.Size(2152, 987);
            this.Load += new System.EventHandler(this.Budg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvFilling)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBudg;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblPb;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.TextBox txtSpecNameFilter;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TextBox OrderId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteOrder_btn;
        private System.Windows.Forms.DataGridViewImageColumn dgv_btn_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvINN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvLegalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvSumWOVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvSumWithVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvSumFinished;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvComment;
        private System.Windows.Forms.DataGridView dgvInvFilling;
        private System.Windows.Forms.Button btnImportInvFilling;
        private System.Windows.Forms.Button btnExportInvFilling;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvPosId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PriceWOVAT;
    }
}
