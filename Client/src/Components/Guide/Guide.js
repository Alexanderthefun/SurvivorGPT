import React, { useState, useEffect } from 'react';
import { me } from '../../Modules/authManager';
import { getAllChatsByUserId } from '../../Modules/AiChatManager';
import { getInventory } from '../../Modules/inventoryManager';
import { getAllCategories } from '../../Modules/CategoryManager';
import "./guide.css"

export const Guide = () => {
    const [user, setUser] = useState(null);
    const [inventory, setInventory] = useState([]);
    const [chats, setChats] = useState([]);
    const [categories, setCategories] = useState([]);
    const [selectedCategory, setSelectedCategory] = useState('');

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


    const handleCategoryChange = (event) => {
        setSelectedCategory(event.target.value);
    };

    const handleGoButtonClick = () => {
        // Filter chats by selected category
    };

    return (
        <div className="chatDaddyContainer">
            <div className="categorySelection">
                <select value={selectedCategory} onChange={handleCategoryChange}>
                    <option value="">Select a category</option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
                <button onClick={handleGoButtonClick}>Go</button>
            </div>
            <div className="chats">
                {chats.map((chat) => (
                    <div key={chat.id} className="chat">
                        <div className="chatContent">{chat?.content}</div>
                        <div className="chatActions">
                            <button>Save</button>
                            <button>Tell me more</button>
                            <button>Delete</button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Guide;