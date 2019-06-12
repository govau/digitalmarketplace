import { expect } from "chai";
import { ElementHandle } from "puppeteer";
import randomWords from "random-words";
// import clipboardy from 'clipboardy'
import Global from "../global";

export default class Utils {
  public static async getElementHandles(xpath: string): Promise<ElementHandle[]> {
    await Global.page.waitFor(xpath);
    const elements = await Global.page.$x(xpath);
    const results = [];
    for (const element of elements) {
      if (await element.boxModel()) {
        results.push(element);
      }
    }
    return results;
  }
  public static async getElementHandle(xpath: string): Promise<ElementHandle> {
    const elements = await this.getElementHandles(xpath);
    if (elements.length > 1) {
      throw new Error(`"${xpath}" returned more than one element`);
    } else if (elements.length === 0) {
      throw new Error(`"${xpath}" didn't return any elements`);
    }
    return elements[0];
  }
  public static async selectCheck(value: string, attribute: string) {
    if (!attribute) {
      attribute = "value";
    }
    console.log(`Selecting check box "//input[@${attribute}="${value}"]"`);
    const radio = await this.getElementHandle(`//input[@${attribute}="${value}"]`);
    await radio.press("Space");
  }
  public static async selectRadio(value: string, attribute?: string) {
    if (!attribute) {
      attribute = "value";
    }
    console.log(`Selecting radio "//input[@${attribute}="${value}"]"`);
    const radio = await this.getElementHandle(`//input[@${attribute}="${value}"]`);
    await radio.press("Space");
  }
  public static words(numberOfWords, numberOfCharacters) {
    let text = randomWords({ exactly: numberOfWords }).join(" ");

    if (numberOfCharacters) {
      text = text.substring(0, numberOfCharacters - 1);
    }
    return text;
  }
  public static async type(id: string, options: {
    value?: string,
    numberOfWords?: number,
    numberOfCharacters?: number,
    reactInput?: boolean,
  }): Promise<string> {
    console.log(`Typing in "//*[@id="${id}"]"`);
    let { value, numberOfWords, reactInput } = options;
    const { numberOfCharacters } = options;
    if (value !== "" && !value) {
      if (numberOfCharacters) {
        numberOfWords = numberOfCharacters;
      }
      value = this.words(numberOfWords, numberOfCharacters);
    }
    reactInput = reactInput && reactInput === true;
    const input = await this.getElementHandle(`//*[@id="${id}"]`);
    if (reactInput) {
      if (value.length > 50) {
        value = value.substring(0, 50);
        console.log(`Shortened typed value to "${value}"`);
      }
      await input.type(value, { delay: 0 });
    } else {
      if (process.env.SHORTEN_TYPED_INPUT === "true") {
        if (value.length > 50) {
          value = value.substring(0, 50);
          console.log(`Shortened typed value to "${value}"`);
        }
      }
      await input.type(value, { delay: 0 });
    }

    return value;
  }
  public static async typeInReactInput(id: string, options: {
    value?: string,
    numberOfWords?: number,
    numberOfCharacters?: number,
  }): Promise<string> {
    // eslint-disable-next-line no-param-reassign
    Object.assign(options, {
      reactInput: true,
    });
    // options.reactInput = true
    const value = await this.type(id, options);
    return value;
  }
  public static async upload(id: string, file: string, title: string) {
    let xpath = `//input[@id="${id}" and @type="file"]`;
    if (title) {
      xpath = `//input[@id="${id}" and @type="file" and @title="${title}"]`;
    }
    console.log(`Uploading "${xpath}"`);
    const input = await this.getElementHandle(xpath);
    await input.uploadFile(file);
  }
  public static async clickButtonvalue(value: string) {
    console.log(`Clicking button "//button[.="${value}"]"`);
    const button = await this.getElementHandle(`//button[.="${value}"]`);
    await button.click();
  }
  public static async clickInputButton(value: string) {
    console.log(`Clicking input button "//input[@value="${value}"]"`);
    const button = await this.getElementHandle(`//input[@value="${value}"]`);
    await button.click();
  }
  public static async clickLink(linkText: string, isUrl?: boolean) {
    console.log(`Clicking link "${linkText}"`);
    let links;
    if (isUrl) {
      links = await this.getElementHandles(`//a[contains(@href, "${linkText}")]`);
    } else {
      links = await this.getElementHandles(`//a[.="${linkText}"]`);
    }
    if (process.env.IGNORE_MULTIPLE_LINKS !== "true") {
      expect(links.length).to.equal(1, `Number of links found for "${linkText}"=${links.length}`);
    } else if (links.length > 1) {
      console.warn(`Number of links found for "${linkText}"=${links.length}`);
    }
    await links[0].click();
    console.log(`Clicked link "${linkText}"`);
  }
  public static async matchText(tag: string, text: string) {
    console.log(`matching text: '//${tag}[contains(text(), "${text}")]'`);
    const elementHandles = await this.getElementHandles(`//${tag}[contains(text(), "${text}")]`);
    expect(elementHandles.length).to.equal(1, `No text found using '//${tag}[contains(text(), "${text}")]'`);
  }
  public static async sleep(ms: number): Promise<NodeJS.Timeout> {
    console.log(`Sleeping for ${ms} milliseconds`);
    return new Promise((resolve) => setTimeout(resolve, ms));
  }
}
