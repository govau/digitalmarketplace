import * as util from "../../utils";

const clickSubmitApplication = async () => {
  await util.clickInputButton("Submit application");
};

const respond = async () => {
  console.log("respond to RFxs");
  await util.type("criteria.0", { numberOfWords: 500 });
  await util.type("criteria.1", { numberOfWords: 500 });
  await clickSubmitApplication();
  await util.matchText("a", "Enter a date for when you can start the project");
  await util.matchText("a", "You must upload your written proposal");
  await util.type("availability", { value: "0123456789" });
  await util.upload("file_writtenProposal_0", "document.pdf");
  await clickSubmitApplication();

  await util.matchText("span", "Your response has been successfully submitted.");
};

export default respond;
