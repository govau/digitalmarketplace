import * as utils from "../../utils";

const clickSaveContinue = async () => {
  await utils.clickInputButton("Save and continue");
};

const createBrief = async () => {
  await utils.clickLink("Training");
  await utils.clickInputButton("Create opportunity");
};

const fillTitle = async (role: string) => {
  await utils.type("input-title", { value: role });
  await clickSaveContinue();
};

const fillOrganisation = async () => {
  await utils.type("input-organisation", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillWhatTraining = async () => {
  await utils.selectCheck("Digital foundations");
  await utils.selectCheck("Agile delivery");
  await utils.selectCheck("User research");
  await utils.selectCheck("Content design");
  await utils.selectCheck("Other");
  await clickSaveContinue();
};

const fillLds = async () => {
  await clickSaveContinue();
  await utils.matchText("a", "What the training needs to cover:");

  await utils.selectRadio("ldsUnits");
  await clickSaveContinue();
  await utils.matchText("a", "Select unit(s)");

  await utils.selectRadio("specify");
  await clickSaveContinue();
  await utils.matchText("a", "Describe what the training needs to cover");

  await utils.selectRadio("ldsUnits");
  await utils.selectCheck("Unit 1: The Australian Government’s digital service context");
  await utils.selectCheck("Unit 2: Agile and working in a multidisciplinary team​");
  await utils.selectCheck("Unit 3: Working in a digital delivery team - design and delivery phases​");
  await utils.selectCheck("Unit 4: Frameworks for digital services");
  await clickSaveContinue();

  await utils.selectRadio("specify");
  await utils.type("input-ldsAgileDeliveryTrainingNeeds", { numberOfWords: 500 });
  await clickSaveContinue();

  for (let i = 0; i < 2; i += 1) {
    // eslint-disable-next-line no-await-in-loop
    await utils.selectRadio("sellerProposal");
    // eslint-disable-next-line no-await-in-loop
    await clickSaveContinue();
  }
};

const fillTrainingDetail = async () => {
  await utils.type("input-trainingDetailType", { numberOfCharacters: 100 });
  await utils.type("input-trainingDetailCover", { numberOfWords: 500 });
  await clickSaveContinue();
};

const fillWhyTraining = async () => {
  await utils.type("input-whyTraining", { numberOfWords: 500 });
  await clickSaveContinue();
};

const fillAudience = async () => {
  await utils.type("input-audience", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillTrainingLength = async () => {
  await utils.type("input-trainingLength", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillTrainingMethod = async () => {
  await utils.selectRadio("ownPreference");
  await clickSaveContinue();
  await utils.matchText("a", "Define preference");
  await utils.type("input-trainingApproachOwn", { numberOfWords: 100 });
  await clickSaveContinue();
};

const fillHowLong = async () => {
  await utils.selectRadio("2 weeks");
  await clickSaveContinue();
};

const fillWhoCanRespond = async () => {
  await utils.selectRadio("allSellers");
  await clickSaveContinue();
};

const fillContactNumber = async () => {
  await utils.type("input-contactNumber", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillCommencementDate = async () => {
  await utils.type("input-startDate", { numberOfCharacters: 100 });
  await utils.type("input-timeframeConstraints", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillLocation = async (locations: string[]) => {
  await utils.type("input-locationCityOrRegion", { numberOfCharacters: 100 });

  await clickSaveContinue();

  locations.forEach(async (location) => {
    await utils.selectCheck(location);
  });

  await clickSaveContinue();
};

const fillBudgetRange = async () => {
  await utils.type("input-budgetRange", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillPaymentApproach = async () => {
  await utils.type("input-paymentApproachAdditionalInformation", { numberOfCharacters: 200 });
  await utils.selectRadio("fixedPrice");
  await clickSaveContinue();
};

const fillSecurityClearance = async () => {
  await utils.type("input-securityClearance", { numberOfCharacters: 100 });
  await clickSaveContinue();
};

const fillContractLength = async () => {
  await utils.type("input-contractLength", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillIntellectualProperty = async () => {
  await utils.selectRadio("commonwealth");
  await clickSaveContinue();
};

const fillAdditionalTerms = async () => {
  await utils.type("input-additionalTerms", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillNumberOfSellers = async () => {
  await utils.type("input-numberOfSuppliers", { value: "3" });
  await clickSaveContinue();
};

const fillEvaluationRating = async () => {
  await utils.type("input-technicalWeighting", { value: "25" });
  await utils.type("input-culturalWeighting", { value: "25" });
  await utils.type("input-priceWeighting", { value: "50" });
  await clickSaveContinue();
};

const fillTechnicalCompetenceCriteria = async () => {
  await utils.type("input-essentialRequirements-1", { numberOfWords: 10 });
  await utils.type("input-niceToHaveRequirements-1", { numberOfWords: 10 });
  await clickSaveContinue();
};

const fillCulturalFitCriteria = async () => {
  await utils.type("input-culturalFitCriteria-1", { numberOfWords: 10 });
  await clickSaveContinue();
};

const fillAssessmentMethods = async (evaluations: string[]) => {
  evaluations.forEach(async (evaluation) => {
    await utils.selectCheck(evaluation);
  });

  await clickSaveContinue();
};

const fillSummary = async () => {
  await utils.type("input-summary", { numberOfWords: 200 });
  await clickSaveContinue();
};

const fillQuestionAnswer = async () => {
  await utils.type("input-questionAndAnswerSessionDetails", { numberOfWords: 200 });
  await clickSaveContinue();
};

const publishBrief = async () => {
  await utils.clickLink("Review and publish your requirements");
  await utils.clickInputButton("Publish brief");
  await utils.matchText("h4", "Your opportunity has been published");
};

const create = async (params: {title: string, locations: string[], evaluations: string[]}) => {
  console.log("Starting to create training brief");
  await createBrief();
  await fillTitle(params.title);
  await fillOrganisation();
  await fillWhatTraining();
  await fillLds();
  await fillTrainingDetail();
  await fillWhyTraining();
  await fillAudience();
  await fillTrainingLength();
  await fillTrainingMethod();
  await fillHowLong();
  await fillWhoCanRespond();
  await fillContactNumber();
  await fillCommencementDate();
  await fillLocation(params.locations);
  await fillBudgetRange();
  await fillPaymentApproach();
  await fillSecurityClearance();
  await fillContractLength();
  await fillIntellectualProperty();
  await fillAdditionalTerms();
  await fillNumberOfSellers();
  await fillEvaluationRating();
  await fillTechnicalCompetenceCriteria();
  await fillCulturalFitCriteria();
  await fillAssessmentMethods(params.evaluations);
  await fillSummary();
  await fillQuestionAnswer();
  await publishBrief();
};

export default create;
