const baseUrl = "http://localhost:5085";

export const getFoods = async () => {
  try {
    console.info("fetching foods");
    const res = await fetch(`${baseUrl}/api/foodApi`, { method: "GET" });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
export const getFood = async (id) => {
  try {
    console.info("fetching food");
    const res = await fetch(`${baseUrl}/api/foodApi/${id}`, { method: "GET" });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const createFood = async (food) => {
  try {
    console.info("fetching foods");
    const res = await fetch(`${baseUrl}/api/foodApi`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(food),
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const updateFood = async (id, food) => {
  try {
    console.info("fetching foods");
    const res = await fetch(`${baseUrl}/api/foodApi/${id}`, {
      method: "PUT",
      body: JSON.stringify(food),
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
export const deleteFood = async (id) => {
  try {
    console.info("fetching foods");
    const res = await fetch(`${baseUrl}/api/foodApi/${id}`, {
      method: "DELETE",
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
