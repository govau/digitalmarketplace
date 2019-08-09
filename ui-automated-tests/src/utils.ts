import { expect } from "chai";
import { ElementHandle } from "puppeteer";
// @ts-ignore: TS1202
import randomWords = require("random-words");
import Global from "./global";

export const getElementHandles = async (xpath: string): Promise<ElementHandle[]> => {
  await Global.page.waitFor(xpath);
  const elements = await Global.page.$x(xpath);
  const results = [];
  for (const element of elements) {
    if (await element.boxModel()) {
      results.push(element);
    }
  }
  return results;
};

export const getElementHandle = async (xpath: string): Promise<ElementHandle> => {
  const elements = await this.getElementHandles(xpath);
  if (elements.length > 1) {
    throw new Error(`"${xpath}" returned more than one element`);
  } else if (elements.length === 0) {
    throw new Error(`"${xpath}" didn't return any elements`);
  }
  return elements[0];
};

export const selectCheck = async (value: string, attribute?: string) => {
  if (!attribute) {
    attribute = "value";
  }
  console.log(`Selecting check box "//input[@${attribute}="${value}"]"`);
  const radio = await this.getElementHandle(`//input[@${attribute}="${value}"]`);
  await radio.press("Space");
};

export const selectRadio = async (value: string, attribute?: string) => {
  if (!attribute) {
    attribute = "value";
  }
  console.log(`Selecting radio "//input[@${attribute}="${value}"]"`);
  const radio = await this.getElementHandle(`//input[@${attribute}="${value}"]`);
  await radio.press("Space");
};

const words = (numberOfWords: number, numberOfCharacters: number): string => {
  let text = randomWords({ exactly: numberOfWords }).join(" ");

  if (numberOfCharacters) {
    text = text.substring(0, numberOfCharacters - 1);
  }
  return text;
};

export const type = async (id: string, options: {
  value?: string,
  numberOfWords?: number,
  numberOfCharacters?: number,
}): Promise<string> => {
  console.log(`Typing in "//*[@id="${id}"]"`);
  let { value, numberOfWords } = options;
  const { numberOfCharacters } = options;
  if (value !== "" && !value) {
    if (numberOfCharacters) {
      numberOfWords = numberOfCharacters;
    }
    value = words(numberOfWords, numberOfCharacters);
  }
  const input = await this.getElementHandle(`//*[@id="${id}"]`);
  if (process.env.SHORTEN_TYPED_INPUT === "true") {
    if (value.length > 50) {
      value = value.substring(0, 50);
      console.log(`Shortened typed value to "${value}"`);
    }
  }
  await input.type(value, { delay: 0 });

  return value;
};

export const upload = async (id: string, file: string, title?: string) => {
  let xpath = `//input[@id="${id}" and @type="file"]`;
  if (title) {
    xpath = `//input[@id="${id}" and @type="file" and @title="${title}"]`;
  }
  console.log(`Uploading "${xpath}"`);
  const input = await this.getElementHandle(xpath);
  await input.uploadFile(file);
};

export const clickButton = async (value: string) => {
  console.log(`Clicking button "//button[.="${value}"]"`);
  const button = await this.getElementHandle(`//button[.="${value}"]`);
  await button.click();
};

export const clickInputButton = async (value: string) => {
  console.log(`Clicking input button "//input[@value="${value}"]"`);
  const button = await this.getElementHandle(`//input[@value="${value}"]`);
  await button.click();
};

export const clickLink = async (linkText: string, isUrl?: boolean) => {
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
};

export const matchText = async (tag: string, text: string) => {
  console.log(`matching text: '//${tag}[contains(text(), "${text}")]'`);
  const elementHandles = await this.getElementHandles(`//${tag}[contains(text(), "${text}")]`);
  expect(elementHandles.length).to.equal(1, `No text found using '//${tag}[contains(text(), "${text}")]'`);
};

export const sleep = async (ms: number): Promise<NodeJS.Timeout> => {
  console.log(`Sleeping for ${ms} milliseconds`);
  return new Promise((resolve) => setTimeout(resolve, ms));
};
