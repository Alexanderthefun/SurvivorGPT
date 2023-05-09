import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";

import Login from "./Auth/Login";
import Register from "./Auth/Register";

export default function ApplicationViews({ isLoggedIn }) {
    return (
        <Routes>
            <Route path="/">
                <Route
                    index
                    element={isLoggedIn ? <p>You've made it this far. Now keep building.</p> : <Navigate to="/login" />}
                />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="*" element={<p>Go away</p>} />
            </Route>
        </Routes>
    );
}