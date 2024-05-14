using Razor.Templating.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.workers;
using ModelLibrary.DBObjects;
using System.Timers;


namespace ModelLibrary
{
    public class Documentor
    {
        internal IList<string> Status { get; set; }
        internal Dictionary<string, bool> Opciones { get; set; }
        internal string DbName { get; set; }
        internal string Tag { get; set; }
        internal string ConnectionStrinig {  get; set; }
        internal string ImageExtension { get; set; }
        internal string ImageData {  get; set; }
        internal string ImageWidth { get; set; }
        internal string IndexColumnClass { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hsql"></param>
        /// <param name="status"></param>
        /// <param name="opciones"></param>
        /// <param name="selectedDB"></param>
        public Documentor(string connectionString,
            IList<string> status,
            Dictionary<string, bool> opciones,
            string selectedDB,
            string tag,
            string image_extension,
            string image_data,
            string image_width,
            string index_column_class
        )
        {
            this.ConnectionStrinig = connectionString;
            this.Status = status;
            this.Opciones = opciones;
            this.DbName = selectedDB;
            this.Tag = tag;
            this.ImageExtension = image_extension;
            this.ImageData = image_data;
            this.ImageWidth = image_width;
            this.IndexColumnClass = index_column_class;
        }


        /// <summary>
        /// Realiza el Proceso de Generación de la Documentación
        /// </summary>
        /// <returns></returns>
        public async Task<string> GenerateDocumentation()
        {

            TableWorker tableWorker = new(this.ConnectionStrinig, this.DbName, this.Tag);
            var tables = await tableWorker.worker();

            ViewWorker viewWorker = new(this.ConnectionStrinig,this.DbName, this.Tag);
            var views = await viewWorker.worker();

            ProcWorker procWorker = new(this.ConnectionStrinig, this.DbName, this.Tag);
            var procs = await procWorker.Worker();

            ScalarFuncWorker scalarFuncWorker = new(this.ConnectionStrinig, this.DbName, this.Tag);
            var scalarFuncs = await scalarFuncWorker.Worker();

            TableFuncWorker tableFuncWorker = new(this.ConnectionStrinig, this.DbName, this.Tag);
            var tableFuncs = await tableFuncWorker.worker();

            if (Opciones["solo_con_comentarios"])
            {
                for (int i = tables.Count-1;i >= 0; i--) 
                    if (string.IsNullOrEmpty(tables[i].Comment))
                        tables.RemoveAt(i);

                for (int i = views.Count - 1; i >= 0; i--) 
                    if (string.IsNullOrEmpty(views[i].Comment)) 
                        views.RemoveAt(i);

                for (int i = procs.Count - 1; i >= 0; i--)
                    if (string.IsNullOrEmpty(procs[i].Comment)) 
                        procs.RemoveAt(i);

                for (int i = scalarFuncs.Count - 1; i >= 0; i--) 
                    if (string.IsNullOrEmpty(scalarFuncs[i].Comment))
                        scalarFuncs.RemoveAt(i);

                for (int i = tableFuncs.Count - 1; i >= 0; i--)
                    if (string.IsNullOrEmpty(tableFuncs[i].Comment))
                        tableFuncs.RemoveAt(i);
            }

            tableWorker.WorkerDetails(tables);
            viewWorker.WorkerDetails(views);
            
            Dictionary<string, object> ViewData = new();
            ViewData.Add("title", "Diccionario de Datos");
            ViewData.Add("img_ext", this.ImageExtension);
            ViewData.Add("image", this.ImageData);
            ViewData.Add("image_width", this.ImageWidth);
            ViewData.Add("database", this.DbName);
            ViewData.Add("index_column_class", this.IndexColumnClass);

            ViewData.Add("status", this.Status);
            ViewData.Add("tables", tables);
            ViewData.Add("views", views);
            ViewData.Add("procs", procs);
            ViewData.Add("scalar_funcs", scalarFuncs);
            ViewData.Add("table_funcs", tableFuncs);

            //Copio las Opciones en ViewData
            foreach (var key in Opciones.Keys)
                ViewData[key] = Opciones[key];


            var html = await RazorTemplateEngine.RenderAsync("/wwwroot/report.cshtml", ViewData);
            return html;
        }


    }
}
