import ReactDOM from "react-dom/client";
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";

import App from "./App";
import Home from "./Home";
import About from "./About";
import Authors from "./Authors";
import Books from "./Books";
import Genres from "./Genres";

import { DevTools } from "jotai-devtools";
import "jotai-devtools/styles.css";
import "./index.css";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { index: true, element: <Home /> },
            { path: "about", element: <About /> },
            { path: "books", element: <Books /> },
            { path: "authors", element: <Authors /> },
            { path: "genres", element: <Genres /> },
        ],
    },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
    <>
        <RouterProvider router={router} />
        <DevTools />
    </>
);

