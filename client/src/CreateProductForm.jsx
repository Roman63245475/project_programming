import {useEffect, useState} from "react";
import {Api} from "../Api";

import {useNavigate} from "react-router";

const api = new Api();

const CreateProductForm = () => {
    const navigate = useNavigate();

    const [categories, setCategories] = useState([]);
    const [loaded, setLoaded] = useState(false);

    const [product, setProduct] = useState({
        name: '',
        image: null,
        price: 0.0,
        quantity: 0,
        available: false,
        category_id: null
    });

    const setField = (name, value) => {
        setProduct({
            ...product,
            [name]: value,
        })
    }

    useEffect(() => {
        api.api.categoryGetCategories().then(r => r.json()).then(data => setCategories(data));
        setLoaded(true);
    },[])

    if (!loaded) {
        return <>loading...</>
    }

    return (
        <div>
            Name: <input type={'text'} placeholder={"Product's name"} onChange={(e) => {setField('name', e.target.value)}}></input>
            Category: <select defaultValue="" onChange={(e) => setField("category_id", Number(e.target.value))}>
                <option value="" disabled>Select a category</option>
                {categories.map((category) => (
                    <option key={category.id} value={category.id}>
                        {category.name}
                    </option>
                ))}
            </select>
            Image: <input type={'file'} onChange={(e) => setField('image', e.target.files[0])}></input>
            Price: <input type={'number'} step={'any'} placeholder={"Product's price"} onChange={(e) => {setField('price', e.target.value)}}></input>
            quantity: <input type={'number'} placeholder={"Product's quantity"} onChange={(e) => {setField('quantity', e.target.value)}}></input>
            Available: <input type={'checkbox'} onChange={(e) => {setField('available', e.target.checked)}}></input>
            <button onClick={() => create_product(product, navigate)}>Create</button>
        </div>
    )
}

const create_product = async (product, navigate) => {
    const formData = new FormData();
    formData.append('name', product.name);
    formData.append('price', product.price);
    formData.append('quantity', product.quantity);
    formData.append('available', product.available);
    formData.append('category_id', product.category_id);
    formData.append('image', product.image);

    for (const [key, value] of formData.entries()) {
        console.log(key, value, typeof value);
    }
    await api.createProduct.productCreateProduct(formData)
    navigate("/")
}

export default CreateProductForm;