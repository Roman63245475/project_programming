import {useNavigate, useParams} from "react-router";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api";

const api = new Api();
const apiImageUrl = "http://localhost:5260";

export default function ProductDetails() {
    const { id } = useParams();
    const [product, setProduct] = useState<Product | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [amount, setAmount] = useState(0);
    const navigate = useNavigate();

    useEffect(() => {
        let cancelled = false;

        async function loadProduct() {
            const productId = Number(id);
            if (!Number.isInteger(productId) || productId <= 0) {
                setError("Invalid product ID.");
                setLoading(false);
                return;
            }

            try {
                // The generated client has a list endpoint, so find this ID in its response.
                const response = await api.getProducts.productGetProducts();
                const foundProduct = response.data.find((item) => item.id === productId);
                if (!cancelled) {
                    if (foundProduct) setProduct(foundProduct);
                    else setError("Product not found.");
                }
            } catch {
                if (!cancelled) setError("Could not load the product.");
            } finally {
                if (!cancelled) setLoading(false);
            }
        }

        void loadProduct();
        return () => { cancelled = true; };
    }, [id]);

    async function purchase(){
        if (!product?.available){
            alert("this product is not for sale now")
            return
        }
        if (amount === 0){
            alert('how many?')
            return
        }
        const random = Math.floor(Math.random() * 10) + 1;
        console.log(random);
        if (random === 1){
            await api.fbiCaught.productFbiCaught({id: Number(id)})
            alert("hahaha I'm fbi agent like lin you're caught")
        }
        else{
            await api.purchase.productPurchase({id: Number(id), purchase_quantity: amount})
        }
        navigate('/')
    }

    return (
        <main>
            {loading ? <p>Loading product…</p> : error ? <p>{error}</p> : product && (
                <>
                    <a>Items in the basket: {amount} </a>
                    <h1>{product.name ?? "Unnamed product"}</h1>
                    <div className={"content-pane"}>
                        {product.image_path && (
                            <div className={"pd-image-holder"}>
                                <img
                                    src={`${apiImageUrl}/${product.image_path.replaceAll("\\", "/")}`}
                                    alt={product.name ?? "Product"}
                                    style={{ maxWidth: "100%", height: "auto" }}
                                />
                            </div>

                        )}
                        <div id={"product-description-c"} className={"space-even-v"}>
                            <div>
                                <p><strong>Price:</strong> {product.price ?? "-"}</p>
                                <p><strong>Quantity:</strong> {product.quantity ?? "-"}</p>
                                <p><strong>Available:</strong> {product.available ? "Yes" : "No"}</p>
                                <p><strong>Category ID:</strong> {product.category_id ?? "-"}</p>
                                <a>🛒</a> <br/><br/><br/>
                                <div className={"space-even-v"}>
                                    <button onClick={() => {
                                        if (product.quantity){
                                            if (amount < product.quantity){
                                                console.log('increase')
                                                setAmount(amount + 1)
                                            }
                                        }
                                        else{
                                            setAmount(amount + 1)
                                        }

                                }}>Add one more item</button>
                                    <button onClick={() => {
                                        if (amount > 0){
                                            setAmount(amount - 1)
                                        }
                                    }}>Remove one item</button>
                                    <button onClick={() => purchase()}>Buy</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </>
            )}
        </main>
    );
}
