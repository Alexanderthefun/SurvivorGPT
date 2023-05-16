import React from "react";
import { Routes, Route, Navigate, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { me } from "../../Modules/authManager";
import "./home.css"
import { getRandomTips } from "./Tips";


export const Home = () => {
    const [user, setUser] = useState({})
    const [randomTip, setRandomTip] = useState('');
    const [weather, setWeather] = useState(null);
    const [city, setCity] = useState('');
    const [state, setState] = useState('');
    const [latitude, setLatitude] = useState(null);
    const [longitude, setLongitude] = useState(null);
    const [newData, setNewData] = useState(null)

    useEffect(() => {
        me().then(data => {
            setUser(data)
        })
    }, [])

    useEffect(() => {
        if (user) {
            setCity(user.city);
            setState(user.state);
        }
    }, [user])

    useEffect(() => {
        setRandomTip(getRandomTips())
    }, [])

    useEffect(() => {
        if (city && state) {
            fetchLatLng();
        }
    }, [city, state])

    useEffect(() => {
        const weatherAPI = process.env.REACT_APP_OPEN_WEATHER_MAP_API_KEY;
        console.log(weatherAPI)
        if (latitude && longitude) {
            fetch(`https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${weatherAPI}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(data => {
                    setWeather(data);
                })
                .catch(error => {
                    console.error('Error fetching weather data:', error);
                });
        }
    }, [latitude, longitude])

    const fetchLatLng = async () => {
        try {
            const OPENCAGE_API_KEY = process.env.REACT_APP_OPENCAGE_API_KEY;
            fetch(`https://api.opencagedata.com/geocode/v1/json?q=${city},${state}&key=${OPENCAGE_API_KEY}`)
                .then(res => res.json()
                    .then(data => {
                        setNewData(data)
                        setLatitude(data.results[0].geometry.lat)
                        setLongitude(data.results[0].geometry.lng)
                    })
                )


        } catch (error) {
            console.error('Error fetching coordinates:', error);
        }
    };
    const handleNewTip = () => {
        setRandomTip(getRandomTips());
    };

    return (
        <div className="grandDadContainer">
            <div className="user"> Hello, {user.displayName}</div>
            <div className="daddyContainer">
                <div className="tipContainer">
                    <p className="tipHeader">Random Survival Tip</p>
                    <p className="tips">{randomTip}</p>
                    <button
                        className="tipButton"
                        onClick={handleNewTip}
                    >Get a new tip</button>
                </div>
                {weather && (
                    <div className="weatherContainer">
                        <h2 className="weatherHeader">
                            {city} Weather Forecast
                        </h2>
                        <p className="weatherContent">Temperature: <span className="data">{Math.round((weather.main.temp - 273.15) * 1.8 + 32)} °F</span></p>
                        <p className="weatherContent">Feels like: <span className="data">{Math.round((weather.main.feels_like - 273.15) * 1.8 + 32)} °F</span></p>
                        <p className="weatherContent">Weather: <span className="data">{weather.weather[0].description}</span></p>
                        <p className="weatherContent">Precipitation: {/rain|shower|thunderstorm/.test(weather.weather[0].description) ? <span className="data">likely to rain today</span> : <span className="data">No rain expected today</span>}</p>
                    </div>
                )}


            </div>
        </div>
    )
}



