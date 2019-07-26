import * as util from "../../utils";

export const navigate = async () => {
  await util.clickLink("Opportunities");
};

export const selectBrief = async (title: string) => {
  await util.clickLink(title);
};

export const applyForAtm = async () => {
  await util.clickLink("Apply for opportunity");
};

export const checkAppliedForAtm = async  (title: string) => {
  await navigate();
  await selectBrief(title);
  await util.matchText("p", "You have already applied for this opportunity.");
};

export const checkAppliedForSpecialist = async (title: string, specialistNumber: number, numberOfSuppliers: string) => {
  await navigate();
  await selectBrief(title);
  if (specialistNumber + 1 === parseInt(numberOfSuppliers, 10)) {
    await util.matchText("div", `Sellers can submit up to `);
  } else {
    await util.matchText("p", `You have submitted ${specialistNumber + 1} candidate`);
  }
};

export const applyForSpecialist = async () => {
  await util.clickLink("Apply for opportunity");
};

export const checkAppliedForRfx = async (title: string) => {
  await navigate();
  await selectBrief(title);
  await util.matchText("p", "You have already applied for this opportunity.");
};

export const applyForRfx = async () => {
  await util.clickLink("Apply for opportunity");
};

export const viewSpecialistApplication = async (title: string) => {
  await util.clickLink("View your application");
  await util.matchText("h1", `Thanks for your application. You've now applied for ‘${title}’`);
};

export const applyForTraining = async () => {
  await util.clickLink("Apply Now");
};

export const viewTrainingApplication = async (title: string) => {
  await util.clickLink("View your application");
  await util.matchText("h1", `Thanks for your application. You've now applied for ‘${title}’`);
};
