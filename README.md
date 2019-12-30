## 更新日志
* 20191230更新至1.7.0
* 然后忘了

## 使用前必看
* 软件采用.Net Core WPF开发, 需下载安装运行时才能正常运行[.Net Core Runtime](https://dotnet.microsoft.com/download)

## 什么是eHallTools?
* 一个小软件，用于方便的查看大学eHall网站上的信息，针对天津理工大学eHall进行的开发，由于不知道其他学校eHall有没有通知公告等功能，故不一定通用

## 为什么开发它
* 初学C#，想写一些项目学习并巩固知识

## 软件功能
* 通知公告
* 师生服务（实现考试安排查询及成绩查询）

## 使用教程
### 登录
* 登录界面

![登录界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129224021.png)

学校服务地址配置存储格式，可编辑添加（/config/server.json）

```sh
{
  "Info": [
    {
      "University": "天津理工大学",
      "AuthserverHttp": "http://authserver.tjut.edu.cn",
      "EhallHttp": "http://ehall.tjut.edu.cn"
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
* 应用选择界面

![应用选择界面](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129230229.png)

## 应用
### 通知公告
* 界面
![通知公告](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230130925.png)

* 双击看详情，如有文件，双击下载（/downloads/标题）
![详情](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230130835.png)

* 输入进行搜索
![搜索](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230130802.png)

### 考试安排查询
![考试安排查询](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230130655.png)

### 成绩查询
![成绩查询](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230130715.png)

## 注意
* 对于某些通知，处理的并不好（没法看）如以下通知，因技术有限，处理成一坨, 如需查看，双击通知内容进入浏览器解析
![案例](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191129231746.png)

使用正则表达式处理一些文章, '<' '>'被吃了，Crayon的锅大概

![正则表达式](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/11/QQ截图20191130095608-1.png)

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

* 添加删除服务地址后，回主页面更新即可

* 电信用户使用可能使用不太顺畅，这是没办法的事，原因是电信绕路
![辣鸡电信](https://wordpress-1253676827.file.myqcloud.com/wp-content/uploads/2019/12/QQ截图20191230131933.png)

## 程序下载
- [eHallTools](https://github.com/Spxg/eHallTools/releases/download/1.7.0/eHallTools.zip)

### 鸣谢
- [Wait](https://github.com/itswait)
