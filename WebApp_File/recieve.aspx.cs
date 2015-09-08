using System;
using System.IO;
using System.Web;
using WebApp_File.Core;
using XK.Common;

namespace WebApp_File {
    public partial class receive : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            SaveFile();
        }

        /// <summary>
        /// 获取传递过来文件的文件名
        /// </summary>
        public string FileName
        {
            get
            {
                string filename = "";
                if (!string.IsNullOrEmpty(Request.QueryString["name"])) {
                    filename = Request.QueryString["name"].Trim();
                }
                else if (!string.IsNullOrEmpty(Request.Headers["name"])) {
                    filename = Request.Headers["name"].Trim();
                }
                return filename;
            }
        }

        private void SaveFile() {
            if (string.IsNullOrEmpty(FileName)) return;

            string msg = "0";//失败返回0
            try {
                string filename = HttpUtility.UrlDecode(FileName);
                Stream stream = Request.InputStream;
                 //成功返回文件
                msg = FileStore.Save(Server, stream, filename);

                AppStart.Log4net.logger.Info(msg);
            }
            catch (Exception ex) {
                AppStart.Log4net.logger.Error(ex);
            }

            Response.Write(msg);
        }

    }
}