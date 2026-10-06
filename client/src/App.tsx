import "./index.css";
import { useEffect, useState } from "react";
import {Api, type Category, type Product} from "../Api.ts";
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
    const navigate = useNavigate();
    useEffect(() => {
        void loadCategories();
        void loadProducts();
    }, []);
    async function loadCategories() {
        const resp = await BackendApi.api.categoryGetCategories();
        setCategory(resp.data);
    }
    async function loadProductsByCategoryId(categoryId : number) {
        if (categoryId > 0) {
            try{
                const response = await BackendApi.getProductsByCategoryId.productGetProductsByCategoryId({
                    id: categoryId
                });
                setProducts(response.data);
            }catch(error){
                setProducts([]);
            }
        }else{
            setProducts([]);
        }
    }

    async function deleteProduct(productId: number) {
        const response = await BackendApi.id.productDeleteProduct(productId);
    }

    async function loadProducts(){
        const resp = await BackendApi.getProducts.productGetProducts();
        setProducts(resp.data);
    }

    function getProductDetails(Product) {
        navigate("/prod/"+Product.id);
    }

    return (
        <main>
            
            {isCategoryOpen && <CategoryComponent onClose={() => setIsCategoryOpen(false)} />}
            <div className="content-pane">
                <h1>List of products</h1>

                <div className={"space-even-h"}>
                    <select defaultValue={""} onChange={(e) => {
                        const selectedId = parseInt(e.target.value);
                        loadProductsByCategoryId(selectedId);
                    }}>
                        <option value="" disabled>Select a category to view products</option>
                        {category.map((category) => (
                            <option key={category.id} value={category.id} >{category.name}</option>
                        ))}
                    </select>
                    <button onClick={() => setIsCategoryOpen(!isCategoryOpen)}>Create sick ass category</button>
                    <button onClick={() => navigate("/create_product")}>Create product</button>
                </div>
                    <div className="product-grid">
                        {products.length === 0 ? (
                            <p>No products found.</p>
                            ) : (
                                products.map((product) => (
                                    <div
                                        key={product.id}
                                        className="product-card"
                                        onClick={() => getProductDetails(product)}
                                    >
                                        <img src={`${ApiImgUrl}/${product.image_path?.replaceAll("\\", "/")}`} alt={product.name ?? "Product"} />
                                        <p>{product.name ?? "Unnamed product"}</p>
                                        <p>Price: {product.price !== undefined ? product.price : "-"}</p>
                                        <p>Quantity: {product.quantity !== undefined ? product.quantity : "-"}</p>
                                        <p>Available: {product.quantity !== undefined && product.available ? "Yes" : "No"}</p>
                                        <div className={"space-even-h"}>
                                            <button onClick={(e) => {
                                                e.stopPropagation();
                                                navigate(`/create_product/${product.id}`);
                                            }}>Edit Product</button>
                                            <button onClick={async (e) => {
                                                e.stopPropagation();
                                                await deleteProduct(product.id)
                                                await loadProducts();
                                            }}>Delete Product</button>
                                        </div>
                                    </div>
                                ))
                            )
                        }
                    </div>
                </div>
        </main>
    );
}
export default App;
