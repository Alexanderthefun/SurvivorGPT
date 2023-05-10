import React from "react";
import "./inventory.css"
import { Routes, Route, Navigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { me } from "../../Modules/authManager";
import {
    addEnergy, addInventory, addMiscellaneous, addTool, addWeapon, getAllEnergies, getAllInventoryUserIds, getAllMiscellaneous,
    getAllTools, getAllWeapons, getInventory, deleteTool, deleteWeapon, deleteEnergy, deleteMiscellaneous
} from "../../Modules/inventoryManager";

export const Inventory = () => {
    const [user, setUser] = useState(null)
    const [inventory, setInventory] = useState({})
    const [userInvId, setUserInvId] = useState(0)
    const [allInvUserIds, setAllInvUserIds] = useState([])
    const [tools, setTools] = useState([])
    const [weapons, setWeapons] = useState([])
    const [energies, setEnergies] = useState([])
    const [miscellaneous, setMiscellaneous] = useState([])
    const [hasBothChanged, setHasBothChanged] = useState(false)
    const [refreshInv, setRefreshInv] = useState(false)


    useEffect(() => {
        me().then(data => { setUser(data) })
        getAllInventoryUserIds().then(data => {
            setAllInvUserIds(data)
        })
    }, [])

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
    //get user inventory after user variable populates
    useEffect(() => {
        if (user) {
            getInventory(user.id).then(data => { setInventory(data) })
        }
    }, [user])

    useEffect(() => {
        if (refreshInv == true) {
            getInventory(user.id).then(data => { setInventory(data) })
            setRefreshInv(false)
        }
    }, [refreshInv])

    useEffect(() => {
        if (hasBothChanged == true) {
            const theOne = createOrSetInventory()
            checkTheOne(theOne)
        }
    }, [hasBothChanged])

    useEffect(() => {
        if (user && allInvUserIds) {
            setHasBothChanged(true)
        }
    }, [user, allInvUserIds])

    //loops all inventoryIds to see if currentUser has existing inventory, if so, that user's inv is set to chosen inv.
    const createOrSetInventory = () => {
        let chosenInv = { empty: true };
        for (const inv of allInvUserIds) {
            if (inv.userId === user.id) {
                chosenInv = inv;
                setUserInvId(chosenInv.id)
            }
        }
        return chosenInv;
    }

    //if chosenInv.empty = true, then a new inventory is made with the user's id. The inv's Id is set to userInvId.
    const checkTheOne = (theChosenInv) => {
        if (theChosenInv.empty === true) {
            const newInventory = {
                UserId: user.id
            };
            console.log(user)
            addInventory(newInventory)
                .then((newData) => {
                    setUserInvId(newData.id)
                });
        }
    }


    const handleAddButton = (Id, itemType) => {
        let inventoryItem = {
            inventoryId: userInvId,
        };

        switch (itemType) {
            case 'tool':
                inventoryItem.toolId = Id;
                addTool(inventoryItem);
                setRefreshInv(true)
                break;
            case 'weapon':
                inventoryItem.weaponId = Id;
                addWeapon(inventoryItem);
                setRefreshInv(true)
                break;
            case 'energy':
                inventoryItem.energyId = Id;
                addEnergy(inventoryItem);
                setRefreshInv(true)
                break;
            case 'misc':
                inventoryItem.miscellaneousId = Id;
                addMiscellaneous(inventoryItem);
                setRefreshInv(true)
                break;
            default:
                console.error('Invalid item type:', itemType);
        }
    };

    const handleRemoveButton = (Id, itemType) => {
        let inventoryItem = {
            inventoryId: userInvId,
        };

        switch (itemType) {
            case 'tool':
                inventoryItem.toolId = Id;
                deleteTool(Id, inventoryItem.inventoryId);
                setRefreshInv(true)
                break;
            case 'weapon':
                inventoryItem.weaponId = Id;
                deleteWeapon(Id, inventoryItem.inventoryId);
                setRefreshInv(true)
                break;
            case 'energy':
                inventoryItem.energyId = Id;
                deleteEnergy(Id, inventoryItem.inventoryId);
                setRefreshInv(true)
                break;
            case 'misc':
                inventoryItem.miscellaneousId = Id;
                deleteMiscellaneous(Id, inventoryItem.inventoryId);
                setRefreshInv(true)
                break;
            default:
                console.error('Invalid item type:', itemType);
        }
    };

    //display inventory items after fetch.
    //compare inventory and if user has then only display remove button, otherwise display add button

    return (
        <div className="daddyContainer">
            <div className="inventoryContainer">
                <div className="itemDisplay">
                    <h3 className="invType">Tools</h3>
                    {tools.map(tool => {
                        return <p key={tool.id} className="invElement">{tool.name}
                            <button
                                className="RemoveButton"
                                id={tool.id}
                                onClick={() => { handleRemoveButton(tool.id, 'tool') }}
                            >-</button>
                            <button
                                className="AddButton"
                                id={tool.id}
                                onClick={() => { handleAddButton(tool.id, 'tool') }}
                            >+</button>
                        </p>
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Weapons</h3>
                    {weapons.map(weapon => {
                        return <p key={weapon.id}>{weapon.name}
                            <button
                                className="RemoveButton"
                                id={weapon.id}
                                onClick={() => { handleRemoveButton(weapon.id, 'weapon') }}
                            >-</button>
                            <button
                                className="AddButton"
                                id={weapon.id}
                                onClick={() => { handleAddButton(weapon.id, 'weapon') }}
                            >+</button>
                        </p>
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Energy</h3>
                    {energies.map(energy => {
                        return <p key={energy.id}>{energy.name}
                            <button
                                className="RemoveButton"
                                id={energy.id}
                                onClick={() => { handleRemoveButton(energy.id, 'energy') }}
                            >-</button>
                            <button
                                className="AddButton"
                                id={energy.id}
                                onClick={() => { handleAddButton(energy.id, 'energy') }}
                            >+</button>
                        </p>
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Miscellaneous</h3>
                    {miscellaneous.map(misc => {
                        return <p key={misc.id}>{misc.name}
                            <button
                                className="RemoveButton"
                                id={misc.id}
                                onClick={() => { handleRemoveButton(misc.id, 'misc') }}
                            >-</button>
                            <button
                                className="AddButton"
                                id={misc.id}
                                onClick={() => { handleAddButton(misc.id, 'misc') }}
                            >+</button>
                        </p>
                    })}
                </div>
            </div>
            <div className="userInv">
                <div className="tools">
                    <h4>Current Tools</h4>
                    {inventory?.tools?.map(tool => {
                        return <div key={tool.id} className="invTools">
                            <p>{tool.name}</p>
                        </div>
                    })}
                </div>
            </div>
        </div>
    )

}