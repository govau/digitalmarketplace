import { format } from "date-fns";
import * as utils from "../../utils";

export interface ICriteriaItem {
  criteria: string;
  weighting: string;
}

export interface IAtmParams {
  title: string;
  locations: string[];
  numberOfCriteria?: number;
}

export interface IAtmResult {
  criteria: ICriteriaItem[];
}

const clickSaveContinue = async () => {
  await utils.clickButton("Save and continue");
};

const createBrief = async () => {
  await utils.clickLink("/2/outcome-choice", true);
  await utils.clickLink("/2/buyer-atm/create", true);
  await utils.clickLink("Create and publish request");
  await utils.clickButton("Start now");
};

const fillWhoCanRespond = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must select who can respond");
  await utils.selectRadio("category");
  await clickSaveContinue();
  await utils.matchText("li", "You must select a panel category");
  await utils.selectRadio("all");
  await clickSaveContinue();
};

const fillAbout = async (title: string, locations: string[]) => {
  await clickSaveContinue();
  await utils.matchText("li", "Enter the title for your brief");
  await utils.matchText("li", "Enter the name of your organisation");
  await utils.matchText("li", "Enter a summary of your brief");
  await utils.matchText("li", "You must select at least one location");
  await utils.type("title", { value: title });
  await utils.type("organisation", { numberOfCharacters: 100 });
  await utils.type("summary", { numberOfWords: 150 });

  locations.forEach(async (location) => {
    await utils.selectCheck(location);
  });

  await clickSaveContinue();
};

const fillResponseFormats = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must specify if you need sellers to supply any other information");
  await utils.selectRadio("yes");
  await clickSaveContinue();
  await utils.matchText("li", "You must select at least one response format");
  await utils.selectCheck("Case study");
  await utils.selectCheck("References");
  await utils.selectCheck("Résumés");
  await utils.selectCheck("Presentation");
  await utils.selectCheck("Prototype");
  await clickSaveContinue();
};

const fillObjectives = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "Enter the reason the work is being done");
  await utils.matchText("li", "Enter the key problem to be solved");
  await utils.matchText("li", "Enter the user needs for your opportunity");
  await utils.matchText("li", "Enter the work already done for your opportunity");

  await utils.type("backgroundInformation", { numberOfWords: 500 });
  await utils.type("outcome", { numberOfWords: 500 });
  await utils.type("endUsers", { numberOfWords: 500 });
  await utils.type("workAlreadyDone", { numberOfWords: 500 });
  await utils.type("industryBriefing", { numberOfWords: 500 });
  await utils.upload("file_0", "document.pdf");
  await clickSaveContinue();
};

const fillTimeframes = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "Enter an estimated start date for the brief");

  await utils.type("start_date", { numberOfWords: 10 });
  await utils.type("timeframeConstraints", { numberOfWords: 150 });
  await clickSaveContinue();
};

const fillResponseCriteria = async (numberOfCriteria: number): Promise<ICriteriaItem[]> => {
  await clickSaveContinue();
  await utils.matchText("li", "You must not have any empty criteria.");
  await utils.selectCheck("yes");
  const criteria = [];
  for (let i = 0; i < numberOfCriteria; i += 1) {
    if (i > 0) {
      // eslint-disable-next-line no-await-in-loop
      await utils.clickLink("Add another criteria");
    }
    // eslint-disable-next-line no-await-in-loop
    const criterion = await utils.type(`criteria_${i}`, { numberOfWords: 50 });
    // eslint-disable-next-line no-await-in-loop
    const weighting = await utils.type(`weighting_${i}`, { value: "50" });
    criteria.push({
      criterion,
      weighting,
    });
  }
  await clickSaveContinue();
  return criteria;
};

const fillClosingDate = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must add a closing date at least 2 days from now");
  await utils.matchText("li", "You must add a contact number");
  const now = new Date();
  const future = new Date(now.setDate(now.getDate() + 14));
  await utils.type("day", { value: `${format(future, "DD")}` });
  await utils.type("month", { value: `${format(future, "MM")}` });
  await utils.type("year", { value: `${format(future, "YYYY")}` });
  await utils.type("contactNumber", { value: "0123456789" });
  await clickSaveContinue();
};

const publishBrief = async () => {
  await utils.selectCheck("cb-declaration", "id");
  await utils.clickButton("Publish");
  await utils.matchText("h1", "Your opportunity is now live.");
};

const create = async (params: IAtmParams): Promise<IAtmResult> => {
  console.log("Starting to create atm brief");
  await createBrief();
  await fillWhoCanRespond();
  await fillAbout(params.title, params.locations);
  await fillResponseFormats();
  await fillObjectives();
  await fillTimeframes();
  const criteria = await fillResponseCriteria(params.numberOfCriteria ? params.numberOfCriteria : 2);
  await fillClosingDate();
  await publishBrief();
  return {
    criteria,
  };
};

export default create;
