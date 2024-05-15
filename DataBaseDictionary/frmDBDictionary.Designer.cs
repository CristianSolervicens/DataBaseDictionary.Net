namespace DataBaseDictionary
{
    partial class frmDBDictionary
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBDictionary));
            txtServer = new TextBox();
            btConnect = new Button();
            txtUser = new TextBox();
            txtPassword = new TextBox();
            laDatabases = new Label();
            label1 = new Label();
            label2 = new Label();
            btGenerateDocument = new Button();
            label3 = new Label();
            chkNoProcedures = new CheckBox();
            chkNoFunctions = new CheckBox();
            chkNoViews = new CheckBox();
            chkNoTables = new CheckBox();
            chkDeprecado = new CheckBox();
            chkEnDesuso = new CheckBox();
            chkInterna = new CheckBox();
            chkRespaldo = new CheckBox();
            chkOnlyWithComments = new CheckBox();
            label4 = new Label();
            txtIndexColumns = new TextBox();
            lsDatabases = new ListBox();
            label5 = new Label();
            txtTag = new TextBox();
            label6 = new Label();
            txtLogo = new TextBox();
            btGetLogo = new Button();
            label7 = new Label();
            txtPixels = new TextBox();
            laStatus = new TextBox();
            SuspendLayout();
            // 
            // txtServer
            // 
            txtServer.Location = new Point(12, 42);
            txtServer.Name = "txtServer";
            txtServer.PlaceholderText = "Database Server";
            txtServer.Size = new Size(223, 23);
            txtServer.TabIndex = 0;
            // 
            // btConnect
            // 
            btConnect.Location = new Point(12, 331);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(190, 29);
            btConnect.TabIndex = 3;
            btConnect.Text = "Connect To Database";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(12, 76);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Database User";
            txtUser.Size = new Size(223, 23);
            txtUser.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(12, 110);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Database Password";
            txtPassword.Size = new Size(223, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // laDatabases
            // 
            laDatabases.AutoSize = true;
            laDatabases.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            laDatabases.Location = new Point(258, 11);
            laDatabases.Name = "laDatabases";
            laDatabases.Size = new Size(116, 15);
            laDatabases.TabIndex = 8;
            laDatabases.Text = "Available Databases";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(124, 15);
            label1.TabIndex = 9;
            label1.Text = "Database Connection";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(608, 11);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 5;
            label2.Text = "Output Options";
            // 
            // btGenerateDocument
            // 
            btGenerateDocument.Location = new Point(12, 369);
            btGenerateDocument.Name = "btGenerateDocument";
            btGenerateDocument.Size = new Size(190, 29);
            btGenerateDocument.TabIndex = 18;
            btGenerateDocument.Text = "Generate Documentation";
            btGenerateDocument.UseVisualStyleBackColor = true;
            btGenerateDocument.Click += btGenerateDocument_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 304);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 12;
            label3.Text = "Actions";
            // 
            // chkNoProcedures
            // 
            chkNoProcedures.AutoSize = true;
            chkNoProcedures.Checked = true;
            chkNoProcedures.CheckState = CheckState.Checked;
            chkNoProcedures.Location = new Point(607, 38);
            chkNoProcedures.Name = "chkNoProcedures";
            chkNoProcedures.Size = new Size(104, 19);
            chkNoProcedures.TabIndex = 6;
            chkNoProcedures.Text = "No Procedures";
            chkNoProcedures.UseVisualStyleBackColor = true;
            // 
            // chkNoFunctions
            // 
            chkNoFunctions.AutoSize = true;
            chkNoFunctions.Checked = true;
            chkNoFunctions.CheckState = CheckState.Checked;
            chkNoFunctions.Location = new Point(607, 70);
            chkNoFunctions.Name = "chkNoFunctions";
            chkNoFunctions.Size = new Size(97, 19);
            chkNoFunctions.TabIndex = 7;
            chkNoFunctions.Text = "No Functions";
            chkNoFunctions.UseVisualStyleBackColor = true;
            // 
            // chkNoViews
            // 
            chkNoViews.AutoSize = true;
            chkNoViews.Checked = true;
            chkNoViews.CheckState = CheckState.Checked;
            chkNoViews.Location = new Point(607, 103);
            chkNoViews.Name = "chkNoViews";
            chkNoViews.Size = new Size(75, 19);
            chkNoViews.TabIndex = 8;
            chkNoViews.Text = "No Views";
            chkNoViews.UseVisualStyleBackColor = true;
            // 
            // chkNoTables
            // 
            chkNoTables.AutoSize = true;
            chkNoTables.Location = new Point(607, 137);
            chkNoTables.Name = "chkNoTables";
            chkNoTables.Size = new Size(77, 19);
            chkNoTables.TabIndex = 9;
            chkNoTables.Text = "No Tables";
            chkNoTables.UseVisualStyleBackColor = true;
            // 
            // chkDeprecado
            // 
            chkDeprecado.AutoSize = true;
            chkDeprecado.Location = new Point(607, 170);
            chkDeprecado.Name = "chkDeprecado";
            chkDeprecado.Size = new Size(83, 19);
            chkDeprecado.TabIndex = 10;
            chkDeprecado.Text = "Deprecado";
            chkDeprecado.UseVisualStyleBackColor = true;
            // 
            // chkEnDesuso
            // 
            chkEnDesuso.AutoSize = true;
            chkEnDesuso.Location = new Point(607, 202);
            chkEnDesuso.Name = "chkEnDesuso";
            chkEnDesuso.Size = new Size(80, 19);
            chkEnDesuso.TabIndex = 11;
            chkEnDesuso.Text = "En Desuso";
            chkEnDesuso.UseVisualStyleBackColor = true;
            // 
            // chkInterna
            // 
            chkInterna.AutoSize = true;
            chkInterna.Location = new Point(607, 233);
            chkInterna.Name = "chkInterna";
            chkInterna.Size = new Size(63, 19);
            chkInterna.TabIndex = 12;
            chkInterna.Text = "Interna";
            chkInterna.UseVisualStyleBackColor = true;
            // 
            // chkRespaldo
            // 
            chkRespaldo.AutoSize = true;
            chkRespaldo.Location = new Point(607, 267);
            chkRespaldo.Name = "chkRespaldo";
            chkRespaldo.Size = new Size(74, 19);
            chkRespaldo.TabIndex = 13;
            chkRespaldo.Text = "Respaldo";
            chkRespaldo.UseVisualStyleBackColor = true;
            // 
            // chkOnlyWithComments
            // 
            chkOnlyWithComments.AutoSize = true;
            chkOnlyWithComments.Checked = true;
            chkOnlyWithComments.CheckState = CheckState.Checked;
            chkOnlyWithComments.Location = new Point(607, 295);
            chkOnlyWithComments.Name = "chkOnlyWithComments";
            chkOnlyWithComments.Size = new Size(143, 19);
            chkOnlyWithComments.TabIndex = 15;
            chkOnlyWithComments.Text = "Sólo con Comentarios";
            chkOnlyWithComments.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(607, 340);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 16;
            label4.Text = "Index Columns";
            // 
            // txtIndexColumns
            // 
            txtIndexColumns.Location = new Point(718, 337);
            txtIndexColumns.Name = "txtIndexColumns";
            txtIndexColumns.Size = new Size(35, 23);
            txtIndexColumns.TabIndex = 17;
            txtIndexColumns.Text = "3";
            // 
            // lsDatabases
            // 
            lsDatabases.FormattingEnabled = true;
            lsDatabases.ItemHeight = 15;
            lsDatabases.Location = new Point(262, 34);
            lsDatabases.Name = "lsDatabases";
            lsDatabases.Size = new Size(327, 364);
            lsDatabases.TabIndex = 4;
            lsDatabases.SelectedIndexChanged += lsDatabases_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(607, 372);
            label5.Name = "label5";
            label5.Size = new Size(25, 15);
            label5.TabIndex = 27;
            label5.Text = "Tag";
            // 
            // txtTag
            // 
            txtTag.Location = new Point(649, 373);
            txtTag.Name = "txtTag";
            txtTag.Size = new Size(135, 23);
            txtTag.TabIndex = 28;
            txtTag.Text = "MS_Description";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 167);
            label6.Name = "label6";
            label6.Size = new Size(34, 15);
            label6.TabIndex = 29;
            label6.Text = "Logo";
            // 
            // txtLogo
            // 
            txtLogo.Location = new Point(12, 195);
            txtLogo.Name = "txtLogo";
            txtLogo.Size = new Size(217, 23);
            txtLogo.TabIndex = 30;
            txtLogo.Text = "FrogFull.png";
            // 
            // btGetLogo
            // 
            btGetLogo.Location = new Point(232, 194);
            btGetLogo.Name = "btGetLogo";
            btGetLogo.Size = new Size(25, 25);
            btGetLogo.TabIndex = 31;
            btGetLogo.Text = "...";
            btGetLogo.UseVisualStyleBackColor = true;
            btGetLogo.Click += btGetLogo_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 231);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 32;
            label7.Text = "Pixels Width";
            // 
            // txtPixels
            // 
            txtPixels.Location = new Point(148, 227);
            txtPixels.Name = "txtPixels";
            txtPixels.Size = new Size(81, 23);
            txtPixels.TabIndex = 33;
            txtPixels.Text = "80";
            // 
            // laStatus
            // 
            laStatus.Enabled = false;
            laStatus.Location = new Point(6, 410);
            laStatus.Name = "laStatus";
            laStatus.Size = new Size(788, 23);
            laStatus.TabIndex = 35;
            laStatus.Text = "Status...";
            // 
            // frmDBDictionary
            // 
            ClientSize = new Size(801, 439);
            Controls.Add(laStatus);
            Controls.Add(txtPixels);
            Controls.Add(label7);
            Controls.Add(btGetLogo);
            Controls.Add(txtLogo);
            Controls.Add(label6);
            Controls.Add(txtTag);
            Controls.Add(label5);
            Controls.Add(lsDatabases);
            Controls.Add(txtIndexColumns);
            Controls.Add(label4);
            Controls.Add(chkOnlyWithComments);
            Controls.Add(chkRespaldo);
            Controls.Add(chkInterna);
            Controls.Add(chkEnDesuso);
            Controls.Add(chkDeprecado);
            Controls.Add(chkNoTables);
            Controls.Add(chkNoViews);
            Controls.Add(chkNoFunctions);
            Controls.Add(chkNoProcedures);
            Controls.Add(label3);
            Controls.Add(btGenerateDocument);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(laDatabases);
            Controls.Add(txtPassword);
            Controls.Add(txtUser);
            Controls.Add(btConnect);
            Controls.Add(txtServer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmDBDictionary";
            Text = "DB Dictionary";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtServer;
        private Button btConnect;
        private TextBox txtUser;
        private TextBox txtPassword;
        private Label laDatabases;
        private Label label1;
        private Label label2;
        private Button btGenerateDocument;
        private Label label3;
        private CheckBox chkNoProcedures;
        private CheckBox chkNoFunctions;
        private CheckBox chkNoViews;
        private CheckBox chkNoTables;
        private CheckBox chkDeprecado;
        private CheckBox chkEnDesuso;
        private CheckBox chkInterna;
        private CheckBox chkRespaldo;
        private CheckBox chkOnlyWithComments;
        private Label label4;
        private TextBox txtIndexColumns;
        private ListBox lsDatabases;
        private Label label5;
        private TextBox txtTag;
        private Label label6;
        private TextBox txtLogo;
        private Button btGetLogo;
        private Label label7;
        private TextBox txtPixels;
        private TextBox laStatus;
    }
}
