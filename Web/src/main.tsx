import React from "react";
import ReactDOM from "react-dom";
import "tailwindcss/tailwind.css";
import "simplebar/dist/simplebar.min.css";
import "./styles.css";
import App from "./App";

ReactDOM.render(
    <React.StrictMode>
        <div className="flex-row min-h-screen px-4">
            <div className="flex flex-col min-h-screen items-center justify-center gap-8 py-4">
                <App />
            </div>
        </div>
    </React.StrictMode>,
    document.getElementById("root")
);
