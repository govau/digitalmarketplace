import * as util from "../../utils";

const clickSubmitApplication = async () => {
  await util.clickInputButton("Submit candidate");
};

const respond = async (params: {specialistNumber: number}) => {
  console.log("Starting to respond to specialist brief");
  await clickSubmitApplication();
  await util.matchText("a", "Given name(s) is required");
  await util.matchText("a", "Surname is required");
  await util.type("specialistGivenNames", { numberOfCharacters: 100 });
  await util.type("specialistSurname", { numberOfCharacters: 100 });

  await util.matchText("a", "Enter a date for when you can start the project");
  await util.matchText("a", "Hourly rate is required");
  await util.matchText("a", "Upload a file for your résumé");
  await util.matchText("a", `the candidate's security clearance is required`);

  await util.type("availability", { numberOfCharacters: 100 });
  await util.type("hourRateExcludingGST", { value: "500" });
  await util.upload("file_resume_0", "document.pdf");
  await util.selectRadio("visaStatus-AustralianCitizen", "id");
  await util.selectRadio("securityClearance-Yes", "id");
  await util.selectRadio("previouslyWorked-Yes", "id");
  await util.type("essentialRequirement.0", { numberOfWords: 150 });
  await util.type("niceToHaveRequirement.0", { numberOfWords: 150 });

  await clickSubmitApplication();
};

export default respond;
