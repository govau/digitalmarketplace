import * as util from "../../utils";

const clickSubmitApplication = async () => {
  await util.clickInputButton("Submit response");
};

const respond = async () => {
  console.log("respond to training");

  await clickSubmitApplication();

  await util.matchText("a", "Enter a date for when you can start the project");
  await util.matchText("a", "Choose a file for your written proposal");
  await util.matchText("a", "Choose a file for your project costs");
  await util.matchText("a", "A contact number is required");

  await util.type("availability", { numberOfCharacters: 100 });
  await util.upload("file_0", "document.pdf");
  await clickSubmitApplication();
  await util.matchText("a", "Choose a file for your project costs");
  await util.matchText("a", "A contact number is required");

  await util.upload("file_1", "document.pdf");
  await clickSubmitApplication();

  await util.matchText("a", "A contact number is required");
  await util.type("respondToPhone", { value: "0123456789" });
  await clickSubmitApplication();

  await util.matchText("h4", "Thanks, your response has been successfully submitted.");
};

export default respond;
