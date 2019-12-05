import * as utils from "../../utils";
import { IAtmResult } from "../brief/atm";

const clickSubmitApplication = async () => {
  await utils.clickInputButton("Submit application");
};

const respond = async (params: IAtmResult) => {
  console.log("Starting to respond to atm brief");
  await clickSubmitApplication();
  await utils.matchText("a", "Enter a date for when you can start the project");
  if (params.criteria) {
    for (const c of params.criteria) {
      await utils.matchText("a", c.criterion);
    }
  }
  await utils.matchText("a", "You must upload your written proposal");
  await utils.matchText("a", "You must add a phone number");
  await utils.type("availability", { numberOfCharacters: 100 });
  if (params.criteria) {
    for (let i = 0; i < params.criteria.length; i += 1) {
      // eslint-disable-next-line no-await-in-loop
      await utils.type(`criteria.${i}`, { numberOfWords: 500 });
    }
  }
  await utils.upload("file_writtenProposal_0", "document.pdf");
  await utils.type("respondToPhone", { value: "0123456789" });
  await clickSubmitApplication();
  await utils.matchText("span", "Your response has been successfully submitted.");
};

export default respond;
