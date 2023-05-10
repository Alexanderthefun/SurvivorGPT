import { getToken } from "./authManager"

const _apiUrl = "/api/inventory"

export const getInventory = (id) => {
    return getToken().then(token => {
        // The query parameters are only added if an argument is provided for them
        return fetch(`${_apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const getAllInventoryUserIds = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/getAllInvUserIds`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addInventory = (inventory) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(inventory)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Inventory made successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occured while trying to add Inventory"
                );
            }
        });
    });
}

export const addFoodType = (food) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ food })
        })
    })
}

export const DeleteFoodType = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteFoodType/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}

export const addFood = (invFood) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/food`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ invFood })
        })
    });
}

export const DeleteFood = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteFood/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}

export const getAllTools = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/tools`, {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addTool = (invTool) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/tool`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ invTool })
        })
    });
}

export const DeleteTool = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteTool/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}

export const getAllWeapons = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/weapons`, {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addWeapon = (invWeapon) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/weapon`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ invWeapon })
        })
    });
}

export const DeleteWeapon = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteWeapon/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}

export const getAllEnergies = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/energy`, {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addEnergy = (invEnergy) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/energy`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ invEnergy })
        })
    });
}

export const DeleteEnergy = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteEnergy/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}

export const getAllMiscellaneous = () => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/miscellaneous`, {
            method: 'GET',
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addMiscellaneous = (invMiscellaneous) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/miscellaneous`, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ invMiscellaneous })
        })
    });
}

export const DeleteMiscellaneous = (id) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/deleteMiscellaneous/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
    });
}











//May not need, but here is the structure for EDITS:
export const editMiscellaneous = (id, miscellaneous) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${miscellaneous}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(miscellaneous)
        })
    });
}

export const editTool = (id, tool) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${tool}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(tool)
        })
    });
}


export const editWeapon = (id, weapon) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${weapon}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(weapon)
        })
    });
}

export const editFood = (id, food) => {
    return getToken().then(token => {
        return fetch(`${_apiUrl}/${food}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(food)
        })
    });
}




