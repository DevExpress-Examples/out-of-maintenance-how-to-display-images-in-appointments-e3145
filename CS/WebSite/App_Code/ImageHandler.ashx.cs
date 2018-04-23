using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

public class ImageHandler : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        string imgId = context.Request["imgId"];
        string path = context.Server.MapPath(string.Format("~/App_Data/{0}.png", imgId));

        context.Response.ContentType = "image/png";

        if (System.IO.File.Exists(path))
            context.Response.WriteFile(path);
        
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}