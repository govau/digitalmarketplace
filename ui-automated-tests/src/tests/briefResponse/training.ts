import create from "../../flows/brief/training";
import respond from "../../flows/briefResponse/training";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin, sellerLogin } from "../../flows/login/actions";
import { applyForTraining, checkAppliedForTraining, navigate, selectBrief } from "../../flows/opportunities/actions";
import { sleep } from "../../utils";

describe("create and respond to training brief", () => {
  // in order to get the right brief we are going for the 'today's date'.
  const today = Date.now();
  const title = `Training ${today.valueOf()}`;

  it("should be able to create training brief", async () => {
    await buyerLogin();
    await startBrief();
    await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title,
    });
  });

  it("should be able to respond to a training brief", async () => {
    await sellerLogin();
    await navigate();
    await selectBrief(title);
    await applyForTraining();
    await respond();
    await checkAppliedForTraining(title);
  });
});
