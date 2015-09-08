using System;
using System.Web;
using XK.Common;

namespace WebApp_Test.xk {
    public partial class UpRecieve : System.Web.UI.Page {


        protected void Page_Load(object sender, EventArgs e) {

            RecieveFile();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        private void RecieveFile() {
    
            HttpFileCollection files = Request.Files;
            if (files.Count > 0) {
               
                int errCount = 0;

                if (files.Count == 1) {
                    //异步批量上传
                    var file = files[0];
                    errCount += Upload(file);

                    if (errCount == 0) {
                        //成功
                        var obj = new {success = "1"};
                        Response.Write(obj.ToJson());
                    }
                }
                else if (files.Count > 1) {
                    //同步批量上传

                    for (int i = 0; i < files.Count; i++) {
                        var file = files[i];
                        errCount += Upload(file);
                    }

                    if (errCount == 0) {
                        //成功
                        var obj = new {success = "1"};
                        Response.Write(obj.ToJson());
                    }

                }
            }
        }

        private int Upload(HttpPostedFile postedFile) {
            string retFile = FileHelper.Upload2Server(
                "http://localhost:41496/receive.aspx",
                postedFile.FileNameExt(),
                postedFile.InputStream);

            return (retFile.Length > 0) ? 0 : 1;
        }


    }
}