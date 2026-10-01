import "./index.css";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api.ts";
import {useNavigate} from "react-router";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {CategoryComponent} from "@/CategoryComponent.tsx";
const BackendApi = new Api();
const ApiImgUrl = "http://localhost:5260";
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
            <div className="content-pane">
                <div className={"space-even-h"}>
                    <select>
                        {category.map((category) => (
                            <option key={category.id}>{category.name}</option>
                        ))}
                    </select>
                    <button onClick={() => setIsCategoryOpen(!isCategoryOpen)}>Create sick ass category</button>
                    <button onClick={() => navigate("/create_product")}>Create product</button>
                    <h1>List of products</h1>
                </div>
                    
                    <div className="product-grid">
                        {products.length === 0 ? (
                            <p>No products found.</p>
                            ) : (
                                products.map((product) => (
                                    <article key={product.id} className="product-card">
                                        <img src={`${ApiImgUrl}/${product.image_path?.replaceAll("\\", "/")}`} alt={product.name ?? "Product"} />
                                        <p>{product.name ?? "Unnamed product"}</p>
                                        <p>Price: {product.price !== undefined ? product.price : "-"}</p>
                                        <p>Quantity: {product.quantity !== undefined ? product.quantity : "-"}</p>
                                        <p>Available: {product.quantity !== undefined && product.available ? "Yes" : "No"}</p>
                                    </article>
                                ))
                            )
                        }
                    </div>
                    
                </div>
        </main>
    );
}
export default App;
