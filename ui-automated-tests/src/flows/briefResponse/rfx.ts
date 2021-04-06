import * as util from "../../utils";

const clickSubmitApplication = async () => {
  await util.clickInputButton("Submit application");
};

const respond = async () => {
  console.log("respond to RFxs");
  await clickSubmitApplication();
  await util.matchText("a", "You must upload your written proposal");
  await util.upload("file_writtenProposal_0", "document.pdf");
  await clickSubmitApplication();

  await util.matchText("span", "Your response has been successfully submitted.");
};

export default respond;
