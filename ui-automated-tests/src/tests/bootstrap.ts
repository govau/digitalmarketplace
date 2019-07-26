import { launch, LaunchOptions } from "puppeteer";
import Global from "../global";

// puppeteer options
const opts: LaunchOptions = {
  headless: process.env.HEADLESS !== "false",
  slowMo: process.env.SLOW_MO ? parseInt(process.env.SLOW_MO, 10) : undefined,
};

console.log(`FRONTEND_ADDRESS: ${process.env.FRONTEND_ADDRESS}`);
console.log(`HEADLESS: ${process.env.HEADLESS}`);

beforeEach(async function() {
  Object.assign(opts, {
    defaultViewport: null,
  });
  const browser = await launch(opts);
  Global.browser = browser;
  const page = await browser.newPage();
  Global.page = page;

  await page.goto(process.env.FRONTEND_ADDRESS);
});

afterEach(async function() {
  if (Global.browser) {
    await Global.browser.close();
  }
  if (Global.page) {
    Global.page = null;
  }
});
