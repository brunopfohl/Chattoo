const path = require("path");
module.exports = {
  webpack: {
    alias: {
      '@interfaces': path.resolve(__dirname, "src/common/intefaces")
    }
  }
}