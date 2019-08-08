import { format } from "date-fns";
import Global from "../../global";
import * as utils from "../../utils";

const clickSaveContinue = async () => {
  await utils.clickButton("Save and continue");
};

const createBrief = async () => {
  await utils.clickLink("/2/outcome-choice", true);
  await utils.clickLink("/2/buyer-rfx/create", true);
  await utils.clickLink("Create and publish request");
  await utils.clickButton("Start now");
};

const selectDropBox = async () => {
  const sellerCategory = process.env.SELLER_CATEGORY;
  await Global.page.select(`#seller-search-category-select`, sellerCategory);

  const sellerName = process.env.SELLER_NAME;
  await utils.sleep(100);
  await utils.type("seller-search", { value: sellerName });
  let searchResult = await utils.getElementHandles(`//input[@id="seller-search"]/../../ul/li[1]/a`);
  let sr = searchResult[0];
  sr.click();

  await utils.type("seller-search", { value: "%%%" });
  searchResult = await utils.getElementHandles('//input[@id="seller-search"]/../../ul/li');
  const resultCount = searchResult.length;
  for (let i = 1; i <= resultCount; i += 1) {
    if (i > 1) {
      // eslint-disable-next-line no-await-in-loop
      await utils.sleep(100);
      // eslint-disable-next-line no-await-in-loop
      await utils.type("seller-search", { value: "%%%" });
    }
    // eslint-disable-next-line no-await-in-loop
    searchResult = await utils.getElementHandles(`//input[@id="seller-search"]/../../ul/li[${i}]/a`);
    sr = searchResult[0];
    sr.click();
  }
  await clickSaveContinue();
};

const fillWhoCanRespond = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must select at least one panel category");
  await selectDropBox();
};

const fillAbout = async (role: string, locations: string[]) => {
  await clickSaveContinue();
  await utils.matchText("li", "You must add a title");
  await utils.matchText("li", "You must add the name of your department, agency or organisation");
  await utils.matchText("li", "You must add a summary of work to be done");
  await utils.matchText("li", "You must add the working arrangements");
  await utils.matchText("li", "You must select a location of where the work can be done");
  await utils.type("title", { value: role });
  await utils.type("organisation", { numberOfCharacters: 150 });
  await utils.type("summary", { numberOfWords: 200 });

  locations.forEach(async (location) => {
    await utils.selectCheck(location);
  });

  await utils.type("working_arrangements", { numberOfCharacters: 150 });
  await utils.type("clearance", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillResponseFormats = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must choose what you would like sellers to provide through the Marketplace");
  await utils.selectCheck("Written proposal");
  await clickSaveContinue();
  await utils.matchText("li", "You must select at least one proposal type.");
  await utils.selectCheck("Breakdown of costs");
  await utils.selectCheck("Case study");
  await utils.selectCheck("References");
  await utils.selectCheck("Résumés");
  await utils.selectCheck("Response template");
  await utils.selectCheck("Presentation");
  await clickSaveContinue();
};

const fillRequirements = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must upload a requirements document");
  await utils.upload("file_0", "document.pdf", "Requirements document");
  await utils.upload("file_0", "document.pdf", "Response template");
  await utils.upload("file_0", "document.pdf", "Additional documents (optional)");
  await utils.type("industryBriefing", { numberOfWords: 150 });
  await clickSaveContinue();
};
const fillTimeframesAndBudget = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must add an estimated start date");
  await utils.matchText("li", "You must add a contract length");
  await utils.type("start_date", { numberOfCharacters: 150 });
  await utils.type("contract_length-label", { numberOfCharacters: 50 });
  await utils.type("contract_extensions", { numberOfCharacters: 50 });
  await utils.type("budget_range", { numberOfWords: 150 });
  await clickSaveContinue();
};

const fillEvaluationCriteria = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must not have any empty criteria.");
  await utils.matchText("li", "Weightings must be greater than 0.");
  await utils.matchText("li", "You must not have any empty criteria.");
  await utils.clickLink("Add another criteria");
  await utils.type("criteria_0", { numberOfWords: 50 });
  await utils.type("weighting_0", { value: "50" });
  await utils.type("criteria_1", { numberOfWords: 50 });
  await utils.type("weighting_1", { value: "50" });
  await clickSaveContinue();
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
  await utils.type("contact", { value: "0123456789" });
  await clickSaveContinue();
};

const fillPublishBrief = async () => {
  await utils.selectCheck("cb-declaration", "id");
  await utils.clickButton("Publish");
  await utils.matchText("h1", "Your opportunity is now live, and the invited sellers have been notified.");
};

const create = async (params: {title: string, locations: string[]}) => {
  console.log("Starting to create outcome RXF brief");
  await createBrief();
  await fillWhoCanRespond();
  await fillAbout(params.title, params.locations);
  await fillResponseFormats();
  await fillRequirements();
  await fillTimeframesAndBudget();
  await fillEvaluationCriteria();
  await fillClosingDate();
  await fillPublishBrief();
};

export default create;
