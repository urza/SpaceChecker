Very simple way how to get email alert when one of your linux servers/VMs/VPS is running out of space.

![space_checker_diagram](https://github.com/urza/SpaceChecker/assets/189804/9358d584-32a8-48b3-bc86-345f768aa950)

## 1. Install HTTP API in docker. This will receive information from your servers. And if some server has less than 10% free space it will send you email.

```
docker pull ghcr.io/urza/spacechecker:main && docker run -d -p 7373:8080 --restart=always --name SpaceCheckAPI -e EmailFrom='FROM@EMAIL' -e EmailTo='TO@EMAIL' -e SmtpUserName='SMTP_LOGIN' -e SmtpPwd='SMTP_PASSWORD' -e SmtpServer='SMTP_SERVER' ghcr.io/urza/spacechecker:main
```

Replace your SMTP info that the API will use to send emails.

Check it is running http://localhost:7373 - you should see a message with list of two valid endpoints: `/last` and `/testmail`

Then grab the URL where this is accesible and reachable from your servers. You may want to put this behind [reverse proxy](https://nginxproxymanager.com/ "reverse proxy").


## 2. Install the cron script on your servers/VMs/VPS etc that you want to monitor. They will periodically post information about their disk ("/") usage to the api. 

    curl -sSL "https://raw.githubusercontent.com/urza/SpaceChecker/main/space_checker_install.sh" | sh -s "http(s)://API_ADDRESS"

Replace `http(s)://API_ADDRESS` with your API's real address. 

If you want the server send info about other filesystem than just "/", modify the [space_checker.sh](https://github.com/urza/SpaceChecker/blob/main/space_checker.sh) script on that server after it is installed.

After a while you can check the `API/last` that it has some data. How often servers post data to api depends on the [cron job](https://github.com/urza/SpaceChecker/blob/main/space_checker_install.sh#L20).
You may test sending emails by `API/testmail`.
