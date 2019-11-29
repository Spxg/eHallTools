## 什么是eHallTools?
* 一个小软件，用于便携的查看大学eHall网站上的信息，理论上没有被魔改的eHall都是通用的

## 为什么开发它
* 初学C#，想写一些项目学习并巩固知识

## 软件功能
* 通知公告
* 师生服务（Developing）
* 等等

## 使用教程
### 登录
![登录界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129224021.png)

学校服务地址配置存储格式，可编辑添加（/config/server.json）

```sh
{
  "Info": [
    {
      "University": "天津理工大学",
      "AuthserverHttp": "http://authserver.tjut.edu.cn",
      "EhallHttp": "http://ehall.tjut.edu.cn"
    },
    {server.json
      "University": "山东理工大学",
      "AuthserverHttp": "http://authserver.sdut.edu.cn",
      "EhallHttp": "http://ehall.sdut.edu.cn"
    }
  ]
}
```

或者在页面添加
![添加服务界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129225250.png)

或删除
![删除服务界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230058.png)

账号密码等信息的保存（/config/settings.json）

```sh
{
  "RememberPassword": true,
  "AutoLogin": false,
  "SelectedUniversityIndex": 0,
  "StudentId": "11111",
  "Password": "xxxx"
}
```

### 应用选择
![应用选择界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230229.png)

## 应用
### 通知公告
![通知公告](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230505.png)

双击看详情
![详情](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230831.png)

如有文件，双击下载（/downloads）
![下载文件](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230840.png)

## 注意
* 对于某些通知，处理的并不好（没法看）如以下通知，因技术有限，处理成一坨
![案例](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129231746.png)

使用正则表达式处理一些文章

```sh
string text = Regex.Replace(content.Content, @"<.*?>|&.*?;", string.Empty);
text = "        " + text;

foreach (var m in Regex.Matches(text, @"(?<=(：|。)\s*)\w{1,2}、"))
{
    text = text.Insert(text.LastIndexOf(m.ToString()), "\n");
}

foreach (var m in Regex.Matches(text, @"(?<=(：|。)\s*)\d(、|\.)"))
{
    text = text.Insert(text.IndexOf(m.ToString()), "\n");
}
```

* 并不需要连接校园网，但是校园网访问很快！

## 程序下载
