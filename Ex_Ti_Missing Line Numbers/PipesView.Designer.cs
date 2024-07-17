
namespace Ex_Ti_Missing_Line_Numbers
{
    partial class PipesView
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
            this.lst_ListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lst_ListView
            // 
            this.lst_ListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lst_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_ListView.HideSelection = false;
            this.lst_ListView.HoverSelection = true;
            this.lst_ListView.Location = new System.Drawing.Point(0, 0);
            this.lst_ListView.Name = "lst_ListView";
            this.lst_ListView.Size = new System.Drawing.Size(391, 349);
            this.lst_ListView.TabIndex = 0;
            this.lst_ListView.UseCompatibleStateImageBehavior = false;
            this.lst_ListView.View = System.Windows.Forms.View.Details;
            this.lst_ListView.SelectedIndexChanged += new System.EventHandler(this.lst_ListView_SelectedIndexChanged);
            // 
            // PipesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 349);
            this.Controls.Add(this.lst_ListView);
            this.Name = "PipesView";
            this.Text = "PipesView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lst_ListView;
    }
}