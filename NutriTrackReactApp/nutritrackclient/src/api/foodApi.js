const baseUrl = "http://localhost:5085";

export const getFoods = async () => {
  try {
    console.info("fetching foods");
    const res = fetch(`${baseUrl}/api/food`, { method: "GET" });
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
export const getFood = async (id) => {
  try {
    console.info("fetching food");
    const res = fetch(`${baseUrl}/api/food/${id}`, { method: "GET" });
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const createFood = async (food) => {
  try {
    console.info("fetching foods");
    const res = fetch(`${baseUrl}/api/food`, {
      method: "GET",
      body: JSON.stringify(food),
    });
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};

export const updateFood = async (id, food) => {
  try {
    console.info("fetching foods");
    const res = fetch(`${baseUrl}/api/food/${id}`, {
      method: "PUT",
      body: JSON.stringify(food),
    });
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
export const deleteFood = async (id) => {
  try {
    console.info("fetching foods");
    const res = fetch(`${baseUrl}/api/food/${id}`, {
      method: "DELETE",
    });
  } catch (error) {
    throw new Error(`error: ${error}`);
  }
};
