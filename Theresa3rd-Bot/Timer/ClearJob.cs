﻿using Mirai.CSharp.Models.ChatMessages;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Theresa3rd_Bot.Common;
using Theresa3rd_Bot.Model.Config;
using Theresa3rd_Bot.Util;

namespace Theresa3rd_Bot.Timer
{
    public class ClearJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ClearDownload();
            ClearUploadTemp();
            await Task.Delay(1000);
        }

        /// <summary>
        /// 清理下载目录
        /// </summary>
        private void ClearDownload()
        {
            try
            {
                string path = FilePath.getDownImgSavePath();
                if (Directory.Exists(path) == false) return;
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Delete(true);
                LogHelper.Info("图片下载目录清理完毕...");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "图片下载目录清理失败");
            }
        }

        /// <summary>
        /// 清理上传临时文件
        /// </summary>
        private void ClearUploadTemp()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ClearWindowsUploadTemp();
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    ClearLinuxUploadTemp();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, "上传临时文件清理失败");
            }
        }

        private void ClearWindowsUploadTemp()
        {
            string dirPath = System.IO.Path.GetTempPath();
            FileInfo[] fileList = FileHelper.searchFiles(dirPath, "file-upload*.tmp");
            if (fileList == null || fileList.Length == 0) return;
            foreach (var item in fileList) FileHelper.deleteFile(item);
            LogHelper.Info($"上传临时文件清理完毕，共计清理 {fileList.Length} 个临时文件...");
        }

        private void ClearLinuxUploadTemp()
        {
            
        }




    }
}
