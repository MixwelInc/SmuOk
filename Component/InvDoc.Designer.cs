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
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_InvDocPosId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_No1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_No2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_PriceWOVAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_TotalSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addDoc_btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBudg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvFilling)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBudg
            // 
            this.dgvBudg.AllowUserToAddRows = false;
            this.dgvBudg.AllowUserToDeleteRows = false;
            this.dgvBudg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.dgvBudg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBudg.Size = new System.Drawing.Size(1014, 925);
            this.dgvBudg.TabIndex = 1;
            this.dgvBudg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBudg_CellClick);
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
            this.btnImport.Location = new System.Drawing.Point(1965, 961);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(85, 23);
            this.btnImport.TabIndex = 32;
            this.btnImport.Text = "Обновить";
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
            this.btnExport.Location = new System.Drawing.Point(2056, 961);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(92, 23);
            this.btnExport.TabIndex = 33;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // OrderId
            // 
            this.OrderId.Location = new System.Drawing.Point(837, 4);
            this.OrderId.Name = "OrderId";
            this.OrderId.Size = new System.Drawing.Size(100, 20);
            this.OrderId.TabIndex = 74;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(738, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Удаление счета";
            // 
            // deleteOrder_btn
            // 
            this.deleteOrder_btn.Location = new System.Drawing.Point(943, 3);
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
            this.dgv_InvDocPosId,
            this.dgv_No1,
            this.dgv_No2,
            this.dgv_Name,
            this.dgv_Unit,
            this.dgv_Amount,
            this.dgv_PriceWOVAT,
            this.dgv_Price,
            this.dgv_TotalSum});
            this.dgvInvFilling.Location = new System.Drawing.Point(1160, 31);
            this.dgvInvFilling.Name = "dgvInvFilling";
            this.dgvInvFilling.ReadOnly = true;
            this.dgvInvFilling.RowHeadersVisible = false;
            this.dgvInvFilling.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvInvFilling.Size = new System.Drawing.Size(988, 924);
            this.dgvInvFilling.TabIndex = 77;
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
            // dgv_InvDocPosId
            // 
            this.dgv_InvDocPosId.DataPropertyName = "InvDocPosId";
            this.dgv_InvDocPosId.HeaderText = "Id позиции счета";
            this.dgv_InvDocPosId.Name = "dgv_InvDocPosId";
            this.dgv_InvDocPosId.ReadOnly = true;
            // 
            // dgv_No1
            // 
            this.dgv_No1.DataPropertyName = "No1";
            this.dgv_No1.HeaderText = "№ п/п";
            this.dgv_No1.Name = "dgv_No1";
            this.dgv_No1.ReadOnly = true;
            // 
            // dgv_No2
            // 
            this.dgv_No2.DataPropertyName = "No2";
            this.dgv_No2.HeaderText = "№ п/п 2";
            this.dgv_No2.Name = "dgv_No2";
            this.dgv_No2.ReadOnly = true;
            // 
            // dgv_Name
            // 
            this.dgv_Name.DataPropertyName = "Name";
            this.dgv_Name.HeaderText = "Наименование";
            this.dgv_Name.Name = "dgv_Name";
            this.dgv_Name.ReadOnly = true;
            // 
            // dgv_Unit
            // 
            this.dgv_Unit.DataPropertyName = "Unit";
            this.dgv_Unit.HeaderText = "Ед. изм.";
            this.dgv_Unit.Name = "dgv_Unit";
            this.dgv_Unit.ReadOnly = true;
            // 
            // dgv_Amount
            // 
            this.dgv_Amount.DataPropertyName = "Amount";
            this.dgv_Amount.HeaderText = "Количество";
            this.dgv_Amount.Name = "dgv_Amount";
            this.dgv_Amount.ReadOnly = true;
            // 
            // dgv_PriceWOVAT
            // 
            this.dgv_PriceWOVAT.DataPropertyName = "PriceWOVAT";
            this.dgv_PriceWOVAT.HeaderText = "Цена за ед. без НДС";
            this.dgv_PriceWOVAT.Name = "dgv_PriceWOVAT";
            this.dgv_PriceWOVAT.ReadOnly = true;
            // 
            // dgv_Price
            // 
            this.dgv_Price.DataPropertyName = "Price";
            this.dgv_Price.HeaderText = "Цена за ед. с НДС";
            this.dgv_Price.Name = "dgv_Price";
            this.dgv_Price.ReadOnly = true;
            // 
            // dgv_TotalSum
            // 
            this.dgv_TotalSum.DataPropertyName = "TotalSum";
            this.dgv_TotalSum.HeaderText = "Сумма с НДС";
            this.dgv_TotalSum.Name = "dgv_TotalSum";
            this.dgv_TotalSum.ReadOnly = true;
            // 
            // addDoc_btn
            // 
            this.addDoc_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addDoc_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addDoc_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.addDoc_btn.FlatAppearance.BorderSize = 0;
            this.addDoc_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDoc_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.addDoc_btn.Image = global::SmuOk.Properties.Resources.plus;
            this.addDoc_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addDoc_btn.Location = new System.Drawing.Point(880, 961);
            this.addDoc_btn.Name = "addDoc_btn";
            this.addDoc_btn.Size = new System.Drawing.Size(137, 23);
            this.addDoc_btn.TabIndex = 78;
            this.addDoc_btn.Text = "Создать новый счет";
            this.addDoc_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addDoc_btn.UseVisualStyleBackColor = true;
            this.addDoc_btn.Click += new System.EventHandler(this.addDoc_btn_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button2.Image = global::SmuOk.Properties.Resources.save;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1691, 961);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(258, 23);
            this.button2.TabIndex = 82;
            this.button2.Text = "Счет разнесен, остаток считать свободным";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InvDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.addDoc_btn);
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
        private System.Windows.Forms.Button addDoc_btn;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_InvDocPosId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_No1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_No2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_PriceWOVAT;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_TotalSum;
        private System.Windows.Forms.Button button2;
    }
}
