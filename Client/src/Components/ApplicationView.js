import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { Home } from "./Home";
import Login from "./Auth/Login";
import Register from "./Auth/Register";
import { Inventory } from "./Inventory/Inventory";

export default function ApplicationViews({ isLoggedIn }) {
    return (
        <Routes>
            <Route path="/">
                <Route
                    index
                    element={isLoggedIn ? <Home /> : <Navigate to="/login" />}
                />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="inventory" element={<Inventory />} />
                <Route path="*" element={<p>Go away</p>} />
            </Route>
        </Routes>
    );
}