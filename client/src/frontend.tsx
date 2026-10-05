/**
 * This file is the entry point for the React app, it sets up the root
 * element and renders the App component to the DOM.
 *
 * It is included in `src/index.html`.
 */

import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { App } from "./App";
import {RouterProvider, createBrowserRouter} from "react-router";
import CreateProductForm from "@/CreateProductForm.jsx";
import ProductDetails from "@/ProductDetails";
const elem = document.getElementById("root")!;
const app = (
  <RouterProvider router={createBrowserRouter([
      {
        path: '/',
        element: <App/>
      },
      {
          path: '/create_product',
          element: <CreateProductForm/>
      },
      {
          path:'/prod/:id',
          element:<ProductDetails />
      }
  ])}/>
);

// https://bun.com/docs/bundler/hot-reloading#import-meta-hot-data
(import.meta.hot.data.root ??= createRoot(elem)).render(app);
