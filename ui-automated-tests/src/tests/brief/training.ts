import create from "../../flows/brief/training";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin } from "../../flows/login/actions";

describe("should be able to create training brief", () => {
  it("should be able to create training brief", async () => {
    await buyerLogin();
    const now = Date.now();
    await startBrief();
    await create({
      evaluations: ["Interview", "References", "Case study", "Presentation"],
      locations: ["Australian Capital Territory", "Tasmania"],
      title: `Digital Training ${now.valueOf()}`,
    });
  });
});
