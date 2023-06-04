using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using IWshRuntimeLibrary;

namespace ProcessMonitor
{
    class AutoStart
    {
        public static bool Create(string directory, string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            try
            {
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
                if (System.IO.File.Exists(shortcutPath))
                {
                    // 快捷方式已存在，不执行任何操作
                    return true;
                }

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 添加引用 Com 中搜索 Windows Script Host Object Model
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut =
                    (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath); // 创建快捷方式对象
                shortcut.TargetPath = targetPath; // 指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath); // 设置起始位置
                shortcut.WindowStyle = 1; // 设置运行方式，默认为常规窗口
                shortcut.Description = description; // 设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation; // 设置图标路径
                shortcut.Save(); // 保存快捷方式

                return true;
            }
            catch
            {
                // 处理异常情况，记录日志等
            }
            return false;
        }
        public static bool Delete(string directory, string shortcutName)
        {
            try
            {
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
                if (System.IO.File.Exists(shortcutPath))
                {
                    System.IO.File.Delete(shortcutPath);
                    return true;
                }
                else
                {
                    // 快捷方式不存在，不执行任何操作
                    return true;
                }
            }
            catch
            {
                // 处理异常情况，记录日志等
            }
            return false;
        }
    }
}
