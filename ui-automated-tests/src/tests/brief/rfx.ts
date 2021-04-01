import create from "../../flows/brief/rfx";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin } from "../../flows/login/actions";

describe("should be able to create RFXs opportunity", () => {
  it.only("should be able to create RFXs opportunity", async () => {
    const now = Date.now();
    await buyerLogin();
    await startBrief();
    await create({
      locations: ["Australian Capital Territory", "Tasmania"],
      title: `RFXs ${now.valueOf()}`,
    });
  });
});
