import "./inventory.css"
import React from "react";
import { Alert } from "reactstrap";
import { useEffect, useState } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { me } from "../../Modules/authManager";
import {
    addEnergy, addInventory, addMiscellaneous, addTool, addWeapon, getAllEnergies, getAllInventoryUserIds, getAllMiscellaneous,
    getAllTools, getAllWeapons, getInventory, deleteTool, deleteWeapon, deleteEnergy, deleteMiscellaneous, editFood
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
    const [visible, setVisible] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [editingFood, setEditingFood] = useState(null);


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

    const handleInfoButton = (itemName) => {
        setAlertMessage(`${itemName} is already in your inventory.`);
        setVisible(true);
    }
    const onDismiss = () => setVisible(false);

    const handleEditButton = (food) => {
        setEditingFood(food);
    };

    const handleSaveButton = (foodId) => {
        editFood(editingFood, foodId)
        setEditingFood(null)
        setRefreshInv(true)
    }



    return (
        <div className="daddyContainer">
            <Alert color="info" className="custom-alert" isOpen={visible} >
                {alertMessage}
                <button
                    type="button"
                    className="closeAlert"
                    onClick={onDismiss}
                    aria-label="Close" >
                    <span aria-hidden="true">&times;</span>
                </button>
            </Alert>
            <div className="inventoryContainer">
                <div className="itemDisplay">
                    <h3 className="invType">Tools</h3>
                    {tools.map(tool => {
                        const isInInventory = inventory?.tools?.some(user_tool => user_tool.id === tool.id);

                        return (
                            <p key={tool.id} className="invElement">
                                {tool.name}
                                {isInInventory ? (
                                    <button
                                        className="InfoButton"
                                        id={tool.id}
                                        onClick={() => handleInfoButton(tool.name)}
                                    >i</button>
                                ) : (
                                    <button
                                        className="AddButton"
                                        id={tool.id}
                                        onClick={() => { handleAddButton(tool.id, 'tool') }}
                                    >+</button>
                                )}
                            </p>
                        );
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Weapons</h3>
                    {weapons.map(weapon => {
                        const isInInventory = inventory?.weapons?.some(user_weapon => user_weapon.id === weapon.id);

                        return (
                            <p key={weapon.id} className="invElement">
                                {weapon.name}
                                {isInInventory ? (
                                    <button
                                        className="InfoButton"
                                        id={weapon.id}
                                        onClick={() => handleInfoButton(weapon.name)}
                                    >i</button>
                                ) : (
                                    <button
                                        className="AddButton"
                                        id={weapon.id}
                                        onClick={() => { handleAddButton(weapon.id, 'weapon') }}
                                    >+</button>
                                )}
                            </p>
                        );
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Energy</h3>
                    {energies.map(energy => {
                        const isInInventory = inventory?.energySources?.some(user_energy => user_energy.id === energy.id);

                        return (
                            <p key={energy.id} className="invElement">
                                {energy.name}
                                {isInInventory ? (
                                    <button
                                        className="InfoButton"
                                        id={energy.id}
                                        onClick={() => handleInfoButton(energy.name)}
                                    >i</button>
                                ) : (
                                    <button
                                        className="AddButton"
                                        id={energy.id}
                                        onClick={() => { handleAddButton(energy.id, 'energySource') }}
                                    >+</button>
                                )}
                            </p>
                        );
                    })}
                </div>
                <div className="itemDisplay">
                    <h3 className="invType">Miscellaneous</h3>
                    {miscellaneous.map(misc => {
                        const isInInventory = inventory?.miscellaneousItems?.some(user_misc => user_misc.id === misc.id);

                        return (
                            <p key={misc.id} className="invElement">
                                {misc.name}
                                {isInInventory ? (
                                    <button
                                        className="InfoButton"
                                        id={misc.id}
                                        onClick={() => handleInfoButton(misc.name)}
                                    >i</button>
                                ) : (
                                    <button
                                        className="AddButton"
                                        id={misc.id}
                                        onClick={() => { handleAddButton(misc.id, 'misc') }}
                                    >+</button>
                                )}
                            </p>
                        );
                    })}
                </div>
            </div>

            {/* ----------------------- Inventory Side ------------------------ */}

            <div className="userInventoryContainer">
                <div className="userInvCard">
                    <h4 className="userInvType">Your Tools</h4>
                    {inventory?.tools?.map(tool => {
                        return <div key={tool.id} className="invTools">
                            <p className="invElement">{tool.name}
                                <button
                                    className="RemoveButton"
                                    id={tool.id}
                                    onClick={() => { handleRemoveButton(tool.id, 'tool') }}
                                >-</button>
                            </p>
                        </div>
                    })}
                </div>
                <div className="userInvCard">
                    <h4 className="userInvType">Your Weapons</h4>
                    {inventory?.weapons?.map(weapon => {
                        return <div key={weapon.id} className="invWeapons">
                            <p className="invElement">{weapon.name}
                                <button
                                    className="RemoveButton"
                                    id={weapon.id}
                                    onClick={() => { handleRemoveButton(weapon.id, 'weapon') }}
                                >-</button>
                            </p>
                        </div>
                    })}
                </div>
                <div className="userInvCard">
                    <h4 className="userInvType">Your Energy Sources</h4>
                    {inventory?.energySources?.map(energy => {
                        return <div key={energy.id} className="invEnergy">
                            <p className="invElement">{energy.name}
                                <button
                                    className="RemoveButton"
                                    id={energy.id}
                                    onClick={() => { handleRemoveButton(energy.id, 'energy') }}
                                >-</button>
                            </p>
                        </div>
                    })}
                </div>
                <div className="userInvCard">
                    <h4 className="userInvType">Your Miscellaneous Items</h4>
                    {inventory?.miscellaneousItems?.map(misc => {
                        return <div key={misc.id} className="invMisc">
                            <p className="invElement">{misc.name}
                                <button
                                    className="RemoveButton"
                                    id={misc.id}
                                    onClick={() => { handleRemoveButton(misc.id, 'miscellaneous') }}
                                >-</button>
                            </p>
                        </div>
                    })}
                </div>

                {/* --------------------- FOOD --------------------- */}

                <div className="foodCard">
                    <h4 className="userInvType">Your Food Supply</h4>
                    <div className="foodLabels">
                        <h5 className="Flabel">Food Name</h5>
                        <h5 className="Flabel">Amount</h5>
                        <h5 className="Flabel"></h5>
                    </div>
                    {inventory?.foods?.map(food => {
                        return (
                            <div key={food.id} className="foodRow">
                                {editingFood?.id === food.id ? (
                                    <input
                                        className="invElement"
                                        value={editingFood.name}
                                        onChange={(e) =>
                                            setEditingFood({ ...editingFood, name: e.target.value })
                                        }
                                    />
                                ) : (
                                    <p className="invElement">{food.name}</p>
                                )}
                                {editingFood?.id === food.id ? (
                                    <input
                                        id="foodCount"
                                        type="number"
                                        value={editingFood.count}
                                        onChange={(e) =>
                                            setEditingFood({ ...editingFood, count: parseInt(e.target.value, 10) })
                                        }
                                    />
                                ) : (
                                    <p id="foodCount">{food.count}</p>
                                )}
                                {editingFood?.id === food.id ? (
                                    <button
                                        className="SaveButton"
                                        onClick={() => handleSaveButton(food.id)}
                                    >
                                        Save
                                    </button>
                                ) : (
                                    <button
                                        className="EditButton"
                                        id={food.id}
                                        onClick={() => handleEditButton(food)}
                                    >
                                        ✎
                                    </button>
                                )}
                            </div>
                        );
                    })}
                </div>
            </div>
        </div>
    )

}