import create from "../../flows/brief/atm";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin } from "../../flows/login/actions";

describe("should be able to ask the market brief", () => {
  it("should be able to ask the market brief", async () => {
    await buyerLogin();
    const now = Date.now();
    await startBrief();
    await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title: `Ask the market ${now.valueOf()}`,
    });
  });
});
