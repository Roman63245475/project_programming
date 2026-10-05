import { useParams } from "react-router";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api";

const api = new Api();
const apiImageUrl = "http://localhost:5260";

export default function ProductDetails() {
    const { id } = useParams();
    const [product, setProduct] = useState<Product | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

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

    return (
        <main className="content-pane">
            {loading ? <p>Loading product…</p> : error ? <p>{error}</p> : product && (
                <>
                    <h1>{product.name ?? "Unnamed product"}</h1>
                    {product.image_path && (
                        <img
                            src={`${apiImageUrl}/${product.image_path.replaceAll("\\", "/")}`}
                            alt={product.name ?? "Product"}
                            style={{ maxWidth: "100%", height: "auto" }}
                        />
                    )}
                    <p>Price: {product.price ?? "-"}</p>
                    <p>Quantity: {product.quantity ?? "-"}</p>
                    <p>Available: {product.available ? "Yes" : "No"}</p>
                    <p>Category ID: {product.category_id ?? "-"}</p>
                </>
            )}
        </main>
    );
}
