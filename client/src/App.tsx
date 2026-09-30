import "./index.css";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api.ts";
import {useNavigate} from "react-router";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {CategoryComponent} from "@/CategoryComponent.tsx";
const BackendApi = new Api();

export function App() {
    const [products, setProducts] = useState<Product[]>([]);
    const [isCategoryOpen, setIsCategoryOpen] = useState(false);
    const [category, setCategory] = useState<Category[]>([]);
    
    async function loadCategories() {
        const resp = await BackendApi.api.categoryGetCategories();
        setCategory(resp.data);
    }
    async function loadProducts() {
        const response = await BackendApi.getProducts.productGetProducts();
        setProducts(response.data);
    }
    const navigate = useNavigate();
    useEffect(() => {
        void loadCategories();
        void loadProducts();
    }, []);
    return (
        <main>
            {isCategoryOpen && <CategoryComponent onClose={() => setIsCategoryOpen(false)} />}
            <h1>List of products</h1>
            <div className="content-pane">
                <div className={"space-even-h"}>
                    <select>
                        {category.map((category) => (
                            <option key={category.id}>{category.name}</option>
                        ))}
                    </select>
                    <button onClick={() => setIsCategoryOpen(!isCategoryOpen)}>Create sick ass category</button>
                    <button onClick={() => navigate("/create_product")}>Create product</button>

                </div>
                {products.length === 0 ? (
                    <p>No products found.</p>
                ) : (
                    <table>
                        <thead>
                        <tr>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Available</th>
                        </tr>
                        </thead>
                        <tbody>
                        {products.map((product) => (
                            <tr key={product.id ?? `${product.name}-${product.price}`}>
                                <td>{product.name ?? "Unnamed product"}</td>
                                <td>{product.price !== undefined ? product.price : "-"}</td>
                                <td>{product.quantity !== undefined ? product.quantity : "-"}</td>
                                <td>{product.quantity !== undefined && product.available ? "Yes" : "No"}</td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                )}
            </div>
        </main>
    );
}
export default App;
