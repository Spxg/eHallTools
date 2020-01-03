using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace eHallTools
{
    class JsonOperation
    {
        public async Task<PageObject> GetPageInfoAsync(string pageNumber, string pageSize, string searchParam)
        {
            pageNumber = (int.Parse(pageNumber) - 1).ToString();

            var pagePostData = new Dictionary<string, string>
            {
                { "PAGE_NO", pageNumber },
                { "PAGE_SIZE", pageSize },
                { "TITLE", searchParam },
                { "COLUMN_ID", "ColumnALL" },
                { "DEPT_ID", "DeptALL" },
                { "PUBLISH_TIME", "TimeALL" }
            };

            var pagePost = new FormUrlEncodedContent(pagePostData);
            var page = await MainWindow.operateClient.PostAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/bulletin/getAllBulletin.do", pagePost);
            var json = await page.Content.ReadAsStringAsync();

            if (json.Contains("\"aList\":null"))
            {
                MessageBox.Show("没有通知");
                return null;
            }
            else
            {
                PageObject pageJson = JsonConvert.DeserializeObject<PageObject>(json);
                return pageJson;
            }
        }

        public async Task<NoticeDetail> GetContentInfoAsync(string noticeId)
        {
            var json = await MainWindow.operateClient.GetStringAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/bulletin/getBulletinById.do?WID=" + noticeId);
            NoticeDetail contentJson = JsonConvert.DeserializeObject<NoticeDetail>(json);
            return contentJson;
        }

        public async Task<FileList> GetFileInfoAsync(string token)
        {
            var filePostData = new Dictionary<string, string>
            {
                { "token", token }
            };

            var filePost = new FormUrlEncodedContent(filePostData);
            var file = await MainWindow.operateClient.PostAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/bulletin/getAttchFiles.do", filePost);
            var json = await file.Content.ReadAsStringAsync();

            FileList fileJson = JsonConvert.DeserializeObject<FileList>(json);
            return fileJson;
        }

        public ServerList GetServerInfo()
        {
            string json;
            var path = Path.Combine(MainWindow.configPath, "server.json");

            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                StreamReader rs = new StreamReader(stream);
                json = rs.ReadToEnd();
                rs.Dispose();
            }
           
            ServerList serverJson = JsonConvert.DeserializeObject<ServerList>(json);
            return serverJson;
        }

        public Setting GetSettingsInfo()
        {
            string json;
            var path = Path.Combine(MainWindow.configPath, "settings.json");
            
            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                StreamReader rs = new StreamReader(stream);
                json = rs.ReadToEnd();
                rs.Dispose();
            }

            Setting settingJson = JsonConvert.DeserializeObject<Setting>(json);
            return settingJson;
        }

        public void UpdateJson<T>(string path, T json)
        {
            string newJson = JsonConvert.SerializeObject(json);
            newJson = JsonConvert.DeserializeObject(newJson).ToString();
            using StreamWriter sw = new StreamWriter(path, false);
            sw.Write(newJson);
        }
    }
}
