import * as util from "../../utils";

const clickStartApplication = async (specialistNumber: number) => {
  if (specialistNumber === 0) {
    await util.clickInputButton("Start application");
  } else {
    await util.clickInputButton("Continue");
  }
};

const clickSubmitApplication = async () => {
  await util.clickInputButton("Submit specialist");
};

const respond = async (params: {specialistNumber: number}) => {
  console.log("Starting to respond to specialist brief");
  await clickStartApplication(params.specialistNumber);
  await util.matchText("a", "Given name(s) is required");
  await util.matchText("a", "Surname is required");
  const givenNames = await util.type("specialistGivenNames", { numberOfCharacters: 100 });
  const surname = await util.type("specialistSurname", { numberOfCharacters: 100 });
  await clickStartApplication(params.specialistNumber);

  await clickSubmitApplication();
  await util.matchText("a", "Enter a date for when you can start the project");
  await util.matchText("a", "Hourly rate is required");
  await util.matchText("a", "Upload a file for your résumé");
  await util.matchText("a", `What is ${givenNames} ${surname}'s citizenship status?`);
  await util.matchText("a", `${givenNames} ${surname}'s security clearance is required`);
  await util.matchText("a", `Has ${givenNames} ${surname} previously worked for the Digital Transformation Agency?`);
  await util.matchText("a", "Upload a file for your résumé");

  await util.type("availability", { numberOfCharacters: 100 });
  await util.type("hourRateExcludingGST", { value: "500" });
  await util.upload("file_0", "document.pdf");
  await util.selectRadio("visaStatus-AustralianCitizen", "id");
  await util.selectRadio("securityClearance-Yes", "id");
  await util.selectRadio("previouslyWorked-Yes", "id");
  await util.type("essentialRequirement.0", { numberOfWords: 150 });
  await util.type("niceToHaveRequirement.0", { numberOfWords: 150 });

  await clickSubmitApplication();
  await util.matchText("strong", `You have submitted`);
};

export default respond;
