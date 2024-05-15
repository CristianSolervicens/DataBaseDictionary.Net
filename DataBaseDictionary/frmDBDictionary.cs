using ModelLibrary;


namespace DataBaseDictionary
{
    public partial class frmDBDictionary : Form
    {

        public MySql hsql = new();
        private ToolTip ttip = new ToolTip();
        public frmDBDictionary()
        {
            InitializeComponent();
            btGenerateDocument.Enabled = false;

        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            var connectionString = MySql.SQLServerConnectionString(txtServer.Text, "master", txtUser.Text, txtPassword.Text, false);
            hsql.ConnectionString = connectionString;
            hsql.ConnectToDB();
            if (!hsql.ConnectionStatus())
                MessageBox.Show("No se pudo conectar con la Base de Datos", "Error");
            LoadDatabases();
        }

        public void LoadDatabases()
        {
            lsDatabases.Items.Clear();
            var databases = hsql.GetDatabases();
            foreach (var database in databases)
            {
                lsDatabases.Items.Add(database);
            }
        }

        private void lsDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsDatabases.SelectedIndex != -1)
            {
                btGenerateDocument.Enabled = true;
            }
        }


        private async void btGenerateDocument_Click(object sender, EventArgs e)
        {
            laStatus.Text = "Running... please wait...";
            this.Refresh();

            string selectedDB = lsDatabases.Text;

            var connectionString = MySql.SQLServerConnectionString(txtServer.Text, "master", txtUser.Text, txtPassword.Text, false);

            IList<string> status = new List<string>();
            if ( ! chkDeprecado.Checked)
                status.Add("DEPRECADO");
            if ( ! chkEnDesuso.Checked)
                status.Add("EN DESUSO");
            if ( ! chkInterna.Checked)
                status.Add("INTERNA");
            if ( ! chkRespaldo.Checked)
                status.Add("RESPALDO");


            Dictionary<string, bool> opciones = new();
            opciones.Add("no_tables", chkNoTables.Checked);
            opciones.Add("no_views", chkNoViews.Checked);
            opciones.Add("no_procs", chkNoProcedures.Checked);
            opciones.Add("no_functions", chkNoFunctions.Checked);
            opciones.Add("solo_con_comentarios", chkOnlyWithComments.Checked);

            string image_extension = "";
            string image_data = "";
            string image_width = txtPixels.Text;
            string index_column_class = "";

            switch (txtIndexColumns.Text)
            {
                case "1":
                    index_column_class = "uni-column";
                    break;
                case "2":
                    index_column_class = "bi-column";
                    break;
                case "3":
                    index_column_class = "tri-column";
                    break;
                case "4":
                    index_column_class = "cuad-column";
                    break;
                default:
                    index_column_class = "tri-column";
                    break;
            }

            //Carga Logo en las variables correspondientes
            if (File.Exists(txtLogo.Text))
            {
                image_extension = Path.GetExtension(txtLogo.Text);
                image_data = Convert.ToBase64String(File.ReadAllBytes(txtLogo.Text));
            }

            var doc = new Documentor(connectionString, status, opciones, selectedDB, txtTag.Text, image_extension, image_data, image_width, index_column_class);
            string fileName = $"ModeloDeDato_{selectedDB}.html";

            var documento = await doc.GenerateDocumentation();
            
            File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\ModeloDeDato_{selectedDB}.html", documento);

            laStatus.Text = $"Archivo Generado!  [MisDocumentos\\{fileName}]";
            Console.Beep();
        }


        private void btGetLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Logo Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            ofd.FilterIndex = 1;
            ofd.FileName = txtLogo.Text;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtLogo.Text = ofd.FileName;
            }
            else
            {
                txtLogo.Text = "";
            }

            ttip.SetToolTip(txtLogo, txtLogo.Text);
        }
    }
}
