import { format } from "date-fns";
import Global from "../../global";
import * as utils from "../../utils";

const clickSaveContinue = async () => {
  await utils.clickButton("Save and continue");
};

const createBrief = async () => {
  await utils.clickLink("Training");
  await utils.clickLink("Create and publish request");
  await utils.clickButton("Start now");
};

const selectDropBox = async () => {
  const sellerName = process.env.SELLER_NAME;
  await utils.sleep(100);
  await utils.type("seller-search", { value: sellerName });
  const searchResult = await utils.getElementHandles(`//input[@id="seller-search"]/../../ul/li[1]/a`);
  const sr = searchResult[0];
  sr.click();
  await clickSaveContinue();
};

const fillWhoCanRespond = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must select at least one seller");
  await selectDropBox();
};

const fillAbout = async (role: string, locations: string[]) => {
  await clickSaveContinue();
  await utils.matchText("li", "You must add a title");
  await utils.matchText("li", "You must add a summary of work to be done");
  await utils.matchText("li", "You must add the working arrangements");
  await utils.matchText("li", "You must select a location of where the work can be done");
  await utils.type("title", { value: role });
  await utils.type("summary", { numberOfWords: 200 });

  for (const location of locations) {
    await utils.selectCheck(location);
  }

  await utils.type("working_arrangements", { numberOfWords: 150 });
  await utils.type("clearance", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillResponseFormats = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must choose what you would like sellers to provide through the Marketplace");
  await utils.selectCheck("Written proposal");
  await utils.selectCheck("Response template");
  await clickSaveContinue();
  await utils.matchText("li", "You must select at least one proposal type.");
  await utils.matchText("li", `You can only select either "Written proposal" or "Completed response template".`, "'");
  await utils.selectCheck("Response template");
  await utils.selectCheck("Breakdown of costs");
  await utils.selectCheck("Case study");
  await utils.selectCheck("References");
  await utils.selectCheck("Résumés");
  await utils.selectCheck("Interview");
  await utils.selectCheck("Presentation");
  await clickSaveContinue();
};

const fillRequirements = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "You must upload a requirements document");
  await utils.upload("file_0", "document.pdf", "Requirements document");
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
  await utils.selectCheck("includeWeightingsNiceToHave", "id");
  await clickSaveContinue();
  await utils.matchText("li", "You cannot have blank essential criteria.");
  await utils.matchText("li", "Essential weightings must be greater than 0.");
  await utils.matchText("li", "You cannot have blank essential criteria.");
  await utils.matchText("li", "Desirable weightings must add up to 100%.");
  await utils.selectCheck("includeWeightingsNiceToHave", "id");
  await utils.clickLink("Add another criteria");
  await utils.type("essential_criteria_0", { numberOfWords: 50 });
  await utils.type("essential_weighting_0", { value: "50" });
  await utils.type("essential_criteria_1", { numberOfWords: 50 });
  await utils.type("essential_weighting_1", { value: "50" });
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
  await utils.selectCheck("comprehensiveTerms", "id");
  await utils.type("internalReference", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillPublishBrief = async () => {
  await utils.selectCheck("cb-declaration", "id");
  await utils.clickButton("Publish");
  await utils.matchText("h1", "Your opportunity is now live, and the invited sellers have been notified.");
};

const create = async (params: { title: string, locations: string[] }) => {
  console.log("Starting to create training brief");
  await createBrief();
  await fillWhoCanRespond();
  await fillAbout(params.title, params.locations);
  await fillEvaluationCriteria();
  await fillResponseFormats();
  await fillRequirements();
  await fillTimeframesAndBudget();
  await fillClosingDate();
  await fillPublishBrief();
};

export default create;
