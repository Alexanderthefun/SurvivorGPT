import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { Home } from "./Home/Home";
import Login from "./Auth/Login";
import Register from "./Auth/Register";
import { Inventory } from "./Inventory/Inventory";
import { Guide } from "./Guide/Guide";


export default function ApplicationViews({ isLoggedIn }) {
    return (
        <Routes>
            <Route path="/">
                <Route
                    index
                    element={isLoggedIn ? <Home /> : <Navigate to="/login" />}
                />
                <Route
                    path="inventory"
                    element={isLoggedIn ? <Inventory /> : <Navigate to="/login" />}
                />
                <Route
                    path="guide"
                    element={isLoggedIn ? <Guide /> : <Navigate to="/login" />}
                />
                <Route path="login" element={<Login />} />
                <Route path="login/register" element={<Register />} />
                <Route path="*" element={<p>Go away</p>} />
            </Route>
        </Routes>
    );
}