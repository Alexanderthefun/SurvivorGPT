import "./inventory.css"
import React from "react";
import { Alert } from "reactstrap";
import { useEffect, useState } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { me } from "../../Modules/authManager";
import {
    addEnergy, addInventory, addMiscellaneous, addTool, addWeapon, getAllEnergies, getAllInventoryUserIds, getAllMiscellaneous,
    getAllTools, getAllWeapons, getInventory, deleteTool, deleteWeapon, deleteEnergy, deleteMiscellaneous, editFood, addFoodType, addFood, DeleteFood, DeleteFoodType
} from "../../Modules/inventoryManager";

export const Inventory = () => {
    const [user, setUser] = useState(null)
    const [inventory, setInventory] = useState({})
    const [userInvId, setUserInvId] = useState(0)
    const [tools, setTools] = useState([])
    const [weapons, setWeapons] = useState([])
    const [energies, setEnergies] = useState([])
    const [miscellaneous, setMiscellaneous] = useState([])
    const [refreshInv, setRefreshInv] = useState(false)
    const [visible, setVisible] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [editingFood, setEditingFood] = useState(null);
    const [showForm, setShowForm] = useState(false);
    const [newFood, setNewFood] = useState({
        name: '',
        count: 0,
        protein: false,
        fruitVeggieFungi: false
    });


    useEffect(() => {
        me().then(data => { setUser(data) })
    }, [])

    useEffect(() => {
        if (user) {
            getInventory(user.id).then(data => {
                console.log(data);
                if (data !== null) {
                    setInventory(data);
                    setUserInvId(data.id)
                } else {
                    const newInventory = {
                        UserId: user.id,
                    };

                    addInventory(newInventory).then((newData) => {
                        setUserInvId(newData.id);
                    });
                }
            });
        }
    }, [user]);

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


    useEffect(() => {
        if (refreshInv == true) {
            getInventory(user.id).then(data => { setInventory(data) })
            setRefreshInv(false)
        }
    }, [refreshInv])


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

    const handleFoodSubmit = (e) => {
        e.preventDefault();

        addFoodType(newFood).then((data) => {
            const invFood = {
                InventoryId: inventory.id,
                Foodid: data.id
            }
            addFood(invFood)
            setRefreshInv(true)
        })

        // Reset the form and hide it
        setNewFood({ name: '', count: 0, protein: false, fruitVeggieFungi: false });
        setShowForm(false);
    };

    const handleDeleteFoodButton = (foodId) => {
        DeleteFood(inventory.id, foodId)
        DeleteFoodType(foodId)
        setRefreshInv(true)
    };



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
                                        onClick={() => { handleAddButton(energy.id, 'energy') }}
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
                                    onClick={() => { handleRemoveButton(misc.id, 'misc') }}
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
                        <h5 className="Flabel">Edit/Delete</h5>
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
                                    <div className="foodButtons">
                                        <button
                                            className="DeleteButton"
                                            onClick={() => handleDeleteFoodButton(food.id)}
                                        >
                                            🗑️
                                        </button>
                                        <button
                                            className="EditButton"
                                            id={food.id}
                                            onClick={() => handleEditButton(food)}
                                        >
                                            ✎
                                        </button>
                                    </div>
                                )}
                            </div>
                        );
                    })}

                    {showForm && (
                        <form
                            className="addFoodContainer"
                            onSubmit={handleFoodSubmit}>
                            <input
                                type="text"
                                placeholder="Name"
                                value={newFood.name}
                                onChange={(e) => setNewFood({ ...newFood, name: e.target.value })}
                            />
                            <input
                                type="number"
                                placeholder="Count"
                                value={newFood.count}
                                onChange={(e) => setNewFood({ ...newFood, count: parseInt(e.target.value, 10) })}
                            />
                            <label>
                                Protein
                                <input
                                    type="checkbox"
                                    checked={newFood.protein}
                                    onChange={(e) => setNewFood({ ...newFood, protein: e.target.checked })}
                                />
                            </label>
                            <label>
                                Fruit/Veggie/Fungi
                                <input
                                    type="checkbox"
                                    checked={newFood.fruitVeggieFungi}
                                    onChange={(e) => setNewFood({ ...newFood, fruitVeggieFungi: e.target.checked })}
                                />
                            </label>
                            <button type="submit">Add Food</button>
                        </form>
                    )}
                    <div className="foodButtonWrapper">
                        <button
                            className="AddFoodButton"
                            onClick={() => setShowForm(!showForm)}>
                            {showForm ? 'Close' : 'Add New Food'}
                        </button>
                    </div>

                </div>
            </div>
        </div>
    )
}