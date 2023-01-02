# vk-to-telegram-repost
# Setting up the application
## 1. In the app directory you need to create Settings.json file with next configuration:
```json
{
  "token": "your VK access token",
  "group": "your group ID",
  "channelTg": "your public channel name",
  "tokenTg": "your bot token"
}
```
## 2. How to get your access token?
1. You need to create a standalone app, [here](https://vk.com/editapp?act=create)
2. Go to the app's settings tab and enable OpenAPI
3. Set app address to http://localhost and host to localhost
4. Save changes
5. Use link template below, take app id from settings tab
```
https://oauth.vk.com/authorize
  ?client_id=YOUR_APP_ID
  &display=page
  &redirect_uri=https://oauth.vk.com/blank.html
  &scope=offline,wall,photos,docs,groups
  &v=5.21&response_type=token
```

For example, 
```
https://oauth.vk.com/authorize
  ?client_id=1234567890
  &display=page
  &redirect_uri=https://oauth.vk.com/blank.html
  &scope=offline,wall,photos,docs,groups
  &v=5.21&response_type=token
```

Go through the link, confirm the action and take access code from url. It will be between ```access_token=``` and ```&expires_in```
```
https://oauth.vk.com/blank.html#access_token=YOUR_TOKEN_WILL_BE_HERE&expires_in=0&user_id&other_data
```
## 3. How to get telegram bot id?

Create bot using [@botfather](https://t.me/BotFather) in Telegram
```
/newbot
```
Name your bot as the application suggests

Get your token using 
```
/token
```


# Build
If you have [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed, simply run 

```
.\build.bat
```

or

```
dotnet build --output your\desired\location
```
In your terminal


# Credits

Libraries used in this project:
1. Telegram.Bot for .NET. [Github page](https://github.com/TelegramBots/Telegram.Bot), [Nuget page](https://www.nuget.org/packages/Telegram.Bot/18.0.0)
2. vknet [Github page](https://github.com/vknet/vk)
