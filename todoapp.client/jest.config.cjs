module.exports = {
 testEnvironment: "jest-environment-jsdom",
 transform: {
  "^.+\\.(js|jsx)$": "babel-jest", 
 },
 setupFilesAfterEnv: ["@testing-library/jest-dom/extend-expect"], 
 moduleFileExtensions: ["js", "jsx"], 
};
