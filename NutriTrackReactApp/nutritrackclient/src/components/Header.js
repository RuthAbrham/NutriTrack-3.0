import React from "react";

const Header = () => {
  const handleLogin = async (e) => {
    e.preventDefault();
    fetch("http://localhost/5085/identity/account/login", { method: "GET" });
  };
  return (
    <header className="bg-gray-800 text-white p-4">
      <h1 className="text-center text-xl">Food App</h1>
      <div className="flex justify-end p-2">
        {/* <button
          className="p-2 bg-blue-300 rounded-md border"
          onClick={(e) => {
            handleLogin(e);
          }}
        >
          Login
        </button> */}
      </div>
    </header>
  );
};

export default Header;
