import {useState} from "react";
import {Api} from "../Api";
import {useNavigate} from "react-router";

const productApi = new Api();

const CreateProductForm = () => {
    const navigate = useNavigate();
    const [product, setProduct] = useState({
        name: '',
        image: null,
        price: 0.0,
        quantity: 0,
        available: false,
    });

    const setField = (name, value) => {
        setProduct({
            ...product,
            [name]: value,
        })
    }

    return (
        <div>
            Name: <input type={'text'} placeholder={"Product's name"} onChange={(e) => {setField('name', e.target.value)}}></input>
            Category: selection field needs to be here.
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
    formData.append('category_id', 1);
    formData.append('image', product.image);

    await productApi.createProduct.productCreateProduct(formData)
    navigate("/")
}

export default CreateProductForm;