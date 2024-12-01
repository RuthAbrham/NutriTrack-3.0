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
    console.info("creating food");
    const res = await fetch(`${baseUrl}/api/foodApi`, {
      method: "POST",
      body: food, // FormData object
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const updateFood = async (id, food) => {
  try {
    console.info("updating food");
    const res = await fetch(`${baseUrl}/api/foodApi/${id}`, {
      method: "PUT",
      body: food, // FormData object
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const deleteFood = async (id) => {
  try {
    console.info("deleting food");
    const res = await fetch(`${baseUrl}/api/foodApi/${id}`, {
      method: "DELETE",
    });
    return res.json();
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
