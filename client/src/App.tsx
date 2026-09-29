import "./index.css";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api.ts";
import {useNavigate} from "react-router";

const productApi = new Api();

export function App() {
  const [products, setProducts] = useState<Product[]>([]);

  async function loadProducts() {
    const response = await productApi.getProducts.productGetProducts();
    setProducts(response.data);
  }

  const navigate = useNavigate();

  useEffect(() => {
    void loadProducts();
  }, []);

  return (
    <main>
      <button onClick={() => navigate("/create_product")}>Create product</button>

      {products.length === 0 ? (
        <p>No products found.</p>
      ) : (
        products.map((product) => (
          <div key={product.id ?? `${product.name}-${product.price}`} className="product">
            <strong>{product.name ?? "Unnamed product"}</strong>
            {product.price !== undefined && <span> — {product.price}</span>}
            {product.quantity !== undefined && (
              <span> (quantity: {product.quantity})</span>
            )}
          </div>
        ))
      )}
    </main>
  );
}

export default App;
