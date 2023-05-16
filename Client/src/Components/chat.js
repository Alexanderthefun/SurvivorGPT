import React, { useEffect, useState } from "react";
import axios from "axios";
import "./chat.css";
import { me } from "../Modules/authManager";
import { getInventory } from "../Modules/inventoryManager";
import { getAllCategories } from "../Modules/CategoryManager";
import { getAllChatsByUserId } from "../Modules/AiChatManager";

//MAKE SURE TO RUN node server.js ON CLIENT LEVEL TO RUN SERVER 8020
export default function Chat() {
    const [user, setUser] = useState(null);
    const [inventory, setInventory] = useState([]);
    const [categories, setCategories] = useState([]);
    const [chats, setChats] = useState([]);
    const [prompt, setPrompt] = useState("");
    const [response, setResponse] = useState("");
    const [isLoading, setIsLoading] = useState(false)
    const [message, setMessage] = useState("Select what guidance type. Your location and inventory resources will be considered in the response.")
    const loadingImageURL = "https://media4.giphy.com/media/RgzryV9nRCMHPVVXPV/giphy.gif?cid=ecf05e47chpm4g395m937666ebghcqjg7mnanaf0lfchu2wv&ep=v1_gifs_search&rid=giphy.gif&ct=g"


    useEffect(() => {
        me().then(data => { setUser(data) });
        getAllCategories().then(data => { setCategories(data) });
    }, [])

    useEffect(() => {
        if (user) {
            getInventory(user.id).then(data => { setInventory(data) })
            getAllChatsByUserId(user.id).then(data => { setChats(data) });
        }
    }, [user]);

    const generatePrompt = (action) => {
        const location = ` ${user.city}, ${user.state}`;
        const inventoryItems = `Tools: ${inventory.tools.map((tool) => { return `${tool.name}` }).join(", ")}, Weapons: ${inventory.weapons.map((weapon) => { return `${weapon.name}` }).join(", ")}, Energy Sources: ${inventory.energySources.map((energy) => { return `${energy.name}` }).join(", ")} and Miscellaneous Items: ${inventory.miscellaneousItems.map((misc) => { return `${misc.name}` }).join(", ")}`;

        switch (action) {
            case 'Hunting':
                return `You are SurvivorGPT! Expert in surviving the wild outdoors. I am trying to survive in ${location} wilderness. I have some supplies that include: ${inventoryItems}. 
                            Based on my location and supplies, could you please tell me all of the wildlife I can hunt for? Please go into great detail about which of my supplies to use and how I should use them.`;
            case 'Shelter':
                return `You are SurvivorGPT! Expert in surviving the wild outdoors. I am trying to survive in ${location} wilderness. I have some supplies that include: ${inventoryItems}. 
                    Based on my location and supplies, could you please give me some detailed ideas on how to build a shelter? What kind of wood, plants, or natural resources in my area can I use? Please go into great detail about which of my supplies to use and how I should use them.`;
            case 'Trapping':
                return `You are SurvivorGPT! Expert in surviving the wild outdoors. I am trying to survive in ${location} wilderness. I have some supplies that include: ${inventoryItems}. 
                    Based on my location and supplies, could you please tell me all of the wildlife I can trap? Explain how to build any kind of traps. Please go into great detail about which of my supplies to use and how I should use them.`;
            case 'Foraging':
                return `You are SurvivorGPT! Expert in surviving the wild outdoors. I am trying to survive in ${location} wilderness. I have some supplies that include: ${inventoryItems}. 
                    Based on my location and supplies, could you please tell me all of the fruits, vegetables and or fungi that I can forage for in my area? Make an extensive list and please go into great detail about which of my supplies to use and how I should use them.`;
        }
    };


    const HTTP = "http://localhost:8020/chat";

    const handleSubmit = (e) => {
        e.preventDefault();
        setMessage("Scanning the landscape for opportunities and obstacles ahead... Survival strategies loading!");
        setIsLoading(true);
        axios
            .post(`${HTTP}`, { prompt })
            .then((res) => {
                setResponse(res.data);
                setMessage("");
                setIsLoading(false);
            })
            .catch((error) => {
                console.log(error);
                setIsLoading(false);
            });
    };

    const handlePrompt = (e) => setPrompt(e.target.value);

    const handleButtonClick = (action) => {
        setResponse("");
        const newPrompt = generatePrompt(action);
        setPrompt(newPrompt);
    }
    return <div className="container">
        <h2 className="Header">Survival Methods & Techniques</h2>

        <form className="form" onSubmit={handleSubmit}>
            <div className="form-group">
                <div className="form-group-content">
                    <div className={`loading-container${response ? " hide" : ""}`}>
                        <label htmlFor="" className="message">{message}</label>
                        {isLoading && (
                            <img
                                src={loadingImageURL}
                                alt="Loading..."
                                className="loading-image"
                            />
                        )}
                    </div>
                    <p className="response">{response}</p>
                </div>
            </div>
            <div className="buttonContainer">
                <button className="buttons" onClick={() => handleButtonClick("Hunting")}>Hunting</button>
                <button className="buttons" onClick={() => handleButtonClick("Trapping")}>Trapping</button>
                <button className="buttons" onClick={() => handleButtonClick("Foraging")}>Foraging</button>
                <button className="buttons" onClick={() => handleButtonClick("Shelter")}>Shelter</button>
            </div>
        </form>
        <div className="chatActions">
            {/* <button>Save</button>
            <button>Tell me more</button> */}
        </div>
    </div>

}