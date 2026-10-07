import {useEffect, useState} from "react";
import {Api} from "../Api";

import {useNavigate, useParams} from "react-router";

const api = new Api();

const CreateProductForm = () => {
    const navigate = useNavigate();
    const { id } = useParams();

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

    useEffect(() => {
        if (id) {
            api.id.productGetProduct(id).then(r => r.json()).then(data => setProduct(data));
        }
    }, [id])

    if (!loaded) {
        return <>loading...</>
    }

    async function save_changes(product, navigate){
        const formData = new FormData();
        formData.append('name', product.name);
        formData.append('price', product.price);
        formData.append('quantity', product.quantity);
        formData.append('available', product.available);
        formData.append('category_id', product.category_id);

        if (product.image instanceof File) {
            formData.append('image', product.image);
        }
        await api.id.productUpdateProduct(id, formData)
        navigate('/');
    }

    return (
        <div className={"space-even-v"}>
            <h1>Product {id ? "Editing" : "Creation"}</h1>
            <div> Name: <input type={'text'} value={product.name} placeholder={"Product's name"} onChange={(e) => {setField('name', e.target.value)}}></input> </div>
            <div> Price: <input type={'number'} value={product.price} step={'any'} placeholder={"Product's price"} onChange={(e) => {setField('price', e.target.value)}}></input> </div>
            <div> Quantity: <input type={'number'} value={product.quantity} placeholder={"Product's quantity"} onChange={(e) => {setField('quantity', e.target.value)}}></input> </div>
            <div> Category: <select value={product.category_id} defaultValue={""} onChange={(e) => setField("category_id", Number(e.target.value))}>
            <option value="" disabled>Select a category</option>
                {categories.map((category) => (
                    <option key={category.id} value={category.id}>
                        {category.name}
                    </option>
                ))}
            </select> </div>
            <div> Image: <input type={'file'} onChange={(e) => setField('image', e.target.files[0])}></input> </div>
            <div> Available: <input type={'checkbox'} checked={product.available} onChange={(e) => {setField('available', e.target.checked)}}></input> </div>

            <button onClick={() => {
                if (!product.category_id) {
                    alert('Please select a category');
                    return;
                }
                if (id) {
                    save_changes(product, navigate)
                }else {
                    create_product(product, navigate)
                }
            }}>{id ? 'Save Changes' : 'Create'}</button>
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