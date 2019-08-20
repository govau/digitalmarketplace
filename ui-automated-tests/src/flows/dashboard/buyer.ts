import * as utils from "../../utils";

const startBrief = async () => {
  await utils.clickButton("Menu");
  await utils.clickLink("Dashboard");
  await utils.clickButton("Create new request");
};

export default startBrief;
