# vk-to-telegram-repost

1. In the app directory you need to create Settings.json file with next configuration:
```json
{
  "token": "your VK access token",
  "group": "your group ID",
  "channelTg": "your public channel name",
  "tokenTg": "your bot token"
}
```
How to get your access token?
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
