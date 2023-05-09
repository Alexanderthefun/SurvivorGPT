import React, { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink,
} from "reactstrap";
import { logout } from "./../Modules/authManager";

export default function Header({ isLoggedIn, userProfile }) {
    const [isOpen, setIsOpen] = useState(false);
    const toggle = () => setIsOpen(!isOpen);

    return (
        <div>
            <Navbar color="dark" dark expand="md">
                <NavbarBrand tag={RRNavLink} to="/">
                    SurvivorGPT
                </NavbarBrand>
                <NavbarToggler onClick={toggle} />
                <Collapse isOpen={isOpen} navbar>
                    <Nav className="mr-auto" navbar>
                        {isLoggedIn && (
                            <>
                                <NavItem>
                                    <a
                                        aria-current="page"
                                        className="nav-link"
                                        style={{ cursor: "pointer" }}
                                        onClick={logout}
                                    >
                                        Logout
                                    </a>
                                </NavItem>
                            </>
                        )}
                        {!isLoggedIn && (
                            <>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/login">
                                        Login
                                    </NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={RRNavLink} to="/register">
                                        Register
                                    </NavLink>
                                </NavItem>
                            </>
                        )}
                    </Nav>
                    <Nav navbar>
                        <NavItem>
                            <a
                                aria-current="page"
                                className="nav-link"
                                href="https://www.youtube.com"
                                target="_new"
                            >
                                About
                            </a>
                        </NavItem>
                        {userProfile && (
                            <NavItem>
                                <NavLink>(DO NOT CLICK)</NavLink>
                            </NavItem>
                        )}
                    </Nav>
                </Collapse>
            </Navbar>
        </div>
    );
}