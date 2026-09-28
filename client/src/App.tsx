import "./index.css";
import {useState, useEffect} from "react";

import logo from "./logo.svg";
import reactLogo from "./react.svg";

export function App() {
    const [response, setResponse] = useState("nothing happened yet");
    useEffect(() => {
        console.log("api request")
      fetch('http://localhost:5260/test_con').then(res => res.json()).then(r => setResponse(JSON.stringify(r)));
    },[])
  return (
    <>{response}</>
  );
}

export default App;
