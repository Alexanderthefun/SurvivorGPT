import React from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import { logout } from "../../Modules/authManager";
import "./header.css";

export default function Header({ isLoggedIn, userProfile }) {
    return (
        <div className="headerContainer">
            <nav className="navbar">
                {isLoggedIn && (
                    <>
                        <RRNavLink className="nav-link" to="/Inventory">
                            Inventory
                        </RRNavLink>
                        <RRNavLink className="nav-link" to="/Guide">
                            Guide
                        </RRNavLink>
                    </>
                )}

                <RRNavLink className="navbar-brand" to="/">
                    SurvivorGPT
                </RRNavLink>

                {isLoggedIn && (
                    <a
                        aria-current="page"
                        className="nav-link logout-link"
                        style={{ cursor: "pointer" }}
                        onClick={logout}
                    >
                        Logout
                    </a>
                )}
            </nav>
        </div>
    );
}