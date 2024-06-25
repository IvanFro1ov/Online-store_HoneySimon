// server.js
const corsAnywhere = require("cors-anywhere");

const host = "localhost";
const port = 8080;

corsAnywhere
  .createServer({
    originWhitelist: [], // Разрешить все источники
    requireHeader: [], // Убираем требование заголовков
    removeHeaders: ["cookie", "cookie2"],
  })
  .listen(port, host, () => {
    console.log(`CORS Anywhere proxy is running on ${host}:${port}`);
  });
