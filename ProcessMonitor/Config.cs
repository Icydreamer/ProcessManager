using System.IO;
using System.Text.Json;

namespace ProcessMonitor
{

    /// <summary>
    /// 定义配置项
    /// </summary>
    class Config
    {
        // 是否开机自启
        public bool StartupEnabled { get; private set; }

        // 主题颜色选择
        public string ThemeColor { get; private set; }

        public static Config LoadConfig()
        {
            string configFilePath = "config.json";

            if (File.Exists(configFilePath))
            {
                // 读取现有配置文件
                string json = File.ReadAllText(configFilePath);
                Config config = JsonSerializer.Deserialize<Config>(json);

                return config;
            }
            else
            {
                // 创建新的配置文件并设置初始值
                Config newConfig = new Config
                {
                    StartupEnabled = true,
                    ThemeColor = "dark"
                };

                // 序列化新的配置对象为JSON字符串
                string json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // 将JSON字符串写入配置文件
                File.WriteAllText(configFilePath, json);

                return newConfig;
            }
        }
    }
}