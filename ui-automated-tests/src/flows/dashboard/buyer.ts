import * as utils from "../../utils";

const startBrief = async () => {
  await utils.clickLink("Menu");
  await utils.clickLink("Dashboard");
  await utils.clickLink("Create new request");
};

export default startBrief;
