namespace PipeSorting
{
    partial class PipeSortingView
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
            this.tb_LineNumberText = new System.Windows.Forms.TextBox();
            this.btn_Sort = new System.Windows.Forms.Button();
            this.lst_ElementIds = new System.Windows.Forms.ListView();
            this.lbl_PipeIds = new System.Windows.Forms.Label();
            this.btn_findAll = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tb_LineNumberText
            // 
            this.tb_LineNumberText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LineNumberText.Location = new System.Drawing.Point(17, 12);
            this.tb_LineNumberText.Name = "tb_LineNumberText";
            this.tb_LineNumberText.Size = new System.Drawing.Size(249, 26);
            this.tb_LineNumberText.TabIndex = 0;
            this.tb_LineNumberText.TextChanged += new System.EventHandler(this.tb_LineNumberText_TextChanged);
            // 
            // btn_Sort
            // 
            this.btn_Sort.Location = new System.Drawing.Point(267, 15);
            this.btn_Sort.Name = "btn_Sort";
            this.btn_Sort.Size = new System.Drawing.Size(75, 23);
            this.btn_Sort.TabIndex = 1;
            this.btn_Sort.Text = "Sort";
            this.btn_Sort.UseVisualStyleBackColor = true;
            this.btn_Sort.Click += new System.EventHandler(this.btn_Sort_Click);
            // 
            // lst_ElementIds
            // 
            this.lst_ElementIds.HideSelection = false;
            this.lst_ElementIds.Location = new System.Drawing.Point(12, 70);
            this.lst_ElementIds.Name = "lst_ElementIds";
            this.lst_ElementIds.Size = new System.Drawing.Size(430, 245);
            this.lst_ElementIds.TabIndex = 2;
            this.lst_ElementIds.UseCompatibleStateImageBehavior = false;
            this.lst_ElementIds.View = System.Windows.Forms.View.List;
            this.lst_ElementIds.SelectedIndexChanged += new System.EventHandler(this.lst_ElementIds_SelectedIndexChanged);
            // 
            // lbl_PipeIds
            // 
            this.lbl_PipeIds.AutoSize = true;
            this.lbl_PipeIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PipeIds.Location = new System.Drawing.Point(13, 47);
            this.lbl_PipeIds.Name = "lbl_PipeIds";
            this.lbl_PipeIds.Size = new System.Drawing.Size(118, 20);
            this.lbl_PipeIds.TabIndex = 3;
            this.lbl_PipeIds.Text = "Sorted Pipe Ids";
            // 
            // btn_findAll
            // 
            this.btn_findAll.Enabled = false;
            this.btn_findAll.Location = new System.Drawing.Point(348, 15);
            this.btn_findAll.Name = "btn_findAll";
            this.btn_findAll.Size = new System.Drawing.Size(75, 23);
            this.btn_findAll.TabIndex = 4;
            this.btn_findAll.Text = "Find All";
            this.btn_findAll.UseVisualStyleBackColor = true;
            this.btn_findAll.Click += new System.EventHandler(this.btn_findAll_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(429, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PipeSortingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 331);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btn_findAll);
            this.Controls.Add(this.lbl_PipeIds);
            this.Controls.Add(this.lst_ElementIds);
            this.Controls.Add(this.btn_Sort);
            this.Controls.Add(this.tb_LineNumberText);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(470, 370);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(470, 370);
            this.Name = "PipeSortingView";
            this.Text = "Pipe Sorting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_LineNumberText;
        private System.Windows.Forms.Button btn_Sort;
        private System.Windows.Forms.ListView lst_ElementIds;
        private System.Windows.Forms.Label lbl_PipeIds;
        private System.Windows.Forms.Button btn_findAll;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}