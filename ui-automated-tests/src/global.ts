import { Browser, Page } from "puppeteer";

export default class Global {
  public static page: Page = null;
  public static browser: Browser = null;
}
