import { buyerLogin, sellerLogin, signOut } from "../../flows/login/actions";
import * as util from "../../utils";

describe("should fail sign in", function() {
  const testCases = [
    { args: ["a@b.cm", "a"], expected: ["a", "Make sure you've entered the right email address and password. Accounts are locked after 5 failed attempts."] }
  ];

  for (const test of testCases) {
    it(`sign in fails ${test.args.length} args`, async function() {
      await buyerLogin(test.args[0], test.args[1]);
      await util.matchText(test.expected[0], test.expected[1]);
    });
  }
});

describe("should sign in", function() {
  it("buyer should be able to login", async function() {
    await buyerLogin(process.env.BUYER_EMAIL, process.env.BUYER_PASSWORD);
    await util.matchText("h1", "Dashboard");
    await signOut("buyer");
  });

  it("seller should be able to login", async function() {
    await sellerLogin(process.env.SELLER_EMAIL, process.env.SELLER_PASSWORD);
    await util.matchText("h1", "Dashboard");
    await signOut("seller");
  });
});
