import create from "../../flows/brief/specialist";
import startBrief from "../../flows/dashboard/buyer";
import { buyerLogin } from "../../flows/login/actions";

describe("should be able to create specialist opportunity", () => {
  const categories = {
    1: "Strategy and Policy",
    3: "User research and Design",
    4: "Agile delivery and Governance",
    6: "Software engineering and Development",
    10: "Support and Operations",
    7: "Content and Publishing",
    14: "Change and Transformation",
    15: "Training, Learning and Development",
    9: "Marketing, Communications and Engagement",
    8: "Cyber security",
    11: "Data science",
    13: "Emerging technologies",
    17: "ICT risk management and audit activities",
    18: "ICT systems integration",
    19: "Digital sourcing and ICT procurement",
    20: "Platforms integration",
    21: "Service Integration and Management",
  };

  Object.keys(categories).forEach((id) => {
    it(`should be able to create ${categories[id]} specalist opportunity`, async () => {
      const now = Date.now();
      await buyerLogin();
      await startBrief();
      await create({
        categoryId: `${id}`,
        categoryName: categories[id],
        locations: ["Tasmania"],
        title: `${categories[id]} Specialist ${now.valueOf()}`,
      });
    });
  });
});
