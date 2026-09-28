import "./index.css";
import {useState, useEffect} from "react";

import logo from "./logo.svg";
import reactLogo from "./react.svg";

export function App() {
    const prod = {name: "Kalivan", price: 20.4, quantity: 1, available: true, category_id: 1};
  return (
    <div>
        <button onClick={() => create_product(prod)}>clk me</button>
    </div>
  );
}

async function create_product(prod: object){
    const res = await fetch("http://localhost:5260/create_product", {method: "Post", headers: {"Content-Type": "application/json"}, body: JSON.stringify(prod)})
    console.log(res.status)
}
export default App;
