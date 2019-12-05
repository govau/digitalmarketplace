import create from "../../flows/brief/rfx";
import respond from "../../flows/briefResponse/rfx";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin, sellerLogin } from "../../flows/login/actions";
import { applyForRfx, checkAppliedForRfx, navigate, selectBrief } from "../../flows/opportunities/actions";
import { sleep } from "../../utils";

describe("create and respond to RFXs brief", () => {
  // in order to get the right brief we are going for the 'today's date'.
  const today = Date.now();
  const title = `RFXs ${today.valueOf()}`;

  it("should be able to create RFXs", async () => {
    await buyerLogin();
    await startBrief();
    await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title,
    });
  });

  it("should be able to respond RFXs brief", async () => {
    await sellerLogin();
    await navigate();
    await selectBrief(title);
    await sleep(1000);
    await applyForRfx();
    await respond();
    await checkAppliedForRfx(title);
  });
});
