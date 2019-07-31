import create, { IAtmResult } from "../../flows/brief/atm";
import respond from "../../flows/briefResponse/atm";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin, sellerLogin } from "../../flows/login/actions";
import { applyForAtm, checkAppliedForAtm, navigate, selectBrief } from "../../flows/opportunities/actions";

describe("should be able to create and respond to ask the market brief", () => {
  const now = Date.now();
  const title = `Ask the market ${now.valueOf()}`;
  let brief: IAtmResult = null;
  it("should be able to create ask the market brief", async () => {
    await buyerLogin();
    await startBrief();
    brief = await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title,
    });
  });
  it("should be able to respond to ask the market brief", async () => {
    await sellerLogin();
    await navigate();
    await selectBrief(title);
    await applyForAtm();
    await respond(brief);
    await checkAppliedForAtm(title);
  });
});
