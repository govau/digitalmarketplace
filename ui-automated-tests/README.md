To run this, first create an environment file with the following:

```
export FRONTEND_ADDRESS=[http://localhost:8000]
export SELLER_EMAIL=[some.seller@company.com]
export SELLER_PASSWORD=[password]
export BUYER_EMAIL=[some.buyer@government.gov.au]
export BUYER_PASSWORD=[password]
export SHORTEN_TYPED_INPUT=[true|false]
export SLOW_MO=[number in milliseconds|undefined]
export HEADLESS=[true|false]
export IGNORE_MULTIPLE_LINKS=[true|false]
export SELLER_CATEGORY=[3]
export SELLER_NAME=[xyz]
```

next run `npm install`  
and start the tests with `source [PATH_TO_ENVIRONMENT_FILE] && npm test`


# Troubleshooting on Ubuntu
There are a few issues you would encounter when running the ui-automation tests on Ubuntu.

If there is issue with an outdated version of node. 
Install the latest version.
`nvm install v14.16.0`

Switch to use that version of node
`nvm use v14.16.0`

running `npm i puppeter` will not install Chromium and you will need to add the following dependecies manually.

`sudo apt-get install gconf-service libasound2 libatk1.0-0 libatk-bridge2.0-0 libc6 libcairo2 libcups2 libdbus-1-3 libexpat1 libfontconfig1 libgcc1 libgconf-2-4 libgdk-pixbuf2.0-0 libglib2.0-0 libgtk-3-0 libnspr4 libpango-1.0-0 libpangocairo-1.0-0 libstdc++6 libx11-6 libx11-xcb1 libxcb1 libxcomposite1 libxcursor1 libxdamage1 libxext6 libxfixes3 libxi6 libxrandr2 libxrender1 libxss1 libxtst6 ca-certificates fonts-liberation libappindicator1 libnss3 lsb-release xdg-utils wget`


Please look at the following links for reference:
https://github.com/puppeteer/puppeteer/issues/3443
https://github.com/puppeteer/puppeteer/issues/6560
