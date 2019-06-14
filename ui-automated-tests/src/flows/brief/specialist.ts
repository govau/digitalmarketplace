import { format } from "date-fns";
import Global from "../../global";
import * as utils from "../../utils";

interface ISelectionCriteria {
  essentialCriteria: ISelectionCriteriaItem;
  niceToHaveCriteria: ISelectionCriteriaItem;
}
interface ISelectionCriteriaItem {
  criteria: string;
  weighting: string;
}

interface ISpecialistParams {
  title: string;
  locations: string[];
  categoryId: string;
  categoryName: string;
}

export interface ISpecialistResult extends ISelectionCriteria {
  numberOfSuppliers: string;
}

const clickSaveContinue = async () => {
  await utils.clickButton("Save and continue");
};

const createBrief = async () => {
  await utils.clickLink("Specialist");
  await utils.clickLink("Create and publish request");
  await utils.clickButton("Start now");
};

const fillAbout = async (role: string, locations: string[]) => {
  await clickSaveContinue();
  await utils.matchText("li", "Enter the title for your opportunity.");
  await utils.matchText("li", "what will the specialist do");
  await utils.matchText("li", "You must select at least one location.");
  await utils.type("title", { value: role });
  await utils.type("summary", { numberOfWords: 1000 });

  locations.forEach(async (location) => {
    await utils.selectCheck(location);
  });

  await clickSaveContinue();
};

const fillWhoCanRespond = async (categoryId: string) => {
  await clickSaveContinue();
  await utils.matchText("li", "You must select a category");
  await Global.page.select(`#select-seller-category-select`, categoryId);

  await utils.selectRadio("selected");
  await clickSaveContinue();
  await utils.matchText("li", "You must add at least one seller");

  await utils.selectRadio("all");
  await clickSaveContinue();
};

const fillSelectionCriteria = async (): Promise<ISelectionCriteria> => {
  await utils.selectCheck("includeWeightingsEssential", "id");
  await utils.selectCheck("includeWeightingsNiceToHave", "id");
  await clickSaveContinue();
  await utils.matchText("li", "You cannot have blank essential criteria.");
  await utils.matchText("li", "You cannot have blank essential weightings.");
  await utils.matchText("li", "Desirable weightings must add up to 100%.");

  const essCriteria = await utils.type("essential_criteria_0", { numberOfWords: 50 });
  const essWeighting = await utils.type("essential_weighting_0", { value: "10" });
  await clickSaveContinue();
  await utils.matchText("li", "Essential weightings must add up to 100%.");
  await utils.type("essential_weighting_0", { value: "0" });
  const essentialCriteria = {
    criteria: essCriteria,
    weighting: essWeighting,
  };

  const nthCriteria = await utils.type("nice_to_have_criteria_0", { numberOfWords: 50 });
  const nthWeighting = await utils.type("nice_to_have_weighting_0", { value: "100" });
  const niceToHaveCriteria = {
    criteria: nthCriteria,
    weighting: nthWeighting,
  };
  await clickSaveContinue();

  return {
    essentialCriteria,
    niceToHaveCriteria,
  };
};

const fillSellerResponses = async (): Promise<{numberOfSuppliers: string}> => {
  await clickSaveContinue();
  await utils.matchText("li", "You must define the security clearance requirements");

  const input = await utils.getElementHandle(`//input[@id="numberOfSuppliers"]`);
  await input.press("Backspace");
  const numberOfSuppliers = await utils.type("numberOfSuppliers", { value: "6" });

  await utils.selectCheck("References");
  await utils.selectCheck("Interviews");
  await utils.selectCheck("Scenarios or tests");
  await utils.selectCheck("Presentations");

  await utils.selectRadio("hourlyRate");
  await utils.type("maxRate", { value: "123" });
  await utils.type("budgetRange", { numberOfCharacters: 100 });

  await utils.selectRadio("mustHave");
  await clickSaveContinue();
  await utils.matchText("li", "You must select a type of security clearance.");
  await Global.page.select(`#securityClearanceCurrent`, "pv");
  await clickSaveContinue();
  return {
    numberOfSuppliers,
  };
};

const fillTimeframes = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "Enter an estimated start date for the opportunity");
  await utils.matchText("li", "You must enter a valid start date");
  await utils.matchText("li", "Enter a contract length for the opportunity");
  const now = new Date();
  const future = new Date(now.setDate(now.getDate() + 14));
  await utils.type("day", { value: `${format(future, "DD")}` });
  await utils.type("month", { value: `${format(future, "MM")}` });
  await utils.type("year", { value: `${format(future, "YYYY")}` });
  await utils.type("contractLength", { numberOfCharacters: 100 });
  await utils.type("contractExtensions", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillAdditionalInformation = async () => {
  await clickSaveContinue();
  await utils.matchText("li", "Contact number is required");
  await utils.upload("file_0", "document.pdf", "Additional documents (optional)");
  await utils.type("contactNumber", { value: "01234455667733" });
  await clickSaveContinue();
};

const publishBrief = async () => {
  await utils.selectCheck("cb-declaration", "id");
  await utils.clickButton("Publish");
};

const create = async (params: ISpecialistParams): Promise<ISpecialistResult> => {
  console.log(`Starting to create ${params.categoryName} brief`);
  await createBrief();
  await fillAbout(params.title, params.locations);
  await fillWhoCanRespond(params.categoryId);
  const criteria = await fillSelectionCriteria();
  const responses = await fillSellerResponses();
  await fillTimeframes();
  await fillAdditionalInformation();
  await publishBrief();

  return {
    essentialCriteria: criteria.essentialCriteria,
    niceToHaveCriteria: criteria.niceToHaveCriteria,
    numberOfSuppliers: responses.numberOfSuppliers,
  };
};

export default create;
