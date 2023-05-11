import React from "react";
import { Routes, Route, Navigate, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { me } from "../../Modules/authManager";
import "./home.css"
import { getRandomTips } from "./Tips";

export const Home = () => {
    const [user, setUser] = useState({})
    const [randomTip, setRandomTip] = useState('');

    useEffect(() => {
        me().then(data => {
            setUser(data)
        })
    }, [])

    useEffect(() => {
        setRandomTip(getRandomTips())
    }, [])

    const navigate = useNavigate()


    const handleNewTip = () => {
        setRandomTip(getRandomTips());
    };


    return (
        <div className="daddyContainer">
            <div className="tipContainer">
                <p className="tipHeader">Random Survival Tip</p>
                <p className="tips">{randomTip}</p>
                <button
                    className="tipButton"
                    onClick={handleNewTip}
                >Get a new tip</button>
            </div>

        </div>
    )
}



