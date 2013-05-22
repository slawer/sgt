namespace SGT
{
    partial class WorksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorksForm));
            this.treeViewWorks = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.InsertNewProject = new System.Windows.Forms.Button();
            this.EditProject = new System.Windows.Forms.Button();
            this.OpenProject = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeViewWorks
            // 
            this.treeViewWorks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewWorks.HideSelection = false;
            this.treeViewWorks.ImageIndex = 0;
            this.treeViewWorks.ImageList = this.imageList;
            this.treeViewWorks.Location = new System.Drawing.Point(12, 12);
            this.treeViewWorks.Name = "treeViewWorks";
            this.treeViewWorks.SelectedImageIndex = 0;
            this.treeViewWorks.Size = new System.Drawing.Size(450, 475);
            this.treeViewWorks.TabIndex = 0;
            this.treeViewWorks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWorks_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "NewDocuments_32x32.png");
            this.imageList.Images.SetKeyName(1, "NewDocuments_32x32.png");
            this.imageList.Images.SetKeyName(2, "NewDocuments_32x32.png");
            this.imageList.Images.SetKeyName(3, "Page.png");
            // 
            // InsertNewProject
            // 
            this.InsertNewProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InsertNewProject.Location = new System.Drawing.Point(12, 493);
            this.InsertNewProject.Name = "InsertNewProject";
            this.InsertNewProject.Size = new System.Drawing.Size(146, 31);
            this.InsertNewProject.TabIndex = 8;
            this.InsertNewProject.Text = "Добавить задание";
            this.InsertNewProject.UseVisualStyleBackColor = true;
            this.InsertNewProject.Click += new System.EventHandler(this.InsertNewProject_Click);
            // 
            // EditProject
            // 
            this.EditProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditProject.Location = new System.Drawing.Point(164, 493);
            this.EditProject.Name = "EditProject";
            this.EditProject.Size = new System.Drawing.Size(146, 31);
            this.EditProject.TabIndex = 6;
            this.EditProject.Text = "Редактировать задание";
            this.EditProject.UseVisualStyleBackColor = true;
            this.EditProject.Click += new System.EventHandler(this.EditProject_Click);
            // 
            // OpenProject
            // 
            this.OpenProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenProject.Location = new System.Drawing.Point(316, 493);
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Size = new System.Drawing.Size(146, 31);
            this.OpenProject.TabIndex = 5;
            this.OpenProject.Text = "Открыть задание";
            this.OpenProject.UseVisualStyleBackColor = true;
            this.OpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(468, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(317, 437);
            this.textBox1.TabIndex = 9;
            this.textBox1.TabStop = false;
            // 
            // WorksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 536);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.InsertNewProject);
            this.Controls.Add(this.EditProject);
            this.Controls.Add(this.OpenProject);
            this.Controls.Add(this.treeViewWorks);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorksForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Задания";
            this.Load += new System.EventHandler(this.WorksForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewWorks;
        private System.Windows.Forms.Button InsertNewProject;
        private System.Windows.Forms.Button EditProject;
        private System.Windows.Forms.Button OpenProject;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TextBox textBox1;
    }
}