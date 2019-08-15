import * as utils from "../../utils";

export const login = async (email: string, password: string) => {
  await utils.clickLink("Log in");
  await utils.matchText("h1", "Sign in to the Marketplace");

  if (email === undefined) {
    console.log("email check");
    // eslint-disable-next-line no-param-reassign
    email = process.env.SELLER_EMAIL;
  }
  if (password === undefined) {
    // eslint-disable-next-line no-param-reassign
    password = process.env.SELLER_PASSWORD;
  }
  await utils.type("input_email_address", { value: email });
  await utils.type("input_password", { value: password });
  await utils.clickInputButton("Sign in");
};

export const buyerLogin = async (email?: string, password?: string) => {
  if (email === undefined) {
    console.log("buyer email");
    // eslint-disable-next-line no-param-reassign
    email = process.env.BUYER_EMAIL;
  }
  if (password === undefined) {
    console.log("buyer password");
    // eslint-disable-next-line no-param-reassign
    password = process.env.BUYER_PASSWORD;
  }

  await login(email, password);
};

export const sellerLogin = async (email?: string, password?: string) => {
  if (email === undefined) {
    // eslint-disable-next-line no-param-reassign
    email = process.env.SELLER_EMAIL;
  }
  if (password === undefined) {
    // eslint-disable-next-line no-param-reassign
    password = process.env.SELLER_PASSWORD;
  }
  await login(email, password);
};

export const signOut = async (userType: string) => {
  if (userType === "buyer") {
    await utils.clickButton("Menu");
  }

  await utils.clickLink("Sign out");
  await utils.matchText("h1", "Sign in to the Marketplace");
};
