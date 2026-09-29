import "./index.css";
import {useState} from "react";
import {Api} from "../Api.ts";
const apiCategory = new Api();

export function CategoryComponent({ onClose }: { onClose: () => void }) {
    const [categoryName, setCategoryName] = useState("");
    const [message, setMessage] = useState("");
    async function createCategory() {
        try{
            const request = await apiCategory.api.categoryCreateCategory({
                name: categoryName
            })
            setMessage(request.data.message ?? "");
        }catch(err: any){
            setMessage(err.error.message ?? "");
        }

    }
    return (
        <div className="pop-up-bg" onClick={onClose}>
            <div className="pop-up" onClick={(e) => e.stopPropagation()}>
                <h2>Create a category</h2>
                <div className={"space-even-v"}>
                    <p>Category Name:</p>
                    <input type={"text"} onChange={(e) => setCategoryName(e.target.value)} value={categoryName} />
                    {
                        message.trim().length > 0 && <p>{message}</p>
                    }
                    <button onClick={() => createCategory()}>Create</button>
                </div>
            </div>
        </div>
    );
}