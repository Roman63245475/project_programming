import "./index.css";
import { useEffect, useState } from "react";
import { Api, type Product } from "../Api.ts";

const productApi = new Api();

export function App() {
  const [products, setProducts] = useState<Product[]>([]);

  async function loadProducts() {
    const response = await productApi.getProducts.productGetProducts();
    setProducts(response.data);
  }

  async function createProduct() {
    const newProduct: Product = {
      name: "Kalivan",
      price: 20.4,
      quantity: 1,
      available: true,
      category_id: 1,
    };

    await productApi.createProduct.productCreateProduct(newProduct);
    await loadProducts();
  }

  useEffect(() => {
    void loadProducts();
  }, []);

  return (
    <main>
      <button onClick={createProduct}>Create product</button>

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
