import "./index.css";
import {useState} from "react";

export function CategoryComponent({ onClose }: { onClose: () => void }) {
    const [categoryName, setCategoryName] = useState("");

    async function createCategory() {
        const newCategory = {
            name: categoryName
        }
        const request = await fetch("http://localhost:5260/api/category/CreateCategory",
            {method: "POST", headers: {"Content-Type": "application/json"}, body: JSON.stringify(newCategory)});
        console.log(request.status);
    }
    return (
        <div className="pop-up-bg" onClick={onClose}>
            <div className="pop-up" onClick={(e) => e.stopPropagation()}>
                <h2>Create a category</h2>
                <div className={"space-even-v"}>
                    <p>Category Name:</p>
                    <input type={"text"} onChange={(e) => setCategoryName(e.target.value)} value={categoryName} />
                    <button onClick={() => {createCategory(); onClose()}}>Create</button>
                </div>
            </div>
        </div>
    );
}