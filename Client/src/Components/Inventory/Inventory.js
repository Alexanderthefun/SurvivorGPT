import React from "react";
import "./inventory.css"
import { Routes, Route, Navigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { me } from "../../Modules/authManager";
import { addInventory, addTool, getAllEnergies, getAllInventoryUserIds, getAllMiscellaneous, getAllTools, getAllWeapons } from "../../Modules/inventoryManager";

export const Inventory = () => {
    const [user, setUser] = useState({})
    const [inventory, setInventory] = useState({})
    const [userInvId, setUserInvId] = useState(0)
    const [allInvUserIds, setAllInvUserIds] = useState([])
    const [tools, setTools] = useState([])
    const [weapons, setWeapons] = useState([])
    const [energies, setEnergies] = useState([])
    const [miscellaneous, setMiscellaneous] = useState([])


    useEffect(() => {
        setUser(me())
        getAllInventoryUserIds().then(data => {
            setAllInvUserIds(data)
        })
        // console.log(allInvUserIds)
    }, [])

    useEffect(() => {
        let chosenInv = {};
        for (const inv of allInvUserIds) {
            if (inv.UserId === user.id) {
                chosenInv = inv;
                setUserInvId(chosenInv.id)
            }
        }
        if (chosenInv === 0) {
            setInventory({
                UserId: user.id
            });
            addInventory(inventory)
                .then(setUserInvId(inventory.id));
        }
        console.log(userInvId)
    }, [allInvUserIds])

    useEffect(() => {
        getAllTools()
            .then(data => { setTools(data) });
        getAllWeapons()
            .then(data => { setWeapons(data) });
        getAllEnergies()
            .then(data => { setEnergies(data) });
        getAllMiscellaneous()
            .then(data => { setMiscellaneous(data) });
    }, [])

    const handleAddButton = (toolId) => {
        const inventoryTool = {
            inventoryId: userInvId,
            toolId: toolId
        }
        addTool(inventoryTool);
    } 


    return (
        <div className="inventoryContainer">
            <div className="itemDisplay">
                <h3 className="invType">Tools</h3>
                {tools.map(tool => {
                    return <p id={tool.id} className="invElement">{tool.name}
                        <button
                            className="AddButton"
                            id={tool.id}
                            onClick={() => {handleAddButton(tool.id)}}
                        >Add to Inventory</button></p>
                })}
            </div>
            <div className="itemDisplay">
            <h3 className="invType">Weapons</h3>
                {weapons.map(weapon => {
                    return <p id={weapon.id}>{weapon.name}
                    <button
                            className="AddButton"
                            id={weapon.id}
                            // onClick={() => {handleAddButton(tool.id)}}
                        >Add to Inventory</button>
                    </p>
                })}
            </div>
            <div className="itemDisplay">
            <h3 className="invType">Energy</h3>
                {energies.map(energy => {
                    return <p id={energy.id}>{energy.name}
                    <button
                            className="AddButton"
                            id={energy.id}
                            // onClick={() => {handleAddButton(tool.id)}}
                        >Add to Inventory</button>
                    </p>
                })}
            </div>
            <div className="itemDisplay">
            <h3 className="invType">Miscellaneous</h3>
                {miscellaneous.map(misc => {
                    return <p id={misc.id}>{misc.name}
                    <button
                            className="AddButton"
                            id={misc.id}
                            // onClick={() => {handleAddButton(tool.id)}}
                        >Add to Inventory</button>
                    </p>
                })}
            </div>
        </div>
    )

}