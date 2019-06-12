import Utils from "../utils";

export const login = async (email: string, password: string) => {
  await Utils.clickLink("Log in");
  await Utils.matchText("h1", "Sign in to the Marketplace");

  if (email === undefined) {
    console.log("email check");
    // eslint-disable-next-line no-param-reassign
    email = process.env.SELLER_EMAIL;
  }
  if (password === undefined) {
    // eslint-disable-next-line no-param-reassign
    password = process.env.SELLER_PASSWORD;
  }
  await Utils.type("input_email_address", { value: email });
  await Utils.type("input_password", { value: password });
  await Utils.clickInputButton("Sign in");
};

export const buyerLogin = async (email: string, password: string) => {
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

export const sellerLogin = async (email: string, password: string) => {
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

export const signOut = async () => {
  await Utils.clickLink("Sign out");
  await Utils.matchText("h1", "Sign in to the Marketplace");
};
