// src/pages/Food.js
import React, { useState, useEffect } from "react";
import { getFoods, createFood, updateFood, deleteFood } from "../api/foodApi";
import { Link } from "react-router-dom";

const Food = () => {
  const [foods, setFoods] = useState([]);
  const [currentFood, setCurrentFood] = useState(null);
  const [newFood, setNewFood] = useState({
    name: "", foodGroup: "", price: "", weight: "", 
    energy: "", fat: "", saturatedFat: "", carbohydrates: "", 
    sugar: "", protein: "", salt: ""
  });
  const [imageFile, setImageFile] = useState(null);
  const [error, setError] = useState(null);
  const [isFormVisible, setIsFormVisible] = useState(false);

  useEffect(() => {
    fetchFoods();
  }, []);

  const fetchFoods = async () => {
    try {
      const res = await getFoods();
      setFoods(res);
      setError(null); // Clear any previous error
    } catch (error) {
      console.error(error);
      setError("Failed to fetch foods. Please make sure the backend server is running.");
      setFoods([]); // Show an empty list on error
    }
  };

  const handleAddOrUpdateFood = async () => {
    const formData = new FormData();
    Object.keys(newFood).forEach((key) => {
      formData.append(key, newFood[key]);
    });
    if (imageFile) {
      formData.append("imageFile", imageFile);
    }

    // Log the FormData contents to debug
    for (let [key, value] of formData.entries()) {
      console.log(key, value);
    }

    try {
      if (currentFood) {
        await updateFood(currentFood.id, formData);
      } else {
        await createFood(formData);
      }
      setNewFood({
        name: "", foodGroup: "", price: "", weight: "", 
        energy: "", fat: "", saturatedFat: "", carbohydrates: "", 
        sugar: "", protein: "", salt: ""
      });
      setImageFile(null);
      fetchFoods();
      setIsFormVisible(false);
      setCurrentFood(null);
    } catch (error) {
      console.error(error);
      setError("Failed to add/update food. Please try again.");
    }
  };

  const handleFileChange = (e) => {
    setImageFile(e.target.files[0]);
  };

  const handleInputChange = (e) => {
    setNewFood({ ...newFood, [e.target.name]: e.target.value });
  };

  const handleEditFood = (food) => {
    setCurrentFood(food);
    setNewFood({
      name: food.name || "", foodGroup: food.foodGroup || "", price: food.price || "", 
      weight: food.weight || "", energy: food.energy || "", fat: food.fat || "", 
      saturatedFat: food.saturatedFat || "", carbohydrates: food.carbohydrates || "", 
      sugar: food.sugar || "", protein: food.protein || "", salt: food.salt || ""
    });
    setIsFormVisible(true);
  };

  const handleDeleteFood = async (id) => {
    try {
      await deleteFood(id);
      await fetchFoods();
    } catch (error) {
      console.error(error);
      setError("Failed to delete food. Please try again.");
    }
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Food</h1>
      {error && <div className="text-red-500 mb-4">{error}</div>}
      <button
        onClick={() => {
          setIsFormVisible(true);
          setCurrentFood(null);
        }}
        className="bg-blue-500 text-white px-4 py-2 rounded mb-4"
      >
        Add Food
      </button>
      <div
        className={`fixed top-0 right-0 w-1/3 h-full bg-white shadow-lg transition-transform transform ${
          isFormVisible ? 'translate-x-0' : 'translate-x-full'
        }`}
      >
        <div className="p-4">
          <button
            onClick={() => {
              setIsFormVisible(false);
              setCurrentFood(null);
            }}
            className="bg-gray-500 text-white px-4 py-2 rounded mb-4"
          >
            Close
          </button>
          <input
            type="text"
            name="name"
            value={newFood.name}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Name"
          />
          <input
            type="text"
            name="foodGroup"
            value={newFood.foodGroup}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Food Group"
          />
          <input
            type="number"
            name="price"
            value={newFood.price}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Price"
          />
          <input
            type="number"
            name="weight"
            value={newFood.weight}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Weight"
          />
          <input
            type="number"
            name="energy"
            value={newFood.energy}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Energy"
          />
          <input
            type="number"
            name="fat"
            value={newFood.fat}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Fat"
          />
          <input
            type="number"
            name="saturatedFat"
            value={newFood.saturatedFat}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Saturated Fat"
          />
          <input
            type="number"
            name="carbohydrates"
            value={newFood.carbohydrates}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Carbohydrates"
          />
          <input
            type="number"
            name="sugar"
            value={newFood.sugar}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Sugar"
          />
          <input
            type="number"
            name="protein"
            value={newFood.protein}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Protein"
          />
          <input
            type="number"
            name="salt"
            value={newFood.salt}
            onChange={handleInputChange}
            className="border p-2 mb-2 w-full"
            placeholder="Salt"
          />
          <input
            type="file"
            onChange={handleFileChange}
            className="border p-2 mb-2 w-full"
            placeholder="Upload Image"
          />
          <button
            onClick={handleAddOrUpdateFood}
            className="bg-green-500 text-white px-4 py-2 rounded"
          >
            {currentFood ? "Update Food" : "Add Food"}
          </button>
        </div>
      </div>
      <ul className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {foods.length > 0 ? (
          foods.map((food) => (
            <li key={food.id} className="border p-4 rounded shadow-md">
              <img src={`http://localhost:5085/${food.imageURL}`} alt={food.name} className="w-full h-48 object-cover mb-2"/>
              <h2 className="text-xl font-bold">{food.name}</h2>
              <p>{food.foodGroup}</p>
              <p>Price: ${food.price}</p>
              <p>Weight: {food.weight}g</p>
              <div className="flex justify-between mt-2">
                <Link to={`/food/${food.id}`} className="bg-green-500 text-white px-2 py-1 rounded">
                  Details
                </Link>
                <button
                  onClick={() => handleEditFood(food)}
                  className="bg-yellow-500 text-white px-2 py-1 rounded mr-2"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDeleteFood(food.id)}
                  className="bg-red-500 text-white px-2 py-1 rounded"
                >
                  Delete
                </button>
              </div>
            </li>
          ))
        ) : (
          !error && <div className="text-gray-500">No foods available. Add some!</div>
        )}
      </ul>
    </div>
  );
};

export default Food;
