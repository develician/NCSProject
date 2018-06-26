namespace BookManager
{
    partial class BookUpdateForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isbnTextBox = new System.Windows.Forms.TextBox();
            this.bookNameTextBox = new System.Windows.Forms.TextBox();
            this.publisherTextBox = new System.Windows.Forms.TextBox();
            this.pageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.updateButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pageTextBox);
            this.groupBox1.Controls.Add(this.publisherTextBox);
            this.groupBox1.Controls.Add(this.bookNameTextBox);
            this.groupBox1.Controls.Add(this.isbnTextBox);
            this.groupBox1.Location = new System.Drawing.Point(22, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 393);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "책 수정";
            // 
            // isbnTextBox
            // 
            this.isbnTextBox.Location = new System.Drawing.Point(55, 67);
            this.isbnTextBox.Name = "isbnTextBox";
            this.isbnTextBox.Size = new System.Drawing.Size(642, 21);
            this.isbnTextBox.TabIndex = 0;
            // 
            // bookNameTextBox
            // 
            this.bookNameTextBox.Location = new System.Drawing.Point(55, 137);
            this.bookNameTextBox.Name = "bookNameTextBox";
            this.bookNameTextBox.Size = new System.Drawing.Size(642, 21);
            this.bookNameTextBox.TabIndex = 1;
            // 
            // publisherTextBox
            // 
            this.publisherTextBox.Location = new System.Drawing.Point(55, 200);
            this.publisherTextBox.Name = "publisherTextBox";
            this.publisherTextBox.Size = new System.Drawing.Size(642, 21);
            this.publisherTextBox.TabIndex = 2;
            // 
            // pageTextBox
            // 
            this.pageTextBox.Location = new System.Drawing.Point(55, 265);
            this.pageTextBox.Name = "pageTextBox";
            this.pageTextBox.Size = new System.Drawing.Size(642, 21);
            this.pageTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "isbn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "도서이름";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "출판사";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "페이지";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(328, 330);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 8;
            this.updateButton.Text = "수정하기";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // BookUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "BookUpdateForm";
            this.Text = "BookUpdateForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox bookNameTextBox;
        private System.Windows.Forms.TextBox isbnTextBox;
        private System.Windows.Forms.TextBox pageTextBox;
        private System.Windows.Forms.TextBox publisherTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button updateButton;
    }
}