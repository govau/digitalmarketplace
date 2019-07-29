import create from "../../flows/brief/training";
import respond from "../../flows/briefResponse/training";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin, sellerLogin } from "../../flows/login/actions";
import { applyForTraining, navigate, selectBrief, viewTrainingApplication } from "../../flows/opportunities/actions";

describe("should be able to create and respond to training brief", () => {
  const now = Date.now();
  const title = `Digital Training ${now.valueOf()}`;
  it(`should create digital training brief`, async () => {
    await buyerLogin();
    await startBrief();
    await create({
      evaluations: ["Interview", "References", "Case study", "Presentation"],
      locations: ["Australian Capital Territory", "Tasmania"],
      title,
    });
  });
  it(`should be able to respond to training brief`, async () => {
    await sellerLogin();
    await navigate();
    await selectBrief(title);
    await applyForTraining();
    await respond();
    await navigate();
    await selectBrief(title);
    await viewTrainingApplication(title);
  });
});
