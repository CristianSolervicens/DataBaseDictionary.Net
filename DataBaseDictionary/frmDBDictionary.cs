using DataBaseDictionary.helpers;
using ModelLibrary;
using System.Timers;
using System.Windows.Forms;


namespace DataBaseDictionary
{
    public partial class frmDBDictionary : Form
    {

        public MySql hsql = new();
        private ToolTip ttip = new ToolTip();
        private static System.Timers.Timer LoadTimer;
        config cfg;
        public frmDBDictionary()
        {
            InitializeComponent();
            btGenerateDocument.Enabled = false;
        }


        private void ShowImage(string path)
        {
            try
            {
                picBoxLogo.ImageLocation = txtLogo.Text;
            }
            catch
            {
                return;
            }

            picBoxLogo.Show();

            if (picBoxLogo.Image == null)
                return;

            var imageSize = picBoxLogo.Image.Size;
            var fitSize = picBoxLogo.ClientSize;

            picBoxLogo.SizeMode = imageSize.Width > fitSize.Width || imageSize.Height > fitSize.Height ?
                PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
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
            if (!chkDeprecado.Checked)
                status.Add("DEPRECADO");
            if (!chkEnDesuso.Checked)
                status.Add("EN DESUSO");
            if (!chkInterna.Checked)
                status.Add("INTERNA");
            if (!chkRespaldo.Checked)
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

            ShowImage(txtLogo.Text);
        }

        private void frmDBDictionary_Load(object sender, EventArgs e)
        {
            cfg = new config();
            cfg = config.LoadFromJson();
            if (cfg.server != "")
                txtServer.Text = cfg.server;
            if (cfg.user != "")
                txtUser.Text = cfg.user;
            if (cfg.password != "")
                txtPassword.Text = cfg.password;
            if (cfg.logo_path != "")
                txtLogo.Text = cfg.logo_path;


            ShowImage(txtLogo.Text);
            LoadTimer = new System.Timers.Timer(300);
            LoadTimer.Elapsed += OnTimedEvent;
            LoadTimer.Start();
        }

        private void OnTimedEvent(Object sourtce, ElapsedEventArgs e)
        {
            LoadTimer.Stop();
            LoadTimer.Enabled = false;
            LoadTimer.Dispose();
            ShowImage(txtLogo.Text);
        }

        private void frmDBDictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtServer.Text != "")
                cfg.server = txtServer.Text;
            if (txtUser.Text != "")
                cfg.user = txtUser.Text;
            if (txtPassword.Text != "")
                cfg.password = txtPassword.Text;
            if (txtLogo.Text != "")
                cfg.logo_path = txtLogo.Text;
            cfg.SaveToJson();
        }
    }
}
