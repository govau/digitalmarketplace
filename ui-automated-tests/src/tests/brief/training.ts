import create from "../../flows/brief/training";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin } from "../../flows/login/actions";

describe("should be able to create training brief", () => {
  it("should be able to create training brief", async () => {
    const now = Date.now();
    await buyerLogin();
    await startBrief();
    await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title: `Training ${now.valueOf()}`,
    });
  });
});
