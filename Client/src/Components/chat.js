import React, { useState } from "react";
import axios from "axios";
import "./chat.css";

//MAKE SURE TO RUN node server.js ON CLIENT LEVEL TO RUN SERVER 8020
export default function Chat() {
    const [prompt, setprompt] = useState("");
    const [response, setResponse] = useState("");

    const HTTP = "http://localhost:8020/chat";
    const handleSubmit = (e) => {
        e.preventDefault();
        axios.post(`${HTTP}`, { prompt }).then(res => setResponse(res.data)).catch((error) => {
            console.log(error);
        })
    }
    const handlePrompt = (e) => setprompt(e.target.value);


    return <div className="container">
        <h1 className="Header">SurvivorGPT</h1>
        <form className="form" onSubmit={handleSubmit}>
            <div className="form-group">
                <label htmlFor="">Ask question</label>
                <p className="response">{response ? response : "..."}</p>
                <input
                    type="text"
                    className="input"
                    placeholder="Enter Text"
                    value={prompt}
                    onChange={handlePrompt}
                ></input>
                <button 
                type="submit"
                className="submitButton"
                >Submit</button>
            </div>
        </form>
        <div className="chatActions">
            {/* <button>Save</button>
            <button>Tell me more</button> */}
        </div>
    </div>

}