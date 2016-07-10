
using System;
using System.IO;
using JJBoom.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace JJBoom
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Globals.ThisAddIn.Application.PresentationSave += ApplicationOnPresentationSave;
        }

        private void ApplicationOnPresentationSave(Presentation pres)
        {
            foreach (BoomCatalogViewModel boomCatalogViewModel in GlobalBoomCatalogs.GetInstance().BoomCatalogViewModels)
            {
                MemoryStream stream = BoomWriter.SerializeToStream(BoomCatalogConvert.ConvertToBoomsCatalog(boomCatalogViewModel));
                BoomWriter.StreamToFile(stream, UserInfoStorage.GetCurrentJJBoomDocumentFolderPath() + boomCatalogViewModel.BoomCatalogName + ".jjb");
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

        }

        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
