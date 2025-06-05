// Stash 脚本专用写法
(async () => {
  const url = "http://ip-api.com/json/?fields=8450015&lang=zh-CN";
  let response = await httpAPI("GET", url);
  let jsonData;
  try {
    jsonData = JSON.parse(response.body);
  } catch (e) {
    $notify("节点信息", "JSON解析失败", e.toString());
    $done({});
    return;
  }

  let query = jsonData.query;
  let isp = jsonData.isp;
  let as = jsonData.as;
  let country = jsonData.country;
  let city = jsonData.city;
  let timezone = jsonData.timezone;
  let lon = jsonData.lon;
  let lat = jsonData.lat;
  let currency = jsonData.currency;
  let emoji = getFlagEmoji(jsonData.countryCode);

  // Stash 参数通过 args 传递
  const params = getParams(args);

  let body = {
    title: "节点信息",
    content: `🗺️IP：${query}\n🖥️ISP：${isp}\n#️⃣ASN：${as}\n🌍国家/地区：${emoji}${country}\n🏙城市：${city}\n🕗时区：${timezone}\n📍经纬度：${lon},${lat}\n🪙货币：${currency}`,
    icon: params.icon || "network",
    "icon-color": params.color || "#007AFF"
  };
  $done(body);

  // 封装请求
  function httpAPI(method, url) {
    return new Promise((resolve, reject) => {
      $httpClient[method.toLowerCase()](url, (error, response, data) => {
        if (error) reject(error);
        else resolve({ response, body: data });
      });
    });
  }

  // 获取国旗 emoji
  function getFlagEmoji(countryCode) {
    if (!countryCode) return "";
    if (countryCode.toUpperCase() === "TW") countryCode = "CN";
    const codePoints = countryCode
      .toUpperCase()
      .split("")
      .map(char => 127397 + char.charCodeAt());
    return String.fromCodePoint(...codePoints);
  }

  // 解析参数
  function getParams(param) {
    if (!param) return {};
    return Object.fromEntries(
      param
        .split("&")
        .map((item) => item.split("="))
        .map(([k, v]) => [k, decodeURIComponent(v)])
    );
  }
})();
